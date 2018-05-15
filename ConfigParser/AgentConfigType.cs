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
using System.Diagnostics;
using System.Xml.Serialization;

namespace ConfigParser
{
    public sealed class AgentConfigType
    {
        /// <summary>
        /// The path to the ProActive Programming or ProActive Scheduling installation directory.</summary>
        [XmlElement("proactiveHome")]
        public string proactiveHome;

        /// <summary>
        /// The path to the Java installation directory.</summary>
        [XmlElement("javaHome")]
        public string javaHome;

        /// <summary>
        /// The list of parameters to be passed to the JVM.</summary>
        [XmlArray("jvmParameters")]
        [XmlArrayItem("param")]
        public string[] jvmParameters;

        /// <summary>
        /// The list of parameters to be passed as argument to proactive agent.</summary>
        [XmlArray("additionalCmdArgs")]
        [XmlArrayItem("param")]
        public string[] additionalCmdArgs;

        /// <summary>
        /// The memory limitation in MBytes. 0 means no memory limit.</summary>
        [XmlElement("memoryLimit")] 
        public ushort memoryLimit;

        /// <summary>
        /// The number of ProActive runtimes to be spawned by the agent.</summary>
        [XmlElement("nbRuntimes")]
        public ushort nbRuntimes; // when parsing check for auto

        /// <summary>
        /// The number of workers per runtime.</summary>
        [XmlElement("nbWorkers")]
        public ushort nbWorkers;

        /// <summary>
        /// The communication protocol to be used by the started ProActive runtime.</summary>
        [XmlElement("protocol")]
        public string protocol;

        /// <summary>
        /// The port range.</summary>
        [XmlElement("portRange")]
        public PortRange portRange;

        /// <summary>
        /// The path to an executable to be started each time a forked ProActive runtime exits. It can be used to cleanup some ressources, or to gather some results.</summary>
        [XmlElement("onRuntimeExitScript")]
        public string onRuntimeExitScript;

        /// <summary>
        /// The process priority is applied to all spawned ProActive Runtime.</summary>
        [XmlElement("processPriority")]
        public ProcessPriorityClass processPriority;

        /// <summary>
        /// The maximum cpu usage.</summary>
        [XmlElement("maxCpuUsage")]
        public ushort maxCpuUsage;

        // Not serialized used only during runtime
        [XmlIgnore]
        public string classpath;

        public AgentConfigType()
        {
            this.portRange = new PortRange();
            this.processPriority = ProcessPriorityClass.Normal;
            this.maxCpuUsage = 100;
        }        
    }

    public sealed class PortRange
    {
        [XmlAttribute("first")]
        public ushort first;
        [XmlAttribute("last")]
        public ushort last;

        public PortRange() {}

        public PortRange(ushort first, ushort last) {
            this.first = first;
            this.last = last;
        }    
    }
}