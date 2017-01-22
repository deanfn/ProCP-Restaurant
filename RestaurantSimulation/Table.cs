using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSimulation
{
    [Serializable]
    class Table : Component
    {
        private static int count = 0;

        public int ID { get; set; }

        public int TableSize { get; set; }

        // Boolean indicating whether the table is on group area spot or not.
        public bool OnGA { get; set; }

        // Boolean indicating whether or not the table is on smoking area.
        public bool OnSA { get; set; }

        // Boolean that indicates if the table is on a waiting area.
        public bool OnWA { get; set; }

        // True if table is available, false otherwise.
        public bool Available { get; set; }

        public CustomerGroup Customers { get; set; }

        /// <summary>
        /// Creates new Table object, with unique ID
        /// </summary>
        /// <param name="size"></param>
        /// <param name="avail"></param>
        /// <param name="merg"></param>
        public Table(int size, Point p) : base(p)
        {
            this.TableSize = size;
            this.Available = true;
            OnGA = false;
            OnSA = false;
            OnWA = false;
        }

        /// <summary>
        /// Returns Table objects size and ID
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "size: " + TableSize + ", id: " + ID;
        }

        /// <summary>
        /// This method draws the table on the restaurant plan.
        /// It uses the coordinates from the mouse click to do so.
        /// </summary>
        /// <param name="pb"></param>
        public override void Draw(Graphics g)
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
            g.DrawString(ID.ToString(), new Font("Arial", 10), Brushes.Black, (X * 40), (Y * 40));

            if (Customers != null)
            {
                g.DrawString(Customers.GroupSize.ToString() + "/" + TableSize.ToString(), newFont, Brushes.Black,
                    (X * 40) + 1, (Y * 40) + 10);
            }
            else
            {
                g.DrawString(0 + "/" + Convert.ToString(TableSize), newFont, Brushes.Black, (X * 40) + 1, (Y * 40) + 10);
            }

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

        // Assigns an id number to the table using the static field count.
        public void AssignID()
        {
            this.ID = count;
            count++;
        }

        public override int GetSize()
        {
            return TableSize;
        }

        // Seats customers to the table.
        public bool SeatCustomersAtTable(CustomerGroup customers)
        {
            Customers = customers;
            Available = false;
            Customers.ID = ID;

            if (OnWA)
            {
                Customers.Wait();
            }
            else
            {
                Customers.StopWaiting();
                Customers.StartEating();
            }

            return true;
        }

        public void ClearTable()
        {
            Customers = null;
            Available = true;
        }

    }
}
