using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSimulation
{
    [Serializable]
    class GroupArea : SpecialArea
    {
        private static List<Component> tablesList = new List<Component>();
        private const int maxTables = 10;

        public GroupArea(Point coordinates) : base(coordinates)
        {

        }

        public override void Draw(Graphics g)
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
            if (c is MergedTable && tablesList.Count < maxTables && !tablesList.Contains(c))
            {
                foreach (Spot s in Spots)
                {
                    if ((c as MergedTable).Coordinates.Contains(s.Coordinates))
                    {
                        s.Free = false;
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

        public override void LoadTableList(List<Component> tables)
        {
            tablesList.Clear();

            if (tables.Count != 0)
            {
                foreach (var table in tables)
                {
                    tablesList.Add(table);
                }
            }
        }

        public void RemoveTable(Component table)
        {
            tablesList.Remove(table);
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
