﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RestaurantSimulation.Properties;

namespace RestaurantSimulation
{
    public partial class RestaurantForm : Form
    {
        RestaurantPlan newPlan;

        //Image Resource
        Image table = Resources.Table;

        //Gridbox Drawing Properties
        Graphics grid;
        Pen gridLine = new Pen(Color.White);
        int gridHeight = 12;
        int gridWidth = 17;
        int cellWidth = 40;
        int cellHeight = 40;
        int row = 0;
        int col = 0;

        //Properties for Merging Table
        int step = 1;

        // Point objects to save the coordinates of the tables to be merged.
        Point table1;
        Point table2;

        // Timer for redrawing the restaurant plan.
        Timer timer;

        // Filepath of the saved/oopened simulation.
        string filename;

        //Enum
        enum component { table, bar, groupArea, smokingArea, waitingArea, eraser, merge, unmerge };
        component? choosenComponent = null;

        public RestaurantForm()
        {
            InitializeComponent();

            table1 = new Point();
            table2 = new Point();

            newPlan = RestaurantPlan.Instance;
            //newPlan = new RestaurantPlan(this);

            ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(nudDinnerDuration, "The time in seconds for a customer group to have dinner.");
            toolTip1.SetToolTip(nudLunchDuration, "The time in seconds for a customer group to have lunch.");
            toolTip1.SetToolTip(nudLunchDuration, "The time in seconds for which a customer group will be generated.");
            toolTip1.SetToolTip(nudCustomerFlow, "The time in seconds that a group of customers will be generated.");
            toolTip1.SetToolTip(cbPeakHour, "If this is checked, customers will be generated every 0.5 seconds.");
            toolTip1.SetToolTip(rbNoon, "If checked the simulation will be during lunch time.");
            toolTip1.SetToolTip(rbEvening, "If checked the simulation will be during dinner time.");

            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;


            lblPeakHourInfo.Text = "With this mode turned on, you will generate \nnew customer group every 0.5 seconds.";
            lblCustSentAwayInfo.Text = "0";
            lblRunTimeCounter.Text = "0:00:00";
            lblServedCustomersInfo.Text = "0";

            btnNone.Enabled = false;

            // Disable the Pause and Stop buttons
            btnStop.Enabled = false;
            btnPause.Enabled = false;

            filename = null;

            rbSize2.Checked = true;
            rbEvening.Checked = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            restaurantPlan.Invalidate();
            LobbyOverview();
            lblRunTimeCounter.Text = newPlan.GetSimulationRunTime().ToString(@"hh\:mm\:ss");
            lblServedCustomersInfo.Text = newPlan.ServedCustomers.ToString();
            lblCustSentAwayInfo.Text = newPlan.CustomersSendAway.ToString();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

        }

        //Drawing Grid Method
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            grid = e.Graphics;

            int x = 0;
            int y = 0;

            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    grid.DrawRectangle(gridLine, x, y, cellWidth, cellHeight);
                    x += cellWidth;
                }
                x = 0;
                y += cellHeight;
            }

            // If there are components on the plan it is going to draw them
            if (!newPlan.Empty())
            {
                newPlan.DrawComponents(e.Graphics);
            }
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            choosenComponent = component.table;
            btnTable.Enabled = false;
            btnBar.Enabled = true;
            btnGroupA.Enabled = true;
            btnWaitingA.Enabled = true;
            btnSmokingA.Enabled = true;
            btnNone.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnBar_Click(object sender, EventArgs e)
        {
            choosenComponent = component.bar;
            btnTable.Enabled = true;
            btnBar.Enabled = false;
            btnGroupA.Enabled = true;
            btnWaitingA.Enabled = true;
            btnSmokingA.Enabled = true;
            btnNone.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void RestaurantPlan_MouseClick(object sender, MouseEventArgs e)
        {
            col = e.Location.X / 40;
            row = e.Location.Y / 40;


            // Add new component to the restaurant plan
            if (choosenComponent != null && choosenComponent != component.eraser && choosenComponent != component.merge && choosenComponent != component.unmerge)
            {
                int size;

                if (rbSize2.Checked)
                {
                    size = 2;
                }
                else
                {
                    size = 4;
                }

                if (newPlan.AddComponent(e.Location, (int)choosenComponent, size))
                {

                }
                else
                {
                    MessageBox.Show("Please, Select a Free Spot");
                }
            }

            else if (choosenComponent == component.eraser)
            {
                newPlan.RemoveComponent(newPlan.GetComponent(col, row), newPlan.GetSpecialArea(col, row));
            }

            else if (choosenComponent == component.merge)
            {
                if (step == 1 && newPlan.GetComponent(col, row) != null)
                {
                    table1.X = col;
                    table1.Y = row;
                }
                else if (step == 2 && newPlan.GetComponent(col, row) != null)
                {
                    table2.X = col;
                    table2.Y = row;
                }
                else if (step == 3 && !table1.Equals(table2))
                {
                    if (newPlan.MergeTables(newPlan.GetComponent(table1.X, table1.Y), newPlan.GetComponent(table2.X, table2.Y),
                        e.Location))
                    {
                        choosenComponent = null;
                    }
                    else
                    {
                        MessageBox.Show("Cannot merge tables!");
                        step = 1;
                    }
                }
                else
                {
                    MessageBox.Show("Cannot merge the same table!");
                    step = 1;
                }
                step++;
            }

            else if (choosenComponent == component.unmerge)
            {
                if ((newPlan.GetComponent(col, row) is MergedTable))
                {
                    table1.X = col;
                    table1.Y = row;

                }
                else if (newPlan.ListCheck(newPlan.GetComponent(table1.X, table1.Y)))
                {
                    if (!newPlan.UnMergeTable(newPlan.GetComponent(table1.X, table1.Y), e.Location))
                    {
                        MessageBox.Show("Cannot place table here or you haven't selected merged table.");
                    }
                }
                if (!newPlan.ListCheck(newPlan.GetComponent(table1.X, table1.Y)))
                {
                    newPlan.RemoveComponent(newPlan.GetComponent(table1.X, table1.Y));
                    choosenComponent = null;
                }
            }

            restaurantPlan.Invalidate();
        }

        private void btnGroupA_Click(object sender, EventArgs e)
        {
            choosenComponent = component.groupArea;
            btnTable.Enabled = true;
            btnBar.Enabled = true;
            btnGroupA.Enabled = false;
            btnWaitingA.Enabled = true;
            btnSmokingA.Enabled = true;
            btnNone.Enabled = true;
        }

        private void btnSmokingA_Click(object sender, EventArgs e)
        {
            choosenComponent = component.smokingArea;
            btnTable.Enabled = true;
            btnBar.Enabled = true;
            btnGroupA.Enabled = true;
            btnWaitingA.Enabled = true;
            btnSmokingA.Enabled = false;
            btnNone.Enabled = true;
        }

        private void btnShowGATables_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GroupArea.GroupAreaTables());
        }

        private void btnShowSATables_Click(object sender, EventArgs e)
        {
            MessageBox.Show(SmokeArea.SmokeAreaTables());
        }

        private void btnWaitingA_Click(object sender, EventArgs e)
        {
            choosenComponent = component.waitingArea;
            btnTable.Enabled = true;
            btnBar.Enabled = true;
            btnGroupA.Enabled = true;
            btnWaitingA.Enabled = false;
            btnSmokingA.Enabled = true;
            btnNone.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            choosenComponent = component.eraser;
            btnDelete.Enabled = false;
            btnTable.Enabled = true;
            btnBar.Enabled = true;
            btnGroupA.Enabled = true;
            btnWaitingA.Enabled = true;
            btnSmokingA.Enabled = true;
            btnNone.Enabled = true;
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            choosenComponent = component.merge;
            step = 1;
            btnTable.Enabled = true;
            btnBar.Enabled = true;
            btnGroupA.Enabled = true;
            btnWaitingA.Enabled = true;
            btnSmokingA.Enabled = true;
            btnNone.Enabled = true;
        }

        private void btnUnmerge_Click(object sender, EventArgs e)
        {
            choosenComponent = component.unmerge;
            btnTable.Enabled = true;
            btnBar.Enabled = true;
            btnGroupA.Enabled = true;
            btnWaitingA.Enabled = true;
            btnSmokingA.Enabled = true;
            btnNone.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            newPlan.StopPauseSimulation(true);
            timer.Stop();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            newPlan.StopPauseSimulation(false);
            timer.Stop();

            lblServedCustomersInfo.Text = "";
            lblCustSentAwayInfo.Text = "";

            lblServedCustomersInfo.Text = newPlan.ServedCustomers.ToString();
            lblCustSentAwayInfo.Text = newPlan.CustomersSendAway.ToString();

            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            saveSimulationDataToolStripMenuItem.Enabled = true;
            openToolStripMenuItem.Enabled = true;
            rbNoon.Enabled = true;
            rbEvening.Enabled = true;

            lbLobbyOverview.Items.Clear();

            restaurantPlan.Invalidate();

            MessageBox.Show("Simulation has been stopped!");
        }

        // Starts the simulation or displays an error message
        private void btnStart_Click_1(object sender, EventArgs e)
        {
            int customerFlow = Convert.ToInt32(nudCustomerFlow.Value);
            int lunchTime = Convert.ToInt32(nudLunchDuration.Value);
            int dinnerTime = Convert.ToInt32(nudDinnerDuration.Value);
            int drinkTime = Convert.ToInt32(nudDrinkDuration.Value);
            bool peakHourOption = cbPeakHour.Checked ? true : false;
            TimeOfDay timeOfDay = rbEvening.Checked ? TimeOfDay.evening : TimeOfDay.afternoon;
            string message = newPlan.StartSimulation(customerFlow, lunchTime, dinnerTime,drinkTime, peakHourOption, true,
                timeOfDay);

            if (message != null)
            {
                MessageBox.Show(message);
            }
            else
            {
                btnStart.Enabled = false;
                btnPause.Enabled = true;
                btnStop.Enabled = true;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                saveSimulationDataToolStripMenuItem.Enabled = false;
                openToolStripMenuItem.Enabled = false;
                rbEvening.Enabled = false;
                rbNoon.Enabled = false;

                timer.Start();
            }
        }

        private void cbPeakHour_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPeakHour.Checked)
            {
                lblPeakHourInfo.Visible = true;
                nudCustomerFlow.Enabled = false;
                lblPeakHourOption.Text = "On";
            }
            else
            {
                lblPeakHourInfo.Visible = false;
                nudCustomerFlow.Enabled = true;
                lblPeakHourOption.Text = "Off";
            }

        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            choosenComponent = null;
            btnNone.Enabled = false;
            btnTable.Enabled = true;
            btnBar.Enabled = true;
            btnGroupA.Enabled = true;
            btnWaitingA.Enabled = true;
            btnSmokingA.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void LobbyOverview()
        {
            lbLobbyOverview.Items.Clear();
            var lobby = newPlan.LobbyCustomers();

            for (int i = 0; i <= lobby.Count - 1; i++)
            {
                lbLobbyOverview.Items.Add("Group ID: " + lobby[i].ID + " Group size: " + lobby[i].GroupSize);
            }
        }

        private void saveSimulationDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            
            saveDialog.DefaultExt = "txt";
            saveDialog.Filter = "Text files (*.txt)|*.txt";
            var result = saveDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                File.WriteAllText(saveDialog.FileName, newPlan.ExportStatistics());
            }
        }

        // Saves the simulation in a binary file.
        private void SaveSimulation()
        {
            if (filename == null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                DialogResult result;

                saveDialog.DefaultExt = "bin";
                saveDialog.Filter = "Binary files (*.bin)|*.bin";
                saveDialog.FileName = filename;

                result = saveDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    filename = saveDialog.FileName;
                }
                else
                {
                    return;
                }
            }

            string msg = newPlan.SaveSimulation(filename);
            MessageBox.Show(msg);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSimulation();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSimulation();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!newPlan.Empty())
            {
                var result = MessageBox.Show("Would you like to save the current simulation?", "Save", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    SaveSimulation();
                }
            }

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = "bin";
            openFile.Filter = "Binary files (*.bin)|*.bin";
            var returned = openFile.ShowDialog();

            if (returned == DialogResult.OK)
            {
                string msg = newPlan.LoadSimulation(openFile.FileName);
                restaurantPlan.Invalidate();
                MessageBox.Show(msg);
            }
        }

    }
}
