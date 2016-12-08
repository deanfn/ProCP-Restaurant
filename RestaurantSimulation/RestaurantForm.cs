using System;
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
        RestaurantPlan newPlan = new RestaurantPlan();

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

        //Enum
        enum component { table, bar, groupArea, smokingArea, waitingArea };
        component? choosenComponent = null;

        public RestaurantForm()
        {
            InitializeComponent();

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
            int size;
            
            if (rbSize2.Checked)
            {
                size = 2;
            }
            else
            {
                size = 4;
            }

            //if(choosenComponent == component.table)
            //{
            //    if (rbSize2.Checked)
            //    {

            //        if (newPlan.AddTable(2, false, e.Location))
            //        {
            //            choosenComponent = null;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please Select a Free Spot");
            //        }
            //    }

            //    else
            //    {
            //        if (newPlan.AddTable(4, false, e.Location))
            //        {
            //            choosenComponent = null;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please Select a Free Spot");
            //        }
            //    }
            //}

            //if (choosenComponent == component.bar)
            //{
            //    if (rbSize2.Checked)
            //    {
            //        if (newPlan.AddBar(2, e.Location))
            //        {
            //            choosenComponent = null;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please Select a Free Spot");
            //        }
            //    }

            //    else
            //    {
            //        if (newPlan.AddBar(4, e.Location))
            //        {
            //            choosenComponent = null;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please Select a Free Spot");
            //        }
            //    }
            //}

            // Add new component to the restaurant plan
            if (choosenComponent != null)
            {
                if (newPlan.AddComponent(e.Location, (int)choosenComponent, size, false))
                {
                    choosenComponent = null;
                }
                else
                {
                    MessageBox.Show("Please, Select a Free Spot");
                }
            }

            RestaurantPlan.Invalidate();
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
    }
}
