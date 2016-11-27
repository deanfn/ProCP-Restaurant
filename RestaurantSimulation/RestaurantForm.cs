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
        public Graphics grid;
        public Pen gridLine = new Pen(Color.White);
        public int gridHeight = 12;
        public int gridWidth = 17;
        public int cellWidth = 40;
        public int cellHeight = 40;
        public int row = 0;
        public int col = 0;

        //Enum
        enum component { table, bar, groupArea, smokingArea };
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

            if(choosenComponent == component.table)
            {
                if (rbSize2.Checked)
                {
                    Table newTable = new Table(2, false, e.Location);

                    if (newPlan.AddComponent(newTable))
                    {
                        newTable.Drawing(ref RestaurantPlan);
                        choosenComponent = null;
                    }
                }

                else
                {
                    Table newTable = new Table(4, false, e.Location);

                    if (newPlan.AddComponent(newTable))
                    {
                        newTable.Drawing(ref RestaurantPlan);
                        choosenComponent = null;
                    }
                }
            }

            if (choosenComponent == component.bar)
            {
                if (rbSize2.Checked)
                {
                    Bar newBar = new Bar(2, e.Location);

                    if (newPlan.AddComponent(newBar))
                    {
                        newBar.Drawing(ref RestaurantPlan);
                        choosenComponent = null;
                    }
                }

                else
                {
                    Bar newBar = new Bar(4, e.Location);
                    if (newPlan.AddComponent(newBar))
                    {
                        newBar.Drawing(ref RestaurantPlan);
                        choosenComponent = null;
                    }
                }
            }

            // Draw group area
            if (choosenComponent == component.groupArea)
            {
                var groupArea = new GroupArea(e.Location);

                if (newPlan.AddComponent(groupArea))
                {
                    groupArea.Drawing(ref RestaurantPlan);
                    choosenComponent = null;
                }
            }

            // Draw smoke area
            if (choosenComponent == component.smokingArea)
            {
                var smokeArea = new SmokeArea(e.Location);

                if (newPlan.AddComponent(smokeArea))
                {
                    smokeArea.Drawing(ref RestaurantPlan);
                    choosenComponent = null;
                }
            }
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
    }
}
