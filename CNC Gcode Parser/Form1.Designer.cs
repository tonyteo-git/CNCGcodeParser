namespace CNC_Gcode_Parser
{
    partial class Form1
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
            System.Windows.Forms.Panel panel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.nStopBtn = new System.Windows.Forms.Button();
            this.nRunBtn = new System.Windows.Forms.Button();
            this.speedLB = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.SpindleStt = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.PLC = new AxActProgTypeLib.AxActProgType();
            this.axActSupportMsg1 = new AxActSupportMsgLib.AxActSupportMsg();
            this.LbStatus = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Jog_speedA = new System.Windows.Forms.NumericUpDown();
            this.ForwardA_button = new System.Windows.Forms.Button();
            this.BackwardA_button = new System.Windows.Forms.Button();
            this.Jog_speedZ = new System.Windows.Forms.NumericUpDown();
            this.Jog_speedY = new System.Windows.Forms.NumericUpDown();
            this.Jog_speedX = new System.Windows.Forms.NumericUpDown();
            this.ForwardZ_button = new System.Windows.Forms.Button();
            this.BackwardZ_button = new System.Windows.Forms.Button();
            this.ForwardY_button = new System.Windows.Forms.Button();
            this.BackwardY_button = new System.Windows.Forms.Button();
            this.ForwardX_button = new System.Windows.Forms.Button();
            this.BackwardX_button = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GCodeBox = new System.Windows.Forms.TextBox();
            this.checkA = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.COM = new System.Windows.Forms.ComboBox();
            this.Open_button = new System.Windows.Forms.Button();
            this.Execute_button = new System.Windows.Forms.Button();
            this.Connect_button = new System.Windows.Forms.Button();
            this.Load_button = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Browse_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Initialize_button = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PLC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axActSupportMsg1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Jog_speedA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jog_speedZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jog_speedY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jog_speedX)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Controls.Add(this.label23);
            panel1.Controls.Add(this.label22);
            panel1.Controls.Add(this.label21);
            panel1.Controls.Add(this.label20);
            panel1.Controls.Add(this.label18);
            panel1.Controls.Add(this.label16);
            panel1.Controls.Add(this.label17);
            panel1.Controls.Add(this.label15);
            panel1.Controls.Add(this.label14);
            panel1.Controls.Add(this.nStopBtn);
            panel1.Controls.Add(this.nRunBtn);
            panel1.Controls.Add(this.speedLB);
            panel1.Controls.Add(this.trackBar1);
            panel1.Controls.Add(this.SpindleStt);
            panel1.Controls.Add(this.label19);
            panel1.Location = new System.Drawing.Point(823, 44);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(395, 443);
            panel1.TabIndex = 33;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(15, 59);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(109, 20);
            this.label23.TabIndex = 40;
            this.label23.Text = "Speed: (RPM)";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label22.Location = new System.Drawing.Point(335, 128);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(40, 13);
            this.label22.TabIndex = 39;
            this.label22.Text = "22 000";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label21.Location = new System.Drawing.Point(289, 128);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(40, 13);
            this.label21.TabIndex = 39;
            this.label21.Text = "20 000";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label20.Location = new System.Drawing.Point(243, 128);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(40, 13);
            this.label20.TabIndex = 38;
            this.label20.Text = "18 000";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label18.Location = new System.Drawing.Point(197, 128);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 13);
            this.label18.TabIndex = 38;
            this.label18.Text = "16 000";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label16.Location = new System.Drawing.Point(105, 128);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 13);
            this.label16.TabIndex = 38;
            this.label16.Text = "12 000";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label17.Location = new System.Drawing.Point(151, 128);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 13);
            this.label17.TabIndex = 37;
            this.label17.Text = "14 000";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label15.Location = new System.Drawing.Point(59, 128);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "10 000";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label14.Location = new System.Drawing.Point(27, 128);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(13, 13);
            this.label14.TabIndex = 37;
            this.label14.Text = "0";
            // 
            // nStopBtn
            // 
            this.nStopBtn.BackColor = System.Drawing.Color.Red;
            this.nStopBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nStopBtn.Location = new System.Drawing.Point(222, 349);
            this.nStopBtn.Name = "nStopBtn";
            this.nStopBtn.Size = new System.Drawing.Size(113, 59);
            this.nStopBtn.TabIndex = 36;
            this.nStopBtn.Text = "Stop";
            this.nStopBtn.UseVisualStyleBackColor = false;
            this.nStopBtn.Click += new System.EventHandler(this.nStopBtn_Click);
            // 
            // nRunBtn
            // 
            this.nRunBtn.BackColor = System.Drawing.Color.Lime;
            this.nRunBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nRunBtn.Location = new System.Drawing.Point(47, 349);
            this.nRunBtn.Name = "nRunBtn";
            this.nRunBtn.Size = new System.Drawing.Size(114, 59);
            this.nRunBtn.TabIndex = 35;
            this.nRunBtn.Text = "Run";
            this.nRunBtn.UseVisualStyleBackColor = false;
            this.nRunBtn.Click += new System.EventHandler(this.nRunBtn_Click);
            // 
            // speedLB
            // 
            this.speedLB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.speedLB.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLB.ForeColor = System.Drawing.Color.Red;
            this.speedLB.Location = new System.Drawing.Point(124, 190);
            this.speedLB.Name = "speedLB";
            this.speedLB.Size = new System.Drawing.Size(205, 55);
            this.speedLB.TabIndex = 34;
            this.speedLB.Text = "Spindle Speed";
            this.speedLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.trackBar1.LargeChange = 2000;
            this.trackBar1.Location = new System.Drawing.Point(19, 96);
            this.trackBar1.Maximum = 22000;
            this.trackBar1.Minimum = 8000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(356, 55);
            this.trackBar1.SmallChange = 2000;
            this.trackBar1.TabIndex = 7;
            this.trackBar1.TickFrequency = 2000;
            this.trackBar1.Value = 8000;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // SpindleStt
            // 
            this.SpindleStt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SpindleStt.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpindleStt.Location = new System.Drawing.Point(124, 270);
            this.SpindleStt.Name = "SpindleStt";
            this.SpindleStt.Size = new System.Drawing.Size(205, 52);
            this.SpindleStt.TabIndex = 32;
            this.SpindleStt.Text = "Spindle Status";
            this.SpindleStt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label19.Location = new System.Drawing.Point(134, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(160, 37);
            this.label19.TabIndex = 26;
            this.label19.Text = "SPINDLE";
            // 
            // PLC
            // 
            this.PLC.Enabled = true;
            this.PLC.Location = new System.Drawing.Point(39, 417);
            this.PLC.Name = "PLC";
            this.PLC.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("PLC.OcxState")));
            this.PLC.Size = new System.Drawing.Size(32, 32);
            this.PLC.TabIndex = 0;
            // 
            // axActSupportMsg1
            // 
            this.axActSupportMsg1.Enabled = true;
            this.axActSupportMsg1.Location = new System.Drawing.Point(1, 417);
            this.axActSupportMsg1.Name = "axActSupportMsg1";
            this.axActSupportMsg1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axActSupportMsg1.OcxState")));
            this.axActSupportMsg1.Size = new System.Drawing.Size(32, 32);
            this.axActSupportMsg1.TabIndex = 1;
            // 
            // LbStatus
            // 
            this.LbStatus.AutoSize = true;
            this.LbStatus.Location = new System.Drawing.Point(14, 536);
            this.LbStatus.Name = "LbStatus";
            this.LbStatus.Size = new System.Drawing.Size(38, 13);
            this.LbStatus.TabIndex = 14;
            this.LbStatus.Text = "Ready";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.Jog_speedA);
            this.tabPage2.Controls.Add(this.ForwardA_button);
            this.tabPage2.Controls.Add(this.BackwardA_button);
            this.tabPage2.Controls.Add(this.Jog_speedZ);
            this.tabPage2.Controls.Add(this.Jog_speedY);
            this.tabPage2.Controls.Add(this.Jog_speedX);
            this.tabPage2.Controls.Add(this.ForwardZ_button);
            this.tabPage2.Controls.Add(this.BackwardZ_button);
            this.tabPage2.Controls.Add(this.ForwardY_button);
            this.tabPage2.Controls.Add(this.BackwardY_button);
            this.tabPage2.Controls.Add(this.ForwardX_button);
            this.tabPage2.Controls.Add(this.BackwardX_button);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1266, 568);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "JOG";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(328, 251);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "mm/min";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(328, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "mm/min";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(328, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "mm/min";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(328, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "mm/min";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(185, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Speed A-axis";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Speed Z-axis";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Speed Y-axis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Speed X-axis";
            // 
            // Jog_speedA
            // 
            this.Jog_speedA.Location = new System.Drawing.Point(260, 246);
            this.Jog_speedA.Name = "Jog_speedA";
            this.Jog_speedA.Size = new System.Drawing.Size(62, 20);
            this.Jog_speedA.TabIndex = 11;
            // 
            // ForwardA_button
            // 
            this.ForwardA_button.Location = new System.Drawing.Point(485, 246);
            this.ForwardA_button.Name = "ForwardA_button";
            this.ForwardA_button.Size = new System.Drawing.Size(75, 23);
            this.ForwardA_button.TabIndex = 10;
            this.ForwardA_button.Text = "Forward";
            this.ForwardA_button.UseVisualStyleBackColor = true;
            // 
            // BackwardA_button
            // 
            this.BackwardA_button.Location = new System.Drawing.Point(386, 246);
            this.BackwardA_button.Name = "BackwardA_button";
            this.BackwardA_button.Size = new System.Drawing.Size(75, 23);
            this.BackwardA_button.TabIndex = 9;
            this.BackwardA_button.Text = "Backward";
            this.BackwardA_button.UseVisualStyleBackColor = true;
            // 
            // Jog_speedZ
            // 
            this.Jog_speedZ.Location = new System.Drawing.Point(260, 197);
            this.Jog_speedZ.Name = "Jog_speedZ";
            this.Jog_speedZ.Size = new System.Drawing.Size(62, 20);
            this.Jog_speedZ.TabIndex = 8;
            // 
            // Jog_speedY
            // 
            this.Jog_speedY.Location = new System.Drawing.Point(260, 147);
            this.Jog_speedY.Name = "Jog_speedY";
            this.Jog_speedY.Size = new System.Drawing.Size(62, 20);
            this.Jog_speedY.TabIndex = 7;
            // 
            // Jog_speedX
            // 
            this.Jog_speedX.Location = new System.Drawing.Point(260, 99);
            this.Jog_speedX.Name = "Jog_speedX";
            this.Jog_speedX.Size = new System.Drawing.Size(62, 20);
            this.Jog_speedX.TabIndex = 6;
            // 
            // ForwardZ_button
            // 
            this.ForwardZ_button.Location = new System.Drawing.Point(485, 197);
            this.ForwardZ_button.Name = "ForwardZ_button";
            this.ForwardZ_button.Size = new System.Drawing.Size(75, 23);
            this.ForwardZ_button.TabIndex = 5;
            this.ForwardZ_button.Text = "Forward";
            this.ForwardZ_button.UseVisualStyleBackColor = true;
            // 
            // BackwardZ_button
            // 
            this.BackwardZ_button.Location = new System.Drawing.Point(386, 197);
            this.BackwardZ_button.Name = "BackwardZ_button";
            this.BackwardZ_button.Size = new System.Drawing.Size(75, 23);
            this.BackwardZ_button.TabIndex = 4;
            this.BackwardZ_button.Text = "Backward";
            this.BackwardZ_button.UseVisualStyleBackColor = true;
            // 
            // ForwardY_button
            // 
            this.ForwardY_button.Location = new System.Drawing.Point(485, 147);
            this.ForwardY_button.Name = "ForwardY_button";
            this.ForwardY_button.Size = new System.Drawing.Size(75, 23);
            this.ForwardY_button.TabIndex = 3;
            this.ForwardY_button.Text = "Forward";
            this.ForwardY_button.UseVisualStyleBackColor = true;
            // 
            // BackwardY_button
            // 
            this.BackwardY_button.Location = new System.Drawing.Point(386, 147);
            this.BackwardY_button.Name = "BackwardY_button";
            this.BackwardY_button.Size = new System.Drawing.Size(75, 23);
            this.BackwardY_button.TabIndex = 2;
            this.BackwardY_button.Text = "Backward";
            this.BackwardY_button.UseVisualStyleBackColor = true;
            // 
            // ForwardX_button
            // 
            this.ForwardX_button.Location = new System.Drawing.Point(485, 99);
            this.ForwardX_button.Name = "ForwardX_button";
            this.ForwardX_button.Size = new System.Drawing.Size(75, 23);
            this.ForwardX_button.TabIndex = 1;
            this.ForwardX_button.Text = "Forward";
            this.ForwardX_button.UseVisualStyleBackColor = true;
            // 
            // BackwardX_button
            // 
            this.BackwardX_button.Location = new System.Drawing.Point(386, 99);
            this.BackwardX_button.Name = "BackwardX_button";
            this.BackwardX_button.Size = new System.Drawing.Size(75, 23);
            this.BackwardX_button.TabIndex = 0;
            this.BackwardX_button.Text = "Backward";
            this.BackwardX_button.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(panel1);
            this.tabPage1.Controls.Add(this.LbStatus);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.GCodeBox);
            this.tabPage1.Controls.Add(this.checkA);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.COM);
            this.tabPage1.Controls.Add(this.Open_button);
            this.tabPage1.Controls.Add(this.Execute_button);
            this.tabPage1.Controls.Add(this.Connect_button);
            this.tabPage1.Controls.Add(this.Load_button);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.Browse_button);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.Initialize_button);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1266, 568);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Positioning";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBox1.Location = new System.Drawing.Point(298, 101);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(482, 385);
            this.textBox1.TabIndex = 12;
            // 
            // GCodeBox
            // 
            this.GCodeBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GCodeBox.Location = new System.Drawing.Point(7, 101);
            this.GCodeBox.Multiline = true;
            this.GCodeBox.Name = "GCodeBox";
            this.GCodeBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GCodeBox.Size = new System.Drawing.Size(285, 385);
            this.GCodeBox.TabIndex = 2;
            // 
            // checkA
            // 
            this.checkA.AutoSize = true;
            this.checkA.Location = new System.Drawing.Point(358, 19);
            this.checkA.Name = "checkA";
            this.checkA.Size = new System.Drawing.Size(63, 17);
            this.checkA.TabIndex = 20;
            this.checkA.Text = "4th Axis";
            this.checkA.UseVisualStyleBackColor = true;
            this.checkA.CheckedChanged += new System.EventHandler(this.checkA_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(696, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Speed (mm/min)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(606, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Position (mm)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(295, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "G-code manager:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "G-code:";
            // 
            // COM
            // 
            this.COM.FormattingEnabled = true;
            this.COM.Location = new System.Drawing.Point(6, 17);
            this.COM.Name = "COM";
            this.COM.Size = new System.Drawing.Size(69, 21);
            this.COM.TabIndex = 3;
            this.COM.SelectedIndexChanged += new System.EventHandler(this.COM_SelectedIndexChanged);
            // 
            // Open_button
            // 
            this.Open_button.Location = new System.Drawing.Point(696, 42);
            this.Open_button.Name = "Open_button";
            this.Open_button.Size = new System.Drawing.Size(84, 36);
            this.Open_button.TabIndex = 13;
            this.Open_button.Text = "Open";
            this.Open_button.UseVisualStyleBackColor = true;
            this.Open_button.Click += new System.EventHandler(this.Open_button_Click);
            // 
            // Execute_button
            // 
            this.Execute_button.Location = new System.Drawing.Point(686, 502);
            this.Execute_button.Name = "Execute_button";
            this.Execute_button.Size = new System.Drawing.Size(94, 47);
            this.Execute_button.TabIndex = 11;
            this.Execute_button.Text = "Execute";
            this.Execute_button.UseVisualStyleBackColor = true;
            this.Execute_button.Click += new System.EventHandler(this.Execute_button_Click);
            // 
            // Connect_button
            // 
            this.Connect_button.Location = new System.Drawing.Point(96, 10);
            this.Connect_button.Name = "Connect_button";
            this.Connect_button.Size = new System.Drawing.Size(75, 32);
            this.Connect_button.TabIndex = 4;
            this.Connect_button.Text = "Connect";
            this.Connect_button.UseVisualStyleBackColor = true;
            this.Connect_button.Click += new System.EventHandler(this.Connect_button_Click);
            // 
            // Load_button
            // 
            this.Load_button.Location = new System.Drawing.Point(209, 502);
            this.Load_button.Name = "Load_button";
            this.Load_button.Size = new System.Drawing.Size(83, 47);
            this.Load_button.TabIndex = 7;
            this.Load_button.Text = "Convert";
            this.Load_button.UseVisualStyleBackColor = true;
            this.Load_button.Click += new System.EventHandler(this.Load_button_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(81, 53);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(446, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // Browse_button
            // 
            this.Browse_button.Location = new System.Drawing.Point(582, 44);
            this.Browse_button.Name = "Browse_button";
            this.Browse_button.Size = new System.Drawing.Size(82, 32);
            this.Browse_button.TabIndex = 6;
            this.Browse_button.Text = "Browse";
            this.Browse_button.UseVisualStyleBackColor = true;
            this.Browse_button.Click += new System.EventHandler(this.Browse_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "File Location:";
            // 
            // Initialize_button
            // 
            this.Initialize_button.Location = new System.Drawing.Point(177, 10);
            this.Initialize_button.Name = "Initialize_button";
            this.Initialize_button.Size = new System.Drawing.Size(87, 32);
            this.Initialize_button.TabIndex = 8;
            this.Initialize_button.Text = "Machine Home";
            this.Initialize_button.UseVisualStyleBackColor = true;
            this.Initialize_button.Click += new System.EventHandler(this.Initialize_button_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1274, 594);
            this.tabControl1.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 601);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.axActSupportMsg1);
            this.Controls.Add(this.PLC);
            this.Name = "Form1";
            this.Text = "4 Axis Wood Carving Machine";
            this.Load += new System.EventHandler(this.Form1_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PLC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axActSupportMsg1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Jog_speedA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jog_speedZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jog_speedY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jog_speedX)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxActProgTypeLib.AxActProgType PLC;
        private AxActSupportMsgLib.AxActSupportMsg axActSupportMsg1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label LbStatus;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown Jog_speedA;
        private System.Windows.Forms.Button ForwardA_button;
        private System.Windows.Forms.Button BackwardA_button;
        private System.Windows.Forms.NumericUpDown Jog_speedZ;
        private System.Windows.Forms.NumericUpDown Jog_speedY;
        private System.Windows.Forms.NumericUpDown Jog_speedX;
        private System.Windows.Forms.Button ForwardZ_button;
        private System.Windows.Forms.Button BackwardZ_button;
        private System.Windows.Forms.Button ForwardY_button;
        private System.Windows.Forms.Button BackwardY_button;
        private System.Windows.Forms.Button ForwardX_button;
        private System.Windows.Forms.Button BackwardX_button;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox GCodeBox;
        public System.Windows.Forms.CheckBox checkA;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox COM;
        private System.Windows.Forms.Button Open_button;
        private System.Windows.Forms.Button Execute_button;
        private System.Windows.Forms.Button Connect_button;
        private System.Windows.Forms.Button Load_button;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Browse_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Initialize_button;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label SpindleStt;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label speedLB;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button nStopBtn;
        private System.Windows.Forms.Button nRunBtn;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
    }
}

