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
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ConfigParser
{
    /// <summary>
    /// Class which objects are responsible
    /// for transformation from XML configuration file
    /// into internal object representation of configuration state.
    /// </summary>
    public sealed class ConfigurationParser
    {
        /// <summary>
        /// The namespace of config files.</summary>
        public const string CONFIG_NAMESPACE = "urn:proactive:agent:1.1:windows";

        private static string reason;

        public static void saveXml(String fileName, AgentType configuration)
        {            
            XmlSerializer serializer = new XmlSerializer(typeof(AgentType));
            TextWriter tw = new StreamWriter(fileName);
            serializer.Serialize(tw, configuration);
            tw.Close();
        }

        /// <summary>
        /// Throws Application exception if the file is invalid.
        /// </summary>
        /// <param name="filePath">The path to the configuration file</param>
        /// <param name="agentHome">The home dir of the agent</param>
        public static void validateXMLFile(string filePath, string agentHome)
        {
            // First try to validate against current version            
            try
            {                
                XmlSchemaSet schemaSet = new XmlSchemaSet();
                schemaSet.Add(CONFIG_NAMESPACE, agentHome + "\\xml\\agent-windows.xsd");
                internalValidate(filePath, agentHome, schemaSet);
            }
            catch (Exception e1)
            {               
               throw new ApplicationException("Invalid configuration file. Against agent-windows.xsd: " + e1.Message, e1);               
            }
        }
        
        /// <summary>
        /// Tries to parse and deserialize as AgentType
        /// </summary>
        /// <param name="agentConfigLocation">The path to the configuration file</param>        
        /// <returns></returns>
        public static AgentType parseXml(String agentConfigLocation)
        {
            TextReader tr1 = new StreamReader(agentConfigLocation);
            try
            {
                // Try to deserialize 
                XmlSerializer serializer = new XmlSerializer(typeof(AgentType));
                return (AgentType)serializer.Deserialize(tr1);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                tr1.Close();
            }
        }

        private static void internalValidate(string filePath, string agentHome, XmlSchemaSet schemaSet)
        {

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schemaSet;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationError);

            XmlReader textReader = XmlReader.Create(filePath, settings);
            try
            {
                while (textReader.Read()) ;
            }
            catch (XmlException e)
            {
                throw new ApplicationException("Could not read the " + filePath + " config file ", e);
            }
            finally
            {
                textReader.Close();
            }
            try
            {
                // If something happened throw an exception
                if (reason != null)
                {
                    throw new ApplicationException("Unable to validate " + filePath + " is invalid: " + reason);
                }
            }
            finally
            {
                reason = null;
            }
        }

        private static void ValidationError(object sender, ValidationEventArgs arguments)
        {
            reason = arguments.Message;
        }       
    }
}
