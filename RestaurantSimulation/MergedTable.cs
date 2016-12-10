using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    class MergedTable:Component
    {
        public int size = 0;
        public List<int> table = new List<int>();
        public List<int> XpointList = new List<int>();
        public List<int> YpointList = new List<int>();
        private static int count = 0;
        private int id;
        int Xpoint, Ypoint;

        public MergedTable(List<int> listSize, Point p):base(p)
        {
            //Determine the size if the merged table
            foreach (int i in listSize)
            {
                size += i;
            }

            //Determine how many grid required
            if(size >= 5 && size <= 8)
            {
                Xpoint = (p.X + 40) / 40;
                Ypoint = (p.Y) / 40;

                XpointList.Add(Xpoint);
                YpointList.Add(Ypoint);
            }

            else if (size >= 9 && size <= 12)
            {
                for(int i = 1; i <= 2; i++)
                {
                    Xpoint = (p.X + 40 * i) / i;
                    Ypoint = (p.Y) / 40;

                    XpointList.Add(Xpoint);
                    YpointList.Add(Ypoint);
                }
            }

            else if (size >= 13 && size <= 16)
            {
                for (int i = 1; i <= 3; i++)
                {
                    Xpoint = (p.X + 40 * i) / i;
                    Ypoint = (p.Y) / 40;

                    XpointList.Add(Xpoint);
                    YpointList.Add(Ypoint);
                }
            }

            else if (size >= 17 && size <= 20)
            {
                for (int i = 1; i <= 4; i++)
                {
                    Xpoint = (p.X + 40 * i) / i;
                    Ypoint = (p.Y) / 40;

                    XpointList.Add(Xpoint);
                    YpointList.Add(Ypoint);
                }
            }

            //Store the table size for the unmerge
            table = listSize;

            //Assign for table ID
            id = count;

            //Increment everytime table is placed
            count++;
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

            if (size <= 4)
            {
                g.DrawImage(i, col, row, width, height);
            }

            else if (size >= 5 && size <= 8)
            {
                g.DrawImage(i, col, row, width, height);
                g.DrawImage(i, col + 40, row, width, height);
            }

            else if (size >= 9 && size <= 12)
            {
                g.DrawImage(i, col, row, width, height);
                g.DrawImage(i, col + 40, row, width, height);
                g.DrawImage(i, col + 80, row, width, height);
            }

            else if (size >= 13 && size <= 16)
            {
                g.DrawImage(i, col, row, width, height);
                g.DrawImage(i, col + 40, row, width, height);
                g.DrawImage(i, col + 80, row, width, height);
                g.DrawImage(i, col + 120, row, width, height);
            }

            else if (size >= 17 && size <= 20)
            {
                g.DrawImage(i, col, row, width, height);
                g.DrawImage(i, col + 40, row, width, height);
                g.DrawImage(i, col + 80, row, width, height);
                g.DrawImage(i, col + 120, row, width, height);
                g.DrawImage(i, col + 160, row, width, height);
            }

            Font newFont = new Font("Arial", 16);
            g.DrawString(Convert.ToString(size), newFont, Brushes.Black, (X * 40) + 10, (Y * 40) + 10);
            g.DrawString(id.ToString(), new Font("Arial", 10), Brushes.Black, (X * 40), (Y * 40));
        }

        public override void DecreaseCount()
        {
            count--;
        }

        public override void DecreaseID()
        {
            id--;
        }

        public override int GetSize()
        {
            throw new NotImplementedException();
        }

        public override List<int> getXpointList()
        {
            return XpointList;
        }

        public override List<int> getYpointList()
        {
            return YpointList;
        }

        public List<int> getTableList()
        {
            return table;
        }
    }
}
