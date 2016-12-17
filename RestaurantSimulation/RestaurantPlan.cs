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
        public List<Component> componentOnPlan;

        public RestaurantPlan()
        {
            componentOnPlan = new List<Component>();
        }

        public bool AddComponent(Point coordinates, int type, int size)
        {
            Component comp;

            switch (type)
            {
                case 0:
                    comp = new Table(size, coordinates);
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
                else if ((comp.X == c.X && comp.Y == c.Y) || (comp.X + 1 == c.X && comp.Y == c.Y) ||
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

        /* Returns the the table that is located
         * at the specified coordinates. Null if there
         * is no such table. */
        public Component GetComponent(int x, int y)
        {
            var mergedTables = componentOnPlan.FindAll(t => t is MergedTable);

            foreach (var t in mergedTables)
            {
                foreach (var point in (t as MergedTable).Coordinates)
                {
                    if (point.X == x && point.Y == y)
                        return t;
                }
            }

            return componentOnPlan.Find(t => (t is Table && t.X == x && t.Y == y));
        }

        /* Checks whether two points are within a Group area and if they are
         * returns that group area, otherwise returns null. */
        public Component GetGroupArea(int x, int y)
        {
            // List of all group areas.
            var groupAreas = componentOnPlan.FindAll(c => c is GroupArea);

            foreach (var ga in groupAreas)
            {
                foreach (var point in (ga as GroupArea).Coordinates)
                {
                    if ((x == point.X && y == point.Y))
                    {
                        return ga;
                    }
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

            if (c != null)
            {
                foreach (Component com in componentOnPlan)
                {
                    if (c.X == com.X && c.Y == com.Y)
                    {
                        if (c is Table || c is Bar || c is MergedTable)
                        {
                            for (int i = ComCounter; i < componentOnPlan.Count; i++)
                            {
                                if (c is Table && componentOnPlan.ElementAt(i) is Table)
                                {
                                    componentOnPlan.ElementAt(i).DecreaseID();
                                }
                            }

                            for (int i = ComCounter; i < componentOnPlan.Count; i++)
                            {
                                if (c is Bar && componentOnPlan.ElementAt(i) is Bar)
                                {
                                    componentOnPlan.ElementAt(i).DecreaseID();
                                }
                            }

                            for (int i = ComCounter; i < componentOnPlan.Count; i++)
                            {
                                if (c is MergedTable && componentOnPlan.ElementAt(i) is MergedTable)
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

        // Merge two tables
        public bool MergeTables(Component t1, Component t2, Point location)
        {
            if (t1 is MergedTable && t2 is MergedTable)
            {
                if (((t1 as MergedTable).Tables.Count > 1 && (t2 as MergedTable).Tables.Count < 3) ||
                    (t1 as MergedTable).Tables.Count < 3 && (t2 as MergedTable).Tables.Count > 1)
                {
                    
                    return false;
                }
            }
            else if (t1 is MergedTable && !(t2 as Table).OnGA)
            {
                return false;
            }
            else if (t2 is MergedTable && !(t1 as Table).OnGA)
            {
                return false;
            }
            else if (!(t1 as Table).OnGA && !(t2 as Table).OnGA)
            {
                return false;
            }

            // Create a MergedTable object
            var mergedTable = new MergedTable(location, t1, t2);

            for (int i = 0; i < mergedTable.Coordinates.Count; i++)
            {
                var ga = GetGroupArea(mergedTable.Coordinates[i].X, mergedTable.Coordinates[i].Y);

                if (ga == null)
                {
                    return false;
                }
                else if (i + 1 == mergedTable.Coordinates.Count)
                {
                    (ga as GroupArea).AddTable(mergedTable);
                    (ga as GroupArea).Remove(t1);
                    (ga as GroupArea).Remove(t2);
                }
            }

            // Add the merged table to the list of all components on the plan
            componentOnPlan.Add(mergedTable);

            // Remove the two tables from the list of components.
            componentOnPlan.Remove(t1);
            componentOnPlan.Remove(t2);

            return true;
        }

        public bool UnMergeTable(Component mt, Point location)
        {
            if (mt is MergedTable)
            {
                if ((mt as MergedTable).Tables[0] is MergedTable)
                {
                    int size = (mt as MergedTable).FirstTableSize();
                    if (size != -1)
                    {
                        AddComponent(location, 0, size);
                        return true;
                    }
                    else
                    {
                        (mt as MergedTable).Tables.RemoveAt(0);
                        size = (mt as MergedTable).Tables[0].GetSize();
                        if (AddComponent(location, 0, size))
                        {
                            (mt as MergedTable).Tables.RemoveAt(0);
                            return true;
                        }
                    }
                }
                else
                {
                    int size = (mt as MergedTable).Tables[0].GetSize();
                    if (AddComponent(location, 0, size))
                    {
                        (mt as MergedTable).Tables.RemoveAt(0);
                        return true;
                    }
                }
              return false;
            }
            return false;
        }
        public bool ListCheck(Component mt)
        {
            if ((mt as MergedTable).Tables.Count != 0)
            {
                return true;
            }
            return false;
        }

    }
}
