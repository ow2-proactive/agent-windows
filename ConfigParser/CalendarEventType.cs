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
using System.Xml.Serialization;
using System;

namespace ConfigParser
{
    /// <summary>
    /// This class represents a calendar event starting at a date during a duration.
    /// </summary>
    public sealed class CalendarEventType
    {
        /// <summary>
        /// The event will start at a precise moment.</summary>
        [XmlElement("start")]
        public Start start;

        /// <summary>
        /// The event duration.</summary>
        [XmlElement("duration", IsNullable = false)]
        public Duration duration;

        /// <summary>
        /// The optional config that overrides the global one.</summary>
        [XmlElement("config", Type = typeof(AgentConfigType))]
        public AgentConfigType config;

        public CalendarEventType()
        {
            this.start = new Start();
            this.duration = new Duration();
            this.config = new AgentConfigType();
        }

        public CalendarEventType(Start start, Duration duration) : this(start, duration, new AgentConfigType()) {}

        public CalendarEventType(Start start, Duration duration, AgentConfigType config)
        {
            this.start = start;
            this.duration = duration;
            this.config = config;
        }

        /////////////////////////////////

        /// <summary>
        /// Return true if this event is an Always Available event
        /// </summary>
        /// <returns></returns>
        public bool isAlwaysAvailable()
        {
            return this.start.day == DayOfWeek.Monday && this.start.hour == 0 && this.start.minute == 0 && this.start.second == 0 &&
                this.duration.days == 6 && this.duration.hours == 23 && this.duration.minutes == 59 && this.duration.seconds == 59;
        }
        
        /// <summary>
        /// Return an integer in the range [0, 6]
        /// </summary>
        /// <returns>The cardinal of the start day</returns>
        public int resolveDay()
        {
            return (int)this.start.day;            
        }


        public override string ToString()
        {
            if (this.isAlwaysAvailable())
            {
                return "Always available";
            }

            //--Compute after duration
            int finishSecond = this.start.second;
            int finishMinute = this.start.minute;
            int finishHour = this.start.hour;
            string finishDay = "";

            finishSecond += this.duration.seconds;
            if (finishSecond >= 60)
            {
                finishMinute += finishSecond - 60;
                finishSecond -= 60;
            }

            finishMinute += this.duration.minutes;
            if (finishMinute >= 60)
            {
                finishHour += finishMinute - 60;
                finishMinute -= 60;
            }

            finishHour += this.duration.hours;
            if (finishHour >= 24)
            {
                finishDay = Enum.GetName(typeof(DayOfWeek), (int)(((this.resolveDay() + this.duration.days) + 1) % 7));
                finishHour -= 24;
            }
            else
            {
                finishDay = Enum.GetName(typeof(DayOfWeek), (int)((this.resolveDay() + this.duration.days) % 7));
            }

            //return this.startDay.Substring(0, 3) + "/" + this.startHour + "/" + this.startMinute + "/" + this.startSecond;
            return this.start.day + " - " + formatDate(this.start.hour) + ":" + formatDate(this.start.minute) + ":" + formatDate(this.start.second) + " => " + finishDay + " - " + formatDate(finishHour) + ":" + formatDate(finishMinute) + ":" + formatDate(finishSecond);
        }

        private static string formatDate(int num)
        {
            if (num < 10)
                return "0" + num.ToString();
            return num.ToString();
        }

        public static CalendarEventType makeAlwaysAvailableDate()
        {
            return new CalendarEventType(
                new Start(DayOfWeek.Monday, 0, 0, 0),
                new Duration(6, 23, 59, 59)
               );
        }
    }

    public sealed class Start
    {
        [XmlAttribute("day")]
        public DayOfWeek day;
        [XmlAttribute("hour")]
        public ushort hour;
        [XmlAttribute("minute")]
        public ushort minute;
        [XmlAttribute("second")]
        public ushort second;

        public Start() {}

        public Start(DayOfWeek day, ushort hour, ushort minute, ushort second)
        {
            this.day = day;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }        
    }

    public sealed class Duration 
    {
        /// <summary>
        /// The number of days the event lasts (0 to 6).
        /// Since this is a weekly planning, cannot be greater than 6.</summary>
        [XmlAttribute("days")]
        public ushort days;
        /// <summary>
        /// The number of hours the event lasts (0 to 23).
        /// A number greater than 23 cannot be used to describe an event
        /// lasting more than one day. The durationDays element must be used.</summary>
        [XmlAttribute("hours")]
        public ushort hours;
        /// <summary>
        /// The number of minutes the event last (0 to 59)
        /// A number greater than 59 cannot be used to describe an event
        /// lasting more than one hour. The durationDays or durationHours
        /// elements must be used..</summary>
        [XmlAttribute("minutes")]
        public ushort minutes;
        /// <summary>
        /// The number of seconds the event last (0 to 59)
        /// A number greater than 59 cannot be used to describe an event
        /// lasting more than one minute. The durationDays, durationHours or
        /// durationMinutes elements must be used.</summary>
        [XmlAttribute("seconds")]
        public ushort seconds;

        public Duration() {}

        public Duration(ushort days, ushort hours, ushort minutes, ushort seconds)
        {
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }
    }
}
