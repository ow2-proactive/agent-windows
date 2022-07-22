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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ConfigParser;

namespace AgentForAgent
{
    public partial class Chart : Form
    {
        private Pen pen;
        private Brush brush;
        private Graphics G;
        private ArrayList rects;

        private static int WITH_BAR = 15;

        public Chart()
        {
            InitializeComponent();

            pen = new Pen(Color.BlueViolet);
            brush = new SolidBrush(Color.BlueViolet);
            rects = new ArrayList();

            //--
            ArrayList list = new ArrayList();
            Label labelHour1;
            Font font;
            font = new Font(FontFamily.GenericSansSerif, 7f);
            for (int i = 0; i <= 24; i++)
            {
                //--Hour
                labelHour1 = new Label();
                labelHour1.Font = font;
                labelHour1.AutoSize = true;
                labelHour1.Location = new System.Drawing.Point(15, ((10 * i) + 25));
                labelHour1.Name = i.ToString();
                labelHour1.Size = new System.Drawing.Size(28, 13);
                labelHour1.TabIndex = 0;
                labelHour1.Text = i.ToString();
                this.Controls.Add(labelHour1);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            G = e.Graphics;
            for (int i = 0; i <= 24; i++)
            {
                //--Line
                G.DrawLine(pen, 40, ((10 * i) + 30), 270, ((10 * i) + 30));
            }

            //--Draw timeslots
            foreach (Rectangle rec in rects)
            {
                G.DrawRectangle(pen, rec);
                G.FillRectangle(brush, rec);
            }
        }

        public void loadEvents(List<CalendarEventType> eventsList)
        {
            rects.Clear();
            foreach (CalendarEventType ev in eventsList)
            {
                CalendarEventType cEv = (CalendarEventType)ev;

                //--Start
                int dayTimeslot = 43 + cEv.resolveDay() * 35;
                int startTimeslot = (30 + (10 * cEv.start.hour)) + ((cEv.start.minute * 10) / 60);


                //--Duration
                int timeRemain = 1440 - (cEv.start.hour * 60) - cEv.start.minute;
                //--On compare le nb de min qu'il reste avec le nb de minute d'une journée
                int duration = (cEv.duration.days * 1440) + (cEv.duration.hours * 60) + cEv.duration.minutes;
                int heightBar = 0;

                if (duration <= timeRemain)
                {
                    //--No overruning
                    heightBar = duration * 10 / 60;
                }
                else
                {
                    heightBar = timeRemain * 10 / 60;
                    //--Overflow
                    duration -= timeRemain;
                    int startDay = cEv.resolveDay();
                    int heightBarNext = 0;
                    Rectangle rectNext;

                    while (duration > 0)
                    {
                        //--Day column selection
                        if (startDay > 5)
                            startDay = 0;
                        else
                            startDay++;
                        if (duration <= 1440)
                            heightBarNext = duration * 10 / 60;
                        else
                            heightBarNext = 1440 * 10 / 60;

                        rectNext = new Rectangle(new Point(43 + startDay * 35, 30), new Size(WITH_BAR, heightBarNext));
                        rects.Add(rectNext);
                        duration -= 1440;
                    }
                }
                Rectangle rect = new Rectangle(new Point(dayTimeslot, startTimeslot), new Size(WITH_BAR, heightBar));
                rects.Add(rect);
            }
        }

        private void Chart_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Chart_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
