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
        public int id;
        public int size;
        public bool available;
        public bool merging;
        private static int count = 0;

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
            this.id = count;
            count++;
        }

        /// <summary>
        /// Returns Table objects size and ID
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "size: " + size + ", id: " + id;
        }
        /// <summary>
        /// TODO <--- Simple explanation please
        /// </summary>
        /// <param name="pb"></param>
        public override void Drawing(ref PictureBox pb)
        {
            Graphics g = pb.CreateGraphics();

            // Location
            int col = ((x) * 40) + 1;
            int row = ((y) * 40) + 1;

            //Image Size
            int width = 39;
            int height = 39;

            Image i = (Bitmap)Properties.Resources.Table.Clone();

            g.DrawImage(i, col, row, width, height);

            Font newFont = new Font("Arial", 16);
            g.DrawString(Convert.ToString(size), newFont, Brushes.Black, (x*40) + 10, (y*40) + 10);

        }
    }
}
