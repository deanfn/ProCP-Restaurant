using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSimulation
{
    class GroupArea : SpecialAreas
    {
        private static List<Component> tablesList = new List<Component>();
        private const int maxTables = 10;

        public GroupArea(Point coordinates) : base(coordinates)
        {
            
        }

        public override void Drawing(Graphics g)
        {
            // Location
            int col = ((X) * 40) + 1;
            int row = ((Y) * 40) + 1;

            //Image Size
            int width = 39;
            int height = 39;

            Image i = (Bitmap)Properties.Resources.GroupArea;

            g.DrawImage(i, col, row, width, height);
            g.DrawImage(i, col + 40, row, width, height);
            g.DrawImage(i, col, row + 40, width, height);
            g.DrawImage(i, col + 40, row + 40, width, height);

            // Incdication that this is a group area
            Font arial = new Font("Arial", 14);
            g.DrawString("GA", arial, Brushes.White, (X * 40) + 5, (Y * 40) + 10);
            g.DrawString("GA", arial, Brushes.White, col + 40 + 5, row + 10);
            g.DrawString("GA", arial, Brushes.White, col + 5, row + 40 + 10);
            g.DrawString("GA", arial, Brushes.White, col + 40 + 5, row + 40 + 10);
        }

        public override bool AddTable(Component c)
        {
            if (c is MergedTable && tablesList.Count < maxTables)
            {
                for (int i = 0; i < Coordinates.Count; i++)
                {
                    if (this.Coordinates[i].Equals(Coordinates[i]) && FreeSpots[i])
                    {
                        this.FreeSpots[i] = false;
                    }
                }

                tablesList.Add(c);
                return true;
            }
            else if (base.AddTable(c) && tablesList.Count < maxTables)
            {
                tablesList.Add(c);
                return true;
            }

            return false;
        }

        public void Remove(Component c)
        {
            tablesList.Remove(c);
        }

        public static string GroupAreaTables()
        {
            if (tablesList.Count != 0)
            {
                string tables = "ID\n";

                foreach (var t in tablesList)
                {
                    tables += (t as Table).ID + "\n";
                }

                return tables;
            }

            return "There are no tables in the group area.";
        }
    }
}
