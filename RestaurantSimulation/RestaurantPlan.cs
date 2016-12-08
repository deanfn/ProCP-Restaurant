using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RestaurantSimulation
{
    class RestaurantPlan
    {
        private List<Component> componentOnPlan;

        public RestaurantPlan()
        {
            componentOnPlan = new List<Component>();
        }

        //public bool AddTable(int size, bool merge, Point p)
        //{
        //    Table newTable = new Table(size, merge, p);

        //    foreach (Component com in componentOnPlan)
        //    {
        //        if (com is GroupArea)
        //        {
        //            if ((com as GroupArea).AddTable(newTable))
        //                break;
        //        }
        //        else if (com is SmokeArea)
        //        {
        //            if ((com as SmokeArea).AddTable(newTable))
        //                break;
        //        }

        //        if ((newTable.X == com.X && newTable.Y == com.Y) || newTable.X + 1 == com.X && newTable.Y == com.Y || newTable.X - 1 == com.X && newTable.Y == com.Y || newTable.X == com.X && newTable.Y + 1 == com.Y ||
        //            newTable.X == com.X && newTable.Y - 1 == com.Y || newTable.X + 1 == com.X && newTable.Y + 1 == com.Y || newTable.X - 1 == com.X && newTable.Y - 1 == com.Y || newTable.X + 1 == com.X && newTable.Y - 1 == com.Y || newTable.X - 1 == com.X && newTable.Y + 1 == com.Y)
        //        {
        //            newTable.DecreaseCount();
        //            return false;
        //        }
        //    }

        //    this.componentOnPlan.Add(newTable);
        //    return true;
        //}

        //public bool AddBar(int size, Point p)
        //{
        //    Bar newBar = new Bar(size, p);

        //    foreach (Component com in componentOnPlan)
        //    {

        //        if ((newBar.X == com.X && newBar.Y == com.Y) || newBar.X + 1 == com.X && newBar.Y == com.Y || newBar.X - 1 == com.X && newBar.Y == com.Y || newBar.X == com.X && newBar.Y + 1 == com.Y ||
        //            newBar.X == com.X && newBar.Y - 1 == com.Y || newBar.X + 1 == com.X && newBar.Y + 1 == com.Y || newBar.X - 1 == com.X && newBar.Y - 1 == com.Y || newBar.X + 1 == com.X && newBar.Y - 1 == com.Y || newBar.X - 1 == com.X && newBar.Y + 1 == com.Y)
        //        {
        //            newBar.DecreaseCount();
        //            return false;
        //        }
        //    }

        //    this.componentOnPlan.Add(newBar);
        //    return true;
        //}

        public bool AddComponent(Point coordinates, int type, int size, bool merged)
        {
            Component comp;

            switch (type)
            {
                case 0:
                    comp = new Table(size, merged, coordinates);
                    break;
                case 1:
                    comp = new Bar(size, coordinates);
                    break;
                case 2:
                    comp = new GroupArea(coordinates);
                    break;
                case 3:
                    comp = new SmokeArea(coordinates);
                    break;
                case 4:
                    comp = new WaitingArea(coordinates);
                    break;
                default:
                    comp = null;
                    break;
            }

            foreach (Component c in componentOnPlan)
            {
                if (c is GroupArea && comp is Table)
                {
                    if ((c as GroupArea).AddTable(comp))
                    {
                        (comp as Table).OnGA = true;
                        break;
                    }
                }
                else if (c is SmokeArea && comp is Table)
                {
                    if ((c as SmokeArea).AddTable(comp))
                    {
                        (comp as Table).OnSA = true;
                        break;
                    }
                }
                else if (c is WaitingArea && comp is Table)
                {
                    if ((c as WaitingArea).AddTable(comp))
                    {
                        (comp as Table).OnWA = true;
                        break;
                    }
                }

                if ((comp.X == c.X && comp.Y == c.Y) || (comp.X + 1 == c.X && comp.Y == c.Y) ||
                    (comp.X == c.X && comp.Y + 1 == c.Y) || (comp.X + 1 == c.X && comp.Y + 1 == c.Y) ||
                    (comp.X - 1 == c.X && comp.Y - 1 == c.Y) || (comp.X - 1 == c.X && comp.Y == c.Y) ||
                    (comp.X == c.X && comp.Y - 1 == c.Y) || (comp.X - 1 == c.X && comp.Y + 1 == c.Y) ||
                    (comp.X + 1 == c.X && comp.Y - 1 == c.Y))
                {
                    if (comp is Table)
                    {
                        (comp as Table).DecreaseCount();
                    }
                    else if (comp is Bar)
                    {
                        (comp as Bar).DecreaseCount();
                    }
                    return false;
                }
            }

            componentOnPlan.Add(comp);
            return true;
        }

        public Component GetComponent(int x, int y)
        {
            foreach (Component c in componentOnPlan)
            {
                if (c.X == x && c.Y == y)
                {
                    return c;
                }
            }
            return null;
        }

        // Check whether there are components
        public bool Empty()
        {
            if (componentOnPlan.Count != 0)
                return false;
            else
                return true;
        }

        public bool RemoveComponent(Component c)
        {
            int ComCounter = 0;

            foreach (Component com in componentOnPlan)
            {
                if (c.X == com.X && c.Y == com.Y)
                {
                    if (c is Table || c is Bar)
                    {
                        for (int i = ComCounter; i < componentOnPlan.Count; i++)
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

        // Draw all the components on the plan
        public void DrawComponents(Graphics g)
        {
            foreach (var c in componentOnPlan)
            {
                c.Drawing(g);
            }
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
