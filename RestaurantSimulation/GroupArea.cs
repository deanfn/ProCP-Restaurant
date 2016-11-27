using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSimulation
{
    class GroupArea : Component
    {
        private static List<Component> tablesList = new List<Component>();
        private const int maxTables = 10;

        // This is used to indicate whether or not there is a table on that spot.
        public bool Free { get; set; }

        public GroupArea(Point coordinates) : base(coordinates)
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

            Image i = (Bitmap)Properties.Resources.GroupArea;

            g.DrawImage(i, col, row, width, height);

            // Incdication that this is a group area
            Font arial = new Font("Arial", 14);
            g.DrawString("GA", arial, Brushes.White, (X * 40) + 5, (Y * 40) + 10);
        }

        public bool AddTable(Component c)
        {
            if (this.Free && tablesList.Count <= maxTables)
            {
                tablesList.Add(c);
                this.Free = false;

                return true;
            }

            return false;
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
