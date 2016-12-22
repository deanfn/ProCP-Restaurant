using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RestaurantSimulation
{
    sealed class RestaurantPlan
    {
        private static readonly RestaurantPlan instance = new RestaurantPlan();
        private List<Component> componentOnPlan;
        private List<CustomerGroup> customers;

        // Property to get the instance
        public static RestaurantPlan Instance
        {
            get
            {
                return instance;
            }
        }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static RestaurantPlan()
        {

        }

        private RestaurantPlan()
        {
            componentOnPlan = new List<Component>();
            customers = new List<CustomerGroup>();
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

            // Prevents overlapping between regular table and merged table.
            if (comp is Table && GetComponent(comp.X, comp.Y) != null)
            {
                return false;
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
                else if (c is Table && comp is GroupArea)
                {
                    foreach (Spot s in (comp as GroupArea).Spots)
                    {
                        if (c.X == s.Coordinates.X && c.Y == s.Coordinates.Y)
                            return false;
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
                foreach (var point in (ga as GroupArea).Spots)
                {
                    if ((x == point.Coordinates.X && y == point.Coordinates.Y))
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
                        if (c is Table && (c as Table).OnGA)
                        {
                            RemoveTableFromGA(c);
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
                c.Draw(g);
            }
        }

        /* Returns all the group area spots on the restaurant plan. */
        private List<Spot> GetGAsFreeSpots()
        {
            var allCoordinates = new List<Spot>();
            var allGAs = componentOnPlan.FindAll(area => area is GroupArea);

            foreach (var ga in allGAs)
            {
                allCoordinates.AddRange((ga as GroupArea).Spots.Where(p => p.Free == true));
            }

            return allCoordinates;
        }

        // Checks if the coordinates are within the set of free group area spots.
        private bool CheckCoordinates(List<Point> coordinates)
        {
            var gaFreeSpots = GetGAsFreeSpots();
            int i = 0;

            foreach (var spot in gaFreeSpots)
            {
                if (coordinates.Contains(spot.Coordinates) && i < coordinates.Count)
                {
                    i++;
                    continue;
                }
            }

            if (i >= coordinates.Count)
            {
                return true;
            }

            return false;
        }

        // Merge two tables
        public bool MergeTables(Component t1, Component t2, Point location)
        {
            MergedTable mergedTable = null;

            if (t1 is MergedTable && t2 is MergedTable)
            {
                List<Component> tablesList;

                if ((t1 as MergedTable).Tables.Count <= 2 && (t2 as MergedTable).Tables.Count <= 2)
                {
                    tablesList = (t1 as MergedTable).Tables.Concat((t2 as MergedTable).Tables).ToList();
                    int size = (t1 as MergedTable).TableSize + (t2 as MergedTable).TableSize;

                    mergedTable = new MergedTable(tablesList, size, location);
                }
                else
                {
                    return false;
                }
            }
            else if ((t1 is MergedTable && !(t2 as Table).OnGA) || (t2 is MergedTable && !(t1 as Table).OnGA) ||
                (!(t1 as Table).OnGA && !(t2 as Table).OnGA) || (t1 is MergedTable && (t1 as MergedTable).Tables.Count >= 3) ||
                (t2 is MergedTable && (t2 as MergedTable).Tables.Count >= 3))
            {
                return false;
            }
            else
            {
                // Creates a MergedTable object
                mergedTable = new MergedTable(location, t1, t2);
            }

            var ga = GetGroupArea(mergedTable.X, mergedTable.Y);

            if (CheckCoordinates(mergedTable.Coordinates))
            {
                componentOnPlan.Add(mergedTable);
                (ga as GroupArea).AddTable(mergedTable);

                foreach (Point p in mergedTable.Coordinates)
                {
                    var groupArea = GetGroupArea(p.X, p.Y);
                    var spot = (groupArea as GroupArea).Spots.Find(s => s.Coordinates.Equals(p));
                    spot.Free = false;
                }
            }
            else
            {
                return false;
            }

            // Removes the tables from the group areas
            RemoveTableFromGA(t1);
            RemoveTableFromGA(t2);

            componentOnPlan.Remove(t1);
            componentOnPlan.Remove(t2);

            return true;
        }

        // Removes tables from a group area and marks the spots as free.
        private void RemoveTableFromGA(Component table)
        {
            if (table is MergedTable)
            {
                foreach (Point p in (table as MergedTable).Coordinates)
                {
                    var ga = GetGroupArea(p.X, p.Y);
                    var spot = (ga as GroupArea).Spots.Find(s => s.Coordinates.Equals(p));
                    spot.Free = true;
                }
            }
            else
            {
                var ga = GetGroupArea(table.X, table.Y);
                var spot = (ga as GroupArea).Spots.Find(s => s.Coordinates.Equals(new Point(table.X, table.Y)));
                spot.Free = true;
            }

            (GetGroupArea(table.X, table.Y) as GroupArea).RemoveTable(table);
        }

        public bool UnMergeTable(Component mt, Point location)
        {
            if (mt is MergedTable)
            {
                int size = (mt as MergedTable).Tables[0].GetSize();
                if (AddComponent(location, 0, size))
                {
                    (mt as MergedTable).Tables.RemoveAt(0);
                    return true;
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
