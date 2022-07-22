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
using System.Collections.Generic;

namespace ConfigParser
{
    public abstract class ConnectionType
    {
        /// <summary>
        /// Each time a runtime dies, it is automatically restarted by the agent.
        /// To avoid system overloading, the agent waits for a given amount of time before restarting the runtime.
        /// This delay depends of the number of restart and the respawnincrement.</summary>
        [XmlElement("respawnIncrement")]
        public uint respawnIncrement;

        /// <summary>
        /// The java class name to be started by the agent.
        /// The element is optional since the agent will automatically starts the class
        /// corresponding to the Connection if not specified. This element can be used to 
        /// override the defaut class if needed.</summary>
        [XmlElement("javaStarterClass")]
        public string javaStarterClass;

        /// <summary>
        /// The ProActive node name to be started.</summary>
        [XmlElement("nodename")]
        public string nodename;

        /// <summary>
        /// True if this action is enabled. False otherwise one and only one action can be enabled.</summary>
        [XmlAttribute("enabled")]
        public bool enabled;

        public ConnectionType() {
            this.respawnIncrement = 10;
        }

        // Sub classes must override this method
        public virtual string[] getArgs() { return new string[0]; }

        // Subclasses may override this method for default jvm options needed for this type of connection
        public virtual void fillDefaultJvmOptions(List<string> jvmOptions, string proactiveLocation)
        {
            jvmOptions.Add("-Dproactive.home=\"" + proactiveLocation + "\"");
            jvmOptions.Add("-Djava.security.manager");
        }
    }
}