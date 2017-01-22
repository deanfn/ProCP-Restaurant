using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RestaurantSimulation
{
    [Serializable]
    class Bar : Component
    {
        private int id;
        private static int count;
        private int size;

        public bool Available { get; set; }

        public CustomerGroup Customers { get; set; }

        /// <summary>
        /// Creates new Bar object
        /// </summary>
        /// <param name="size"></param>
        public Bar(int size, Point p)
            : base(p)
        {
            this.size = size;
            this.Available = true;
        }

        //Draw Bar
        public override void Draw(Graphics g)
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

            if (Customers != null)
            {
                g.DrawString(Customers.GroupSize.ToString() + "/" + size.ToString(), newFont, Brushes.Black,
                    (X * 40) + 1, (Y * 40) + 10);
            }
            else
            {
                g.DrawString(0 + "/" + Convert.ToString(size), newFont, Brushes.Black, (X * 40) + 1, (Y * 40) + 10);
            }

            newFont = new Font("Arial", 10);
            g.DrawString(id.ToString(), newFont, Brushes.Black, (X * 40), (Y * 40));
        }


        public bool SeatCustomersAtTable(CustomerGroup customers)
        {
            Customers = customers;
            Available = false;
            Customers.ID = id;
            Customers.StopWaiting();
            Customers.StartEating();

            return true;
        }
        public void ClearBar()
        {
            Customers = null;
            Available = true;
        }

        public void AssignID()
        {
            this.id = count;
            count++;
        }

        public override int GetSize()
        {
            return size;
        }
    }
}
