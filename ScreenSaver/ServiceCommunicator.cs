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
using System.Text;
using System.ServiceProcess;
using System.Windows.Forms;
using ProActiveAgent;

/**
 * This class is used to communicate with ProActive Agent system service
 * It is using ServiceController class to achieve this goal
 */

namespace ScreenSaver
{
    public class ServiceCommunicator
    {
        private readonly ServiceController sc = new ServiceController(Constants.SERVICE_NAME);

        public void sendStartAction()
        {
            try
            {
                sc.ExecuteCommand((int)PAACommands.ScreenSaverStart);
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void sendStopAction()
        {
            try
            {
                sc.ExecuteCommand((int)PAACommands.ScreenSaverStop);
            }
            catch (InvalidOperationException)
            {
            }
        }

    }
}