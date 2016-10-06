namespace AliveAPIDotNet
{
    partial class DebugWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugWindow));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuObject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyVTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteVTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mindControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lineGraphMemory = new AliveAPIDotNet.LineGraph();
            this.textBoxMemoryLog = new System.Windows.Forms.TextBox();
            this.labelTotal = new System.Windows.Forms.Label();
            this.buttonUpdateMemory = new System.Windows.Forms.Button();
            this.listMemoryAllocations = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonSpawnObject = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.checkBoxRenderMode = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnPathRefresh = new System.Windows.Forms.Button();
            this.panelCurrentScreen = new AliveAPIDotNet.DoubleBufferPanel();
            this.listBoxPath = new System.Windows.Forms.ListBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRecord = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.button3 = new System.Windows.Forms.Button();
            this.contextMenuObject.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 120;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.ContextMenuStrip = this.contextMenuObject;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(388, 368);
            this.listBox1.TabIndex = 4;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // contextMenuObject
            // 
            this.contextMenuObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAddressToolStripMenuItem,
            this.toolStripSeparator2,
            this.copyVTableToolStripMenuItem,
            this.pasteVTableToolStripMenuItem,
            this.toolStripSeparator1,
            this.mindControlToolStripMenuItem,
            this.toolStripSeparator3,
            this.removeToolStripMenuItem,
            this.duplicateToolStripMenuItem});
            this.contextMenuObject.Name = "contextMenuObject";
            this.contextMenuObject.Size = new System.Drawing.Size(159, 154);
            // 
            // copyAddressToolStripMenuItem
            // 
            this.copyAddressToolStripMenuItem.Name = "copyAddressToolStripMenuItem";
            this.copyAddressToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyAddressToolStripMenuItem.Text = "Copy Address";
            this.copyAddressToolStripMenuItem.Click += new System.EventHandler(this.copyAddressToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // copyVTableToolStripMenuItem
            // 
            this.copyVTableToolStripMenuItem.Name = "copyVTableToolStripMenuItem";
            this.copyVTableToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyVTableToolStripMenuItem.Text = "Copy VTable";
            this.copyVTableToolStripMenuItem.Click += new System.EventHandler(this.copyVTableToolStripMenuItem_Click);
            // 
            // pasteVTableToolStripMenuItem
            // 
            this.pasteVTableToolStripMenuItem.Name = "pasteVTableToolStripMenuItem";
            this.pasteVTableToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.pasteVTableToolStripMenuItem.Text = "Paste VTable";
            this.pasteVTableToolStripMenuItem.Click += new System.EventHandler(this.pasteVTableToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // mindControlToolStripMenuItem
            // 
            this.mindControlToolStripMenuItem.Name = "mindControlToolStripMenuItem";
            this.mindControlToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.mindControlToolStripMenuItem.Text = "Mind Control";
            this.mindControlToolStripMenuItem.Click += new System.EventHandler(this.mindControlToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.removeToolStripMenuItem.Text = "Remove (Temp)";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate (R.I.P)";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 381);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(388, 97);
            this.textBox1.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(408, 510);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(400, 484);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Game";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.numericUpDown2);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.numericUpDown1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(400, 484);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sound";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(71, 14);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(57, 20);
            this.numericUpDown2.TabIndex = 3;
            this.numericUpDown2.Value = new decimal(new int[] {
            55,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(134, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Play Sound";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(8, 14);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(57, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lineGraphMemory);
            this.tabPage2.Controls.Add(this.textBoxMemoryLog);
            this.tabPage2.Controls.Add(this.labelTotal);
            this.tabPage2.Controls.Add(this.buttonUpdateMemory);
            this.tabPage2.Controls.Add(this.listMemoryAllocations);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(400, 484);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Memory";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lineGraphMemory
            // 
            this.lineGraphMemory.BackColor = System.Drawing.Color.White;
            this.lineGraphMemory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(125)))), ((int)(((byte)(187)))));
            this.lineGraphMemory.Location = new System.Drawing.Point(9, 215);
            this.lineGraphMemory.MaxPoints = 256;
            this.lineGraphMemory.Name = "lineGraphMemory";
            this.lineGraphMemory.Size = new System.Drawing.Size(381, 185);
            this.lineGraphMemory.TabIndex = 8;
            this.lineGraphMemory.Title = "Memory Usage";
            // 
            // textBoxMemoryLog
            // 
            this.textBoxMemoryLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMemoryLog.Location = new System.Drawing.Point(9, 381);
            this.textBoxMemoryLog.Multiline = true;
            this.textBoxMemoryLog.Name = "textBoxMemoryLog";
            this.textBoxMemoryLog.ReadOnly = true;
            this.textBoxMemoryLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMemoryLog.Size = new System.Drawing.Size(383, 97);
            this.textBoxMemoryLog.TabIndex = 7;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(8, 183);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(34, 13);
            this.labelTotal.TabIndex = 2;
            this.labelTotal.Text = "Total:";
            // 
            // buttonUpdateMemory
            // 
            this.buttonUpdateMemory.Location = new System.Drawing.Point(317, 186);
            this.buttonUpdateMemory.Name = "buttonUpdateMemory";
            this.buttonUpdateMemory.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateMemory.TabIndex = 1;
            this.buttonUpdateMemory.Text = "Update";
            this.buttonUpdateMemory.UseVisualStyleBackColor = true;
            this.buttonUpdateMemory.Click += new System.EventHandler(this.buttonUpdateMemory_Click);
            // 
            // listMemoryAllocations
            // 
            this.listMemoryAllocations.FormattingEnabled = true;
            this.listMemoryAllocations.Location = new System.Drawing.Point(9, 7);
            this.listMemoryAllocations.Name = "listMemoryAllocations";
            this.listMemoryAllocations.Size = new System.Drawing.Size(383, 173);
            this.listMemoryAllocations.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.buttonSpawnObject);
            this.tabPage4.Controls.Add(this.listBox2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(400, 484);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Spawn";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // buttonSpawnObject
            // 
            this.buttonSpawnObject.Location = new System.Drawing.Point(4, 365);
            this.buttonSpawnObject.Name = "buttonSpawnObject";
            this.buttonSpawnObject.Size = new System.Drawing.Size(388, 23);
            this.buttonSpawnObject.TabIndex = 1;
            this.buttonSpawnObject.Text = "Spawn";
            this.buttonSpawnObject.UseVisualStyleBackColor = true;
            this.buttonSpawnObject.Click += new System.EventHandler(this.buttonSpawnObject_Click);
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(4, 4);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(388, 355);
            this.listBox2.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.checkBoxRenderMode);
            this.tabPage5.Controls.Add(this.button2);
            this.tabPage5.Controls.Add(this.btnPathRefresh);
            this.tabPage5.Controls.Add(this.panelCurrentScreen);
            this.tabPage5.Controls.Add(this.listBoxPath);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(400, 484);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Path";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // checkBoxRenderMode
            // 
            this.checkBoxRenderMode.AutoSize = true;
            this.checkBoxRenderMode.Location = new System.Drawing.Point(9, 215);
            this.checkBoxRenderMode.Name = "checkBoxRenderMode";
            this.checkBoxRenderMode.Size = new System.Drawing.Size(53, 17);
            this.checkBoxRenderMode.TabIndex = 3;
            this.checkBoxRenderMode.Text = "Mode";
            this.checkBoxRenderMode.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(9, 157);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(383, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Add Test Collision Line";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnPathRefresh
            // 
            this.btnPathRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPathRefresh.Location = new System.Drawing.Point(9, 183);
            this.btnPathRefresh.Name = "btnPathRefresh";
            this.btnPathRefresh.Size = new System.Drawing.Size(383, 23);
            this.btnPathRefresh.TabIndex = 1;
            this.btnPathRefresh.Text = "Refresh";
            this.btnPathRefresh.UseVisualStyleBackColor = true;
            this.btnPathRefresh.Click += new System.EventHandler(this.btnPathRefresh_Click);
            // 
            // panelCurrentScreen
            // 
            this.panelCurrentScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCurrentScreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCurrentScreen.Location = new System.Drawing.Point(9, 238);
            this.panelCurrentScreen.Name = "panelCurrentScreen";
            this.panelCurrentScreen.Size = new System.Drawing.Size(383, 238);
            this.panelCurrentScreen.TabIndex = 2;
            this.panelCurrentScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCurrentScreen_Paint);
            // 
            // listBoxPath
            // 
            this.listBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPath.FormattingEnabled = true;
            this.listBoxPath.Location = new System.Drawing.Point(9, 4);
            this.listBoxPath.Name = "listBoxPath";
            this.listBoxPath.Size = new System.Drawing.Size(383, 147);
            this.listBoxPath.TabIndex = 0;
            this.listBoxPath.DoubleClick += new System.EventHandler(this.listBoxPath_DoubleClick);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.button3);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(400, 484);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Dev";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator4,
            this.toolStripButtonRecord,
            this.toolStripSeparator5,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(408, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Pause Game";
            this.toolStripButton1.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Step Frame";
            this.toolStripButton2.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRecord
            // 
            this.toolStripButtonRecord.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRecord.Image")));
            this.toolStripButtonRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRecord.Name = "toolStripButtonRecord";
            this.toolStripButtonRecord.Size = new System.Drawing.Size(64, 22);
            this.toolStripButtonRecord.Text = "Record";
            this.toolStripButtonRecord.ToolTipText = "Record";
            this.toolStripButtonRecord.Click += new System.EventHandler(this.toolStripButtonRecord_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "LAZORS";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 535);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DebugWindow";
            this.Text = "Alive Toolkit";
            this.Load += new System.EventHandler(this.DebugWindow_Load);
            this.contextMenuObject.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuObject;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listMemoryAllocations;
        private System.Windows.Forms.Button buttonUpdateMemory;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.TextBox textBoxMemoryLog;
        private System.Windows.Forms.ToolStripMenuItem copyVTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteVTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private LineGraph lineGraphMemory;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ToolStripMenuItem copyAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button buttonSpawnObject;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ToolStripMenuItem mindControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnPathRefresh;
        private System.Windows.Forms.ListBox listBoxPath;
        private DoubleBufferPanel panelCurrentScreen;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.CheckBox checkBoxRenderMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonRecord;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.Button button3;
    }
}