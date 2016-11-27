using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSimulation
{
    class SmokeArea : Component
    {
        private static List<Component> tablesList = new List<Component>();
        private const int maxTables = 5;

        public bool Free { get; set; }

        public SmokeArea(Point coordinates) : base(coordinates)
        {
            Free = true;
        }

        public override void Drawing(ref PictureBox pb)
        {
            // In order to draw on the picturebox we must create a Graphics object
            Graphics g = pb.CreateGraphics();

            // Location
            int col = ((X) * 40) + 1;
            int row = ((Y) * 40) + 1;

            //Image Size
            int width = 39;
            int height = 39;

            Image i = (Bitmap)Properties.Resources.smoking_allowed;

            g.DrawImage(i, col, row, width, height);
        }

        public bool AddTable(Component c)
        {
            if (this.Free && tablesList.Count <= maxTables)
            {
                tablesList.Add(c);
                Free = false;

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
