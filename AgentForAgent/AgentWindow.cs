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
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using ConfigParser;
using Microsoft.Win32;
using ProActiveAgent;

namespace AgentForAgent
{
    public partial class AgentWindow : Form
    {        
        private readonly string agentLocation;
        private readonly string configFileLocation;
        private readonly string logsDirectory;
        private ServiceController sc;
        private Thread pipeClientThread;

        public AgentWindow(string agentLocation, string configFileLocation, string logsDirectory, ServiceController sc)
        {
            this.agentLocation = agentLocation;
            this.configFileLocation = configFileLocation;
            this.logsDirectory = logsDirectory;
            this.sc = sc;

            // Init all visuals components
            InitializeComponent();
        }

        // !! Event !!
        private void AgentWindow_Load(object sender, EventArgs e)
        {
            this.configFileLocationTextBox.Enabled = false;
            this.configFileLocationTextBox.Text = this.configFileLocation;
            this.configFileLocationTextBox.Enabled = true;

            // Administrators only can change the account used to spawn runtimes
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            if (principal.IsInRole(WindowsBuiltInRole.Administrator) || principal.IsInRole(WindowsBuiltInRole.AccountOperator) || principal.IsInRole(WindowsBuiltInRole.SystemOperator))
            {
                // role should be allowed to change the account
            } else {
                this.changeAccountButton.Enabled = false;
                this.changeAccountButton.Visible = false;
            }

            // Update the status 
            this.updateFromServiceStatus();
        }

        private void updateFromServiceStatus()
        {
            if (this.sc == null)
            {
                return;
            }
            // Refresh the service status
            this.sc.Refresh();
            // Update the status text on the GUI
            this.agentStatusValue.Text = this.agentStatusNotifyIcon.Text = Enum.GetName(typeof(ServiceControllerStatus), this.sc.Status);
            switch (this.sc.Status)
            {
                case ServiceControllerStatus.StopPending:
                    this.startService.Enabled = false;
                    this.stopService.Enabled = false;
                    this.startServiceToolStripMenuItem.Enabled = false;
                    this.stopServiceToolStripMenuItem.Enabled = false;
                    break;
                case ServiceControllerStatus.Stopped:
                    this.startService.Enabled = true;
                    this.stopService.Enabled = false;
                    this.startServiceToolStripMenuItem.Enabled = true;
                    this.stopServiceToolStripMenuItem.Enabled = false;

                    // Set the pipe to null
                    this.pipeClientThread = null;
                    break;
                case ServiceControllerStatus.StartPending:
                    this.startService.Enabled = false;
                    this.stopService.Enabled = false;
                    this.startServiceToolStripMenuItem.Enabled = false;
                    this.stopServiceToolStripMenuItem.Enabled = false;
                    break;
                case ServiceControllerStatus.Running:
                    this.startService.Enabled = false;
                    this.stopService.Enabled = true;
                    this.startServiceToolStripMenuItem.Enabled = false;
                    this.stopServiceToolStripMenuItem.Enabled = true;

                    // Start a new thread that will receive data from the service through the pipe
                    // it should be started only once per started service
                    this.startPipeClientThread();
                    break;
                default:
                    this.startService.Enabled = false;
                    this.stopService.Enabled = false;
                    this.startServiceToolStripMenuItem.Enabled = false;
                    this.stopServiceToolStripMenuItem.Enabled = false;
                    break;
            }

            // If the service is not running a red icon will appear
            if (sc.Status != ServiceControllerStatus.Running)
            {
                agentStatusNotifyIcon.Icon = Icon = (Icon)Resource1.icon_stop;
            }
        }

        // !! WARNING - MUST BE CALLED ONLY IF THE SERVICE IS STARTED !!
        private void startPipeClientThread()
        {
            // There must be no already running pipe client thread
            // and the service must be running otherwise the pipe.Connect()
            // can hang on at 100% CPU
            if (this.pipeClientThread == null)
            {
                this.pipeClientThread = new Thread(receiveFromPipe);
                this.pipeClientThread.IsBackground = true;
                pipeClientThread.Start();
            }
        }

        private void receiveFromPipe()
        {
            using (NamedPipeClientStream pipeStream = new NamedPipeClientStream(".", Constants.PIPE_NAME, PipeDirection.In))
            {
                try
                {
                    // The connect function will indefinately wait for the pipe to become available
                    // If that is not acceptable specify a maximum waiting time (in ms)
                    try
                    {
                        pipeStream.Connect(3 * 1000); // timeout avoids undefinite occupation of 100% cpu on waiting
                    }
                    catch (Exception)
                    {
                        // There was a problem exit from this method
                        return;
                    }

                    using (StreamReader sr = new StreamReader(pipeStream))
                    {
                        string temp;
                        // We read from the pipe to the end
                        while ((temp = sr.ReadLine()) != null)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                if (this.spawnedRuntimesValue.Text != temp)
                                {
                                    this.spawnedRuntimesValue.Text = "" + temp; // runs on UI thread
                                    int runningExecutorsCount = Convert.ToInt32(temp);
                                    agentStatusNotifyIcon.Icon = Icon = (runningExecutorsCount > 0 ? (Icon)Resource1.icon_active : (Icon)Resource1.icon_passive);
                                }
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    // Pipe connection lost, maybe later we can handle this and report it to the user
                    // anyway if the service is restarted manually, the gui will need to be restarted 
                }
                this.Invoke((MethodInvoker)delegate
                {
                    this.spawnedRuntimesValue.Text = "";
                });
            }
        }

        public void setConfigLocation(string title)
        {
            configFileLocationTextBox.Text = title;
        }

        public void askAndRestart()
        {
            if (this.sc.Status == ServiceControllerStatus.Running)
            {
                DialogResult res = MessageBox.Show("The ProActive Agent must be restarted to apply changes.\nRestart now?", "Restart the ProActive Agent", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    this.stopService_Click(null, null);
                    this.sc.WaitForStatus(ServiceControllerStatus.Stopped);
                    this.startService_Click(null, null);
                }
            }
        }

        // !! Event !!
        private void editConfig_Click(object sender, EventArgs e)
        {
            string filename = this.configFileLocationTextBox.Text;
            Process p = new Process();
            p.StartInfo.FileName = "notepad.exe";
            p.StartInfo.Arguments = filename;
            //p.StartInfo.UseShellExecute = false;
            p.Start();
        }

        // !! Event !!
        private void startService_Click(object sender, EventArgs e)
        {
            // 1 - Parse the xml config file            
            try
            {
                ConfigurationParser.parseXml(configFileLocationTextBox.Text);
            }
            catch (Exception ex) // since the parseXml method does not declare any exceptions like IncorrectConfigurationException 
            {
                // Cannot continue show error message box and exit from this method
                MessageBox.Show("The configuration file is broken. " + ex.ToString(), "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2 - Start the service
            try
            {
                sc.Start();
            }
            catch (Exception ex)
            {
                // Cannot continue show error message box and exit from this method
                MessageBox.Show("Could not start the service. " + ex.ToString(), "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3 - Disable gui components to avoid user errors
            startService.Enabled = false;
            stopService.Enabled = false;
            startServiceToolStripMenuItem.Enabled = false;
            stopServiceToolStripMenuItem.Enabled = false;
        }

        // !! Event !!
        private void stopService_Click(object sender, EventArgs e)
        {
            // 1 - Refresh the service status
            this.sc.Refresh();

            // 2 - Stop the service through the service controller
            try
            {
                if (this.sc.CanStop && this.sc.Status != ServiceControllerStatus.Stopped)
                {
                    this.sc.Stop();
                }
            }
            catch (Exception ex)
            {
                // Cannot continue show error message box and exit from this method
                MessageBox.Show("Cannot stop the service. " + ex.ToString(), "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 3 - Disable all controls
            startService.Enabled = false;
            stopService.Enabled = false;
            startServiceToolStripMenuItem.Enabled = false;
            stopServiceToolStripMenuItem.Enabled = false;

            // 4 - The pipe client thread will terminate and the ref is no longer needed
            this.pipeClientThread = null;
        }

        // !! Event !!
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.updateFromServiceStatus();
            }
            catch (Exception)
            {
                // In case of exception just exit silently
                // This can happend when the service is terminated
            }
        }

        // !! Event !!
        private void browse_Click(object sender, EventArgs args)
        {
            browseConfig.FileName = configFileLocationTextBox.Text;
            browseConfig.Filter = "Xml File|*.xml";
            browseConfig.ShowDialog();
            configFileLocationTextBox.Text = browseConfig.FileName;

            // Try to validate against current version schema
            try
            {
                ConfigurationParser.validateXMLFile(configFileLocationTextBox.Text, agentLocation);
            }
            catch (Exception e)
            {
                MessageBox.Show("The selected file is not a valid xml file du to " + e.Message + " The Agent will probably not work. You can edit the file in the text editor or the graphical one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // !! Event !!
        private void scrSvrStart_Click(object sender, EventArgs e)
        {
            Process p = new Process();

            p.StartInfo.FileName = Environment.GetEnvironmentVariable("SystemRoot") + "\\system32\\ProActiveSSaver.scr";
            try
            {
                p.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("The ProActive Screen Saver could not be started", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // !! Event !!
        private void ConfigurationDialog_Resize(object sender, EventArgs e)
        {

            if (FormWindowState.Minimized == this.WindowState)
            {
                agentStatusNotifyIcon.Visible = true;
            }
        }

        // !! Event !!
        private void ConfigurationDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (this.sc != null)
                {
                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();
                    e.Cancel = true;
                }
            }
        }

        // !! Event !!
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        // !! Event !!
        private void guiEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                AgentType conf = ConfigurationParser.parseXml(configFileLocationTextBox.Text);
                ConfigurationEditor configEditor = new ConfigurationEditor(conf, configFileLocationTextBox.Text, agentLocation, this);
                configEditor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The configuration file is broken. " + ex.ToString(), "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // !! Event !!
        private void changeAccountButton_Click(object sender, EventArgs e)
        {
            // Check if the service is in stopped state
            if (this.sc.Status != ServiceControllerStatus.Stopped)
            {
                MessageBox.Show("The ProActive Agent must stopped.", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // The following will show the account change dialog
            ChangeAccount.createChangeAccountAndShow();
        }

        // !! Event !!
        private void closeAdministrationPanelToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("This operation doesn't change the state of ProActive service", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (res == DialogResult.OK)
            {
                try
                {
                    this.sc.Close();
                    this.sc = null;
                    base.Close();
                }
                catch (Exception)
                {
                }

                System.Windows.Forms.Application.Exit();
            }
        }

        // !! Event !!
        private void configFileLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            // To intercept initial assignement of the TextBox
            if (this.configFileLocationTextBox.Enabled)
            {
                RegistryKey confKey = Registry.LocalMachine.OpenSubKey(Constants.REG_SUBKEY, true);
                if (confKey != null)
                {
                    confKey.SetValue(Constants.CONFIG_LOCATION_REG_VALUE_NAME, configFileLocationTextBox.Text);
                    confKey.Close();
                }
            }
        }

        // !! Event !!
        private void docActiveeonLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.docActiveeonLinkLabel.Text);
        }

        // !! Event !!
        private void activeeonLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.activeeonLinkLabel.Text);
        }

        // !! Event !!
        private void proactiveLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.proActiveLinkLabel.Text);
        }

        private Process logsNotepadProcess;

        // !! Event !!
        private void withNotepadButton_Click(object sender, EventArgs e)
        {
            string logfile = this.logsDirectory + "\\ProActiveAgent-log.txt";

            if (!File.Exists(logfile))
            {
                return;
            }

            if (logsNotepadProcess != null && !logsNotepadProcess.HasExited)
            {
                logsNotepadProcess.Kill();
            }

            logsNotepadProcess = new Process();
            logsNotepadProcess.StartInfo.FileName = "notepad.exe";
            logsNotepadProcess.StartInfo.Arguments = logfile;
            logsNotepadProcess.Start();
        }

        private Process logsBrowserProcess;

        // !! Event !!
        private void withIExplorerButton_Click(object sender, EventArgs e)
        {
            string logfile = this.logsDirectory + "\\ProActiveAgent-log.txt";

            if (!File.Exists(logfile))
            {
                return;
            }

            if (logsBrowserProcess == null || logsBrowserProcess.HasExited)
            {
                logsBrowserProcess = new Process();
                logsBrowserProcess.StartInfo.FileName = "iexplore.exe";
                logsBrowserProcess.StartInfo.Arguments = logfile;
                logsBrowserProcess.Start();
            }
            else
            {
                int hwnd = (int)logsBrowserProcess.MainWindowHandle;
                ShowWindow(hwnd, SW_SHOWNORMAL);
                SetForegroundWindow(hwnd);
            }
        }

        // !! Event !!
        private void viewLogsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!Directory.Exists(this.logsDirectory))
            {
                return;
            }
            System.Diagnostics.Process.Start(this.logsDirectory);
        }

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        private const int SW_SHOWNORMAL = 1;
    }
}
