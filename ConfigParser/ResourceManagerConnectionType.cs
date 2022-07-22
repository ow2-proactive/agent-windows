using System.Collections.Generic;
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

namespace ConfigParser
{
    /// <summary>    
    /// This class represents as set of information required by a ProActive runtime 
    /// in order to connect to the Resource Manager.
    /// </summary>    
    public sealed class ResoureManagerConnectionType: ConnectionType
    {
        /// <summary>
        /// The java class that corresponds to this action.</summary>
        public const string DEFAULT_JAVA_STARTER_CLASS = "org.ow2.proactive.resourcemanager.utils.RMNodeStarter";

        /// <summary>
        /// The URL of the ProActive Resource Manager.</summary>
        [XmlElement("url")]
        public string url;

        /// <summary>
        /// The name of the ProActive Resource Manager node source.</summary>
        [XmlElement("nodeSourceName")]
        public string nodeSourceName;

        /// <summary>
        /// The path of the file containing the credentials to use to connect to the ProActive Ressource Manager.</summary>
        [XmlElement("credential")]
        public string credential;

        public ResoureManagerConnectionType() {}

        public override string[] getArgs()
        {
            string urlOpt = "";
            if (this.url != null && !this.url.Equals(""))
            {
               urlOpt = "-r " + this.url;
            }
            string nodeNameOpt = "";
            if (base.nodename != null && !base.nodename.Equals(""))
            {
                nodeNameOpt = "-n " + base.nodename;
            }

            string nodeSourceNameOpt = "";
            if (this.nodeSourceName != null && !this.nodeSourceName.Equals(""))
            {
                nodeSourceNameOpt = "-s " + this.nodeSourceName;
            }

            if (this.credential == null && !"".Equals(this.credential))
            {
                return new string[] { urlOpt, nodeNameOpt, nodeSourceNameOpt };
            }
            else
            {
                string credentialLocationOpt = "-f \"" + this.credential + "\"";             
                return new string[] { urlOpt, nodeNameOpt, nodeSourceNameOpt, credentialLocationOpt };
            }
        }

        // Default jvm options needed for this type of connection
        public override void fillDefaultJvmOptions(List<string> jvmOptions, string proactiveLocation)
        {
            base.fillDefaultJvmOptions(jvmOptions, proactiveLocation);
            jvmOptions.Add("-Djava.security.policy=\"" + proactiveLocation + "\\config\\security.java.policy-client\"");
            jvmOptions.Add("-Dproactive.configuration=\"file:" + proactiveLocation + "\\config\\network\\node.ini\"");
            jvmOptions.Add("-Dpa.scheduler.home=\"" + proactiveLocation + "\"");
            jvmOptions.Add("-Dpa.rm.home=\"" + proactiveLocation + "\"");
        }
    }
}