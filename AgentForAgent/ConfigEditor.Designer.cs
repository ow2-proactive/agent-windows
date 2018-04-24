using System;
using System.Management;

namespace AgentForAgent
{
    public class RefreshingListBox : System.Windows.Forms.ListBox
    {
        public new void RefreshItem(int index)
        {
            base.RefreshItem(index);
        }
    }

    partial class ConfigurationEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationEditor));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.removeArgsParameterButton = new System.Windows.Forms.Button();
            this.addArgsParameterButton = new System.Windows.Forms.Button();
            this.argsOptionsListBox = new System.Windows.Forms.ListBox();
            this.jvmOptionsListBox = new System.Windows.Forms.ListBox();
            this.removeJvmParameterButton = new System.Windows.Forms.Button();
            this.addJvmParameterButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.jvmLocationButton = new System.Windows.Forms.Button();
            this.proactiveLocationButton = new System.Windows.Forms.Button();
            this.jvmDirectory = new System.Windows.Forms.TextBox();
            this.proactiveDirectory = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.runtimeIncomingProtocolGroupBox = new System.Windows.Forms.GroupBox();
            this.protocolComboBox = new System.Windows.Forms.ComboBox();
            this.protocolLabel = new System.Windows.Forms.Label();
            this.portInitialValue = new System.Windows.Forms.Label();
            this.portInitialValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.onRuntimeExitGroupBox = new System.Windows.Forms.GroupBox();
            this.scriptLocationButton = new System.Windows.Forms.Button();
            this.scriptLocationTextBox = new System.Windows.Forms.TextBox();
            this.multiRuntimeGroupBox = new System.Windows.Forms.GroupBox();
            this.nbWorkersLabel = new System.Windows.Forms.Label();
            this.nbWorkersNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.nbRuntimesLabel = new System.Windows.Forms.Label();
            this.availableCPUsValue = new System.Windows.Forms.Label();
            this.availableCPUsLabel = new System.Windows.Forms.Label();
            this.nbRuntimesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.networkInterfaceListGroupBox = new System.Windows.Forms.GroupBox();
            this.useNetworkInterfaceButton = new System.Windows.Forms.Button();
            this.networkInterfacesListBox = new System.Windows.Forms.ListBox();
            this.refreshNetworkInterfacesButton = new System.Windows.Forms.Button();
            this.memoryLimitGroupBox = new System.Windows.Forms.GroupBox();
            this.memoryLimitNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.memoryLimitLabel = new System.Windows.Forms.Label();
            this.availablePhysicalMemoryValue = new System.Windows.Forms.Label();
            this.availablePhysicalMemoryLabel = new System.Windows.Forms.Label();
            this.connectionTabPage = new System.Windows.Forms.TabPage();
            this.customRadioButton = new System.Windows.Forms.RadioButton();
            this.resourceManagerRegistrationRadioButton = new System.Windows.Forms.RadioButton();
            this.localRegistrationRadioButton = new System.Windows.Forms.RadioButton();
            this.connectionTypeTabControl = new System.Windows.Forms.TabControl();
            this.localRegistrationTabPage = new System.Windows.Forms.TabPage();
            this.rmiRegistrationAdditionalConfigurationGroupBox = new System.Windows.Forms.GroupBox();
            this.rmiRegistrationJavaActionClassTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.localRegistrationGroupBox = new System.Windows.Forms.GroupBox();
            this.localbindNodeNameLabel = new System.Windows.Forms.Label();
            this.localRegistrationNodeName = new System.Windows.Forms.TextBox();
            this.resourceManagerRegistrationTabPage = new System.Windows.Forms.TabPage();
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox = new System.Windows.Forms.GroupBox();
            this.resourceManagerRegistrationJavaActionClassTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.rmActionGroup = new System.Windows.Forms.GroupBox();
            this.nodeSourceNameLabel = new System.Windows.Forms.Label();
            this.nodeSourceNameTextBox = new System.Windows.Forms.TextBox();
            this.nodeNameTextBox = new System.Windows.Forms.TextBox();
            this.nodeNameLabel = new System.Windows.Forms.Label();
            this.rmUrl = new System.Windows.Forms.TextBox();
            this.resourceManagerUrlLabel = new System.Windows.Forms.Label();
            this.authenticationCredentialGroupBox = new System.Windows.Forms.GroupBox();
            this.credentialBrowseLocationButton = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.credentialLocationTextBox = new System.Windows.Forms.TextBox();
            this.customTabPage = new System.Windows.Forms.TabPage();
            this.customAdditionalConfigurationGroupBox = new System.Windows.Forms.GroupBox();
            this.customJavaActionClassTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.customActionGroup = new System.Windows.Forms.GroupBox();
            this.customSaveArgumentButton = new System.Windows.Forms.Button();
            this.customArgumentTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.customDeleteButton = new System.Windows.Forms.Button();
            this.customAddButton = new System.Windows.Forms.Button();
            this.customArgumentsListBox = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.processManagementGroupBox = new System.Windows.Forms.GroupBox();
            this.nbWorkersEventUpDown = new System.Windows.Forms.NumericUpDown();
            this.nbWorkersEventLabel = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.maxCpuUsageNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.maxCpuUsageLabel = new System.Windows.Forms.Label();
            this.processPriorityLabel = new System.Windows.Forms.Label();
            this.processPriorityComboBox = new System.Windows.Forms.ComboBox();
            this.alwaysAvailableCheckBox = new System.Windows.Forms.CheckBox();
            this.eventEditorGroup = new System.Windows.Forms.GroupBox();
            this.startTimeGroupBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.secondStart = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.weekdayStart = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.hourStart = new System.Windows.Forms.NumericUpDown();
            this.minuteStart = new System.Windows.Forms.NumericUpDown();
            this.durationGroupBox = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.secondsDuration = new System.Windows.Forms.NumericUpDown();
            this.dayDuration = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.minutesDuration = new System.Windows.Forms.NumericUpDown();
            this.hoursDuration = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.planningGroupBox = new System.Windows.Forms.GroupBox();
            this.showButton = new System.Windows.Forms.Button();
            this.createEventButton = new System.Windows.Forms.Button();
            this.deleteEventButton = new System.Windows.Forms.Button();
            this.saveConfig = new System.Windows.Forms.Button();
            this.closeConfig = new System.Windows.Forms.Button();
            this.proActiveLocationBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.jvmLocationBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.saveConfigAs = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.scriptLocationOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.credentialLocationOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.eventsList = new AgentForAgent.RefreshingListBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.runtimeIncomingProtocolGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portInitialValueNumericUpDown)).BeginInit();
            this.onRuntimeExitGroupBox.SuspendLayout();
            this.multiRuntimeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbWorkersNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbRuntimesNumericUpDown)).BeginInit();
            this.networkInterfaceListGroupBox.SuspendLayout();
            this.memoryLimitGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoryLimitNumericUpDown)).BeginInit();
            this.connectionTabPage.SuspendLayout();
            this.connectionTypeTabControl.SuspendLayout();
            this.localRegistrationTabPage.SuspendLayout();
            this.rmiRegistrationAdditionalConfigurationGroupBox.SuspendLayout();
            this.localRegistrationGroupBox.SuspendLayout();
            this.resourceManagerRegistrationTabPage.SuspendLayout();
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.SuspendLayout();
            this.rmActionGroup.SuspendLayout();
            this.authenticationCredentialGroupBox.SuspendLayout();
            this.customTabPage.SuspendLayout();
            this.customAdditionalConfigurationGroupBox.SuspendLayout();
            this.customActionGroup.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.processManagementGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbWorkersEventUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxCpuUsageNumericUpDown)).BeginInit();
            this.eventEditorGroup.SuspendLayout();
            this.startTimeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteStart)).BeginInit();
            this.durationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondsDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dayDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutesDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursDuration)).BeginInit();
            this.planningGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.removeArgsParameterButton);
            this.groupBox1.Controls.Add(this.addArgsParameterButton);
            this.groupBox1.Controls.Add(this.argsOptionsListBox);
            this.groupBox1.Controls.Add(this.jvmOptionsListBox);
            this.groupBox1.Controls.Add(this.removeJvmParameterButton);
            this.groupBox1.Controls.Add(this.addJvmParameterButton);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.jvmLocationButton);
            this.groupBox1.Controls.Add(this.proactiveLocationButton);
            this.groupBox1.Controls.Add(this.jvmDirectory);
            this.groupBox1.Controls.Add(this.proactiveDirectory);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 175);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ProActive Configuration";
            // 
            // removeArgsParameterButton
            // 
            this.removeArgsParameterButton.Location = new System.Drawing.Point(3, 121);
            this.removeArgsParameterButton.Name = "removeArgsParameterButton";
            this.removeArgsParameterButton.Size = new System.Drawing.Size(86, 21);
            this.removeArgsParameterButton.TabIndex = 15;
            this.removeArgsParameterButton.Text = "Remove args";
            this.removeArgsParameterButton.UseVisualStyleBackColor = true;
            this.removeArgsParameterButton.Click += new System.EventHandler(this.removeArgsOptionButton_Click);
            // 
            // addArgsParameterButton
            // 
            this.addArgsParameterButton.Location = new System.Drawing.Point(3, 94);
            this.addArgsParameterButton.Name = "addArgsParameterButton";
            this.addArgsParameterButton.Size = new System.Drawing.Size(86, 21);
            this.addArgsParameterButton.TabIndex = 14;
            this.addArgsParameterButton.Text = "Add args";
            this.addArgsParameterButton.UseVisualStyleBackColor = true;
            this.addArgsParameterButton.Click += new System.EventHandler(this.addArgsOptionButton_Click);
            // 
            // argsOptionsListBox
            // 
            this.argsOptionsListBox.FormattingEnabled = true;
            this.argsOptionsListBox.Location = new System.Drawing.Point(95, 94);
            this.argsOptionsListBox.Name = "argsOptionsListBox";
            this.argsOptionsListBox.Size = new System.Drawing.Size(175, 69);
            this.argsOptionsListBox.TabIndex = 13;
            this.toolTip.SetToolTip(this.argsOptionsListBox, "If the parameter contains ${rank} it will be dynamically replaced by the Runtime " +
        "rank.");
            this.argsOptionsListBox.SelectedIndexChanged += new System.EventHandler(this.argsOptionsListBox_SelectedIndexChanged);
            this.argsOptionsListBox.DoubleClick += new System.EventHandler(this.argsOptionsListBox_DoubleClick);
            this.argsOptionsListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.argsOptionsListBox_KeyPress);
            // 
            // jvmOptionsListBox
            // 
            this.jvmOptionsListBox.FormattingEnabled = true;
            this.jvmOptionsListBox.Location = new System.Drawing.Point(397, 95);
            this.jvmOptionsListBox.Name = "jvmOptionsListBox";
            this.jvmOptionsListBox.Size = new System.Drawing.Size(262, 69);
            this.jvmOptionsListBox.TabIndex = 12;
            this.toolTip.SetToolTip(this.jvmOptionsListBox, "If the parameter contains ${rank} it will be dynamically replaced by the Runtime " +
        "rank.");
            this.jvmOptionsListBox.SelectedIndexChanged += new System.EventHandler(this.jvmOptionsListBox_SelectedIndexChanged);
            this.jvmOptionsListBox.DoubleClick += new System.EventHandler(this.jvmOptionsListBox_DoubleClick);
            this.jvmOptionsListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.jvmOptionsListBox_KeyPress);
            // 
            // removeJvmParameterButton
            // 
            this.removeJvmParameterButton.Location = new System.Drawing.Point(276, 122);
            this.removeJvmParameterButton.Name = "removeJvmParameterButton";
            this.removeJvmParameterButton.Size = new System.Drawing.Size(114, 21);
            this.removeJvmParameterButton.TabIndex = 11;
            this.removeJvmParameterButton.Text = "Remove JVM Option";
            this.removeJvmParameterButton.UseVisualStyleBackColor = true;
            this.removeJvmParameterButton.Click += new System.EventHandler(this.removeJvmOptionButton_Click);
            // 
            // addJvmParameterButton
            // 
            this.addJvmParameterButton.Location = new System.Drawing.Point(276, 95);
            this.addJvmParameterButton.Name = "addJvmParameterButton";
            this.addJvmParameterButton.Size = new System.Drawing.Size(114, 21);
            this.addJvmParameterButton.TabIndex = 10;
            this.addJvmParameterButton.Text = "Add JVM Option";
            this.addJvmParameterButton.UseVisualStyleBackColor = true;
            this.addJvmParameterButton.Click += new System.EventHandler(this.addJvmOptionButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(145, 71);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(162, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Use system-wide Java Home";
            this.toolTip.SetToolTip(this.checkBox1, "Uses JVM location specified by the JAVA_HOME environment variable.");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // jvmLocationButton
            // 
            this.jvmLocationButton.Location = new System.Drawing.Point(6, 45);
            this.jvmLocationButton.Name = "jvmLocationButton";
            this.jvmLocationButton.Size = new System.Drawing.Size(133, 20);
            this.jvmLocationButton.TabIndex = 8;
            this.jvmLocationButton.Text = "Browse Java Home:";
            this.jvmLocationButton.UseVisualStyleBackColor = true;
            this.jvmLocationButton.Click += new System.EventHandler(this.jvmLocationButton_Click);
            // 
            // proactiveLocationButton
            // 
            this.proactiveLocationButton.Location = new System.Drawing.Point(6, 16);
            this.proactiveLocationButton.Name = "proactiveLocationButton";
            this.proactiveLocationButton.Size = new System.Drawing.Size(133, 20);
            this.proactiveLocationButton.TabIndex = 7;
            this.proactiveLocationButton.Text = "Browse ProActive Home:";
            this.proactiveLocationButton.UseVisualStyleBackColor = true;
            this.proactiveLocationButton.Click += new System.EventHandler(this.proactiveLocationButton_Click);
            // 
            // jvmDirectory
            // 
            this.jvmDirectory.Location = new System.Drawing.Point(145, 45);
            this.jvmDirectory.Name = "jvmDirectory";
            this.jvmDirectory.Size = new System.Drawing.Size(514, 20);
            this.jvmDirectory.TabIndex = 1;
            this.toolTip.SetToolTip(this.jvmDirectory, "Java Home location.");
            this.jvmDirectory.TextChanged += new System.EventHandler(this.jvmDirectory_TextChanged);
            // 
            // proactiveDirectory
            // 
            this.proactiveDirectory.Location = new System.Drawing.Point(145, 16);
            this.proactiveDirectory.Name = "proactiveDirectory";
            this.proactiveDirectory.Size = new System.Drawing.Size(514, 20);
            this.proactiveDirectory.TabIndex = 0;
            this.toolTip.SetToolTip(this.proactiveDirectory, "Location of the ProActive or Scheduling home.");
            this.proactiveDirectory.TextChanged += new System.EventHandler(this.proactiveLocation_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.generalTabPage);
            this.tabControl1.Controls.Add(this.connectionTabPage);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(10, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 429);
            this.tabControl1.TabIndex = 1;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.runtimeIncomingProtocolGroupBox);
            this.generalTabPage.Controls.Add(this.onRuntimeExitGroupBox);
            this.generalTabPage.Controls.Add(this.multiRuntimeGroupBox);
            this.generalTabPage.Controls.Add(this.networkInterfaceListGroupBox);
            this.generalTabPage.Controls.Add(this.memoryLimitGroupBox);
            this.generalTabPage.Controls.Add(this.groupBox1);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalTabPage.Size = new System.Drawing.Size(674, 403);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // runtimeIncomingProtocolGroupBox
            // 
            this.runtimeIncomingProtocolGroupBox.Controls.Add(this.protocolComboBox);
            this.runtimeIncomingProtocolGroupBox.Controls.Add(this.protocolLabel);
            this.runtimeIncomingProtocolGroupBox.Controls.Add(this.portInitialValue);
            this.runtimeIncomingProtocolGroupBox.Controls.Add(this.portInitialValueNumericUpDown);
            this.runtimeIncomingProtocolGroupBox.Location = new System.Drawing.Point(10, 344);
            this.runtimeIncomingProtocolGroupBox.Name = "runtimeIncomingProtocolGroupBox";
            this.runtimeIncomingProtocolGroupBox.Size = new System.Drawing.Size(378, 50);
            this.runtimeIncomingProtocolGroupBox.TabIndex = 6;
            this.runtimeIncomingProtocolGroupBox.TabStop = false;
            this.runtimeIncomingProtocolGroupBox.Text = "Runtime Incoming Protocol";
            // 
            // protocolComboBox
            // 
            this.protocolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocolComboBox.FormattingEnabled = true;
            this.protocolComboBox.Items.AddRange(new object[] {
            "undefined",
            "rmi",
            "http",
            "pamr",
            "pnp",
            "pnps"});
            this.protocolComboBox.Location = new System.Drawing.Point(145, 19);
            this.protocolComboBox.Name = "protocolComboBox";
            this.protocolComboBox.Size = new System.Drawing.Size(74, 21);
            this.protocolComboBox.TabIndex = 3;
            this.toolTip.SetToolTip(this.protocolComboBox, "Defines the protocol to be used by the Runtime.");
            this.protocolComboBox.SelectedIndexChanged += new System.EventHandler(this.protocolComboBox_SelectedIndexChanged);
            // 
            // protocolLabel
            // 
            this.protocolLabel.AutoSize = true;
            this.protocolLabel.Location = new System.Drawing.Point(90, 22);
            this.protocolLabel.Name = "protocolLabel";
            this.protocolLabel.Size = new System.Drawing.Size(49, 13);
            this.protocolLabel.TabIndex = 2;
            this.protocolLabel.Text = "Protocol:";
            // 
            // portInitialValue
            // 
            this.portInitialValue.AutoSize = true;
            this.portInitialValue.Location = new System.Drawing.Point(222, 22);
            this.portInitialValue.Name = "portInitialValue";
            this.portInitialValue.Size = new System.Drawing.Size(86, 13);
            this.portInitialValue.TabIndex = 1;
            this.portInitialValue.Text = "Port Initial Value:";
            // 
            // portInitialValueNumericUpDown
            // 
            this.portInitialValueNumericUpDown.Location = new System.Drawing.Point(314, 20);
            this.portInitialValueNumericUpDown.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.portInitialValueNumericUpDown.Minimum = new decimal(new int[] {
            1099,
            0,
            0,
            0});
            this.portInitialValueNumericUpDown.Name = "portInitialValueNumericUpDown";
            this.portInitialValueNumericUpDown.Size = new System.Drawing.Size(57, 20);
            this.portInitialValueNumericUpDown.TabIndex = 0;
            this.toolTip.SetToolTip(this.portInitialValueNumericUpDown, "Defines the value of the \"-Dproactive.SELECTED_PROTOCOL.port\" property. This valu" +
        "e will always be increased by 1 or more if there is more than one Runtime.");
            this.portInitialValueNumericUpDown.Value = new decimal(new int[] {
            1099,
            0,
            0,
            0});
            this.portInitialValueNumericUpDown.ValueChanged += new System.EventHandler(this.initialValueNumericUpDown_ValueChanged);
            // 
            // onRuntimeExitGroupBox
            // 
            this.onRuntimeExitGroupBox.Controls.Add(this.scriptLocationButton);
            this.onRuntimeExitGroupBox.Controls.Add(this.scriptLocationTextBox);
            this.onRuntimeExitGroupBox.Location = new System.Drawing.Point(4, 184);
            this.onRuntimeExitGroupBox.Name = "onRuntimeExitGroupBox";
            this.onRuntimeExitGroupBox.Size = new System.Drawing.Size(384, 49);
            this.onRuntimeExitGroupBox.TabIndex = 5;
            this.onRuntimeExitGroupBox.TabStop = false;
            this.onRuntimeExitGroupBox.Text = "On Runtime Exit";
            // 
            // scriptLocationButton
            // 
            this.scriptLocationButton.Location = new System.Drawing.Point(5, 18);
            this.scriptLocationButton.Name = "scriptLocationButton";
            this.scriptLocationButton.Size = new System.Drawing.Size(133, 20);
            this.scriptLocationButton.TabIndex = 2;
            this.scriptLocationButton.Text = "Browse Script Location:";
            this.scriptLocationButton.UseVisualStyleBackColor = true;
            this.scriptLocationButton.Click += new System.EventHandler(this.scriptLocationButton_Click);
            // 
            // scriptLocationTextBox
            // 
            this.scriptLocationTextBox.Location = new System.Drawing.Point(144, 18);
            this.scriptLocationTextBox.Name = "scriptLocationTextBox";
            this.scriptLocationTextBox.Size = new System.Drawing.Size(234, 20);
            this.scriptLocationTextBox.TabIndex = 0;
            this.toolTip.SetToolTip(this.scriptLocationTextBox, "Location of the script executed after a Runtime terminates.");
            this.scriptLocationTextBox.TextChanged += new System.EventHandler(this.scriptLocationTextBox_TextChanged);
            // 
            // multiRuntimeGroupBox
            // 
            this.multiRuntimeGroupBox.Controls.Add(this.nbWorkersLabel);
            this.multiRuntimeGroupBox.Controls.Add(this.nbWorkersNumericUpDown);
            this.multiRuntimeGroupBox.Controls.Add(this.nbRuntimesLabel);
            this.multiRuntimeGroupBox.Controls.Add(this.availableCPUsValue);
            this.multiRuntimeGroupBox.Controls.Add(this.availableCPUsLabel);
            this.multiRuntimeGroupBox.Controls.Add(this.nbRuntimesNumericUpDown);
            this.multiRuntimeGroupBox.Location = new System.Drawing.Point(235, 239);
            this.multiRuntimeGroupBox.Name = "multiRuntimeGroupBox";
            this.multiRuntimeGroupBox.Size = new System.Drawing.Size(153, 99);
            this.multiRuntimeGroupBox.TabIndex = 4;
            this.multiRuntimeGroupBox.TabStop = false;
            this.multiRuntimeGroupBox.Text = "Multi-Runtime";
            // 
            // nbWorkersLabel
            // 
            this.nbWorkersLabel.AutoSize = true;
            this.nbWorkersLabel.Location = new System.Drawing.Point(41, 45);
            this.nbWorkersLabel.Name = "nbWorkersLabel";
            this.nbWorkersLabel.Size = new System.Drawing.Size(50, 13);
            this.nbWorkersLabel.TabIndex = 5;
            this.nbWorkersLabel.Text = "Workers:";
            // 
            // nbWorkersNumericUpDown
            // 
            this.nbWorkersNumericUpDown.Location = new System.Drawing.Point(97, 43);
            this.nbWorkersNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nbWorkersNumericUpDown.Name = "nbWorkersNumericUpDown";
            this.nbWorkersNumericUpDown.Size = new System.Drawing.Size(48, 20);
            this.nbWorkersNumericUpDown.TabIndex = 4;
            this.toolTip.SetToolTip(this.nbWorkersNumericUpDown, "Specifies the number of Workers to spawn.");
            this.nbWorkersNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbWorkersNumericUpDown.ValueChanged += new System.EventHandler(this.nbWorkersNumericUpDown_ValueChanged);
            // 
            // nbRuntimesLabel
            // 
            this.nbRuntimesLabel.AutoSize = true;
            this.nbRuntimesLabel.Location = new System.Drawing.Point(37, 73);
            this.nbRuntimesLabel.Name = "nbRuntimesLabel";
            this.nbRuntimesLabel.Size = new System.Drawing.Size(54, 13);
            this.nbRuntimesLabel.TabIndex = 3;
            this.nbRuntimesLabel.Text = "Runtimes:";
            // 
            // availableCPUsValue
            // 
            this.availableCPUsValue.AutoSize = true;
            this.availableCPUsValue.Location = new System.Drawing.Point(97, 21);
            this.availableCPUsValue.Name = "availableCPUsValue";
            this.availableCPUsValue.Size = new System.Drawing.Size(13, 13);
            this.availableCPUsValue.TabIndex = 2;
            this.availableCPUsValue.Text = "0";
            // 
            // availableCPUsLabel
            // 
            this.availableCPUsLabel.AutoSize = true;
            this.availableCPUsLabel.Location = new System.Drawing.Point(8, 21);
            this.availableCPUsLabel.Name = "availableCPUsLabel";
            this.availableCPUsLabel.Size = new System.Drawing.Size(83, 13);
            this.availableCPUsLabel.TabIndex = 1;
            this.availableCPUsLabel.Text = "Available CPUs:";
            // 
            // nbRuntimesNumericUpDown
            // 
            this.nbRuntimesNumericUpDown.Location = new System.Drawing.Point(97, 71);
            this.nbRuntimesNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nbRuntimesNumericUpDown.Name = "nbRuntimesNumericUpDown";
            this.nbRuntimesNumericUpDown.Size = new System.Drawing.Size(48, 20);
            this.nbRuntimesNumericUpDown.TabIndex = 0;
            this.toolTip.SetToolTip(this.nbRuntimesNumericUpDown, "Specifies the number of Runtimes to spawn.");
            this.nbRuntimesNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbRuntimesNumericUpDown.ValueChanged += new System.EventHandler(this.nbRuntimesNumericUpDown_ValueChanged);
            // 
            // networkInterfaceListGroupBox
            // 
            this.networkInterfaceListGroupBox.Controls.Add(this.useNetworkInterfaceButton);
            this.networkInterfaceListGroupBox.Controls.Add(this.networkInterfacesListBox);
            this.networkInterfaceListGroupBox.Controls.Add(this.refreshNetworkInterfacesButton);
            this.networkInterfaceListGroupBox.Location = new System.Drawing.Point(394, 227);
            this.networkInterfaceListGroupBox.Name = "networkInterfaceListGroupBox";
            this.networkInterfaceListGroupBox.Size = new System.Drawing.Size(274, 167);
            this.networkInterfaceListGroupBox.TabIndex = 3;
            this.networkInterfaceListGroupBox.TabStop = false;
            this.networkInterfaceListGroupBox.Text = "Available Network Interfaces (Java 6 only)";
            // 
            // useNetworkInterfaceButton
            // 
            this.useNetworkInterfaceButton.Enabled = false;
            this.useNetworkInterfaceButton.Location = new System.Drawing.Point(112, 184);
            this.useNetworkInterfaceButton.Name = "useNetworkInterfaceButton";
            this.useNetworkInterfaceButton.Size = new System.Drawing.Size(75, 20);
            this.useNetworkInterfaceButton.TabIndex = 3;
            this.useNetworkInterfaceButton.Text = "Use";
            this.toolTip.SetToolTip(this.useNetworkInterfaceButton, "Uses the selected network interface.");
            this.useNetworkInterfaceButton.UseVisualStyleBackColor = true;
            this.useNetworkInterfaceButton.Click += new System.EventHandler(this.useNetworkInterfaceButton_Click);
            // 
            // networkInterfacesListBox
            // 
            this.networkInterfacesListBox.FormattingEnabled = true;
            this.networkInterfacesListBox.HorizontalScrollbar = true;
            this.networkInterfacesListBox.Location = new System.Drawing.Point(6, 19);
            this.networkInterfacesListBox.Name = "networkInterfacesListBox";
            this.networkInterfacesListBox.Size = new System.Drawing.Size(262, 134);
            this.networkInterfacesListBox.TabIndex = 2;
            this.toolTip.SetToolTip(this.networkInterfacesListBox, "The list of available network interfaces.");
            this.networkInterfacesListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.networkInterfacesListBox_MouseDoubleClick);
            // 
            // refreshNetworkInterfacesButton
            // 
            this.refreshNetworkInterfacesButton.Location = new System.Drawing.Point(193, 184);
            this.refreshNetworkInterfacesButton.Name = "refreshNetworkInterfacesButton";
            this.refreshNetworkInterfacesButton.Size = new System.Drawing.Size(75, 20);
            this.refreshNetworkInterfacesButton.TabIndex = 1;
            this.refreshNetworkInterfacesButton.Text = "Refresh";
            this.refreshNetworkInterfacesButton.UseVisualStyleBackColor = true;
            this.refreshNetworkInterfacesButton.Click += new System.EventHandler(this.listNetworkInterfacesButton_Click);
            // 
            // memoryLimitGroupBox
            // 
            this.memoryLimitGroupBox.Controls.Add(this.memoryLimitNumericUpDown);
            this.memoryLimitGroupBox.Controls.Add(this.memoryLimitLabel);
            this.memoryLimitGroupBox.Controls.Add(this.availablePhysicalMemoryValue);
            this.memoryLimitGroupBox.Controls.Add(this.availablePhysicalMemoryLabel);
            this.memoryLimitGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoryLimitGroupBox.Location = new System.Drawing.Point(3, 239);
            this.memoryLimitGroupBox.Name = "memoryLimitGroupBox";
            this.memoryLimitGroupBox.Size = new System.Drawing.Size(226, 99);
            this.memoryLimitGroupBox.TabIndex = 1;
            this.memoryLimitGroupBox.TabStop = false;
            this.memoryLimitGroupBox.Text = "Runtime Memory Limit (Mbytes)";
            // 
            // memoryLimitNumericUpDown
            // 
            this.memoryLimitNumericUpDown.Location = new System.Drawing.Point(145, 43);
            this.memoryLimitNumericUpDown.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.memoryLimitNumericUpDown.Name = "memoryLimitNumericUpDown";
            this.memoryLimitNumericUpDown.Size = new System.Drawing.Size(74, 20);
            this.memoryLimitNumericUpDown.TabIndex = 5;
            this.toolTip.SetToolTip(this.memoryLimitNumericUpDown, "0 means no memory limit and at least 128 is required for a ProActive Runtime.");
            this.memoryLimitNumericUpDown.ValueChanged += new System.EventHandler(this.memoryLimitNumericUpDown_ValueChanged);
            // 
            // memoryLimitLabel
            // 
            this.memoryLimitLabel.AutoSize = true;
            this.memoryLimitLabel.Location = new System.Drawing.Point(68, 45);
            this.memoryLimitLabel.Name = "memoryLimitLabel";
            this.memoryLimitLabel.Size = new System.Drawing.Size(71, 13);
            this.memoryLimitLabel.TabIndex = 4;
            this.memoryLimitLabel.Text = "Memory Limit:";
            // 
            // availablePhysicalMemoryValue
            // 
            this.availablePhysicalMemoryValue.AutoSize = true;
            this.availablePhysicalMemoryValue.Location = new System.Drawing.Point(147, 21);
            this.availablePhysicalMemoryValue.Name = "availablePhysicalMemoryValue";
            this.availablePhysicalMemoryValue.Size = new System.Drawing.Size(13, 13);
            this.availablePhysicalMemoryValue.TabIndex = 3;
            this.availablePhysicalMemoryValue.Text = "0";
            // 
            // availablePhysicalMemoryLabel
            // 
            this.availablePhysicalMemoryLabel.AutoSize = true;
            this.availablePhysicalMemoryLabel.Location = new System.Drawing.Point(4, 21);
            this.availablePhysicalMemoryLabel.Name = "availablePhysicalMemoryLabel";
            this.availablePhysicalMemoryLabel.Size = new System.Drawing.Size(135, 13);
            this.availablePhysicalMemoryLabel.TabIndex = 2;
            this.availablePhysicalMemoryLabel.Text = "Available Physical Memory:";
            // 
            // connectionTabPage
            // 
            this.connectionTabPage.Controls.Add(this.customRadioButton);
            this.connectionTabPage.Controls.Add(this.resourceManagerRegistrationRadioButton);
            this.connectionTabPage.Controls.Add(this.localRegistrationRadioButton);
            this.connectionTabPage.Controls.Add(this.connectionTypeTabControl);
            this.connectionTabPage.Location = new System.Drawing.Point(4, 22);
            this.connectionTabPage.Name = "connectionTabPage";
            this.connectionTabPage.Size = new System.Drawing.Size(674, 403);
            this.connectionTabPage.TabIndex = 3;
            this.connectionTabPage.Text = "Connection";
            this.connectionTabPage.UseVisualStyleBackColor = true;
            // 
            // customRadioButton
            // 
            this.customRadioButton.AutoSize = true;
            this.customRadioButton.Location = new System.Drawing.Point(3, 47);
            this.customRadioButton.Name = "customRadioButton";
            this.customRadioButton.Size = new System.Drawing.Size(14, 13);
            this.customRadioButton.TabIndex = 3;
            this.customRadioButton.TabStop = true;
            this.toolTip.SetToolTip(this.customRadioButton, "Enables a user defined connection. See the \"Custom\" tab below.");
            this.customRadioButton.UseVisualStyleBackColor = true;
            this.customRadioButton.CheckedChanged += new System.EventHandler(this.customRadioButton_CheckedChanged);
            // 
            // resourceManagerRegistrationRadioButton
            // 
            this.resourceManagerRegistrationRadioButton.AutoSize = true;
            this.resourceManagerRegistrationRadioButton.Location = new System.Drawing.Point(3, 27);
            this.resourceManagerRegistrationRadioButton.Name = "resourceManagerRegistrationRadioButton";
            this.resourceManagerRegistrationRadioButton.Size = new System.Drawing.Size(14, 13);
            this.resourceManagerRegistrationRadioButton.TabIndex = 2;
            this.resourceManagerRegistrationRadioButton.TabStop = true;
            this.toolTip.SetToolTip(this.resourceManagerRegistrationRadioButton, "Enables the Resource Manager Registration. The Runtime will try to register to th" +
        "e specified Resource Manager.");
            this.resourceManagerRegistrationRadioButton.UseVisualStyleBackColor = true;
            this.resourceManagerRegistrationRadioButton.CheckedChanged += new System.EventHandler(this.resourceManagerRegistrationRadioButton_CheckedChanged);
            // 
            // localRegistrationRadioButton
            // 
            this.localRegistrationRadioButton.AutoSize = true;
            this.localRegistrationRadioButton.Location = new System.Drawing.Point(3, 7);
            this.localRegistrationRadioButton.Name = "localRegistrationRadioButton";
            this.localRegistrationRadioButton.Size = new System.Drawing.Size(14, 13);
            this.localRegistrationRadioButton.TabIndex = 1;
            this.localRegistrationRadioButton.TabStop = true;
            this.toolTip.SetToolTip(this.localRegistrationRadioButton, "Enables the Local Registration.The runtime will be registered locally.");
            this.localRegistrationRadioButton.UseVisualStyleBackColor = true;
            this.localRegistrationRadioButton.CheckedChanged += new System.EventHandler(this.rmiRegistrationRadioButton_CheckedChanged);
            // 
            // connectionTypeTabControl
            // 
            this.connectionTypeTabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.connectionTypeTabControl.Controls.Add(this.localRegistrationTabPage);
            this.connectionTypeTabControl.Controls.Add(this.resourceManagerRegistrationTabPage);
            this.connectionTypeTabControl.Controls.Add(this.customTabPage);
            this.connectionTypeTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.connectionTypeTabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.connectionTypeTabControl.ItemSize = new System.Drawing.Size(19, 175);
            this.connectionTypeTabControl.Location = new System.Drawing.Point(23, 3);
            this.connectionTypeTabControl.Multiline = true;
            this.connectionTypeTabControl.Name = "connectionTypeTabControl";
            this.connectionTypeTabControl.SelectedIndex = 0;
            this.connectionTypeTabControl.Size = new System.Drawing.Size(648, 368);
            this.connectionTypeTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.connectionTypeTabControl.TabIndex = 0;
            this.connectionTypeTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.actionTypeTabControl_DrawItem);
            // 
            // localRegistrationTabPage
            // 
            this.localRegistrationTabPage.Controls.Add(this.rmiRegistrationAdditionalConfigurationGroupBox);
            this.localRegistrationTabPage.Controls.Add(this.localRegistrationGroupBox);
            this.localRegistrationTabPage.Location = new System.Drawing.Point(179, 4);
            this.localRegistrationTabPage.Name = "localRegistrationTabPage";
            this.localRegistrationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.localRegistrationTabPage.Size = new System.Drawing.Size(465, 360);
            this.localRegistrationTabPage.TabIndex = 0;
            this.localRegistrationTabPage.Text = "Local Registration";
            this.localRegistrationTabPage.UseVisualStyleBackColor = true;
            // 
            // rmiRegistrationAdditionalConfigurationGroupBox
            // 
            this.rmiRegistrationAdditionalConfigurationGroupBox.Controls.Add(this.rmiRegistrationJavaActionClassTextBox);
            this.rmiRegistrationAdditionalConfigurationGroupBox.Controls.Add(this.label13);
            this.rmiRegistrationAdditionalConfigurationGroupBox.Location = new System.Drawing.Point(6, 306);
            this.rmiRegistrationAdditionalConfigurationGroupBox.Name = "rmiRegistrationAdditionalConfigurationGroupBox";
            this.rmiRegistrationAdditionalConfigurationGroupBox.Size = new System.Drawing.Size(453, 48);
            this.rmiRegistrationAdditionalConfigurationGroupBox.TabIndex = 2;
            this.rmiRegistrationAdditionalConfigurationGroupBox.TabStop = false;
            this.rmiRegistrationAdditionalConfigurationGroupBox.Text = "Additional Configuration";
            // 
            // rmiRegistrationJavaActionClassTextBox
            // 
            this.rmiRegistrationJavaActionClassTextBox.Location = new System.Drawing.Point(106, 19);
            this.rmiRegistrationJavaActionClassTextBox.Name = "rmiRegistrationJavaActionClassTextBox";
            this.rmiRegistrationJavaActionClassTextBox.Size = new System.Drawing.Size(341, 20);
            this.rmiRegistrationJavaActionClassTextBox.TabIndex = 1;
            this.toolTip.SetToolTip(this.rmiRegistrationJavaActionClassTextBox, "Defines the Java class to run.");
            this.rmiRegistrationJavaActionClassTextBox.TextChanged += new System.EventHandler(this.rmiRegistrationJavaActionClassTextBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Java Starter Class:";
            // 
            // localRegistrationGroupBox
            // 
            this.localRegistrationGroupBox.Controls.Add(this.localbindNodeNameLabel);
            this.localRegistrationGroupBox.Controls.Add(this.localRegistrationNodeName);
            this.localRegistrationGroupBox.Location = new System.Drawing.Point(6, 6);
            this.localRegistrationGroupBox.Name = "localRegistrationGroupBox";
            this.localRegistrationGroupBox.Size = new System.Drawing.Size(453, 53);
            this.localRegistrationGroupBox.TabIndex = 1;
            this.localRegistrationGroupBox.TabStop = false;
            this.localRegistrationGroupBox.Text = "Local Registration";
            // 
            // localbindNodeNameLabel
            // 
            this.localbindNodeNameLabel.AutoSize = true;
            this.localbindNodeNameLabel.Location = new System.Drawing.Point(6, 22);
            this.localbindNodeNameLabel.Name = "localbindNodeNameLabel";
            this.localbindNodeNameLabel.Size = new System.Drawing.Size(67, 13);
            this.localbindNodeNameLabel.TabIndex = 2;
            this.localbindNodeNameLabel.Text = "Node Name:";
            // 
            // localRegistrationNodeName
            // 
            this.localRegistrationNodeName.Location = new System.Drawing.Point(79, 19);
            this.localRegistrationNodeName.Name = "localRegistrationNodeName";
            this.localRegistrationNodeName.Size = new System.Drawing.Size(368, 20);
            this.localRegistrationNodeName.TabIndex = 1;
            this.toolTip.SetToolTip(this.localRegistrationNodeName, "Defines the name of the node. The node will be registered under url like URL://IP" +
        "_OR_HOSTNAME:PORT/NODE_NAME");
            this.localRegistrationNodeName.TextChanged += new System.EventHandler(this.rmiNodeName_TextChanged);
            // 
            // resourceManagerRegistrationTabPage
            // 
            this.resourceManagerRegistrationTabPage.Controls.Add(this.resourceManagerRegistrationAdditionalConfigurationGroupBox);
            this.resourceManagerRegistrationTabPage.Controls.Add(this.rmActionGroup);
            this.resourceManagerRegistrationTabPage.Controls.Add(this.authenticationCredentialGroupBox);
            this.resourceManagerRegistrationTabPage.Location = new System.Drawing.Point(179, 4);
            this.resourceManagerRegistrationTabPage.Name = "resourceManagerRegistrationTabPage";
            this.resourceManagerRegistrationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.resourceManagerRegistrationTabPage.Size = new System.Drawing.Size(465, 360);
            this.resourceManagerRegistrationTabPage.TabIndex = 1;
            this.resourceManagerRegistrationTabPage.Text = "Resource Manager Registration";
            this.resourceManagerRegistrationTabPage.UseVisualStyleBackColor = true;
            // 
            // resourceManagerRegistrationAdditionalConfigurationGroupBox
            // 
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.Controls.Add(this.resourceManagerRegistrationJavaActionClassTextBox);
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.Controls.Add(this.label18);
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.Location = new System.Drawing.Point(6, 306);
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.Name = "resourceManagerRegistrationAdditionalConfigurationGroupBox";
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.Size = new System.Drawing.Size(453, 48);
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.TabIndex = 4;
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.TabStop = false;
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.Text = "Additional Configuration";
            // 
            // resourceManagerRegistrationJavaActionClassTextBox
            // 
            this.resourceManagerRegistrationJavaActionClassTextBox.Location = new System.Drawing.Point(106, 19);
            this.resourceManagerRegistrationJavaActionClassTextBox.Name = "resourceManagerRegistrationJavaActionClassTextBox";
            this.resourceManagerRegistrationJavaActionClassTextBox.Size = new System.Drawing.Size(341, 20);
            this.resourceManagerRegistrationJavaActionClassTextBox.TabIndex = 1;
            this.toolTip.SetToolTip(this.resourceManagerRegistrationJavaActionClassTextBox, "Defines the Java class to run.");
            this.resourceManagerRegistrationJavaActionClassTextBox.TextChanged += new System.EventHandler(this.resourceManagerRegistrationJavaActionClassTextBox_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(95, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Java Starter Class:";
            // 
            // rmActionGroup
            // 
            this.rmActionGroup.Controls.Add(this.nodeSourceNameLabel);
            this.rmActionGroup.Controls.Add(this.nodeSourceNameTextBox);
            this.rmActionGroup.Controls.Add(this.nodeNameTextBox);
            this.rmActionGroup.Controls.Add(this.nodeNameLabel);
            this.rmActionGroup.Controls.Add(this.rmUrl);
            this.rmActionGroup.Controls.Add(this.resourceManagerUrlLabel);
            this.rmActionGroup.Location = new System.Drawing.Point(6, 6);
            this.rmActionGroup.Name = "rmActionGroup";
            this.rmActionGroup.Size = new System.Drawing.Size(453, 100);
            this.rmActionGroup.TabIndex = 3;
            this.rmActionGroup.TabStop = false;
            this.rmActionGroup.Text = "Resource Manager Registration";
            // 
            // nodeSourceNameLabel
            // 
            this.nodeSourceNameLabel.AutoSize = true;
            this.nodeSourceNameLabel.Location = new System.Drawing.Point(28, 75);
            this.nodeSourceNameLabel.Name = "nodeSourceNameLabel";
            this.nodeSourceNameLabel.Size = new System.Drawing.Size(104, 13);
            this.nodeSourceNameLabel.TabIndex = 11;
            this.nodeSourceNameLabel.Text = "Node Source Name:";
            // 
            // nodeSourceNameTextBox
            // 
            this.nodeSourceNameTextBox.Location = new System.Drawing.Point(138, 72);
            this.nodeSourceNameTextBox.Name = "nodeSourceNameTextBox";
            this.nodeSourceNameTextBox.Size = new System.Drawing.Size(309, 20);
            this.nodeSourceNameTextBox.TabIndex = 10;
            this.toolTip.SetToolTip(this.nodeSourceNameTextBox, "The name of the node source without whitespaces.");
            this.nodeSourceNameTextBox.TextChanged += new System.EventHandler(this.nodeSourceNameTextBox_TextChanged);
            // 
            // nodeNameTextBox
            // 
            this.nodeNameTextBox.Location = new System.Drawing.Point(138, 45);
            this.nodeNameTextBox.Name = "nodeNameTextBox";
            this.nodeNameTextBox.Size = new System.Drawing.Size(309, 20);
            this.nodeNameTextBox.TabIndex = 3;
            this.toolTip.SetToolTip(this.nodeNameTextBox, "The name of the node without whitespaces.");
            this.nodeNameTextBox.TextChanged += new System.EventHandler(this.rmNodeName_TextChanged);
            // 
            // nodeNameLabel
            // 
            this.nodeNameLabel.AutoSize = true;
            this.nodeNameLabel.Location = new System.Drawing.Point(65, 48);
            this.nodeNameLabel.Name = "nodeNameLabel";
            this.nodeNameLabel.Size = new System.Drawing.Size(67, 13);
            this.nodeNameLabel.TabIndex = 2;
            this.nodeNameLabel.Text = "Node Name:";
            // 
            // rmUrl
            // 
            this.rmUrl.Location = new System.Drawing.Point(138, 19);
            this.rmUrl.Name = "rmUrl";
            this.rmUrl.Size = new System.Drawing.Size(309, 20);
            this.rmUrl.TabIndex = 1;
            this.toolTip.SetToolTip(this.rmUrl, "Example: PROTOCOL://HOSTNAME_OR_IP_ADDRESS:PORT, can be empty if discovery is use" +
        "d.");
            this.rmUrl.TextChanged += new System.EventHandler(this.rmUrl_TextChanged);
            // 
            // resourceManagerUrlLabel
            // 
            this.resourceManagerUrlLabel.AutoSize = true;
            this.resourceManagerUrlLabel.Location = new System.Drawing.Point(6, 22);
            this.resourceManagerUrlLabel.Name = "resourceManagerUrlLabel";
            this.resourceManagerUrlLabel.Size = new System.Drawing.Size(126, 13);
            this.resourceManagerUrlLabel.TabIndex = 0;
            this.resourceManagerUrlLabel.Text = "Resource Manager URL:";
            // 
            // authenticationCredentialGroupBox
            // 
            this.authenticationCredentialGroupBox.Controls.Add(this.credentialBrowseLocationButton);
            this.authenticationCredentialGroupBox.Controls.Add(this.label16);
            this.authenticationCredentialGroupBox.Controls.Add(this.label4);
            this.authenticationCredentialGroupBox.Controls.Add(this.credentialLocationTextBox);
            this.authenticationCredentialGroupBox.Location = new System.Drawing.Point(6, 112);
            this.authenticationCredentialGroupBox.Name = "authenticationCredentialGroupBox";
            this.authenticationCredentialGroupBox.Size = new System.Drawing.Size(453, 50);
            this.authenticationCredentialGroupBox.TabIndex = 9;
            this.authenticationCredentialGroupBox.TabStop = false;
            this.authenticationCredentialGroupBox.Text = "Authentication Credential";
            // 
            // credentialBrowseLocationButton
            // 
            this.credentialBrowseLocationButton.Location = new System.Drawing.Point(6, 20);
            this.credentialBrowseLocationButton.Name = "credentialBrowseLocationButton";
            this.credentialBrowseLocationButton.Size = new System.Drawing.Size(126, 20);
            this.credentialBrowseLocationButton.TabIndex = 4;
            this.credentialBrowseLocationButton.Text = "Browse Location:";
            this.credentialBrowseLocationButton.UseVisualStyleBackColor = true;
            this.credentialBrowseLocationButton.Click += new System.EventHandler(this.credentialBrowseLocationButton_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(65, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 13);
            this.label16.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 1;
            // 
            // credentialLocationTextBox
            // 
            this.credentialLocationTextBox.Location = new System.Drawing.Point(138, 20);
            this.credentialLocationTextBox.Name = "credentialLocationTextBox";
            this.credentialLocationTextBox.Size = new System.Drawing.Size(309, 20);
            this.credentialLocationTextBox.TabIndex = 0;
            this.toolTip.SetToolTip(this.credentialLocationTextBox, "Location of the file that contains the credential.");
            this.credentialLocationTextBox.TextChanged += new System.EventHandler(this.credentialLocationTextBox_TextChanged);
            // 
            // customTabPage
            // 
            this.customTabPage.Controls.Add(this.customAdditionalConfigurationGroupBox);
            this.customTabPage.Controls.Add(this.customActionGroup);
            this.customTabPage.Location = new System.Drawing.Point(179, 4);
            this.customTabPage.Name = "customTabPage";
            this.customTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.customTabPage.Size = new System.Drawing.Size(465, 360);
            this.customTabPage.TabIndex = 2;
            this.customTabPage.Text = "Custom";
            this.customTabPage.UseVisualStyleBackColor = true;
            // 
            // customAdditionalConfigurationGroupBox
            // 
            this.customAdditionalConfigurationGroupBox.Controls.Add(this.customJavaActionClassTextBox);
            this.customAdditionalConfigurationGroupBox.Controls.Add(this.label19);
            this.customAdditionalConfigurationGroupBox.Location = new System.Drawing.Point(6, 306);
            this.customAdditionalConfigurationGroupBox.Name = "customAdditionalConfigurationGroupBox";
            this.customAdditionalConfigurationGroupBox.Size = new System.Drawing.Size(453, 48);
            this.customAdditionalConfigurationGroupBox.TabIndex = 5;
            this.customAdditionalConfigurationGroupBox.TabStop = false;
            this.customAdditionalConfigurationGroupBox.Text = "Additional Configuration";
            // 
            // customJavaActionClassTextBox
            // 
            this.customJavaActionClassTextBox.Location = new System.Drawing.Point(106, 19);
            this.customJavaActionClassTextBox.Name = "customJavaActionClassTextBox";
            this.customJavaActionClassTextBox.Size = new System.Drawing.Size(341, 20);
            this.customJavaActionClassTextBox.TabIndex = 1;
            this.toolTip.SetToolTip(this.customJavaActionClassTextBox, "Defines the Java class to run.");
            this.customJavaActionClassTextBox.TextChanged += new System.EventHandler(this.customJavaActionClassTextBox_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Java Starter Class:";
            // 
            // customActionGroup
            // 
            this.customActionGroup.Controls.Add(this.customSaveArgumentButton);
            this.customActionGroup.Controls.Add(this.customArgumentTextBox);
            this.customActionGroup.Controls.Add(this.label15);
            this.customActionGroup.Controls.Add(this.customDeleteButton);
            this.customActionGroup.Controls.Add(this.customAddButton);
            this.customActionGroup.Controls.Add(this.customArgumentsListBox);
            this.customActionGroup.Controls.Add(this.label14);
            this.customActionGroup.Location = new System.Drawing.Point(6, 6);
            this.customActionGroup.Name = "customActionGroup";
            this.customActionGroup.Size = new System.Drawing.Size(453, 151);
            this.customActionGroup.TabIndex = 3;
            this.customActionGroup.TabStop = false;
            this.customActionGroup.Text = "Custom";
            // 
            // customSaveArgumentButton
            // 
            this.customSaveArgumentButton.Enabled = false;
            this.customSaveArgumentButton.Location = new System.Drawing.Point(372, 93);
            this.customSaveArgumentButton.Name = "customSaveArgumentButton";
            this.customSaveArgumentButton.Size = new System.Drawing.Size(75, 23);
            this.customSaveArgumentButton.TabIndex = 6;
            this.customSaveArgumentButton.Text = "Save Arg";
            this.toolTip.SetToolTip(this.customSaveArgumentButton, "Saves a modified argument.");
            this.customSaveArgumentButton.UseVisualStyleBackColor = true;
            this.customSaveArgumentButton.Click += new System.EventHandler(this.customSaveArgumentButton_Click);
            // 
            // customArgumentTextBox
            // 
            this.customArgumentTextBox.Location = new System.Drawing.Point(69, 122);
            this.customArgumentTextBox.Name = "customArgumentTextBox";
            this.customArgumentTextBox.Size = new System.Drawing.Size(378, 20);
            this.customArgumentTextBox.TabIndex = 5;
            this.toolTip.SetToolTip(this.customArgumentTextBox, "Enter an argument without whitespaces.");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 125);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Argument:";
            // 
            // customDeleteButton
            // 
            this.customDeleteButton.Location = new System.Drawing.Point(372, 63);
            this.customDeleteButton.Name = "customDeleteButton";
            this.customDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.customDeleteButton.TabIndex = 3;
            this.customDeleteButton.Text = "Delete";
            this.toolTip.SetToolTip(this.customDeleteButton, "Deletes a selected argument in the \"Arguments\" list.");
            this.customDeleteButton.UseVisualStyleBackColor = true;
            this.customDeleteButton.Click += new System.EventHandler(this.customDeleteArgumentButton_Click);
            // 
            // customAddButton
            // 
            this.customAddButton.Location = new System.Drawing.Point(372, 34);
            this.customAddButton.Name = "customAddButton";
            this.customAddButton.Size = new System.Drawing.Size(75, 23);
            this.customAddButton.TabIndex = 2;
            this.customAddButton.Text = "Add";
            this.toolTip.SetToolTip(this.customAddButton, "Adds the argument defined in the \"Argument\" field.");
            this.customAddButton.UseVisualStyleBackColor = true;
            this.customAddButton.Click += new System.EventHandler(this.customAddHostButton_Click);
            // 
            // customArgumentsListBox
            // 
            this.customArgumentsListBox.FormattingEnabled = true;
            this.customArgumentsListBox.Location = new System.Drawing.Point(9, 34);
            this.customArgumentsListBox.Name = "customArgumentsListBox";
            this.customArgumentsListBox.Size = new System.Drawing.Size(357, 82);
            this.customArgumentsListBox.TabIndex = 1;
            this.toolTip.SetToolTip(this.customArgumentsListBox, "A list of arguments that will be passed as parameters of the Java Starter Class.");
            this.customArgumentsListBox.SelectedIndexChanged += new System.EventHandler(this.customArgumentsListBox_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Arguments:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.processManagementGroupBox);
            this.tabPage2.Controls.Add(this.alwaysAvailableCheckBox);
            this.tabPage2.Controls.Add(this.eventEditorGroup);
            this.tabPage2.Controls.Add(this.planningGroupBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(674, 403);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Planning";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // processManagementGroupBox
            // 
            this.processManagementGroupBox.Controls.Add(this.nbWorkersEventUpDown);
            this.processManagementGroupBox.Controls.Add(this.nbWorkersEventLabel);
            this.processManagementGroupBox.Controls.Add(this.label20);
            this.processManagementGroupBox.Controls.Add(this.maxCpuUsageNumericUpDown);
            this.processManagementGroupBox.Controls.Add(this.maxCpuUsageLabel);
            this.processManagementGroupBox.Controls.Add(this.processPriorityLabel);
            this.processManagementGroupBox.Controls.Add(this.processPriorityComboBox);
            this.processManagementGroupBox.Location = new System.Drawing.Point(259, 142);
            this.processManagementGroupBox.Name = "processManagementGroupBox";
            this.processManagementGroupBox.Size = new System.Drawing.Size(409, 97);
            this.processManagementGroupBox.TabIndex = 20;
            this.processManagementGroupBox.TabStop = false;
            this.processManagementGroupBox.Text = "Process Management";
            // 
            // nbWorkersEventUpDown
            // 
            this.nbWorkersEventUpDown.Location = new System.Drawing.Point(327, 54);
            this.nbWorkersEventUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nbWorkersEventUpDown.Name = "nbWorkersEventUpDown";
            this.nbWorkersEventUpDown.Size = new System.Drawing.Size(48, 20);
            this.nbWorkersEventUpDown.TabIndex = 6;
            this.toolTip.SetToolTip(this.nbWorkersEventUpDown, "Specifies the number of Runtimes to spawn.");
            this.nbWorkersEventUpDown.ValueChanged += new System.EventHandler(this.nbWorkersEventUpDown_ValueChanged);
            // 
            // nbWorkersEventLabel
            // 
            this.nbWorkersEventLabel.AutoSize = true;
            this.nbWorkersEventLabel.Location = new System.Drawing.Point(220, 56);
            this.nbWorkersEventLabel.Name = "nbWorkersEventLabel";
            this.nbWorkersEventLabel.Size = new System.Drawing.Size(99, 13);
            this.nbWorkersEventLabel.TabIndex = 5;
            this.nbWorkersEventLabel.Text = "Number of workers:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(382, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(15, 13);
            this.label20.TabIndex = 4;
            this.label20.Text = "%";
            // 
            // maxCpuUsageNumericUpDown
            // 
            this.maxCpuUsageNumericUpDown.Location = new System.Drawing.Point(327, 20);
            this.maxCpuUsageNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxCpuUsageNumericUpDown.Name = "maxCpuUsageNumericUpDown";
            this.maxCpuUsageNumericUpDown.Size = new System.Drawing.Size(49, 20);
            this.maxCpuUsageNumericUpDown.TabIndex = 3;
            this.toolTip.SetToolTip(this.maxCpuUsageNumericUpDown, "Specifies the maximum allowed CPU usage of the Runtime process and its children p" +
        "rocesses during this plan.");
            this.maxCpuUsageNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.maxCpuUsageNumericUpDown.ValueChanged += new System.EventHandler(this.maxCpuUsageNumericUpDown_ValueChanged);
            // 
            // maxCpuUsageLabel
            // 
            this.maxCpuUsageLabel.AutoSize = true;
            this.maxCpuUsageLabel.Location = new System.Drawing.Point(229, 22);
            this.maxCpuUsageLabel.Name = "maxCpuUsageLabel";
            this.maxCpuUsageLabel.Size = new System.Drawing.Size(92, 13);
            this.maxCpuUsageLabel.TabIndex = 2;
            this.maxCpuUsageLabel.Text = "Max CPU Usage: ";
            // 
            // processPriorityLabel
            // 
            this.processPriorityLabel.AutoSize = true;
            this.processPriorityLabel.Location = new System.Drawing.Point(6, 22);
            this.processPriorityLabel.Name = "processPriorityLabel";
            this.processPriorityLabel.Size = new System.Drawing.Size(82, 13);
            this.processPriorityLabel.TabIndex = 1;
            this.processPriorityLabel.Text = "Process Priority:";
            // 
            // processPriorityComboBox
            // 
            this.processPriorityComboBox.FormattingEnabled = true;
            this.processPriorityComboBox.Items.AddRange(new object[] {
            "Normal",
            "Idle",
            "High",
            "RealTime",
            "BelowNormal",
            "AboveNormal"});
            this.processPriorityComboBox.Location = new System.Drawing.Point(94, 19);
            this.processPriorityComboBox.Name = "processPriorityComboBox";
            this.processPriorityComboBox.Size = new System.Drawing.Size(108, 21);
            this.processPriorityComboBox.TabIndex = 0;
            this.toolTip.SetToolTip(this.processPriorityComboBox, "Specifies the priority of the Runtime process and its children processes during t" +
        "his plan.");
            this.processPriorityComboBox.SelectedIndexChanged += new System.EventHandler(this.processPriorityComboBox_SelectedIndexChanged);
            // 
            // alwaysAvailableCheckBox
            // 
            this.alwaysAvailableCheckBox.AutoSize = true;
            this.alwaysAvailableCheckBox.Location = new System.Drawing.Point(564, 348);
            this.alwaysAvailableCheckBox.Name = "alwaysAvailableCheckBox";
            this.alwaysAvailableCheckBox.Size = new System.Drawing.Size(104, 17);
            this.alwaysAvailableCheckBox.TabIndex = 18;
            this.alwaysAvailableCheckBox.Text = "Always available";
            this.toolTip.SetToolTip(this.alwaysAvailableCheckBox, "No weekly planning, the ProActive Agent will be always available.");
            this.alwaysAvailableCheckBox.UseVisualStyleBackColor = true;
            this.alwaysAvailableCheckBox.CheckStateChanged += new System.EventHandler(this.alwaysAvailableCheckBox_CheckStateChanged);
            // 
            // eventEditorGroup
            // 
            this.eventEditorGroup.Controls.Add(this.startTimeGroupBox);
            this.eventEditorGroup.Controls.Add(this.durationGroupBox);
            this.eventEditorGroup.Enabled = false;
            this.eventEditorGroup.Location = new System.Drawing.Point(259, 3);
            this.eventEditorGroup.Name = "eventEditorGroup";
            this.eventEditorGroup.Size = new System.Drawing.Size(409, 133);
            this.eventEditorGroup.TabIndex = 1;
            this.eventEditorGroup.TabStop = false;
            this.eventEditorGroup.Text = "Plan Editor";
            // 
            // startTimeGroupBox
            // 
            this.startTimeGroupBox.Controls.Add(this.label8);
            this.startTimeGroupBox.Controls.Add(this.label7);
            this.startTimeGroupBox.Controls.Add(this.secondStart);
            this.startTimeGroupBox.Controls.Add(this.label5);
            this.startTimeGroupBox.Controls.Add(this.weekdayStart);
            this.startTimeGroupBox.Controls.Add(this.label6);
            this.startTimeGroupBox.Controls.Add(this.hourStart);
            this.startTimeGroupBox.Controls.Add(this.minuteStart);
            this.startTimeGroupBox.Location = new System.Drawing.Point(6, 19);
            this.startTimeGroupBox.Name = "startTimeGroupBox";
            this.startTimeGroupBox.Size = new System.Drawing.Size(397, 51);
            this.startTimeGroupBox.TabIndex = 19;
            this.startTimeGroupBox.TabStop = false;
            this.startTimeGroupBox.Text = "Start Time";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Day:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(297, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Seconds:";
            // 
            // secondStart
            // 
            this.secondStart.Location = new System.Drawing.Point(355, 19);
            this.secondStart.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.secondStart.Name = "secondStart";
            this.secondStart.Size = new System.Drawing.Size(36, 20);
            this.secondStart.TabIndex = 13;
            this.toolTip.SetToolTip(this.secondStart, "Specifies the start second.");
            this.secondStart.ValueChanged += new System.EventHandler(this.startSecondChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(116, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Hours:";
            // 
            // weekdayStart
            // 
            this.weekdayStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weekdayStart.FormattingEnabled = true;
            this.weekdayStart.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.weekdayStart.Location = new System.Drawing.Point(45, 18);
            this.weekdayStart.Name = "weekdayStart";
            this.weekdayStart.Size = new System.Drawing.Size(65, 21);
            this.weekdayStart.TabIndex = 10;
            this.toolTip.SetToolTip(this.weekdayStart, "Specifies the start day of the week.");
            this.weekdayStart.SelectedIndexChanged += new System.EventHandler(this.weekdayStart_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(202, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Minutes:";
            // 
            // hourStart
            // 
            this.hourStart.Location = new System.Drawing.Point(160, 19);
            this.hourStart.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.hourStart.Name = "hourStart";
            this.hourStart.Size = new System.Drawing.Size(36, 20);
            this.hourStart.TabIndex = 11;
            this.toolTip.SetToolTip(this.hourStart, "Specifies the start hour.");
            this.hourStart.ValueChanged += new System.EventHandler(this.startHourChanged);
            // 
            // minuteStart
            // 
            this.minuteStart.Location = new System.Drawing.Point(255, 19);
            this.minuteStart.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minuteStart.Name = "minuteStart";
            this.minuteStart.Size = new System.Drawing.Size(36, 20);
            this.minuteStart.TabIndex = 12;
            this.toolTip.SetToolTip(this.minuteStart, "Specifies the start minute.");
            this.minuteStart.ValueChanged += new System.EventHandler(this.startMinuteChanged);
            // 
            // durationGroupBox
            // 
            this.durationGroupBox.Controls.Add(this.label9);
            this.durationGroupBox.Controls.Add(this.secondsDuration);
            this.durationGroupBox.Controls.Add(this.dayDuration);
            this.durationGroupBox.Controls.Add(this.label12);
            this.durationGroupBox.Controls.Add(this.label10);
            this.durationGroupBox.Controls.Add(this.minutesDuration);
            this.durationGroupBox.Controls.Add(this.hoursDuration);
            this.durationGroupBox.Controls.Add(this.label11);
            this.durationGroupBox.Location = new System.Drawing.Point(6, 76);
            this.durationGroupBox.Name = "durationGroupBox";
            this.durationGroupBox.Size = new System.Drawing.Size(397, 51);
            this.durationGroupBox.TabIndex = 18;
            this.durationGroupBox.TabStop = false;
            this.durationGroupBox.Text = "Duration";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Days:";
            // 
            // secondsDuration
            // 
            this.secondsDuration.Location = new System.Drawing.Point(355, 19);
            this.secondsDuration.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.secondsDuration.Name = "secondsDuration";
            this.secondsDuration.Size = new System.Drawing.Size(36, 20);
            this.secondsDuration.TabIndex = 17;
            this.toolTip.SetToolTip(this.secondsDuration, "The number of duration seconds.");
            this.secondsDuration.ValueChanged += new System.EventHandler(this.durationSecondChanged);
            // 
            // dayDuration
            // 
            this.dayDuration.Location = new System.Drawing.Point(74, 19);
            this.dayDuration.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.dayDuration.Name = "dayDuration";
            this.dayDuration.Size = new System.Drawing.Size(36, 20);
            this.dayDuration.TabIndex = 14;
            this.toolTip.SetToolTip(this.dayDuration, "The number of duration days.");
            this.dayDuration.ValueChanged += new System.EventHandler(this.durationDayChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(297, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "Seconds:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(116, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Hours:";
            // 
            // minutesDuration
            // 
            this.minutesDuration.Location = new System.Drawing.Point(255, 19);
            this.minutesDuration.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minutesDuration.Name = "minutesDuration";
            this.minutesDuration.Size = new System.Drawing.Size(36, 20);
            this.minutesDuration.TabIndex = 16;
            this.toolTip.SetToolTip(this.minutesDuration, "The number of duration minutes.");
            this.minutesDuration.ValueChanged += new System.EventHandler(this.durationMinuteChanged);
            // 
            // hoursDuration
            // 
            this.hoursDuration.Location = new System.Drawing.Point(160, 19);
            this.hoursDuration.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.hoursDuration.Name = "hoursDuration";
            this.hoursDuration.Size = new System.Drawing.Size(36, 20);
            this.hoursDuration.TabIndex = 15;
            this.toolTip.SetToolTip(this.hoursDuration, "The number of duration hours.");
            this.hoursDuration.ValueChanged += new System.EventHandler(this.durationHourChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(202, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Minutes:";
            // 
            // planningGroupBox
            // 
            this.planningGroupBox.Controls.Add(this.showButton);
            this.planningGroupBox.Controls.Add(this.createEventButton);
            this.planningGroupBox.Controls.Add(this.deleteEventButton);
            this.planningGroupBox.Controls.Add(this.eventsList);
            this.planningGroupBox.Location = new System.Drawing.Point(3, 3);
            this.planningGroupBox.Name = "planningGroupBox";
            this.planningGroupBox.Size = new System.Drawing.Size(247, 362);
            this.planningGroupBox.TabIndex = 0;
            this.planningGroupBox.TabStop = false;
            this.planningGroupBox.Text = "Weekly Planning";
            // 
            // showButton
            // 
            this.showButton.Location = new System.Drawing.Point(171, 333);
            this.showButton.Name = "showButton";
            this.showButton.Size = new System.Drawing.Size(70, 23);
            this.showButton.TabIndex = 6;
            this.showButton.Text = "Show";
            this.toolTip.SetToolTip(this.showButton, "Shows a chart that represents the weekly planning.");
            this.showButton.UseVisualStyleBackColor = true;
            this.showButton.Click += new System.EventHandler(this.showButton_Click);
            // 
            // createEventButton
            // 
            this.createEventButton.Location = new System.Drawing.Point(6, 333);
            this.createEventButton.Name = "createEventButton";
            this.createEventButton.Size = new System.Drawing.Size(87, 23);
            this.createEventButton.TabIndex = 3;
            this.createEventButton.Text = "Create plan";
            this.toolTip.SetToolTip(this.createEventButton, "Creates a new plan.");
            this.createEventButton.UseVisualStyleBackColor = true;
            this.createEventButton.Click += new System.EventHandler(this.createEventButton_Click);
            // 
            // deleteEventButton
            // 
            this.deleteEventButton.Location = new System.Drawing.Point(99, 333);
            this.deleteEventButton.Name = "deleteEventButton";
            this.deleteEventButton.Size = new System.Drawing.Size(66, 23);
            this.deleteEventButton.TabIndex = 1;
            this.deleteEventButton.Text = "Delete";
            this.toolTip.SetToolTip(this.deleteEventButton, "Deletes a selected plan.");
            this.deleteEventButton.UseVisualStyleBackColor = true;
            this.deleteEventButton.Click += new System.EventHandler(this.deleteEventButton_Click);
            // 
            // saveConfig
            // 
            this.saveConfig.Enabled = false;
            this.saveConfig.Location = new System.Drawing.Point(536, 447);
            this.saveConfig.Name = "saveConfig";
            this.saveConfig.Size = new System.Drawing.Size(75, 23);
            this.saveConfig.TabIndex = 20;
            this.saveConfig.Text = "Save";
            this.toolTip.SetToolTip(this.saveConfig, "Saves modifications.");
            this.saveConfig.UseVisualStyleBackColor = true;
            this.saveConfig.Click += new System.EventHandler(this.saveConfig_Click);
            // 
            // closeConfig
            // 
            this.closeConfig.Location = new System.Drawing.Point(617, 447);
            this.closeConfig.Name = "closeConfig";
            this.closeConfig.Size = new System.Drawing.Size(75, 23);
            this.closeConfig.TabIndex = 21;
            this.closeConfig.Text = "Close";
            this.toolTip.SetToolTip(this.closeConfig, "Closes this window.");
            this.closeConfig.UseVisualStyleBackColor = true;
            this.closeConfig.Click += new System.EventHandler(this.closeConfig_Click);
            // 
            // proActiveLocationBrowser
            // 
            this.proActiveLocationBrowser.Description = "Choose location for ProActive";
            // 
            // jvmLocationBrowser
            // 
            this.jvmLocationBrowser.Description = "Choose location for JVM";
            // 
            // saveConfigAs
            // 
            this.saveConfigAs.Location = new System.Drawing.Point(455, 447);
            this.saveConfigAs.Name = "saveConfigAs";
            this.saveConfigAs.Size = new System.Drawing.Size(75, 23);
            this.saveConfigAs.TabIndex = 19;
            this.saveConfigAs.Text = "Save as ...";
            this.toolTip.SetToolTip(this.saveConfigAs, "Saves the current configuration under a user specified filename.");
            this.saveConfigAs.UseVisualStyleBackColor = true;
            this.saveConfigAs.Click += new System.EventHandler(this.saveConfigAs_Click);
            // 
            // scriptLocationOpenDialog
            // 
            this.scriptLocationOpenDialog.DefaultExt = "bat";
            this.scriptLocationOpenDialog.Filter = "Scripts .bat/.cmd|*.bat;*.cmd|Executables .exe|*.exe";
            // 
            // credentialLocationOpenDialog
            // 
            this.credentialLocationOpenDialog.DefaultExt = "cred";
            this.credentialLocationOpenDialog.Filter = "Credentials .cred|*.cred";
            // 
            // eventsList
            // 
            this.eventsList.FormattingEnabled = true;
            this.eventsList.Location = new System.Drawing.Point(6, 19);
            this.eventsList.Name = "eventsList";
            this.eventsList.Size = new System.Drawing.Size(235, 303);
            this.eventsList.TabIndex = 0;
            this.toolTip.SetToolTip(this.eventsList, "The list of weekly plans.");
            this.eventsList.SelectedIndexChanged += new System.EventHandler(this.eventsList_SelectedIndexChanged);
            // 
            // ConfigurationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 477);
            this.Controls.Add(this.closeConfig);
            this.Controls.Add(this.saveConfigAs);
            this.Controls.Add(this.saveConfig);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigurationEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigEditor_FormClosing);
            this.Load += new System.EventHandler(this.ConfigEditor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.runtimeIncomingProtocolGroupBox.ResumeLayout(false);
            this.runtimeIncomingProtocolGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portInitialValueNumericUpDown)).EndInit();
            this.onRuntimeExitGroupBox.ResumeLayout(false);
            this.onRuntimeExitGroupBox.PerformLayout();
            this.multiRuntimeGroupBox.ResumeLayout(false);
            this.multiRuntimeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbWorkersNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbRuntimesNumericUpDown)).EndInit();
            this.networkInterfaceListGroupBox.ResumeLayout(false);
            this.memoryLimitGroupBox.ResumeLayout(false);
            this.memoryLimitGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoryLimitNumericUpDown)).EndInit();
            this.connectionTabPage.ResumeLayout(false);
            this.connectionTabPage.PerformLayout();
            this.connectionTypeTabControl.ResumeLayout(false);
            this.localRegistrationTabPage.ResumeLayout(false);
            this.rmiRegistrationAdditionalConfigurationGroupBox.ResumeLayout(false);
            this.rmiRegistrationAdditionalConfigurationGroupBox.PerformLayout();
            this.localRegistrationGroupBox.ResumeLayout(false);
            this.localRegistrationGroupBox.PerformLayout();
            this.resourceManagerRegistrationTabPage.ResumeLayout(false);
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.ResumeLayout(false);
            this.resourceManagerRegistrationAdditionalConfigurationGroupBox.PerformLayout();
            this.rmActionGroup.ResumeLayout(false);
            this.rmActionGroup.PerformLayout();
            this.authenticationCredentialGroupBox.ResumeLayout(false);
            this.authenticationCredentialGroupBox.PerformLayout();
            this.customTabPage.ResumeLayout(false);
            this.customAdditionalConfigurationGroupBox.ResumeLayout(false);
            this.customAdditionalConfigurationGroupBox.PerformLayout();
            this.customActionGroup.ResumeLayout(false);
            this.customActionGroup.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.processManagementGroupBox.ResumeLayout(false);
            this.processManagementGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbWorkersEventUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxCpuUsageNumericUpDown)).EndInit();
            this.eventEditorGroup.ResumeLayout(false);
            this.startTimeGroupBox.ResumeLayout(false);
            this.startTimeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteStart)).EndInit();
            this.durationGroupBox.ResumeLayout(false);
            this.durationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondsDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dayDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutesDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursDuration)).EndInit();
            this.planningGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox jvmDirectory;
        private System.Windows.Forms.TextBox proactiveDirectory;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button jvmLocationButton;
        private System.Windows.Forms.Button proactiveLocationButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button saveConfig;
        private System.Windows.Forms.Button closeConfig;
        private System.Windows.Forms.GroupBox planningGroupBox;
        private RefreshingListBox eventsList;
        private System.Windows.Forms.GroupBox eventEditorGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown hourStart;
        private System.Windows.Forms.ComboBox weekdayStart;
        private System.Windows.Forms.Button createEventButton;
        private System.Windows.Forms.Button deleteEventButton;
        private System.Windows.Forms.NumericUpDown secondStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown minuteStart;
        private System.Windows.Forms.NumericUpDown dayDuration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown minutesDuration;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown hoursDuration;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown secondsDuration;
        private System.Windows.Forms.FolderBrowserDialog proActiveLocationBrowser;
        private System.Windows.Forms.FolderBrowserDialog jvmLocationBrowser;
        private System.Windows.Forms.Button saveConfigAs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button showButton;
        private System.Windows.Forms.CheckBox alwaysAvailableCheckBox;
        private System.Windows.Forms.GroupBox memoryLimitGroupBox;
        private System.Windows.Forms.Label availablePhysicalMemoryLabel;
        private System.Windows.Forms.Label availablePhysicalMemoryValue;
        private System.Windows.Forms.Button removeJvmParameterButton;
        private System.Windows.Forms.Button addJvmParameterButton;
        private System.Windows.Forms.ListBox jvmOptionsListBox;
        private System.Windows.Forms.ListBox argsOptionsListBox;
        private System.Windows.Forms.Label memoryLimitLabel;
        private System.Windows.Forms.NumericUpDown memoryLimitNumericUpDown;
        private System.Windows.Forms.GroupBox durationGroupBox;
        private System.Windows.Forms.GroupBox startTimeGroupBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox processManagementGroupBox;
        private System.Windows.Forms.Label processPriorityLabel;
        private System.Windows.Forms.ComboBox processPriorityComboBox;
        private System.Windows.Forms.TabPage connectionTabPage;
        private System.Windows.Forms.TabPage localRegistrationTabPage;
        private System.Windows.Forms.TabPage resourceManagerRegistrationTabPage;
        private System.Windows.Forms.TabPage customTabPage;
        private System.Windows.Forms.RadioButton localRegistrationRadioButton;
        private System.Windows.Forms.RadioButton customRadioButton;
        private System.Windows.Forms.RadioButton resourceManagerRegistrationRadioButton;
        private System.Windows.Forms.TabControl connectionTypeTabControl;
        private System.Windows.Forms.GroupBox customActionGroup;
        private System.Windows.Forms.Button customSaveArgumentButton;
        private System.Windows.Forms.TextBox customArgumentTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button customDeleteButton;
        private System.Windows.Forms.Button customAddButton;
        private System.Windows.Forms.ListBox customArgumentsListBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox localRegistrationGroupBox;
        private System.Windows.Forms.TextBox localRegistrationNodeName;
        private System.Windows.Forms.GroupBox rmActionGroup;
        private System.Windows.Forms.TextBox rmUrl;
        private System.Windows.Forms.Label resourceManagerUrlLabel;
        private System.Windows.Forms.TextBox nodeNameTextBox;
        private System.Windows.Forms.Label nodeNameLabel;
        private System.Windows.Forms.GroupBox rmiRegistrationAdditionalConfigurationGroupBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox rmiRegistrationJavaActionClassTextBox;
        private System.Windows.Forms.GroupBox resourceManagerRegistrationAdditionalConfigurationGroupBox;
        private System.Windows.Forms.TextBox resourceManagerRegistrationJavaActionClassTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox customAdditionalConfigurationGroupBox;
        private System.Windows.Forms.TextBox customJavaActionClassTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label maxCpuUsageLabel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown maxCpuUsageNumericUpDown;
        private System.Windows.Forms.GroupBox networkInterfaceListGroupBox;
        private System.Windows.Forms.Button refreshNetworkInterfacesButton;
        private System.Windows.Forms.ListBox networkInterfacesListBox;
        private System.Windows.Forms.Button useNetworkInterfaceButton;
        private System.Windows.Forms.GroupBox multiRuntimeGroupBox;
        private System.Windows.Forms.Label availableCPUsValue;
        private System.Windows.Forms.Label availableCPUsLabel;
        private System.Windows.Forms.NumericUpDown nbRuntimesNumericUpDown;
        private System.Windows.Forms.Label nbRuntimesLabel;
        private System.Windows.Forms.GroupBox onRuntimeExitGroupBox;
        private System.Windows.Forms.TextBox scriptLocationTextBox;
        private System.Windows.Forms.Button scriptLocationButton;
        private System.Windows.Forms.OpenFileDialog scriptLocationOpenDialog;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox authenticationCredentialGroupBox;
        private System.Windows.Forms.TextBox nodeSourceNameTextBox;
        private System.Windows.Forms.Label nodeSourceNameLabel;
        private System.Windows.Forms.TextBox credentialLocationTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button credentialBrowseLocationButton;
        private System.Windows.Forms.OpenFileDialog credentialLocationOpenDialog;
        private System.Windows.Forms.Label localbindNodeNameLabel;
        private System.Windows.Forms.GroupBox runtimeIncomingProtocolGroupBox;
        private System.Windows.Forms.ComboBox protocolComboBox;
        private System.Windows.Forms.Label protocolLabel;
        private System.Windows.Forms.Label portInitialValue;
        private System.Windows.Forms.NumericUpDown portInitialValueNumericUpDown;
        private System.Windows.Forms.Label nbWorkersLabel;
        private System.Windows.Forms.NumericUpDown nbWorkersNumericUpDown;
        private System.Windows.Forms.Button addArgsParameterButton;
        private System.Windows.Forms.Button removeArgsParameterButton;
        private System.Windows.Forms.NumericUpDown nbWorkersEventUpDown;
        private System.Windows.Forms.Label nbWorkersEventLabel;
    }


}