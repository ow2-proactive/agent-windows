using ConfigParser;
using log4net;
using Microsoft.Win32;
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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace ProActiveAgent
{
    /// <summary>
    /// Class used for constants definition</summary>
    /// <remarks>
    /// Some of these constants may be used by other projects. The entire solution may need to be regenerated in case of modification.</remarks>
    public static class Constants
    {
        /// <summary>
        /// The name of the ProActive Agent service</summary>
        public const string SERVICE_NAME = "ProActiveAgent";
        /// <summary>
        /// The default install dir location of the ProActive Agent.</summary>
        public const string DEFAULT_INSTALL_LOCATION = "C:\\Program Files\\ProActiveAgent";
        /// <summary>
        /// The default config location of the ProActive Agent.</summary>
        public const string DEFAULT_CONFIG_LOCATION = DEFAULT_INSTALL_LOCATION + "\\config\\PAAgent-config.xml";
        /// <summary>
        /// The windows registry subkey used by ProActive Agent.</summary>
        public const string REG_SUBKEY = "Software\\ProActiveAgent";
        /// <summary>
        /// The windows registry subkey that contains credentials of the forker.</summary>
        public const string REG_CREDS_SUBKEY = "Software\\ProActiveAgent\\Creds";
        /// <summary>
        /// The name of the reg value used for install location.</summary>
        public const string INSTALL_LOCATION_REG_VALUE_NAME = "AgentLocation";
        /// <summary>
        /// The name of the reg value used for config location.</summary>
        public const string CONFIG_LOCATION_REG_VALUE_NAME = "ConfigLocation";
        /// <summary>
        /// The name of the reg value used for logs directory.</summary>
        public const string LOGS_DIR_REG_VALUE_NAME = "LogsDirectory";
        /// <summary>
        /// A boolean flag to allow memory limitation per job (set of processes).</summary>
        public const bool ALLOW_PROCESS_MEMORY_LIMIT = true;
        /// <summary>
        /// The name of the job object used for usage limitations.</summary>
        public const string JOB_OBJECT_NAME = "ProActiveAgentJobObject";
        /// <summary>
        /// The name of the classpath variable.</summary>
        public const string CLASSPATH = "CLASSPATH";
        /// <summary>
        /// The name of the classpath variable.</summary>
        public const string JAVA_HOME = "JAVA_HOME";
        /// <summary>
        /// The java.exe location relative to JAVA_HOME.</summary>
        public const string BIN_JAVA = @"\bin\java.exe";
        /// <summary>
        /// The name of the ProActive Communication Protocol java property.</summary>
        public const string PROACTIVE_COMMUNICATION_PROTOCOL_JAVA_PROPERTY = "-Dproactive.communication.protocol";
        /// <summary>
        /// The name of the ProActive Agent rank java property.</summary>
        public const string PROACTIVE_AGENT_RANK_JAVA_PROPERTY = "-Dproactive.agent.rank";
        /// <summary>
        /// The maximum value allowed for the proactive rmi port.</summary>
        public const int MAX_PROACTIVE_RMI_PORT = 65534;
        /// <summary>
        /// The link to the official documentation.</summary>
        public const string DOC_LINK = "http://proactive.inria.fr/release-doc/Resourcing/multiple_html/ProActiveWindowsAgent_89.html";
        /// <summary>
        /// The default Resource Manager url.</summary>
        public const string DEFAULT_RM_URL = "rmi://localhost:1099";
        /// <summary>
        /// The name of the pipe used by the ProActive agent to communicate with the gui.</summary>
        public const string PIPE_NAME = "ProActiveAgentPipe";
        /// <summary>
        /// A unique ISO8601 like date format string.</summary>
        public const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
    }

    /// <summary>
    /// Enumeration type of ProActive Agent Service custom commands.
    /// There are following commands:
    ///    Start - trigger action
    ///    Stop  - stops triggered action or do nothing when it has not been triggered
    /// </summary>
    public enum PAACommands
    {
        ScreenSaverStart = 128,
        ScreenSaverStop = 129
    }

    /// <summary>
    /// Enumeration type of ProActive Agent running type.
    /// </summary>
    public enum ApplicationType
    {
        AgentScheduler = 1,
        AgentScreensaver = 2
    }

    /// <summary>
    /// A static class that contains several utilitary methods</summary>
    public static class Utils
    {
        [DllImport("pacrypt.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int encryptData(
            string inputData,
            StringBuilder outputData);

        [DllImport("pacrypt.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int decryptData(
            string inputData,
            StringBuilder outputData);


        /// <summary>
        /// Encrypts the password given as param then stores all creds into the registry.
        /// Later we should use SecureString here ...
        /// </summary> 
        /// <param name="domain">The account domain</param>
        /// <param name="username">The account username</param>
        /// <param name="password">The account password</param>
        public static void storeRuntimeAccount(string domain, string username, string password)
        {

            // 1 - Try to encrypt the password            
            StringBuilder encryptedPass = new StringBuilder(512);
            int res = Utils.encryptData(password, encryptedPass);
            if (res != 0)
            {
                throw new ApplicationException("Problem " + res);
            }

            // 2 - Store the credentials into the registry
            RegistryKey confKey = null;
            try
            {
                confKey = Registry.LocalMachine.OpenSubKey(Constants.REG_CREDS_SUBKEY, true);

                if (confKey == null)
                    throw new ApplicationException("Unable to open credentials sub key");

                confKey.SetValue("domain", domain);
                confKey.SetValue("username", username);
                confKey.SetValue("password", encryptedPass.ToString());

                confKey.Close();
            }
            finally
            {
                if (confKey != null)
                    confKey.Close();
            }
        }

        /// <summary>
        /// Returns a decimal value of the available physical memory in mbytes of this computer.
        /// </summary> 
        /// <returns>The available physical memory of this computer.</returns>
        public static Decimal getAvailablePhysicalMemory()
        {
            Decimal result = 0;
            // THE FOLLOWING DOES NOT WORK ON VISTA :
            //ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_LogicalMemoryConfiguration");
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);
            //foreach (ManagementObject item in searcher.Get())
            //{
            //    String s = item["TotalPhysicalMemory"].ToString();
            //    result = System.Decimal.Ceiling(System.Decimal.Parse(s) / 1024); // convert to Mbytes
            //}
            // This seems more appropriate            
            // result = System.Decimal.Ceiling(new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory / (1024 * 1024)); // from bytes to mbytes
            // Another method
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            result = Convert.ToDecimal(ramCounter.NextValue());
            return result;
        }

        /// <summary>
        /// Reads the configuration file, UNC path are accessed within the forker context.
        /// </summary>
        /// <param name="agentConfigFilePath">The path to the configuraiton file</param>
        /// <param name="agentInstallLocation">The path to the agent install location</param>        
        public static ConfigParser.AgentType readConfig(string agentConfigFilePath, string agentInstallLocation)
        {           
            // First check if the configuration file is in UNC format
            if (agentConfigFilePath.StartsWith(@"\\"))
            {
                string username = null;
                string domain = null;
                string password = null;

                // Get forker credentials from registry
                RegistryKey confKey = Registry.LocalMachine.OpenSubKey(Constants.REG_CREDS_SUBKEY);

                if (confKey == null)
                    throw new ApplicationException("Unable to read credentials from registry");

                username = (string)confKey.GetValue("username");
                domain = (string)confKey.GetValue("domain");
                // The password is encrypted, decryption is needed
                string encryptedPassword = (string)confKey.GetValue("password");
                confKey.Close();

                // To decrypt the password call the decryptData using pinvoke
                StringBuilder decryptedPassword = new StringBuilder(32); // suppose passwords are 32 chars max

                {
                    // Save current dir
                    string cd = Directory.GetCurrentDirectory();

                    // Current dir must be the location of the agent
                    Directory.SetCurrentDirectory(agentInstallLocation);

                    // Decrypt the password
                    int res = decryptData(encryptedPassword, decryptedPassword);
                    password = decryptedPassword.ToString();

                    if (res != 0)
                        throw new ApplicationException("Problem " + res);

                    // Restore current dir                    
                    Directory.SetCurrentDirectory(cd);
                }

                // Impersonate the forker (get its access rights) in his context UNC paths are accepted
                using (new Impersonator(username, domain, password))
                {
                    // First check if the directory exists
                    if (!File.Exists(agentConfigFilePath))
                        throw new ApplicationException("Unable to read the configuration file, the " + agentConfigFilePath + " does not exist");
                    
                    return ConfigurationParser.parseXml(agentConfigFilePath);
                }
            }
            else
            {
                // The paths are local no need to impersonate
                // First check if the directory exists
                if (!File.Exists(agentConfigFilePath))
                    throw new ApplicationException("Unable to read the configuration file, the " + agentConfigFilePath + " does not exist");

                return ConfigurationParser.parseXml(agentConfigFilePath);
            }
        }

        /// <summary>
        /// Reads the value of the CLASSPATH variable defined in a script and stores it into the configuration.
        /// This method checks if the provided ProActive location contains \bin\windows\init.bat script.
        /// </summary>
        /// <param name="config">The user defined configuration.</param>
        public static void readClasspath(AgentType configuration)
        {
            AgentConfigType config = configuration.config;
            if (config.proactiveHome == null || config.proactiveHome.Equals(""))
            {
                throw new ApplicationException("Unable to read the classpath, the ProActive location is unknown");
            }

            // 1) Use the java location if it's specified in the configuration
            // 2) If not specified use JAVA_HOME variable
            // 3) If JAVA_HOME is not defined or empty throw exception

            if (String.IsNullOrEmpty(config.javaHome))
            {
                // The classpath will be filled using the JAVA_HOME variable defined in the parent environement
                config.javaHome = System.Environment.GetEnvironmentVariable(Constants.JAVA_HOME);
                if (String.IsNullOrEmpty(config.javaHome))
                    throw new ApplicationException("Unable to read the classpath, please specify the java location in the configuration or set JAVA_HOME environement variable");                
            }

            // If bin\init.bat exists use it to get the classpath
            // If bin\windows\init.bat exists use it to get the classpath
            // If none of them exists use standard classpath

            string binInit = config.proactiveHome + @"\bin\init.bat";
            string binWindowsInit = config.proactiveHome + @"\bin\windows\init.bat";

            // First check if the proactiveHome or javaHome path are in UNC format
            if (config.proactiveHome.StartsWith(@"\\") || config.javaHome.StartsWith(@"\\"))
            {
                string username = null;
                string domain = null;
                string password = null;

                // Get forker credentials from registry
                RegistryKey confKey = Registry.LocalMachine.OpenSubKey(Constants.REG_CREDS_SUBKEY);

                if (confKey == null)
                    throw new ApplicationException("Unable to read credentials from registry");

                username = (string)confKey.GetValue("username");
                domain = (string)confKey.GetValue("domain");
                // The password is encrypted, decryption is needed
                string encryptedPassword = (string)confKey.GetValue("password");
                confKey.Close();

                // To decrypt the password call the decryptData using pinvoke
                StringBuilder decryptedPassword = new StringBuilder(32); // suppose passwords are 32 chars max

                {
                    // Save current dir
                    string cd = Directory.GetCurrentDirectory();

                    // Current dir must be the location of the agent
                    Directory.SetCurrentDirectory(configuration.agentInstallLocation);

                    // Decrypt the password
                    int res = decryptData(encryptedPassword, decryptedPassword);
                    password = decryptedPassword.ToString();

                    if (res != 0)
                        throw new ApplicationException("Problem unable to decrypt the password, exitCode=" + res);

                    // Restore current dir
                    Directory.SetCurrentDirectory(cd);
                }

                bool readClasspathFromScript = false;
                string initScript = null;
                string initScriptParentAbsolutePath = null;

                // Impersonate the forker (get its access rights) in his context UNC paths are accepted
                using (new Impersonator(username, domain, password))
                {
                    if (Directory.Exists(config.javaHome)) {
                        if (File.Exists(binInit))
                        {
                            initScript = binInit;
                            initScriptParentAbsolutePath = config.proactiveHome + @"\bin";
                            readClasspathFromScript = true;
                        }
                        else if (File.Exists(binWindowsInit))
                        {
                            initScript = binWindowsInit;
                            initScriptParentAbsolutePath = config.proactiveHome + @"\bin\windows";
                            readClasspathFromScript = true;
                        }
                        else
                        {
                            readClasspathFromScript = false;
                        }
                    } else {
                        readClasspathFromScript = false;
                    }
                }

                if (readClasspathFromScript)
                    config.classpath = VariableEchoer.echoVariableAsForker(
                        config.javaHome,                      // the Java install dir
                        configuration.agentInstallLocation,   // the ProActive Agent install dir
                        initScriptParentAbsolutePath,         // the current directory where to run the initScript
                        initScript,                           // the full path of the initScript
                        Constants.CLASSPATH);                 // the name of the variable to echo
                else                
                    config.classpath = config.proactiveHome + @"\dist\lib\*;" + config.proactiveHome + @"\addons\*";
            }
            else
            {
                // The paths are local no need to impersonate
                ProcessStartInfo info = new ProcessStartInfo();
                info.EnvironmentVariables["PA_SCHEDULER"] = config.proactiveHome;
                info.EnvironmentVariables["PROACTIVE"] = config.proactiveHome;
                info.EnvironmentVariables[Constants.JAVA_HOME] = config.javaHome;

                bool javaHomeExists = Directory.Exists(config.javaHome);

                if (File.Exists(binInit) && javaHomeExists)
                    config.classpath = VariableEchoer.echoVariable(binInit, Constants.CLASSPATH, info);
                else if (File.Exists(binWindowsInit) && javaHomeExists)
                    config.classpath = VariableEchoer.echoVariable(binWindowsInit, Constants.CLASSPATH, info);
                else
                    config.classpath = config.proactiveHome + @"\dist\lib\*;" + config.proactiveHome + @"\addons\*";
            }
        }

        /// <summary>
        /// This method checks if a tcp port is available. 
        /// !! DOES NOT CHECK FOR MAX VALUE !!
        /// </summary>
        /// <param name="config">The user defined configuration.</param>
        public static bool isTcpPortAvailable(int port)
        {
            // Evaluate current system tcp connections. This is the same information provided
            // by the netstat -ano | find "port_num" command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will return false
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            // Search through active tcp listeners            
            foreach (IPEndPoint tcpl in ipGlobalProperties.GetActiveTcpListeners())
            {
                if (tcpl.Port == port)
                {

                    return false;
                }
            }
            // Search through active tcp connections
            foreach (TcpConnectionInformation tcpi in ipGlobalProperties.GetActiveTcpConnections())
            {
                if (tcpi.LocalEndPoint.Port == port)
                {

                    return false;
                }
            }

            return true;
        }

        public static string[] listJavaNetworkInterfaces(string javaLocation, string agentLocation)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            // Prepare to create a process that will run the java class
            info.FileName = javaLocation + Constants.BIN_JAVA;
            // Add the agent location as classpath and the name of the java class to execute
            info.Arguments = "-cp \"" + agentLocation + "\" ListNetworkInterfaces";
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.RedirectStandardOutput = true;

            // Create new process 
            Process p = new Process();
            p.StartInfo = info;

            try
            {
                // Start the process
                if (!p.Start())
                {
                    throw new ApplicationException("Unable to start the process " + info.FileName);
                }

                StreamReader myStreamReader = p.StandardOutput;
                List<string> ar = new List<string>();
                string line = myStreamReader.ReadLine();
                while (line != null && !line.Equals(""))
                {
                    ar.Add(line);
                    // Read the standard output of the spawned process.
                    line = myStreamReader.ReadLine();
                }
                p.Close();

                return ar.ToArray();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Unable to list java network interfaces ", e);
            }
        }
    }

    /// <summary>
    /// A static class that spawns a process in order to echo a variable defined in the specified script.</summary>
    public static class VariableEchoer
    {
        private static readonly string PROMPT = ">";

        private static StringBuilder initializerOutput;

        public static String echoVariable(string scriptFilename, string variableToEcho, ProcessStartInfo info)
        {
            // Prepare to create a process that will run the script            
            info.FileName = "cmd.exe";
            // /V:ON is equivalent to setlocal enabledelayedexpansion
            info.Arguments = "/V:ON /K \"" + scriptFilename + "\"";
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;

            // Create new process 
            Process p = new Process();
            p.StartInfo = info;

            // Create output buffer
            VariableEchoer.initializerOutput = new StringBuilder("");

            // Set our event handler to asynchronously read the output.
            p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);

            try
            {
                // Start the process
                if (!p.Start())
                {
                    throw new ApplicationException("Unable to run the script " + scriptFilename);
                }

                // Use a stream writer to synchronously write the sort input.
                System.IO.StreamWriter streamWriter = p.StandardInput;

                // Start the asynchronous read of the output stream.
                p.BeginOutputReadLine();

                // Write the command that will print the full java command
                streamWriter.WriteLine("echo %" + variableToEcho + "%");

                // End the input stream 
                streamWriter.Close();

                // Wait for the process to write the text lines
                p.WaitForExit();

                // Kill the process if its not finished
                if (!p.HasExited)
                {
                    p.Kill();
                }

                return VariableEchoer.initializerOutput.ToString();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Unable to echo the variable " + variableToEcho, e);
            }
            finally
            {
                p.Close();
            }
        }

        private static readonly ILog LOGGER = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static String echoVariableAsForker(string javaHome, string installLocation, string workingdir, string scriptFilename, string variableToEcho)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            // Prepare to create a process that will run the script            
            info.FileName = installLocation + "\\parunas.exe";
            // /V:ON is equivalent to setlocal enabledelayedexpansion
            // The JAVA_HOME is injected since its is required by the initScript
            // The cmd.exe does not support UNC path as working dir therefore the CD is injected to get the correct
            // The initScript will be surrounded with \" (escaped quotes)
            info.Arguments = "\"cmd.exe /V:ON /K set JAVA_HOME=" + javaHome + "&& set CD=" + workingdir + "&& \\\"" + scriptFilename + "\\\"\"";

            info.WorkingDirectory = installLocation;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;

            // Create new process 
            Process p = new Process();
            p.StartInfo = info;

            // Create output buffer
            VariableEchoer.initializerOutput = new StringBuilder("");

            // Set our event handler to asynchronously read the output.
            p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);

            try
            {
                // Start the process
                if (!p.Start())
                {
                    throw new ApplicationException("Unable to run the script " + scriptFilename);
                }

                // Use a stream writer to synchronously write the sort input.
                System.IO.StreamWriter streamWriter = p.StandardInput;

                // Start the asynchronous read of the output stream.
                p.BeginOutputReadLine();

                // Write the command that will print the full java command
                streamWriter.WriteLine("echo %" + variableToEcho + "%");

                // End the input stream 
                streamWriter.Close();

                // Wait for the process to write the text lines
                p.WaitForExit();

                // Kill the process if its not finished
                if (!p.HasExited)
                {
                    p.Kill();
                }

                return VariableEchoer.initializerOutput.ToString();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Unable to echo the variable " + variableToEcho + " Filename=" + info.FileName + " Args=" + info.Arguments, e);
            }
            finally
            {
                p.Close();
            }
        }

        private static void OutputHandler(object sendingProcess, System.Diagnostics.DataReceivedEventArgs outLine)
        {
            if (outLine.Data == null || outLine.Data.Equals("") || outLine.Data.Contains(PROMPT))
            {
                return;
            }
            // Add the text to the collected output
            initializerOutput.Append(outLine.Data);
        }
    }

    /// <summary>
    /// A static class that spawns a process in order to execute a specified script.</summary>
    public static class ScriptExecutor
    {
        public static string executeScript(string installLocation, string scriptAbsolutePath, string[] scriptParams)
        {
            ProcessStartInfo info = new ProcessStartInfo();

            // Prepare to create a process that will run the specified script
            info.FileName = installLocation + "\\parunas.exe";
            info.Arguments = "\"" + scriptAbsolutePath + " " + string.Join(" ", scriptParams) + "\"";
            info.WorkingDirectory = installLocation;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.RedirectStandardOutput = true;
            // Create new process 
            Process p = new Process();
            p.StartInfo = info;

            try
            {
                // Start the process
                if (!p.Start())
                {
                    throw new ApplicationException("Unable to start the process " + info.FileName);
                }

                StreamReader myStreamReader = p.StandardOutput;
                string output = myStreamReader.ReadToEnd();
                p.Close();
                return output;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Unable to execute the script " + scriptAbsolutePath, e);
            }
        }
    }

    /// <summary>
    /// Check running processes for an already-running instance. Implements a simple and
    /// always effective algorithm to find currently running processes with a main window
    /// matching a given substring and focus it.
    /// Combines code written by Lion Shi (MS) and Sam Allen.
    /// </summary>
    public static class ProcessChecker
    {
        /// <summary>
        /// Stores a required string that must be present in the window title for it
        /// to be detected.
        /// </summary>
        static string _requiredString;

        /// <summary>
        /// Contains signatures for C++ DLLs using interop.
        /// </summary>
        internal static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

            [DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern bool EnumWindows(EnumWindowsProcDel lpEnumFunc,
                Int32 lParam);

            [DllImport("user32.dll")]
            public static extern int GetWindowThreadProcessId(IntPtr hWnd,
                ref Int32 lpdwProcessId);

            [DllImport("user32.dll")]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString,
                Int32 nMaxCount);

            public const int SW_SHOWNORMAL = 1;
        }

        public delegate bool EnumWindowsProcDel(IntPtr hWnd, Int32 lParam);

        /// <summary>
        /// Perform finding and showing of running window.
        /// </summary>
        /// <returns>Bool, which is important and must be kept to match up
        /// with system call.</returns>
        static private bool EnumWindowsProc(IntPtr hWnd, Int32 lParam)
        {
            int processId = 0;
            NativeMethods.GetWindowThreadProcessId(hWnd, ref processId);

            StringBuilder caption = new StringBuilder(1024);
            NativeMethods.GetWindowText(hWnd, caption, 1024);

            // Use IndexOf to make sure our required string is in the title.
            if (processId == lParam && (caption.ToString().IndexOf(_requiredString,
                StringComparison.OrdinalIgnoreCase) != -1))
            {
                // Restore the window.
                NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_SHOWNORMAL);
                NativeMethods.SetForegroundWindow(hWnd);
            }
            return true; // Keep this.
        }

        /// <summary>
        /// Find out if we need to continue to load the current process. If we
        /// don't focus the old process that is equivalent to this one.
        /// </summary>
        /// <param name="forceTitle">This string must be contained in the window
        /// to restore. Use a string that contains the most
        /// unique sequence possible. If the program has windows with the string
        /// "Journal", pass that word.</param>
        /// <returns>False if no previous process was activated. True if we did
        /// focus a previous process and should simply exit the current one.</returns>
        static public bool IsOnlyProcess(string forceTitle)
        {
            _requiredString = forceTitle;
            foreach (Process proc in Process.GetProcessesByName(Application.ProductName))
            {
                if (proc.Id != Process.GetCurrentProcess().Id)
                {
                    NativeMethods.EnumWindows(new EnumWindowsProcDel(EnumWindowsProc),
                        proc.Id);
                    return false;
                }
            }
            return true;
        }
    }

    /////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Impersonation of a user. Allows to execute code under another
    /// user context.
    /// Please note that the account that instantiates the Impersonator class
    /// needs to have the 'Act as part of operating system' privilege set.
    /// </summary>
    /// <remarks>	
    /// This class is based on the information in the Microsoft knowledge base
    /// article http://support.microsoft.com/default.aspx?scid=kb;en-us;Q306158
    /// 
    /// Encapsulate an instance into a using-directive like e.g.:
    /// 
    ///		...
    ///		using ( new Impersonator( "myUsername", "myDomainname", "myPassword" ) )
    ///		{
    ///			...
    ///			[code that executes under the new context]
    ///			...
    ///		}
    ///		...
    /// 
    /// Please contact the author Uwe Keim (mailto:uwe.keim@zeta-software.de)
    /// for questions regarding this class.
    /// </remarks>
    public class Impersonator :
        IDisposable
    {
        #region Public methods.
        // ------------------------------------------------------------------

        /// <summary>
        /// Constructor. Starts the impersonation with the given credentials.
        /// Please note that the account that instantiates the Impersonator class
        /// needs to have the 'Act as part of operating system' privilege set.
        /// </summary>
        /// <param name="userName">The name of the user to act as.</param>
        /// <param name="domainName">The domain name of the user to act as.</param>
        /// <param name="password">The password of the user to act as.</param>
        public Impersonator(
            string userName,
            string domainName,
            string password)
        {
            ImpersonateValidUser(userName, domainName, password);
        }

        // ------------------------------------------------------------------
        #endregion

        #region IDisposable member.
        // ------------------------------------------------------------------

        public void Dispose()
        {
            UndoImpersonation();
        }

        // ------------------------------------------------------------------
        #endregion

        #region P/Invoke.
        // ------------------------------------------------------------------

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int LogonUser(
            string lpszUserName,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(
            IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(
            IntPtr handle);

        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        // ------------------------------------------------------------------
        #endregion

        #region Private member.
        // ------------------------------------------------------------------

        /// <summary>
        /// Does the actual impersonation.
        /// </summary>
        /// <param name="userName">The name of the user to act as.</param>
        /// <param name="domainName">The domain name of the user to act as.</param>
        /// <param name="password">The password of the user to act as.</param>
        private void ImpersonateValidUser(
            string userName,
            string domain,
            string password)
        {
            WindowsIdentity tempWindowsIdentity = null;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            try
            {
                if (RevertToSelf())
                {
                    if (LogonUser(
                        userName,
                        domain,
                        password,
                        LOGON32_LOGON_INTERACTIVE,
                        LOGON32_PROVIDER_DEFAULT,
                        ref token) != 0)
                    {
                        if (DuplicateToken(token, 3, ref tokenDuplicate) != 0)
                        {
                            tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                            impersonationContext = tempWindowsIdentity.Impersonate();
                        }
                        else
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                    else
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                if (token != IntPtr.Zero)
                {
                    CloseHandle(token);
                }
                if (tokenDuplicate != IntPtr.Zero)
                {
                    CloseHandle(tokenDuplicate);
                }
            }
        }

        /// <summary>
        /// Reverts the impersonation.
        /// </summary>
        private void UndoImpersonation()
        {
            if (impersonationContext != null)
            {
                impersonationContext.Undo();
            }
        }

        private WindowsImpersonationContext impersonationContext = null;

        // ------------------------------------------------------------------
        #endregion
    }

    /////////////////////////////////////////////////////////////////////////

}
