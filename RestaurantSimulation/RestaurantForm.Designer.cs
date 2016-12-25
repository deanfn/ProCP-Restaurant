﻿namespace RestaurantSimulation
{
    partial class RestaurantForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nudCustomerFlow = new System.Windows.Forms.NumericUpDown();
            this.nudDinnerDuration = new System.Windows.Forms.NumericUpDown();
            this.nudLunchDuration = new System.Windows.Forms.NumericUpDown();
            this.lblDinnerDuration = new System.Windows.Forms.Label();
            this.lblLunchDuration = new System.Windows.Forms.Label();
            this.cbPeakHour = new System.Windows.Forms.CheckBox();
            this.tbRestaurantStaff = new System.Windows.Forms.TextBox();
            this.PeakHourOptionlbl = new System.Windows.Forms.Label();
            this.lblPeakHours = new System.Windows.Forms.Label();
            this.PeakHourlbl = new System.Windows.Forms.Label();
            this.RestaurantStafflbl = new System.Windows.Forms.Label();
            this.CustomerFlowlbl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnShowSATables = new System.Windows.Forms.Button();
            this.btnShowGATables = new System.Windows.Forms.Button();
            this.btnUnmerge = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.rbSize4 = new System.Windows.Forms.RadioButton();
            this.rbSize2 = new System.Windows.Forms.RadioButton();
            this.TableSizelbl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBar = new System.Windows.Forms.Button();
            this.btnGroupA = new System.Windows.Forms.Button();
            this.btnSmokingA = new System.Windows.Forms.Button();
            this.btnWaitingA = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.restaurantPlan = new System.Windows.Forms.PictureBox();
            this.gbSimulationOverview = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblServedCustomers = new System.Windows.Forms.Label();
            this.lblRunTime = new System.Windows.Forms.Label();
            this.gbStartStopPause = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCustomerFlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDinnerDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLunchDuration)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.restaurantPlan)).BeginInit();
            this.gbSimulationOverview.SuspendLayout();
            this.gbStartStopPause.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nudCustomerFlow);
            this.groupBox3.Controls.Add(this.nudDinnerDuration);
            this.groupBox3.Controls.Add(this.nudLunchDuration);
            this.groupBox3.Controls.Add(this.lblDinnerDuration);
            this.groupBox3.Controls.Add(this.lblLunchDuration);
            this.groupBox3.Controls.Add(this.cbPeakHour);
            this.groupBox3.Controls.Add(this.tbRestaurantStaff);
            this.groupBox3.Controls.Add(this.PeakHourOptionlbl);
            this.groupBox3.Controls.Add(this.lblPeakHours);
            this.groupBox3.Controls.Add(this.PeakHourlbl);
            this.groupBox3.Controls.Add(this.RestaurantStafflbl);
            this.groupBox3.Controls.Add(this.CustomerFlowlbl);
            this.groupBox3.Location = new System.Drawing.Point(7, 312);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(273, 173);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Restaurant properties";
            // 
            // nudCustomerFlow
            // 
            this.nudCustomerFlow.Location = new System.Drawing.Point(91, 27);
            this.nudCustomerFlow.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudCustomerFlow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCustomerFlow.Name = "nudCustomerFlow";
            this.nudCustomerFlow.Size = new System.Drawing.Size(94, 20);
            this.nudCustomerFlow.TabIndex = 11;
            this.nudCustomerFlow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudDinnerDuration
            // 
            this.nudDinnerDuration.Location = new System.Drawing.Point(216, 109);
            this.nudDinnerDuration.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudDinnerDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDinnerDuration.Name = "nudDinnerDuration";
            this.nudDinnerDuration.Size = new System.Drawing.Size(51, 20);
            this.nudDinnerDuration.TabIndex = 10;
            this.nudDinnerDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudLunchDuration
            // 
            this.nudLunchDuration.Location = new System.Drawing.Point(216, 82);
            this.nudLunchDuration.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudLunchDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLunchDuration.Name = "nudLunchDuration";
            this.nudLunchDuration.Size = new System.Drawing.Size(51, 20);
            this.nudLunchDuration.TabIndex = 9;
            this.nudLunchDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDinnerDuration
            // 
            this.lblDinnerDuration.AutoSize = true;
            this.lblDinnerDuration.Location = new System.Drawing.Point(127, 111);
            this.lblDinnerDuration.Name = "lblDinnerDuration";
            this.lblDinnerDuration.Size = new System.Drawing.Size(84, 13);
            this.lblDinnerDuration.TabIndex = 8;
            this.lblDinnerDuration.Text = "Dinner Duration:";
            // 
            // lblLunchDuration
            // 
            this.lblLunchDuration.AutoSize = true;
            this.lblLunchDuration.Location = new System.Drawing.Point(127, 85);
            this.lblLunchDuration.Name = "lblLunchDuration";
            this.lblLunchDuration.Size = new System.Drawing.Size(83, 13);
            this.lblLunchDuration.TabIndex = 7;
            this.lblLunchDuration.Text = "Lunch Duration:";
            // 
            // cbPeakHour
            // 
            this.cbPeakHour.AutoSize = true;
            this.cbPeakHour.Location = new System.Drawing.Point(100, 84);
            this.cbPeakHour.Name = "cbPeakHour";
            this.cbPeakHour.Size = new System.Drawing.Size(15, 14);
            this.cbPeakHour.TabIndex = 5;
            this.cbPeakHour.UseVisualStyleBackColor = true;
            // 
            // tbRestaurantStaff
            // 
            this.tbRestaurantStaff.Location = new System.Drawing.Point(91, 55);
            this.tbRestaurantStaff.Name = "tbRestaurantStaff";
            this.tbRestaurantStaff.Size = new System.Drawing.Size(94, 20);
            this.tbRestaurantStaff.TabIndex = 4;
            // 
            // PeakHourOptionlbl
            // 
            this.PeakHourOptionlbl.AutoSize = true;
            this.PeakHourOptionlbl.Location = new System.Drawing.Point(97, 111);
            this.PeakHourOptionlbl.Name = "PeakHourOptionlbl";
            this.PeakHourOptionlbl.Size = new System.Drawing.Size(19, 13);
            this.PeakHourOptionlbl.TabIndex = 2;
            this.PeakHourOptionlbl.Text = "off";
            // 
            // lblPeakHours
            // 
            this.lblPeakHours.AutoSize = true;
            this.lblPeakHours.Location = new System.Drawing.Point(7, 111);
            this.lblPeakHours.Name = "lblPeakHours";
            this.lblPeakHours.Size = new System.Drawing.Size(94, 13);
            this.lblPeakHours.TabIndex = 2;
            this.lblPeakHours.Text = "Peak hours         : ";
            // 
            // PeakHourlbl
            // 
            this.PeakHourlbl.AutoSize = true;
            this.PeakHourlbl.Location = new System.Drawing.Point(7, 85);
            this.PeakHourlbl.Name = "PeakHourlbl";
            this.PeakHourlbl.Size = new System.Drawing.Size(94, 13);
            this.PeakHourlbl.TabIndex = 2;
            this.PeakHourlbl.Text = "Peak hour option: ";
            // 
            // RestaurantStafflbl
            // 
            this.RestaurantStafflbl.AutoSize = true;
            this.RestaurantStafflbl.Location = new System.Drawing.Point(7, 58);
            this.RestaurantStafflbl.Name = "RestaurantStafflbl";
            this.RestaurantStafflbl.Size = new System.Drawing.Size(85, 13);
            this.RestaurantStafflbl.TabIndex = 1;
            this.RestaurantStafflbl.Text = "Restaurant staff:";
            // 
            // CustomerFlowlbl
            // 
            this.CustomerFlowlbl.AutoSize = true;
            this.CustomerFlowlbl.Location = new System.Drawing.Point(7, 29);
            this.CustomerFlowlbl.Name = "CustomerFlowlbl";
            this.CustomerFlowlbl.Size = new System.Drawing.Size(82, 13);
            this.CustomerFlowlbl.TabIndex = 0;
            this.CustomerFlowlbl.Text = "Customer flow:  ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnShowSATables);
            this.groupBox2.Controls.Add(this.btnShowGATables);
            this.groupBox2.Controls.Add(this.btnUnmerge);
            this.groupBox2.Controls.Add(this.btnMerge);
            this.groupBox2.Controls.Add(this.btnConfirm);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Location = new System.Drawing.Point(7, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(189, 133);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Component properties";
            // 
            // btnShowSATables
            // 
            this.btnShowSATables.Location = new System.Drawing.Point(9, 87);
            this.btnShowSATables.Name = "btnShowSATables";
            this.btnShowSATables.Size = new System.Drawing.Size(81, 40);
            this.btnShowSATables.TabIndex = 8;
            this.btnShowSATables.Text = "Show Smoke Area Tables";
            this.btnShowSATables.UseVisualStyleBackColor = true;
            this.btnShowSATables.Click += new System.EventHandler(this.btnShowSATables_Click);
            // 
            // btnShowGATables
            // 
            this.btnShowGATables.Location = new System.Drawing.Point(101, 87);
            this.btnShowGATables.Name = "btnShowGATables";
            this.btnShowGATables.Size = new System.Drawing.Size(81, 40);
            this.btnShowGATables.TabIndex = 7;
            this.btnShowGATables.Text = "Show Group Area Tables";
            this.btnShowGATables.UseVisualStyleBackColor = true;
            this.btnShowGATables.Click += new System.EventHandler(this.btnShowGATables_Click);
            // 
            // btnUnmerge
            // 
            this.btnUnmerge.Location = new System.Drawing.Point(101, 58);
            this.btnUnmerge.Name = "btnUnmerge";
            this.btnUnmerge.Size = new System.Drawing.Size(81, 23);
            this.btnUnmerge.TabIndex = 6;
            this.btnUnmerge.Text = "Un-merge";
            this.btnUnmerge.UseVisualStyleBackColor = true;
            this.btnUnmerge.Click += new System.EventHandler(this.btnUnmerge_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(9, 58);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(81, 23);
            this.btnMerge.TabIndex = 5;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(9, 29);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(81, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(101, 29);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(81, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // rbSize4
            // 
            this.rbSize4.AutoSize = true;
            this.rbSize4.Location = new System.Drawing.Point(56, 55);
            this.rbSize4.Name = "rbSize4";
            this.rbSize4.Size = new System.Drawing.Size(31, 17);
            this.rbSize4.TabIndex = 2;
            this.rbSize4.TabStop = true;
            this.rbSize4.Text = "4";
            this.rbSize4.UseVisualStyleBackColor = true;
            // 
            // rbSize2
            // 
            this.rbSize2.AutoSize = true;
            this.rbSize2.Location = new System.Drawing.Point(9, 55);
            this.rbSize2.Name = "rbSize2";
            this.rbSize2.Size = new System.Drawing.Size(31, 17);
            this.rbSize2.TabIndex = 1;
            this.rbSize2.TabStop = true;
            this.rbSize2.Text = "2";
            this.rbSize2.UseVisualStyleBackColor = true;
            // 
            // TableSizelbl
            // 
            this.TableSizelbl.AutoSize = true;
            this.TableSizelbl.Location = new System.Drawing.Point(6, 39);
            this.TableSizelbl.Name = "TableSizelbl";
            this.TableSizelbl.Size = new System.Drawing.Size(61, 13);
            this.TableSizelbl.TabIndex = 0;
            this.TableSizelbl.Text = "Table size: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBar);
            this.groupBox1.Controls.Add(this.btnGroupA);
            this.groupBox1.Controls.Add(this.btnSmokingA);
            this.groupBox1.Controls.Add(this.btnWaitingA);
            this.groupBox1.Controls.Add(this.rbSize4);
            this.groupBox1.Controls.Add(this.btnTable);
            this.groupBox1.Controls.Add(this.rbSize2);
            this.groupBox1.Controls.Add(this.TableSizelbl);
            this.groupBox1.Location = new System.Drawing.Point(10, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 165);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Component menu";
            // 
            // btnBar
            // 
            this.btnBar.Location = new System.Drawing.Point(8, 113);
            this.btnBar.Name = "btnBar";
            this.btnBar.Size = new System.Drawing.Size(81, 23);
            this.btnBar.TabIndex = 0;
            this.btnBar.Text = "Bar";
            this.btnBar.UseVisualStyleBackColor = true;
            this.btnBar.Click += new System.EventHandler(this.btnBar_Click);
            // 
            // btnGroupA
            // 
            this.btnGroupA.Location = new System.Drawing.Point(101, 113);
            this.btnGroupA.Name = "btnGroupA";
            this.btnGroupA.Size = new System.Drawing.Size(81, 23);
            this.btnGroupA.TabIndex = 0;
            this.btnGroupA.Text = "Group area";
            this.btnGroupA.UseVisualStyleBackColor = true;
            this.btnGroupA.Click += new System.EventHandler(this.btnGroupA_Click);
            // 
            // btnSmokingA
            // 
            this.btnSmokingA.Location = new System.Drawing.Point(101, 84);
            this.btnSmokingA.Name = "btnSmokingA";
            this.btnSmokingA.Size = new System.Drawing.Size(81, 23);
            this.btnSmokingA.TabIndex = 0;
            this.btnSmokingA.Text = "Smoking area";
            this.btnSmokingA.UseVisualStyleBackColor = true;
            this.btnSmokingA.Click += new System.EventHandler(this.btnSmokingA_Click);
            // 
            // btnWaitingA
            // 
            this.btnWaitingA.Location = new System.Drawing.Point(101, 55);
            this.btnWaitingA.Name = "btnWaitingA";
            this.btnWaitingA.Size = new System.Drawing.Size(81, 23);
            this.btnWaitingA.TabIndex = 0;
            this.btnWaitingA.Text = "Waiting area";
            this.btnWaitingA.UseVisualStyleBackColor = true;
            this.btnWaitingA.Click += new System.EventHandler(this.btnWaitingA_Click);
            // 
            // btnTable
            // 
            this.btnTable.Location = new System.Drawing.Point(8, 84);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(81, 23);
            this.btnTable.TabIndex = 0;
            this.btnTable.Text = "Table";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // restaurantPlan
            // 
            this.restaurantPlan.Location = new System.Drawing.Point(470, 12);
            this.restaurantPlan.Name = "restaurantPlan";
            this.restaurantPlan.Size = new System.Drawing.Size(680, 483);
            this.restaurantPlan.TabIndex = 6;
            this.restaurantPlan.TabStop = false;
            this.restaurantPlan.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.restaurantPlan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RestaurantPlan_MouseClick);
            // 
            // gbSimulationOverview
            // 
            this.gbSimulationOverview.Controls.Add(this.label10);
            this.gbSimulationOverview.Controls.Add(this.label9);
            this.gbSimulationOverview.Controls.Add(this.label8);
            this.gbSimulationOverview.Controls.Add(this.label7);
            this.gbSimulationOverview.Controls.Add(this.label6);
            this.gbSimulationOverview.Controls.Add(this.label5);
            this.gbSimulationOverview.Controls.Add(this.label4);
            this.gbSimulationOverview.Controls.Add(this.label3);
            this.gbSimulationOverview.Controls.Add(this.lblServedCustomers);
            this.gbSimulationOverview.Controls.Add(this.lblRunTime);
            this.gbSimulationOverview.Location = new System.Drawing.Point(205, 2);
            this.gbSimulationOverview.Name = "gbSimulationOverview";
            this.gbSimulationOverview.Size = new System.Drawing.Size(259, 304);
            this.gbSimulationOverview.TabIndex = 7;
            this.gbSimulationOverview.TabStop = false;
            this.gbSimulationOverview.Text = "Simulation Overview";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(138, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "label10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(138, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "label9";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(138, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(138, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(138, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Customers Send Away:";
            // 
            // lblServedCustomers
            // 
            this.lblServedCustomers.AutoSize = true;
            this.lblServedCustomers.Location = new System.Drawing.Point(15, 54);
            this.lblServedCustomers.Name = "lblServedCustomers";
            this.lblServedCustomers.Size = new System.Drawing.Size(96, 13);
            this.lblServedCustomers.TabIndex = 1;
            this.lblServedCustomers.Text = "Served Customers:";
            // 
            // lblRunTime
            // 
            this.lblRunTime.AutoSize = true;
            this.lblRunTime.Location = new System.Drawing.Point(15, 27);
            this.lblRunTime.Name = "lblRunTime";
            this.lblRunTime.Size = new System.Drawing.Size(56, 13);
            this.lblRunTime.TabIndex = 0;
            this.lblRunTime.Text = "Run Time:";
            // 
            // gbStartStopPause
            // 
            this.gbStartStopPause.Controls.Add(this.btnStop);
            this.gbStartStopPause.Controls.Add(this.btnPause);
            this.gbStartStopPause.Controls.Add(this.btnStart);
            this.gbStartStopPause.Location = new System.Drawing.Point(286, 312);
            this.gbStartStopPause.Name = "gbStartStopPause";
            this.gbStartStopPause.Size = new System.Drawing.Size(178, 173);
            this.gbStartStopPause.TabIndex = 8;
            this.gbStartStopPause.TabStop = false;
            this.gbStartStopPause.Text = "Control Simulation";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(43, 99);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 30);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(43, 63);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(80, 30);
            this.btnPause.TabIndex = 7;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(43, 27);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(80, 30);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click_1);
            // 
            // RestaurantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 494);
            this.Controls.Add(this.gbStartStopPause);
            this.Controls.Add(this.gbSimulationOverview);
            this.Controls.Add(this.restaurantPlan);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "RestaurantForm";
            this.Text = "Restaurant simulator";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCustomerFlow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDinnerDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLunchDuration)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.restaurantPlan)).EndInit();
            this.gbSimulationOverview.ResumeLayout(false);
            this.gbSimulationOverview.PerformLayout();
            this.gbStartStopPause.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbPeakHour;
        private System.Windows.Forms.TextBox tbRestaurantStaff;
        private System.Windows.Forms.Label PeakHourOptionlbl;
        private System.Windows.Forms.Label lblPeakHours;
        private System.Windows.Forms.Label PeakHourlbl;
        private System.Windows.Forms.Label RestaurantStafflbl;
        private System.Windows.Forms.Label CustomerFlowlbl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUnmerge;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.RadioButton rbSize4;
        private System.Windows.Forms.RadioButton rbSize2;
        private System.Windows.Forms.Label TableSizelbl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBar;
        private System.Windows.Forms.Button btnGroupA;
        private System.Windows.Forms.Button btnSmokingA;
        private System.Windows.Forms.Button btnWaitingA;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.PictureBox restaurantPlan;
        private System.Windows.Forms.Button btnShowGATables;
        private System.Windows.Forms.Button btnShowSATables;
        private System.Windows.Forms.NumericUpDown nudDinnerDuration;
        private System.Windows.Forms.NumericUpDown nudLunchDuration;
        private System.Windows.Forms.Label lblDinnerDuration;
        private System.Windows.Forms.Label lblLunchDuration;
        private System.Windows.Forms.NumericUpDown nudCustomerFlow;
        private System.Windows.Forms.GroupBox gbSimulationOverview;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblServedCustomers;
        private System.Windows.Forms.Label lblRunTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbStartStopPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStart;
    }
}

