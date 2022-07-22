namespace AgentForAgent
{
    partial class AgentWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentWindow));
            this.configurationFileLocationLabel = new System.Windows.Forms.Label();
            this.configFileLocationTextBox = new System.Windows.Forms.TextBox();
            this.browseConfigFileLocation = new System.Windows.Forms.Button();
            this.configurationGroupBox = new System.Windows.Forms.GroupBox();
            this.changeAccountButton = new System.Windows.Forms.Button();
            this.guiEditButton = new System.Windows.Forms.Button();
            this.textEditConfig = new System.Windows.Forms.Button();
            this.infoGroupBox = new System.Windows.Forms.GroupBox();
            this.spawnedRuntimesValue = new System.Windows.Forms.Label();
            this.agentStatusValue = new System.Windows.Forms.Label();
            this.spawnedRuntimesLabel = new System.Windows.Forms.Label();
            this.agentStatusLabel = new System.Windows.Forms.Label();
            this.stopService = new System.Windows.Forms.Button();
            this.startService = new System.Windows.Forms.Button();
            this.proActiveLogoPictureBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.browseConfig = new System.Windows.Forms.OpenFileDialog();
            this.agentStatusNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAdministrationPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proActiveLinkLabel = new System.Windows.Forms.LinkLabel();
            this.controlsGroupBox = new System.Windows.Forms.GroupBox();
            this.logsGroupBox = new System.Windows.Forms.GroupBox();
            this.viewLogsWithNotepadButton = new System.Windows.Forms.Button();
            this.viewLogsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.viewLogsWithExplorerButton = new System.Windows.Forms.Button();
            this.activeeonLinkLabel = new System.Windows.Forms.LinkLabel();
            this.docActiveeonLinkLabel = new System.Windows.Forms.LinkLabel();
            this.configurationGroupBox.SuspendLayout();
            this.infoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.proActiveLogoPictureBox)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.controlsGroupBox.SuspendLayout();
            this.logsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // configurationFileLocationLabel
            // 
            this.configurationFileLocationLabel.AutoSize = true;
            this.configurationFileLocationLabel.Location = new System.Drawing.Point(6, 21);
            this.configurationFileLocationLabel.Name = "configurationFileLocationLabel";
            this.configurationFileLocationLabel.Size = new System.Drawing.Size(51, 13);
            this.configurationFileLocationLabel.TabIndex = 0;
            this.configurationFileLocationLabel.Text = "Location:";
            // 
            // configFileLocationTextBox
            // 
            this.configFileLocationTextBox.Enabled = false;
            this.configFileLocationTextBox.Location = new System.Drawing.Point(63, 18);
            this.configFileLocationTextBox.Name = "configFileLocationTextBox";
            this.configFileLocationTextBox.ReadOnly = true;
            this.configFileLocationTextBox.Size = new System.Drawing.Size(427, 20);
            this.configFileLocationTextBox.TabIndex = 1;
            this.configFileLocationTextBox.TextChanged += new System.EventHandler(this.configFileLocationTextBox_TextChanged);
            // 
            // browseConfigFileLocation
            // 
            this.browseConfigFileLocation.Location = new System.Drawing.Point(496, 16);
            this.browseConfigFileLocation.Name = "browseConfigFileLocation";
            this.browseConfigFileLocation.Size = new System.Drawing.Size(75, 23);
            this.browseConfigFileLocation.TabIndex = 2;
            this.browseConfigFileLocation.Text = "Browse";
            this.browseConfigFileLocation.UseVisualStyleBackColor = true;
            this.browseConfigFileLocation.Click += new System.EventHandler(this.browse_Click);
            // 
            // configurationGroupBox
            // 
            this.configurationGroupBox.Controls.Add(this.changeAccountButton);
            this.configurationGroupBox.Controls.Add(this.guiEditButton);
            this.configurationGroupBox.Controls.Add(this.configFileLocationTextBox);
            this.configurationGroupBox.Controls.Add(this.textEditConfig);
            this.configurationGroupBox.Controls.Add(this.browseConfigFileLocation);
            this.configurationGroupBox.Controls.Add(this.configurationFileLocationLabel);
            this.configurationGroupBox.Location = new System.Drawing.Point(9, 117);
            this.configurationGroupBox.Name = "configurationGroupBox";
            this.configurationGroupBox.Size = new System.Drawing.Size(577, 81);
            this.configurationGroupBox.TabIndex = 3;
            this.configurationGroupBox.TabStop = false;
            this.configurationGroupBox.Text = "Configuration";
            // 
            // changeAccountButton
            // 
            this.changeAccountButton.Location = new System.Drawing.Point(9, 48);
            this.changeAccountButton.Name = "changeAccountButton";
            this.changeAccountButton.Size = new System.Drawing.Size(99, 23);
            this.changeAccountButton.TabIndex = 4;
            this.changeAccountButton.Text = "Change Account";
            this.changeAccountButton.UseVisualStyleBackColor = true;
            this.changeAccountButton.Click += new System.EventHandler(this.changeAccountButton_Click);
            // 
            // guiEditButton
            // 
            this.guiEditButton.Location = new System.Drawing.Point(415, 48);
            this.guiEditButton.Name = "guiEditButton";
            this.guiEditButton.Size = new System.Drawing.Size(75, 23);
            this.guiEditButton.TabIndex = 3;
            this.guiEditButton.Text = "GUI Edit";
            this.guiEditButton.UseVisualStyleBackColor = true;
            this.guiEditButton.Click += new System.EventHandler(this.guiEditButton_Click);
            // 
            // textEditConfig
            // 
            this.textEditConfig.Location = new System.Drawing.Point(496, 48);
            this.textEditConfig.Name = "textEditConfig";
            this.textEditConfig.Size = new System.Drawing.Size(75, 23);
            this.textEditConfig.TabIndex = 2;
            this.textEditConfig.Text = "Text Edit";
            this.textEditConfig.UseVisualStyleBackColor = true;
            this.textEditConfig.Click += new System.EventHandler(this.editConfig_Click);
            // 
            // infoGroupBox
            // 
            this.infoGroupBox.Controls.Add(this.spawnedRuntimesValue);
            this.infoGroupBox.Controls.Add(this.agentStatusValue);
            this.infoGroupBox.Controls.Add(this.spawnedRuntimesLabel);
            this.infoGroupBox.Controls.Add(this.agentStatusLabel);
            this.infoGroupBox.Location = new System.Drawing.Point(9, 204);
            this.infoGroupBox.Name = "infoGroupBox";
            this.infoGroupBox.Size = new System.Drawing.Size(189, 81);
            this.infoGroupBox.TabIndex = 3;
            this.infoGroupBox.TabStop = false;
            this.infoGroupBox.Text = "Info";
            // 
            // spawnedRuntimesValue
            // 
            this.spawnedRuntimesValue.AutoSize = true;
            this.spawnedRuntimesValue.Location = new System.Drawing.Point(114, 53);
            this.spawnedRuntimesValue.Name = "spawnedRuntimesValue";
            this.spawnedRuntimesValue.Size = new System.Drawing.Size(13, 13);
            this.spawnedRuntimesValue.TabIndex = 3;
            this.spawnedRuntimesValue.Text = "0";
            // 
            // agentStatusValue
            // 
            this.agentStatusValue.AutoSize = true;
            this.agentStatusValue.Location = new System.Drawing.Point(114, 24);
            this.agentStatusValue.Name = "agentStatusValue";
            this.agentStatusValue.Size = new System.Drawing.Size(53, 13);
            this.agentStatusValue.TabIndex = 2;
            this.agentStatusValue.Text = "Unknown";
            // 
            // spawnedRuntimesLabel
            // 
            this.spawnedRuntimesLabel.AutoSize = true;
            this.spawnedRuntimesLabel.Location = new System.Drawing.Point(6, 53);
            this.spawnedRuntimesLabel.Name = "spawnedRuntimesLabel";
            this.spawnedRuntimesLabel.Size = new System.Drawing.Size(102, 13);
            this.spawnedRuntimesLabel.TabIndex = 1;
            this.spawnedRuntimesLabel.Text = "Spawned Runtimes:";
            // 
            // agentStatusLabel
            // 
            this.agentStatusLabel.AutoSize = true;
            this.agentStatusLabel.Location = new System.Drawing.Point(68, 24);
            this.agentStatusLabel.Name = "agentStatusLabel";
            this.agentStatusLabel.Size = new System.Drawing.Size(40, 13);
            this.agentStatusLabel.TabIndex = 0;
            this.agentStatusLabel.Text = "Status:";
            // 
            // stopService
            // 
            this.stopService.Location = new System.Drawing.Point(110, 48);
            this.stopService.Name = "stopService";
            this.stopService.Size = new System.Drawing.Size(75, 23);
            this.stopService.TabIndex = 2;
            this.stopService.Text = "Stop";
            this.stopService.UseVisualStyleBackColor = true;
            this.stopService.Click += new System.EventHandler(this.stopService_Click);
            // 
            // startService
            // 
            this.startService.Location = new System.Drawing.Point(110, 19);
            this.startService.Name = "startService";
            this.startService.Size = new System.Drawing.Size(75, 23);
            this.startService.TabIndex = 2;
            this.startService.Text = "Start";
            this.startService.UseVisualStyleBackColor = true;
            this.startService.Click += new System.EventHandler(this.startService_Click);
            // 
            // proActiveLogoPictureBox
            // 
            this.proActiveLogoPictureBox.BackColor = System.Drawing.Color.White;
            this.proActiveLogoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("proActiveLogoPictureBox.Image")));
            this.proActiveLogoPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("proActiveLogoPictureBox.InitialImage")));
            this.proActiveLogoPictureBox.Location = new System.Drawing.Point(9, 9);
            this.proActiveLogoPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.proActiveLogoPictureBox.Name = "proActiveLogoPictureBox";
            this.proActiveLogoPictureBox.Size = new System.Drawing.Size(438, 105);
            this.proActiveLogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.proActiveLogoPictureBox.TabIndex = 4;
            this.proActiveLogoPictureBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(472, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ProActive Agent";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(450, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "(C) 2015 INRIA/ActiveEon";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 800;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // browseConfig
            // 
            this.browseConfig.FileName = "openFileDialog1";
            this.browseConfig.Title = "Choose configuration file";
            // 
            // agentStatusNotifyIcon
            // 
            this.agentStatusNotifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.agentStatusNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("agentStatusNotifyIcon.Icon")));
            this.agentStatusNotifyIcon.Text = "ProActiveAgent";
            this.agentStatusNotifyIcon.Visible = true;
            this.agentStatusNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServiceToolStripMenuItem,
            this.stopServiceToolStripMenuItem,
            this.closeAdministrationPanelToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(112, 70);
            // 
            // startServiceToolStripMenuItem
            // 
            this.startServiceToolStripMenuItem.Name = "startServiceToolStripMenuItem";
            this.startServiceToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.startServiceToolStripMenuItem.Text = "Start Service";
            this.startServiceToolStripMenuItem.Click += new System.EventHandler(this.startService_Click);
            // 
            // stopServiceToolStripMenuItem
            // 
            this.stopServiceToolStripMenuItem.Name = "stopServiceToolStripMenuItem";
            this.stopServiceToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.stopServiceToolStripMenuItem.Text = "Stop Service";
            this.stopServiceToolStripMenuItem.Click += new System.EventHandler(this.stopService_Click);
            // 
            // closeAdministrationPanelToolStripMenuItem
            // 
            this.closeAdministrationPanelToolStripMenuItem.Name = "closeAdministrationPanelToolStripMenuItem";
            this.closeAdministrationPanelToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.closeAdministrationPanelToolStripMenuItem.Text = "Close";
            this.closeAdministrationPanelToolStripMenuItem.Click += new System.EventHandler(this.closeAdministrationPanelToolStripMenuItem_Click_1);
            // 
            // proActiveLinkLabel
            // 
            this.proActiveLinkLabel.AutoSize = true;
            this.proActiveLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.proActiveLinkLabel.DisabledLinkColor = System.Drawing.Color.White;
            this.proActiveLinkLabel.Location = new System.Drawing.Point(473, 66);
            this.proActiveLinkLabel.Name = "proActiveLinkLabel";
            this.proActiveLinkLabel.Size = new System.Drawing.Size(113, 13);
            this.proActiveLinkLabel.TabIndex = 9;
            this.proActiveLinkLabel.TabStop = true;
            this.proActiveLinkLabel.Text = "http://proactive.inria.fr";
            this.proActiveLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.proactiveLinkLabel_LinkClicked);
            // 
            // controlsGroupBox
            // 
            this.controlsGroupBox.Controls.Add(this.startService);
            this.controlsGroupBox.Controls.Add(this.stopService);
            this.controlsGroupBox.Location = new System.Drawing.Point(395, 204);
            this.controlsGroupBox.Name = "controlsGroupBox";
            this.controlsGroupBox.Size = new System.Drawing.Size(191, 81);
            this.controlsGroupBox.TabIndex = 10;
            this.controlsGroupBox.TabStop = false;
            this.controlsGroupBox.Text = "Controls";
            // 
            // logsGroupBox
            // 
            this.logsGroupBox.Controls.Add(this.viewLogsWithNotepadButton);
            this.logsGroupBox.Controls.Add(this.viewLogsLinkLabel);
            this.logsGroupBox.Controls.Add(this.viewLogsWithExplorerButton);
            this.logsGroupBox.Location = new System.Drawing.Point(204, 204);
            this.logsGroupBox.Name = "logsGroupBox";
            this.logsGroupBox.Size = new System.Drawing.Size(185, 81);
            this.logsGroupBox.TabIndex = 11;
            this.logsGroupBox.TabStop = false;
            this.logsGroupBox.Text = "Logs";
            // 
            // viewLogsWithNotepadButton
            // 
            this.viewLogsWithNotepadButton.Location = new System.Drawing.Point(83, 19);
            this.viewLogsWithNotepadButton.Name = "viewLogsWithNotepadButton";
            this.viewLogsWithNotepadButton.Size = new System.Drawing.Size(78, 23);
            this.viewLogsWithNotepadButton.TabIndex = 6;
            this.viewLogsWithNotepadButton.Text = "with Notepad";
            this.viewLogsWithNotepadButton.UseVisualStyleBackColor = true;
            this.viewLogsWithNotepadButton.Click += new System.EventHandler(this.withNotepadButton_Click);
            // 
            // viewLogsLinkLabel
            // 
            this.viewLogsLinkLabel.AutoSize = true;
            this.viewLogsLinkLabel.Location = new System.Drawing.Point(21, 39);
            this.viewLogsLinkLabel.Name = "viewLogsLinkLabel";
            this.viewLogsLinkLabel.Size = new System.Drawing.Size(56, 13);
            this.viewLogsLinkLabel.TabIndex = 0;
            this.viewLogsLinkLabel.TabStop = true;
            this.viewLogsLinkLabel.Text = "View Logs";
            this.viewLogsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.viewLogsLinkLabel_LinkClicked);
            // 
            // viewLogsWithExplorerButton
            // 
            this.viewLogsWithExplorerButton.Location = new System.Drawing.Point(83, 48);
            this.viewLogsWithExplorerButton.Name = "viewLogsWithExplorerButton";
            this.viewLogsWithExplorerButton.Size = new System.Drawing.Size(78, 23);
            this.viewLogsWithExplorerButton.TabIndex = 5;
            this.viewLogsWithExplorerButton.Text = "with Explorer";
            this.viewLogsWithExplorerButton.UseVisualStyleBackColor = true;
            this.viewLogsWithExplorerButton.Click += new System.EventHandler(this.withIExplorerButton_Click);
            // 
            // activeeonLinkLabel
            // 
            this.activeeonLinkLabel.AutoSize = true;
            this.activeeonLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.activeeonLinkLabel.DisabledLinkColor = System.Drawing.Color.White;
            this.activeeonLinkLabel.Location = new System.Drawing.Point(451, 79);
            this.activeeonLinkLabel.Name = "activeeonLinkLabel";
            this.activeeonLinkLabel.Size = new System.Drawing.Size(135, 13);
            this.activeeonLinkLabel.TabIndex = 13;
            this.activeeonLinkLabel.TabStop = true;
            this.activeeonLinkLabel.Text = "http://www.activeeon.com";
            this.activeeonLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.activeeonLinkLabel_LinkClicked);
            // 
            // docActiveeonLinkLabel
            // 
            this.docActiveeonLinkLabel.AutoSize = true;
            this.docActiveeonLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.docActiveeonLinkLabel.DisabledLinkColor = System.Drawing.Color.White;
            this.docActiveeonLinkLabel.Location = new System.Drawing.Point(457, 22);
            this.docActiveeonLinkLabel.Name = "docActiveeonLinkLabel";
            this.docActiveeonLinkLabel.Size = new System.Drawing.Size(129, 13);
            this.docActiveeonLinkLabel.TabIndex = 14;
            this.docActiveeonLinkLabel.TabStop = true;
            this.docActiveeonLinkLabel.Text = "http://doc.activeeon.com";
            this.docActiveeonLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.docActiveeonLinkLabel_LinkClicked);
            // 
            // AgentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 295);
            this.Controls.Add(this.docActiveeonLinkLabel);
            this.Controls.Add(this.activeeonLinkLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.controlsGroupBox);
            this.Controls.Add(this.logsGroupBox);
            this.Controls.Add(this.infoGroupBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.configurationGroupBox);
            this.Controls.Add(this.proActiveLinkLabel);
            this.Controls.Add(this.proActiveLogoPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AgentWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProActive Agent Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationDialog_FormClosing);
            this.Load += new System.EventHandler(this.AgentWindow_Load);
            this.Resize += new System.EventHandler(this.ConfigurationDialog_Resize);
            this.configurationGroupBox.ResumeLayout(false);
            this.configurationGroupBox.PerformLayout();
            this.infoGroupBox.ResumeLayout(false);
            this.infoGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.proActiveLogoPictureBox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.controlsGroupBox.ResumeLayout(false);
            this.logsGroupBox.ResumeLayout(false);
            this.logsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label configurationFileLocationLabel;
        private System.Windows.Forms.TextBox configFileLocationTextBox;
        private System.Windows.Forms.Button browseConfigFileLocation;
        private System.Windows.Forms.GroupBox configurationGroupBox;
        private System.Windows.Forms.Button textEditConfig;
        private System.Windows.Forms.GroupBox infoGroupBox;
        private System.Windows.Forms.Button stopService;
        private System.Windows.Forms.Button startService;
        private System.Windows.Forms.Label agentStatusLabel;
        private System.Windows.Forms.PictureBox proActiveLogoPictureBox;        
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;        
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.OpenFileDialog browseConfig;        
        private System.Windows.Forms.NotifyIcon agentStatusNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Button guiEditButton;
        private System.Windows.Forms.ToolStripMenuItem closeAdministrationPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopServiceToolStripMenuItem;
        private System.Windows.Forms.LinkLabel proActiveLinkLabel;
        private System.Windows.Forms.GroupBox controlsGroupBox;
        private System.Windows.Forms.GroupBox logsGroupBox;
        private System.Windows.Forms.Label spawnedRuntimesLabel;
        private System.Windows.Forms.Button viewLogsWithExplorerButton;
        private System.Windows.Forms.Label agentStatusValue;
        private System.Windows.Forms.Label spawnedRuntimesValue;
        private System.Windows.Forms.LinkLabel viewLogsLinkLabel;
        private System.Windows.Forms.Button viewLogsWithNotepadButton;
        private System.Windows.Forms.LinkLabel activeeonLinkLabel;
        private System.Windows.Forms.Button changeAccountButton;
        private System.Windows.Forms.LinkLabel docActiveeonLinkLabel;       
    }
}

