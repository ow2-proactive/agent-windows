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
using ConfigParser;
using log4net;

namespace ProActiveAgent
{
    /// <summary>
    /// Common information collected at runtime and shared between multiple ProActive Runtime Executors
    /// </summary> 
    sealed class CommonStartInfo
    {
        private static readonly ILog LOGGER = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // Initialized by the WindowsService.main(), the directory will contain all logs file (runtime and executors logs)
        public static string logsDirectory;
        /// <summary>
        /// The configuration used to run the process</summary>
        private readonly AgentType _configuration;
        /// <summary>
        /// The selected connection</summary>
        private readonly ConnectionType _enabledConnection;
        /// <summary>
        /// All jvm options (default and user defined)</summary>
        private readonly string[] _jvmOptions;
        /// <summary>
        /// The runtime start delay to avoid concurrent access problems</summary>
        private readonly int _runtimeStartDelayInMs = 500;
        /// <summary>
        /// The runtime start delay is enabled if the PA_AGENT_RUNTIME_START_DELAY system env variable is defined</summary>
        private readonly bool _runtimeStartDelayEnabled;
        /// <summary>
        /// If always available and max cpu usage is 100, no need to use the cpu limiter</summary>
        private readonly bool _cpuLimiterEnabled;
        /// <summary>
        /// The constructor of this class.</summary>
        public CommonStartInfo(AgentType configuration)
        {
            this._configuration = configuration;
            // Get the selected connection, if no connection is enabled it is an error
            this._enabledConnection = configuration.getEnabledConnection();
            if (this._enabledConnection == null)
            {
                LOGGER.Error("No selected connection in the configuration. Exiting ...");
                Environment.Exit(0);
            }
            else
            {
                LOGGER.Info("Selected connection " + this._enabledConnection.GetType().Name);
            }            

            // The list of jvm options (default + user defined)
            List<string> mergedJvmOptionsList = new List<string>();
            // Add default parameters
            this._enabledConnection.fillDefaultJvmOptions(mergedJvmOptionsList, this._configuration.config.proactiveHome);
            // Add user defined
            if (this._configuration.config.jvmParameters != null)
            {
                mergedJvmOptionsList.AddRange(this._configuration.config.jvmParameters);
            }
            this._jvmOptions = mergedJvmOptionsList.ToArray();

            // The system env variable must not be null otherwise the delay is not enabled
            string value = System.Environment.GetEnvironmentVariable("PA_AGENT_RUNTIME_START_DELAY", EnvironmentVariableTarget.Machine);
            if (value != null)
            {
                if (!Int32.TryParse(value, out this._runtimeStartDelayInMs))
                    LOGGER.Warn("Unable to parse the runtime start delay using default value " + this._runtimeStartDelayInMs + " ms");
                else
                {
                    this._runtimeStartDelayEnabled = true;
                    LOGGER.Info("Runtime start delay is set to " + this._runtimeStartDelayInMs + " ms");
                }
            }
            this._cpuLimiterEnabled = !(configuration.isAlwaysAvailable() && configuration.config.maxCpuUsage == 100);
        }

        public AgentType configuration
        {
            get
            {
                return this._configuration;
            }
        }

        public ConnectionType enabledConnection
        {
            get
            {
                return this._enabledConnection;
            }
        }

        public string[] jvmOptions
        {
            get
            {
                return this._jvmOptions;
            }
        }

        public string starterClass
        {
            get
            {
                return this._enabledConnection.javaStarterClass;
            }
        }

        public int runtimeStartDelayInMs
        {
            get
            {
                return this._runtimeStartDelayInMs;
            }
        }

        public bool isRuntimeStartDelayEnabled
        {
            get 
            {
                return this._runtimeStartDelayEnabled;
            }
        }

        public bool isCpuLimiterEnabled
        {
            get
            {
                return this._cpuLimiterEnabled;
            }
        }
    }
}
