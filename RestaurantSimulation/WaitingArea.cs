using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    [Serializable]
    class WaitingArea : SpecialArea
    {
        private const int maxTables = 6;
        private static List<Component> tablesList = new List<Component>();

        public WaitingArea(Point coordinates) : base(coordinates)
        {

        }

        public override bool AddTable(Component c)
        {
            if (base.AddTable(c) && tablesList.Count < maxTables)
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

        public override void Draw(Graphics g)
        {
            // Location
            int col = ((X) * 40) + 1;
            int row = ((Y) * 40) + 1;

            //Image Size
            int width = 39;
            int height = 39;

            Image i = (Bitmap)Properties.Resources.Icon_WaitingArea;

            g.DrawImage(i, col, row, width, height);
            g.DrawImage(i, col + 40, row, width, height);
            g.DrawImage(i, col, row + 40, width, height);
            g.DrawImage(i, col + 40, row + 40, width, height);
        }
    }
}
