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
    /// This sub-class of ConnectionType represents as set of information required for custom connection.</summary>
    public sealed class CustomConnectionType : ConnectionType
    {
        /// <summary>
        /// The java class that corresponds to this action.</summary>
        public const string DEFAULT_JAVA_STARTER_CLASS = "user.Starter";

        /// <summary>
        /// The arguments to be passed to the java class.</summary>
        [XmlArray("args")]
        [XmlArrayItem("arg")]
        public string[] args;

        public CustomConnectionType() {}

        public override string[] getArgs()
        {
            return this.args;
        }
    }
}
