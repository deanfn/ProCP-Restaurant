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
        public int size;
        public bool available;
        public bool merging;
        private static int count = 0;

        public int ID { get; }

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
        /// TODO <--- Simple explanation please
        /// </summary>
        /// <param name="pb"></param>
        public override void Drawing(ref PictureBox pb)
        {
            Graphics g = pb.CreateGraphics();

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
        }
    }
}
