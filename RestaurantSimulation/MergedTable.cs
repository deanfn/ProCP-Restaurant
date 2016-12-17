using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    class MergedTable : Table
    {
        private static int count = 0;

        // The list of tables that are merged.
        public List<Component> Tables { get; set; }

        // List with coordinates for the merged table.
        public List<Point> Coordinates { get; set; }


        public MergedTable(Point p, Component t1, Component t2)
            : base((t1 as Table).TableSize + (t2 as Table).TableSize, p)
        {
            Tables = new List<Component>();
            Coordinates = new List<Point>();

            // Adds the two tables 
            Tables.Add(t1);
            Tables.Add(t2);


            OnGA = true;

            /* Determine the size of the merged table.
             * If between six and eight the merged table will lie on two squares,
             * if between 10 or 12 it will cover 3 squares. */
            if (TableSize >= 6 && TableSize <= 8)
            {
                Coordinates.Add(new Point(p.X / 40, p.Y / 40));
                Coordinates.Add(new Point((p.X + 40) / 40, p.Y / 40));
            }
            else if (TableSize >= 10 && TableSize <= 12)
            {
                Coordinates.Add(new Point(p.X / 40, p.Y / 40));
                Coordinates.Add(new Point((p.X + 40) / 40, p.Y / 40));
                Coordinates.Add(new Point((p.X / 40) + 2, p.Y / 40));
            }

            //Assign for table ID
            this.ID = count;

            //Increment everytime table is placed
            count++;

            //foreach (int i in listSize)
            //{
            //    size += i;
            //}

            //Determine how many grid required
            //if(size >= 5 && size <= 8)
            //{
            //    Xpoint = (p.X + 40) / 40;
            //    Ypoint = (p.Y) / 40;

            //    XpointList.Add(Xpoint);
            //    YpointList.Add(Ypoint);
            //}

            //else if (size >= 9 && size <= 12)
            //{
            //    for(int i = 1; i <= 2; i++)
            //    {
            //        Xpoint = (p.X + 40 * i) / i;
            //        Ypoint = (p.Y) / 40;

            //        XpointList.Add(Xpoint);
            //        YpointList.Add(Ypoint);
            //    }
            //}

            //else if (size >= 13 && size <= 16)
            //{
            //    for (int i = 1; i <= 3; i++)
            //    {
            //        Xpoint = (p.X + 40 * i) / i;
            //        Ypoint = (p.Y) / 40;

            //        XpointList.Add(Xpoint);
            //        YpointList.Add(Ypoint);
            //    }
            //}

            //else if (size >= 17 && size <= 20)
            //{
            //    for (int i = 1; i <= 4; i++)
            //    {
            //        Xpoint = (p.X + 40 * i) / i;
            //        Ypoint = (p.Y) / 40;

            //        XpointList.Add(Xpoint);
            //        YpointList.Add(Ypoint);
            //    }
            //}

            //Store the table size for the unmerge
            //table = listSize;
        }

        public override void Drawing(Graphics g)
        {
            // Location
            int col = ((X) * 40) + 1;
            int row = ((Y) * 40) + 1;

            //Image Size
            int width = 39;
            int height = 39;

            Image i = (Bitmap)Properties.Resources.MergedTable;

            if (TableSize == 4)
            {
                g.DrawImage(i, col, row, width, height);
            }
            else if (TableSize >= 6 && TableSize <= 8)
            {
                g.DrawImage(i, col, row, width, height);
                g.DrawImage(i, col + 40, row, width, height);
            }

            else if (TableSize >= 10 && TableSize <= 12)
            {
                g.DrawImage(i, col, row, width, height);
                g.DrawImage(i, col + 40, row, width, height);
                g.DrawImage(i, col + 80, row, width, height);
            }

            Font newFont = new Font("Arial", 16);
            g.DrawString(TableSize.ToString(), newFont, Brushes.Black, (X * 40) + 10, (Y * 40) + 10);
            g.DrawString(ID.ToString(), new Font("Arial", 10), Brushes.Black, (X * 40), (Y * 40));
        }

        /* Checks if a tables coordinates are the same as
         * the coordinates of merging table. */
        public bool CheckCoordinates(Component c)
        {
            foreach (Point p in Coordinates)
            {
                if (p.Equals(c))
                {
                    return true;
                }
            }

            return false;
        }

        public override void DecreaseCount()
        {
            count--;
        }

        public override void DecreaseID()
        {
            ID--;
        }

        //For Unmerging 
        public override int GetSize()
        {
            int size;
            try
            {
                size = Tables[0].GetSize();
                Tables.RemoveAt(0);
                return size;
            }
            catch (ArgumentOutOfRangeException)
            {
                return -1;
            }
        }


        public int FirstTableSize()
        {
            int size = Tables[0].GetSize();
            if (size != -1)
            {
                return size;
            }
            return -1;


        }

        //For Unmerging 
        //public int MergedTableSize()
        //{
        //    int size = 0;

        //    for (int i = 0; i <= Tables.Count-1; i++)
        //    {
        //        size += Tables[i].GetSize();
        //    }
        //    return size;
        //}
        ////For Unmerging 
        //public bool RemoveFirstObject()
        //{
        //    if(Tables.Count != 0)
        //    {
        //        Tables.RemoveAt(0);
        //        return true;
        //    }
        //    return false;
        //}
    }
}
