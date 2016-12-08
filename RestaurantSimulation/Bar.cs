using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RestaurantSimulation
{
    class Bar:Component
    {
        private int id;
        private static int count;
        private int size;
        private bool available;

        /// <summary>
        /// Creates new Bar object
        /// </summary>
        /// <param name="size"></param>
        public Bar(int size, Point p):base(p)
        {
            this.size = size;
            this.id = count;
            this.available = true;
            count++;
        }

        //Draw Bar
        public override void Drawing(Graphics g)
        {
            // Location
            int col = ((X) * 40) + 1;
            int row = ((Y) * 40) + 1;

            //Image Size
            int width = 39;
            int height = 39;

            Image i = (Bitmap)Properties.Resources.Bar.Clone();

            g.DrawImage(i, col, row, width, height);

            Font newFont = new Font("Arial", 16);
            g.DrawString(Convert.ToString(size), newFont, Brushes.Black, (X * 40) + 10, (Y * 40) + 10);

        }

        public void DecreaseCount()
        {
            count -= 1;
        }
    }
}
