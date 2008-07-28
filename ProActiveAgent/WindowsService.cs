﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;
using System.Diagnostics;
using System.IO;
using ConfigParser;
using System.Threading;
using Microsoft.Win32;

namespace ProActiveAgent
{
    class WindowsService : ServiceBase
    {
        private static string CONFIG_LOCATION = "c:\\PAAgent-config.xml";
        private static string AGENT_LOCATION = "";

        private string configLocation;
        private string agentLocation;
        private Configuration configuration;
        private TimerManager timerManager;
        private ProActiveExec exec;
        private Logger logger;

        /// <summary>

        /// Public Constructor for WindowsService.

        /// - Put all of your Initialization code here.

        /// </summary>

        public WindowsService()
        {
            this.configLocation = CONFIG_LOCATION;
            this.agentLocation = AGENT_LOCATION;

            readRegistryConfiguration();

            this.ServiceName = "ProActive Agent";
            this.EventLog.Log = "Application";

            // These Flags set whether or not to handle that specific

            //  type of event. Set to true if you need it, false otherwise.

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;

            this.timerManager = null;
            this.exec = null;
        }

        private void readRegistryConfiguration()
        {

            RegistryKey confKey = Registry.LocalMachine.OpenSubKey("Software\\ProActiveAgent");
            if (confKey != null)
            {
                if (confKey.GetValue("ConfigLocation") != null)
                {
                    this.configLocation = (string)confKey.GetValue("ConfigLocation");
                }
                if (confKey.GetValue("AgentDirectory") != null)
                {
                    this.agentLocation = (string)confKey.GetValue("AgentDirectory");
                }
            }
        }

        /// <summary>

        /// The Main Thread: This is where your Service is Run.

        /// </summary>

        static void Main()
        {
            ServiceBase.Run(new WindowsService());
        }

        /// <summary>

        /// Dispose of objects that need it here.

        /// </summary>

        /// <param name="disposing">Whether

        ///    or not disposing is going on.</param>

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>

        /// OnStart(): Put startup code here

        ///  - Start threads, get inital data, etc.

        /// </summary>

        /// <param name="args"></param>

        protected override void OnStart(string[] args)
        {
            // TODO: zero all of the members/properties

            if (args.Length > 0)
                this.configLocation = args[0];
            
            this.configuration = ConfigurationParser.parseXml(configLocation, agentLocation);
            LoggerComposite composite = new LoggerComposite();
            composite.addLogger(new FileLogger(this.agentLocation));
            composite.addLogger(new EventLogger());
            this.logger = composite;
            logger.log("Starting ProActiveAgent Service", LogLevel.TRACE);
            this.exec = new ProActiveExec(logger, agentLocation, configuration.agentConfig.jvmParams,
                configuration.agentConfig.javaHome, configuration.agentConfig.proactiveLocation,
                configuration.action.priority);
            this.timerManager = new TimerManager(this.configuration, exec);
            this.exec.setTimerMgr(timerManager);

            base.OnStart(args);
        }

        /// <summary>

        /// OnStop(): Put your stop code here

        /// - Stop threads, set final data, etc.

        /// </summary>

        protected override void OnStop()
        {
            logger.log("Stopping ProActiveAgent Service", LogLevel.TRACE);
            exec.dispose();
            timerManager.dispose();
            exec.sendGlobalStop();
            logger.onStopService();
            this.configuration = null;
            this.timerManager = null;
            this.exec = null;
            this.logger = null;
            base.OnStop();
        }

        /// <summary>

        /// OnPause: Put your pause code here

        /// - Pause working threads, etc.

        /// </summary>

        protected override void OnPause()
        {
            timerManager.onPause();
            base.OnPause();
        }

        /// <summary>

        /// OnContinue(): Put your continue code here

        /// - Un-pause working threads, etc.

        /// </summary>

        protected override void OnContinue()
        {
            timerManager.onResume();
            base.OnContinue();
        }

        /// <summary>

        /// OnShutdown(): Called when the System is shutting down

        /// - Put code here when you need special handling

        ///   of code that deals with a system shutdown, such

        ///   as saving special data before shutdown.

        /// </summary>

        protected override void OnShutdown()
        {
            logger.log("ONShutdown called!", LogLevel.TRACE);
            exec.disableRestarting();
/*            exec.disableRestarting();
            exec.sendGlobalStop();
            this.configuration = null;
            this.timerManager = null;
            this.exec = null; */
            base.OnShutdown();
        }

        /// <summary>

        /// OnCustomCommand(): If you need to send a command to your

        ///   service without the need for Remoting or Sockets, use

        ///   this method to do custom methods.

        /// </summary>

        /// <param name="command">Arbitrary Integer between 128 & 256</param>

        protected override void OnCustomCommand(int command)
        {
            //  A custom command can be sent to a service by using this method:

            //#  int command = 128; //Some Arbitrary number between 128 & 256

            //#  ServiceController sc = new ServiceController("NameOfService");

            //#  sc.ExecuteCommand(command);

            if (command == (int)PAACommands.ScreenSaverStart)
            {
                logger.log("Received start command from ProActive ScreenSaver", LogLevel.TRACE);
                exec.sendStartAction(configuration.action, ApplicationType.AgentScreensaver);
            }
            else if (command == (int)PAACommands.ScreenSaverStop)
            {
                logger.log("Received stop command from ProActive ScreenSaver", LogLevel.TRACE);
                exec.sendStopAction(configuration.action, ApplicationType.AgentScreensaver);
            }
            else if (command == (int)PAACommands.GlobalStop)
            {
                logger.log("Received global stop command", LogLevel.TRACE);
                exec.sendGlobalStop();
            }

            base.OnCustomCommand(command);
        }

        /// <summary>

        /// OnPowerEvent(): Useful for detecting power status changes,

        ///   such as going into Suspend mode or Low Battery for laptops.

        /// </summary>

        /// <param name="powerStatus">The Power Broadcast Status

        /// (BatteryLow, Suspend, etc.)</param>

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        /// <summary>

        /// OnSessionChange(): To handle a change event

        ///   from a Terminal Server session.

        ///   Useful if you need to determine

        ///   when a user logs in remotely or logs off,

        ///   or when someone logs into the console.

        /// </summary>

        /// <param name="changeDescription">The Session Change

        /// Event that occured.</param>

        protected override void OnSessionChange(
                  SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }


    }
}