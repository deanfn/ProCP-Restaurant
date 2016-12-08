using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSimulation
{
    class Table : Component
    {
        private int size;
        private bool available;
        private bool merging;
        private static int count = 0;

        public int ID { get; }

        // Boolean indicating whether the table is on group area spot or not.
        public bool OnGA { get; set; }

        // Boolean indicating whether or not the table is on smoking area.
        public bool OnSA { get; set; }

        // Boolean that indicates if the table is on a waiting area.
        public bool OnWA { get; set; }

        /// <summary>
        /// Creates new Table object, with unique ID
        /// </summary>
        /// <param name="size"></param>
        /// <param name="avail"></param>
        /// <param name="merg"></param>
        public Table(int size, bool merg, Point p) : base(p)
        {
            this.size = size;
            this.available = true;
            this.merging = merg;
            OnGA = false;
            OnSA = false;
            OnWA = false;
            this.ID = count;
            count++;
        }

        /// <summary>
        /// Returns Table objects size and ID
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "size: " + size + ", id: " + ID;
        }

        /// <summary>
        /// This method draws the table on the restaurant plan.
        /// It uses the coordinates from the mouse click to do so.
        /// </summary>
        /// <param name="pb"></param>
        public override void Drawing(Graphics g)
        {
            // Location
            int col = ((X) * 40) + 1;
            int row = ((Y) * 40) + 1;

            //Image Size
            int width = 39;
            int height = 39;

            Image i = (Bitmap)Properties.Resources.Table.Clone();

            g.DrawImage(i, col, row, width, height);

            Font newFont = new Font("Arial", 16);
            g.DrawString(Convert.ToString(size), newFont, Brushes.Black, (X*40) + 10, (Y*40) + 10);
            g.DrawString(ID.ToString(), new Font("Arial", 10), Brushes.Black, (X * 40), (Y * 40));

            /* This checks if the table is placed on a special area. And if it is
             * it will draw a string in the top right corner indicating what is the area. */
            if (OnSA)
            {
                g.DrawString("SA", new Font("Arial", 8), Brushes.Black, (X * 40) + 19, Y * 40);
            }
            else if (OnGA)
            {
                g.DrawString("GA", new Font("Arial", 8), Brushes.Black, (X * 40) + 19, Y * 40);
            }
            else if (OnWA)
            {
                g.DrawString("WA", new Font("Arial", 8), Brushes.Black, (X * 40) + 19, Y * 40);
            }
        }

        public void DecreaseCount()
        {
            count -= 1;
        }
    }
}
