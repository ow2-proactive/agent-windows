/*
 * ################################################################
 *
 * ProActive Parallel Suite(TM): The Java(TM) library for
 *    Parallel, Distributed, Multi-Core Computing for
 *    Enterprise Grids & Clouds
 *
 * Copyright (C) 1997-2011 INRIA/University of
 *                 Nice-Sophia Antipolis/ActiveEon
 * Contact: proactive@ow2.org or contact@activeeon.com
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Affero General Public License
 * as published by the Free Software Foundation; version 3 of
 * the License.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307
 * USA
 *
 * If needed, contact us to obtain a release under GPL Version 2 or 3
 * or a different license than the AGPL.
 *
 *  Initial developer(s):               The ActiveEon Team
 *                        http://www.activeeon.com/
 *  Contributor(s):
 *
 * ################################################################
 * $$ACTIVEEON_INITIAL_DEV$$
 */
using System;
using System.Collections.Generic;
using System.Threading;
using ConfigParser;
using log4net;

namespace ProActiveAgent
{
    /// <summary>
    /// ExecutorsManager manages scheduled start/stop of executors.
    /// The configuration cannot contain overlapping calendar events.</summary>
    sealed class ExecutorsManager
    {
        private static readonly ILog LOGGER = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static TimeSpan WEEK_DELAY = new TimeSpan(7, 0, 0, 0);
        private static int BARRIER_SAFETY_MARGIN_MS = 15000;
        // The barrier a safety margin interval    
        private static TimeSpan SAFETY_MARGIN_TIMESPAN = new TimeSpan(0, 0, 0, 0, BARRIER_SAFETY_MARGIN_MS);
        // start - action timers
        private readonly List<Timer> startTimers;
        // stop - action timers
        private readonly List<Timer> stopTimers;
        /// <summary>
        /// The list of executors.</summary>
        private readonly List<ProActiveRuntimeExecutor> proActiveRuntimeExecutors;

        // The constructor should be called only during starting the service
        public ExecutorsManager(AgentType configuration)
        {
            // The configuration specifies the number of executors
            int nbProcesses = configuration.config.nbRuntimes == 0 ? 1 : configuration.config.nbRuntimes;
            LOGGER.Info("Creating " + nbProcesses + " executors");

            // Get the runtime common start info shared between all executors
            CommonStartInfo commonStartInfo = new CommonStartInfo(configuration);

            // Create as many executors with a unique rank as specified in the configuration
            this.proActiveRuntimeExecutors = new List<ProActiveRuntimeExecutor>(nbProcesses);
            for (int rank = 0; rank < nbProcesses; rank++)
            {
                ProActiveRuntimeExecutor executor = new ProActiveRuntimeExecutor(commonStartInfo, rank);
                this.proActiveRuntimeExecutors.Add(executor);
            }

            // Create the start/stop timers for scheduled events
            this.startTimers = new List<Timer>();
            this.stopTimers = new List<Timer>();

            // If always available simply invoke start method with the stop time at max value
            if (configuration.isAlwaysAvailable())
            {
                LOGGER.Info("Using always available planning");
                this.mySendStartAction(new StartActionInfo(
                    commonStartInfo.enabledConnection,
                    DateTime.MaxValue,
                    commonStartInfo.configuration.config.processPriority,
                    commonStartInfo.configuration.config.maxCpuUsage,
                    commonStartInfo.configuration.config.nbWorkers));
            }
            else
            {
                // Fix the current time (usefull when there is a lot of events)            
                DateTime currentFixedTime = DateTime.Now;
                int currentDayOfWeek = (int)currentFixedTime.DayOfWeek;

                foreach (CalendarEventType cEvent in configuration.events)
                {

                    // for each calendar event we calculate remaining time to start and stop service
                    // and according to that register timers                     

                    // we provide the day of the week to present start time
                    // the algorithm to count next start is as follows:
                    // 1. how many days are to start action
                    // 2. we add this amount to the current date
                    // 3. we create time event that will be the exact start time (taking year, month and
                    //    day from the current date and other fields from configuration
                    // 4. we keep duration of task
                    // 5. we count due time for beginning and stopping the task
                    // 6. if time is negative, we move it into next week (to avoid waiting for past events)

                    int eventDayOfWeek = (int)cEvent.start.day;
                    int daysAhead = dayDifference(currentDayOfWeek, eventDayOfWeek);

                    DateTime startTime = currentFixedTime.AddDays(daysAhead);

                    // Absolute start time
                    DateTime absoluteStartTime = new DateTime(startTime.Year, startTime.Month, startTime.Day,
                        cEvent.start.hour, cEvent.start.minute, cEvent.start.second);

                    // Delay to wait until start
                    TimeSpan delayUntilStart = absoluteStartTime - currentFixedTime;

                    // Get the time span duration
                    TimeSpan duration = new TimeSpan(cEvent.duration.days, cEvent.duration.hours, cEvent.duration.minutes,
                                        cEvent.duration.seconds);

                    // Delay to wait until stop
                    TimeSpan delayUntilStop = delayUntilStart.Add(duration);

                    // Absolute stop time
                    DateTime absoluteStopTime = absoluteStartTime.Add(duration);

                    // Check if we need to start immidiately
                    bool startNow = (delayUntilStart < TimeSpan.Zero && delayUntilStop > TimeSpan.Zero);

                    if (delayUntilStart < TimeSpan.Zero)
                    {
                        delayUntilStart = delayUntilStart.Add(WEEK_DELAY);
                    }

                    if (delayUntilStop < TimeSpan.Zero)
                    {
                        delayUntilStop = delayUntilStop.Add(WEEK_DELAY);
                    }

                    StartActionInfo startInfo = new StartActionInfo(
                        commonStartInfo.enabledConnection,
                        absoluteStopTime,
                        cEvent.config.processPriority,
                        cEvent.config.maxCpuUsage,
                        cEvent.config.nbWorkers);

                    LOGGER.Info("Loading weekly event [" + absoluteStartTime.DayOfWeek + ":" + absoluteStartTime.ToString(Constants.DATE_FORMAT) + "] -> [" +
                            absoluteStopTime.DayOfWeek + ":" + absoluteStopTime.ToString(Constants.DATE_FORMAT) + "]");

                    // After dueStart milliseconds this timer will invoke only once per week the callback
                    Timer startT = new Timer(new TimerCallback(mySendStartAction), startInfo, delayUntilStart, WEEK_DELAY);
                    this.startTimers.Add(startT);

                    // After dueStop milliseconds this timer will invoke only once per week the callback
                    Timer stopT = new Timer(new TimerCallback(mySendStopAction), null, delayUntilStop, WEEK_DELAY);
                    this.stopTimers.Add(stopT);                    
                    
                    if (startNow)
                    {
                        this.mySendStartAction(startInfo);
                    }
                }
            }
        }

        public List<ProActiveRuntimeExecutor> getExecutors()
        {
            return this.proActiveRuntimeExecutors;
        }

        //note : it does not refer to restarted actions, but started according to calendar event
        private void mySendStartAction(object action)
        {
            StartActionInfo actionInfo = (StartActionInfo)action;
            DateTime stopTime = actionInfo.stopTime;

            while (stopTime < DateTime.Now)
            {
                stopTime = stopTime.Add(WEEK_DELAY);
            }

            foreach (ProActiveRuntimeExecutor p in this.proActiveRuntimeExecutors)
            {
                DateTime restartBarrierDateTime = p.restartBarrierDateTime;

                if (restartBarrierDateTime < stopTime)
                {
                    restartBarrierDateTime = stopTime;
                }
                // Substract from the barrier a safety margin interval
                p.restartBarrierDateTime = restartBarrierDateTime.Subtract(SAFETY_MARGIN_TIMESPAN);
                // Send the amount of nbWokers of the action, might vary on the planning
                p.setNbWorkers(actionInfo.nbWorkers);
                // Send the start
                p.sendStartAction(ApplicationType.AgentScheduler);
                // Apply process priority and max cpu usage
                p.setProcessBehaviour(actionInfo.processPriority, actionInfo.maxCpuUsage);
            }
        }

        private void mySendStopAction(object stateInfo)
        {
            foreach (ProActiveRuntimeExecutor p in this.proActiveRuntimeExecutors)
            {
                p.restartBarrierDateTime = System.DateTime.Now;
                p.sendStopAction(ApplicationType.AgentScheduler);
            }
        }

        // counting day difference
        private int dayDifference(int dayA, int dayB)
        {
            if ((dayB - dayA) < 0)
            {
                return ((dayB - dayA) % 7);
            }
            else if ((dayB - dayA) > 0)
            {
                return -((7 - (dayB - dayA)) % 7);
            }
            else
                return 0;
        }

        // releasing resources
        public void dispose()
        {
            // Dispose all start timers
            foreach (Timer t in this.startTimers)
            {
                t.Dispose();
            }
            this.startTimers.Clear();
            // Dispose all stop timers
            foreach (Timer t in this.stopTimers)
            {
                t.Dispose();
            }
            this.stopTimers.Clear();
            // Dispose all executors
            foreach (ProActiveRuntimeExecutor p in this.proActiveRuntimeExecutors)
            {
                p.dispose();
            }
            this.proActiveRuntimeExecutors.Clear();
        }
    }
}
