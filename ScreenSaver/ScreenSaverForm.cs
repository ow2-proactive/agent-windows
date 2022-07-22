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
using System.Drawing;
using System.Windows.Forms;

/** Implementation of screen saver
 * We will try to contact ProActive Agent System Service
 * to start action
 * 
 * When screensaver is off we will try to stop action
 */

namespace ScreenSaver
{
    public class ScreenSaverForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;
        private Point MouseXY;
        private System.Windows.Forms.Timer timer;           // timer for moving picture
        private Rectangle VisibleRect;
        private System.Windows.Forms.PictureBox pictureBox;
        private Random rand;

        private readonly ScreenSaver.ServiceCommunicator exec = new ScreenSaver.ServiceCommunicator();

        public ScreenSaverForm()
        {
            InitializeComponent();
            exec.sendStartAction();
        }

        protected override void Dispose(bool disposing)
        {
            exec.sendStopAction(); // we contact system service

            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        // screen saver GUI initialization

        private void ScreenSaverForm_Load(object sender, System.EventArgs e)
        {                        
            this.Bounds = new Rectangle(
                0,
                0,
                SystemInformation.VirtualScreen.Width,
                SystemInformation.VirtualScreen.Height);            
            VisibleRect = new Rectangle(0, 0, Bounds.Width - pictureBox.Width, Bounds.Height - pictureBox.Height);
            Cursor.Hide();
            TopMost = true;
        }

        // timer event handler --- we are moving the picture

        private void timer_Tick(object sender, System.EventArgs e)
        {
            rand = new Random();
            pictureBox.Location = new Point(rand.Next(VisibleRect.Width), rand.Next(VisibleRect.Height));
        }

        // the screensaver closes on mouse movement or click

        private void OnMouseEvent(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!MouseXY.IsEmpty)
            {
                if (MouseXY != new Point(e.X, e.Y))
                    Close();
                if (e.Clicks > 0)
                    Close();
            }
            MouseXY = new Point(e.X, e.Y);
        }

        // and on key pressed

        private void ScreenSaverForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            Close();
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenSaverForm));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pictureBox
            // 
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(27, 95);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(479, 119);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // ScreenSaverForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(532, 309);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ScreenSaverForm";
            this.Text = "ScreenSaver";
            this.Load += new System.EventHandler(this.ScreenSaverForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenSaverForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }
}
