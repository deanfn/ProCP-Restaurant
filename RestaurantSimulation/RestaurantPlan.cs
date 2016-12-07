using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSimulation
{
    class RestaurantPlan
    {
        private List<Component> componentOnPlan = new List<Component>();

        public bool AddTable(int size, bool merge, Point p)
        {
            Table newTable = new Table(size, merge, p);

            foreach (Component com in componentOnPlan)
            {
                
                if ((newTable.X == com.X && newTable.Y == com.Y) || newTable.X + 1 == com.X && newTable.Y == com.Y || newTable.X - 1 == com.X && newTable.Y == com.Y || newTable.X == com.X && newTable.Y + 1 == com.Y ||
                    newTable.X == com.X && newTable.Y - 1 == com.Y || newTable.X + 1 == com.X && newTable.Y + 1 == com.Y || newTable.X -1 == com.X && newTable.Y - 1 == com.Y || newTable.X + 1 == com.X && newTable.Y - 1 == com.Y || newTable.X - 1 == com.X && newTable.Y + 1 == com.Y)
                {
                    newTable.DecreaseCount();
                    return false;
                }
            }

            this.componentOnPlan.Add(newTable);
            return true;
        }

        public bool AddBar(int size, Point p)
        {
            Bar newBar = new Bar(size, p);
            
            foreach (Component com in componentOnPlan)
            {
                
                if ((newBar.X == com.X && newBar.Y == com.Y) || newBar.X + 1 == com.X && newBar.Y == com.Y || newBar.X - 1 == com.X && newBar.Y == com.Y || newBar.X == com.X && newBar.Y + 1 == com.Y ||
                    newBar.X == com.X && newBar.Y - 1 == com.Y || newBar.X + 1 == com.X && newBar.Y + 1 == com.Y || newBar.X -1 == com.X && newBar.Y - 1 == com.Y || newBar.X + 1 == com.X && newBar.Y - 1 == com.Y || newBar.X - 1 == com.X && newBar.Y + 1 == com.Y)
                {
                    newBar.DecreaseCount();
                    return false;
                }
            }

            this.componentOnPlan.Add(newBar);
            return true;
        }



        public Component GetComponent(int x, int y)
        {
            foreach (Component c in componentOnPlan)
            {
                if(c.X == x && c.Y == y)
                {
                    return c;
                }
            }
            return null;
        }

        public bool removeComponent(Component c)
        {
            int ComCounter = 0;

            foreach(Component com in componentOnPlan)
            {
                if (c.X == com.X && c.Y == com.Y)
                {
                    if(c is Table || c is Bar)
                    {
                        for(int i = ComCounter; i<componentOnPlan.Count; i++)
                        {
                            if (c is Table)
                            {
                                componentOnPlan.ElementAt(i).DecreaseID();
                            }
                        }

                        for (int i = ComCounter; i < componentOnPlan.Count; i++)
                        {
                            if (c is Bar)
                            {
                                componentOnPlan.ElementAt(i).DecreaseID();
                            }
                        }

                        c.DecreaseCount();

                    }

                    componentOnPlan.Remove(c);
                    return true;
                }

                ComCounter++;
            }

            return false;
        }

        public void Redraw(ref PictureBox pb)
        {
            pb.Refresh();
            foreach (Component c in componentOnPlan)
            {
                c.Drawing(ref pb);
            }
        }
    }
}

//if ((com is GroupArea || com is SmokeArea) && c is Bar)
//{
//    MessageBox.Show("The bar can be placed only outside of special areas!");
//    return false;
//}

//else if (c.X == com.X && c.Y == com.Y && com is GroupArea && c is Table)
//{
//    if ((com as GroupArea).AddTable(c))
//    {
//        break;
//    }
//}

//else if (c.X == com.X && c.Y == com.Y && com is SmokeArea && c is Table)
//{
//    if ((com as SmokeArea).AddTable(c))
//    {
//        break;
//    }
//}
