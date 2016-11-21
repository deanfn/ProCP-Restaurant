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

            if(size == 4)
            {
                this.x2 = p.X / 40;
                this.y2 = (p.Y + 40) / 40;
            }

            else
            {
                this.x2 = -1;
                this.y2 = -1;
            }
        }

        /// <summary>
        /// Returns Table objects size and ID
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "size: " + size + ", id: " + id;
        }

        public override void Drawing(ref PictureBox pb)
        {
            if (size == 2)
            {
                Graphics g = pb.CreateGraphics();

                // Location
                int col = ((x1) * 40) + 1;
                int row = ((y1) * 40) + 1;

                //Image Size
                int width = 39;
                int height = 39;

                Image i = (Bitmap)Properties.Resources.Table.Clone();

                g.DrawImage(i, col, row, width, height);
            }

            if (size == 4)
            {
                Graphics g = pb.CreateGraphics();

                // Location
                int col = ((x1) * 40) + 1;
                int row = ((y1) * 40) + 1;
                int col1 = ((x2) * 40) + 1;
                int row1 = ((y2) * 40) + 1;

                //Image Size
                int width = 39;
                int height = 39;

                Image i = (Bitmap)Properties.Resources.Table.Clone();

                g.DrawImage(i, col, row, width, height);
                g.DrawImage(i, col1, row1, width, height);
            }

        }
    }
}
