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

        //Properties for Merging Table
        int step = 1;

        // Point objects to save the coordinates of the tables to be merged.
        Point table1;
        Point table2;

        //Enum
        enum component { table, bar, groupArea, smokingArea, waitingArea, eraser, merge };
        component? choosenComponent = null;

        public RestaurantForm()
        {
            InitializeComponent();

            table1 = new Point();
            table2 = new Point();

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
            if (choosenComponent != null && choosenComponent != component.eraser && choosenComponent != component.merge)
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
                
                //if (step == 1)
                //{
                //    Component com1 = newPlan.GetNoArea(col, row);

                //    if (com1 != null)
                //    {
                //        if (com1 is MergedTable)
                //        {
                //            foreach (int i in (com1 as MergedTable).table)
                //            {
                //                sizeList.Add(i);
                //            }
                //        }

                //        else
                //        {
                //            if ((com1 as Table).OnGA == true)
                //            {
                //                com2size = com1.GetSize();
                //                sizeList.Add(com2size);
                //            }

                //            else
                //            {
                //                MessageBox.Show("Only Table on Group Area can be Merged");
                //                step--;
                //            }
                //        }

                //        newPlan.RemoveComponent(com1);
                //    }

                //    else
                //    {
                //        MessageBox.Show("Please select a Table");
                //        step--;
                //    }

                //    com1 = null;
                //}

                //if (step == 2)
                //{
                //    Component com2 = newPlan.GetNoArea(col, row);

                //    if (com2 != null)
                //    {
                //        if (com2 is MergedTable)
                //        {
                //            foreach (int i in (com2 as MergedTable).getTableList())
                //            {
                //                sizeList.Add(i);
                //            }
                //        }

                //        else
                //        {
                //            if ((com2 as Table).OnGA == true)
                //            {
                //                com2size = com2.GetSize();
                //                sizeList.Add(com2size);
                //            }

                //            else
                //            {
                //                MessageBox.Show("Only Table on Group Area can be Merged");
                //                step--;
                //            }
                //        }

                //        newPlan.RemoveComponent(com2);
                //    }

                //    else
                //    {
                //        MessageBox.Show("Please select a Table");
                //        step--;
                //    }

                //    com2 = null;
                //}

                //if (step == 3)
                //{
                //    if (newPlan.GetComponent(col, row) is GroupArea)
                //    {
                //        if (newPlan.AddMergedTable(sizeList, e.Location))
                //        {
                //            step = 0;
                //            sizeList.Clear();
                //            choosenComponent = null;
                //        }
                //        else
                //        {
                //            MessageBox.Show("Please Select a Free Spot");
                //            step--;
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Merged Table Can Only be Put on Group Area");
                //        step--;
                //    }
                //}

                step++;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            choosenComponent = component.eraser;
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            choosenComponent = component.merge;
            step = 1;
        }
    }
}
