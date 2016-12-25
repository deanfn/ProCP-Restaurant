﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        //Enum
        enum component { table, bar, groupArea, smokingArea, waitingArea, eraser, merge, unmerge };
        component? choosenComponent = null;

        public RestaurantForm()
        {
            InitializeComponent();

            table1 = new Point();
            table2 = new Point();

            newPlan = RestaurantPlan.Instance;

            ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(nudDinnerDuration, "The time in seconds for a customer group to have dinner.");
            toolTip1.SetToolTip(nudLunchDuration, "The time in seconds for a customer group to have lunch.");
            toolTip1.SetToolTip(nudLunchDuration, "The interval for which a customer group will be generated.");

            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            restaurantPlan.Invalidate();
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
        }

        private void btnBar_Click(object sender, EventArgs e)
        {
            choosenComponent = component.bar;
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
                    choosenComponent = null;
                }
                else
                {
                    MessageBox.Show("Please, Select a Free Spot");
                }
            }

            else if (choosenComponent == component.eraser)
            {
                newPlan.RemoveComponent(newPlan.GetComponent(col, row));
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
                    if (!newPlan.MergeTables(newPlan.GetComponent(table1.X, table1.Y), newPlan.GetComponent(table2.X, table2.Y),
                        e.Location))
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
        }

        private void btnSmokingA_Click(object sender, EventArgs e)
        {
            choosenComponent = component.smokingArea;
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
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            choosenComponent = component.eraser;
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            choosenComponent = component.merge;
            step = 1;
        }

        private void btnUnmerge_Click(object sender, EventArgs e)
        {
            choosenComponent = component.unmerge;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            newPlan.StopPauseSimulation(true);
            timer.Stop();

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            newPlan.StopPauseSimulation(false);
            timer.Stop();

            label7.Text = "";
            label8.Text = "";

            label7.Text = newPlan.Data()[0].ToString();
            label8.Text = newPlan.Data()[1].ToString();

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            int customerFlow = Convert.ToInt32(nudCustomerFlow.Value);
            int lunchTime = Convert.ToInt32(nudLunchDuration.Value);
            int dinnerTime = Convert.ToInt32(nudDinnerDuration.Value);
            bool peakHourOption = cbPeakHour.Checked ? true : false;
            string message = newPlan.StartSimulation(customerFlow, lunchTime, dinnerTime, peakHourOption, true);

            if (message != null)
            {
                MessageBox.Show(message);
            }
            else
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                timer.Start();
            }
        }
    }
}
