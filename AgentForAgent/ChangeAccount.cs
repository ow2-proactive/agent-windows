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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using ProActiveAgent;

namespace AgentForAgent
{
    public partial class ChangeAccount : Form
    {
        public static void createChangeAccountAndShow()
        {
            string domain, username;

            // First try to load the values from the registry            
            RegistryKey confKey = null;
            try
            {
                confKey = Registry.LocalMachine.OpenSubKey(Constants.REG_CREDS_SUBKEY);

                if (confKey == null)
                {
                    MessageBox.Show("Unable to read credentials from registry, maybe you should reinstall this software.", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                domain = (string)confKey.GetValue("domain");
                username = (string)confKey.GetValue("username");
            }
            finally
            {
                if (confKey != null)
                    confKey.Close();
            }

            ChangeAccount acc = new ChangeAccount();
            acc.domainTextBox.Text = domain;
            acc.usernameTextBox.Text = username;

            acc.ShowDialog();
        }

        private ChangeAccount()
        {
            InitializeComponent();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            string domain = this.domainTextBox.Text;
            string username = this.usernameTextBox.Text;
            string pass = this.passwordTextBox.Text;
            string confirmPass = this.confirmTextBox.Text;               

            if ("".Equals(domain))
            {
                MessageBox.Show("Invalid domain", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if ("".Equals(username))
            {
                MessageBox.Show("Invalid username", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            bool emptyPass = "".Equals(pass) || "".Equals(confirmPass);
            bool samePass = !emptyPass && pass.Equals(confirmPass);
            if (!samePass)
            {
                MessageBox.Show("Invalid password", "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                Utils.storeRuntimeAccount(domain, username, pass);
            } catch (Exception ex) 
            {
                MessageBox.Show("Unable to store the Runtime Account: " + ex.ToString(), "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            base.Close();            
        }
    }

    /**
     * 
     * // Impersonate the forker (get its access rights) in his context UNC paths are accepted
            using (new Impersonator(username, domain, password))
            {
                // First check if the directory exists
                if (!Directory.Exists(config.javaHome))
                {
                    throw new ApplicationException("Unable to read the classpath, the Java Home directory " + config.javaHome + " does not exist");
                }

                // Get the initScript 
                initScript = findInitScriptInternal(config.proactiveHome);
            }

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal up = UserPrincipal.FindByIdentity(
                    pc,
                    IdentityType.SamAccountName,
                    username);

                bool UserExists = (up != null);
            }
     * */
}
