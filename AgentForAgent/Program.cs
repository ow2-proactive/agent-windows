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
 * $$ACTIVEEON_CONTRIBUTOR$$
 */
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceProcess;
using System.Windows.Forms;
using Microsoft.Win32;
using ProActiveAgent;

namespace AgentForAgent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!ProcessChecker.IsOnlyProcess("ProActive Agent Control"))
            {
                return;
            }

            // Guest accounts Check if the current user have admin rights   
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            if (principal.IsInRole(WindowsBuiltInRole.Guest))
            {
                // report error and exit                     
                MessageBox.Show("Guest users cannot run the ProActive Agent Control.", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if this application is already running
            Process[] alreadyRunningProcesses = Process.GetProcessesByName("AgentForAgent");
            if (alreadyRunningProcesses != null && alreadyRunningProcesses.Length > 1)
            {
                return;
            }

            // Check agent and config locations in the registry (setted during agent installation)
            // ie check if the agent was correctly installed
            try
            {
                RegistryKey agentKey = Registry.LocalMachine.OpenSubKey(Constants.REG_SUBKEY);
                if (agentKey == null)
                {
                    // Cannot continue, report error message box and exit                         
                    MessageBox.Show("Can not open the following registry subkey (LocalMachine) " + Constants.REG_SUBKEY + ". It appears that the agent might not have been installed properly.", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string agentLocation = (string)agentKey.GetValue(Constants.INSTALL_LOCATION_REG_VALUE_NAME);
                if (agentLocation == null)
                {
                    // report error and exit                     
                    MessageBox.Show("Cannot get the agent location in " + Constants.REG_SUBKEY + ". It appears that the agent might not have been installed properly.", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Close the registry key
                    agentKey.Close();
                    return;
                }

                string configLocation = (string)agentKey.GetValue(Constants.CONFIG_LOCATION_REG_VALUE_NAME);
                if (configLocation == null)
                {
                    // report error and exit
                    MessageBox.Show("Cannot get the config location in " + Constants.REG_SUBKEY + ". It appears that the agent might not have been installed properly.", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Close the registry key
                    agentKey.Close();
                    return;
                }

                string logsDirectory = (string)agentKey.GetValue(Constants.LOGS_DIR_REG_VALUE_NAME);
                if (logsDirectory == null)
                {
                    // report error and exit
                    MessageBox.Show("Cannot get the logs directory in " + Constants.REG_SUBKEY + ". It appears that the agent might not have been installed properly.", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Close the registry key
                    agentKey.Close();
                    return;
                }

                // Close the registry key
                agentKey.Close();

                // Connect to the agent service
                ServiceController sc = null;
                try
                {
                    sc = new ServiceController(Constants.SERVICE_NAME);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not connect to the service " + Constants.SERVICE_NAME + ". It appears that the agent might not have been installed properly. " + e.ToString(), "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Launch the GUI
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                AgentWindow dialog = new AgentWindow(agentLocation, configLocation, logsDirectory, sc);
                Application.Run(dialog);                
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to run the ProActive Agent Control " + e.ToString(), "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
