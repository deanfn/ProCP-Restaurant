using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RestaurantSimulation
{
    class SmokeArea : SpecialAreas
    {
        private static List<Component> tablesList = new List<Component>();
        private const int maxTables = 5;

        public SmokeArea(Point coordinates) : base(coordinates)
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

            Image i = (Bitmap)Properties.Resources.smoking_allowed;

            g.DrawImage(i, col, row, width, height);
            g.DrawImage(i, col + 40, row, width, height);
            g.DrawImage(i, col, row + 40, width, height);
            g.DrawImage(i, col + 40, row + 40, width, height);
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

        public static string SmokeAreaTables()
        {
            if (tablesList.Count != 0)
            {
                string tables = "ID:\n";

                foreach (Component t in tablesList)
                {
                    tables += (t as Table).ID + "\n";
                }

                return tables;
            }

            return "There are no tables in the smoking area!";
        }
    }
}
