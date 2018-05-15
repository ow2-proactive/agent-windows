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
    [XmlRoot("agent", Namespace = "urn:proactive:agent:1.1:windows")]
    public sealed class AgentType
    {
        /// <summary>
        /// The global configuration of the ProActive Agent. This configuration can be overriden by each event.</summary>
        [XmlElement("config", Type = typeof(AgentConfigType))]
        public AgentConfigType config;

        /// <summary>
        /// The events triggered by the agent will use the selected connection and run a runtime.</summary>
        [XmlArray("events")]
        [XmlArrayItem("event", typeof(CalendarEventType))]
        public CalendarEventType[] events;

        /// <summary>
        /// The 3 available connections, only a single one can be used.</summary>
        [XmlArray("connections")]
        [XmlArrayItem("localBind", Type = typeof(LocalBindConnectionType)),
        XmlArrayItem("rmConnection", Type = typeof(ResoureManagerConnectionType)),
        XmlArrayItem("customConnection", Type = typeof(CustomConnectionType))]
        public ConnectionType[] connections;

        /// <summary>
        /// The location where the agent is installed. This attribute is not serialized !</summary>
        [XmlIgnore]
        public string agentInstallLocation;

        public AgentType() { }

        public AgentType(AgentConfigType config, CalendarEventType[] events, ConnectionType[] connections)
        {
            this.config = config;
            this.events = events;
            this.connections = connections;
        }

        public ConnectionType getEnabledConnection()
        {
            // Find enabled action, ONLY ONE ACTION CAN BE ENABLED            
            foreach (ConnectionType con in this.connections)
            {
                if (con.enabled)
                {
                    return con;
                }
            }
            // If no action was selected ...
            return null;
        }

        public bool isAlwaysAvailable()
        {
            return this.events.Length == 0 || (this.events.Length == 1 && this.events[0].isAlwaysAvailable());
        }
    }
}
