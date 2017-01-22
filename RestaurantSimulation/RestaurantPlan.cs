using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace RestaurantSimulation
{
    enum TimeOfDay { afternoon, evening }

    [Serializable]
    sealed class RestaurantPlan
    {
        private static readonly RestaurantPlan instance = new RestaurantPlan();
        private List<Component> componentOnPlan;


        //Properties for simulation running
        private List<CustomerGroup> customerList;

        // Lobby object. Note the Lobby class is singleton, only one object can exists.
        private Lobby lobby;

        // Fields to store the duration of the lunch, dinner and drinking respectively.
        private int lunchDuration;
        private int dinnerDuration;
        private int drinkDuration;

        // Fields that keep track of all the data that later can be saved.
        private int customersSendAway;
        private int servedCustomers;

        // Timer and stopwatch
        [NonSerialized]
        private Timer totalTimer, secondsTimer;
        [NonSerialized]
        private Stopwatch stopWatch;

        // Indicates whether a simulation is paused or not
        private bool pauseSim;

        public int ServedCustomers { get { return servedCustomers; } }

        public int CustomersSendAway { get { return customersSendAway; } }

        // Time of the day. It could be noon or evening.
        public TimeOfDay TimeOfDay { get; private set; }

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
            customerList = new List<CustomerGroup>();

            lobby = Lobby.Instance;
            stopWatch = new Stopwatch();
            totalTimer = new Timer();
            secondsTimer = new Timer();
            pauseSim = false;
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
                    return false;
                }
            }

            if (comp is Table)
            {
                (comp as Table).AssignID();
            }
            else if (comp is Bar)
            {
                (comp as Bar).AssignID();
            }

            componentOnPlan.Add(comp);
            return true;
        }

        /* Returns the mergedtable that is located
         * at the specified coordinates. Table/Bar otherwise. */
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

            return componentOnPlan.Find(t => (((t is Table) || (t is Bar)) && t.X == x && t.Y == y));
        }

        /* Checks whether two points are within a Group area and if they are
         * returns that group area, otherwise returns null. */
        public GroupArea GetGroupArea(int x, int y)
        {
            // List of all group areas.
            var groupAreas = componentOnPlan.FindAll(c => c is GroupArea);

            foreach (var ga in groupAreas)
            {
                foreach (var point in (ga as GroupArea).Spots)
                {
                    if ((x == point.Coordinates.X && y == point.Coordinates.Y))
                    {
                        return (GroupArea)ga;
                    }
                }
            }

            return null;
        }

        /* Checks whether two points are within a Special area and if they are
        * returns that special area, otherwise returns null. */
        public SpecialArea GetSpecialArea(int x, int y)
        {
            // List of all group areas.
            var specialAreas = componentOnPlan.FindAll(c => c is SpecialArea);

            foreach (var areas in specialAreas)
            {
                foreach (var point in (areas as SpecialArea).Spots)
                {
                    if ((x == point.Coordinates.X && y == point.Coordinates.Y))
                    {
                        return (SpecialArea)areas;
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

        public bool RemoveComponent(Component c = null, SpecialArea sa = null)
        {
            if (c != null || sa != null)
            {
                if (c != null)
                {
                    foreach (Component com in componentOnPlan)
                    {
                        if (c.X == com.X && c.Y == com.Y)
                        {
                            if (c is Table && (c as Table).OnGA)
                            {
                                RemoveTableFromGA(c);
                            }

                            componentOnPlan.Remove(c);
                            return true;
                        }
                    }
                }
                else
                {
                    componentOnPlan.Remove(sa);
                    return true;
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

            var ga = GetSpecialArea(mergedTable.X, mergedTable.Y);

            if (CheckCoordinates(mergedTable.Coordinates) && ga is GroupArea)
            {
                componentOnPlan.Add(mergedTable);
                (ga as GroupArea).AddTable(mergedTable);

                foreach (Point p in mergedTable.Coordinates)
                {
                    var groupArea = GetSpecialArea(p.X, p.Y);

                    if (groupArea is GroupArea)
                    {
                        var spot = (groupArea as GroupArea).Spots.Find(s => s.Coordinates.Equals(p));
                        spot.Free = false;
                    }
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
                    var ga = GetSpecialArea(p.X, p.Y);

                    if (ga is GroupArea)
                    {
                        var spot = (ga as GroupArea).Spots.Find(s => s.Coordinates.Equals(p));
                        spot.Free = true;
                    }
                }
            }
            else
            {
                var ga = GetSpecialArea(table.X, table.Y);

                if (ga is SpecialArea)
                {
                    var spot = (ga as GroupArea).Spots.Find(s => s.Coordinates.Equals(new Point(table.X, table.Y)));
                    spot.Free = true;
                }
                
            }

            var area = GetSpecialArea(table.X, table.Y);

            if (area is GroupArea)
            {
                (area as GroupArea).RemoveTable(table);
            }
        }

        /// <summary>
        /// Unmerges merged table into it's original tables
        /// </summary>
        /// <param name="mt"></param>
        /// <param name="location"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if unmerged table, is unmerged completely
        /// </summary>
        /// <param name="mt"></param>
        /// <returns></returns>
        public bool ListCheck(Component mt)
        {
            if ((mt as MergedTable).Tables.Count != 0)
            {
                return true;
            }
            return false;
        }

        // Creates a Customer group on TimerTick.
        private CustomerGroup GenerateGroup(int dinnerTime, int lunchTime, int drinkTime)
        {
            var custGroup = new CustomerGroup(dinnerTime, lunchTime, drinkTime);

            return custGroup;
        }

        // Places group on available table that closely matches the size of the group.
        private bool CheckForFreeTable(CustomerGroup group)
        {
            var availableTable = componentOnPlan.Find(t => t is Table && (t as Table).Available &&
            (t as Table).TableSize >= group.GroupSize && !(t as Table).OnWA);
            var availableBar = componentOnPlan.Find(c => c is Bar && (c as Bar).Available && (c as Bar).GetSize() >= group.GroupSize);

            if (group.MealT != mealType.drinks)
            {
                if (availableTable != null)
                {
                    (availableTable as Table).SeatCustomersAtTable(group);
                    customerList.Add(group);
                }
                else
                {
                    // Check if there are any available waiting area tables.
                    var waitingAreaTable = componentOnPlan.Find(t => t is Table && (t as Table).Available &&
                    (t as Table).TableSize >= group.GroupSize && (t as Table).OnWA);

                    // If there is no table on waiting area for the group place group in the lobby.
                    if (waitingAreaTable != null)
                    {
                        (waitingAreaTable as Table).SeatCustomersAtTable(group);
                    }
                    else if (!lobby.AddCustGroupToLobby(group))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                if (availableBar != null)
                {
                    (availableBar as Bar).SeatCustomersAtTable(group);
                    customerList.Add(group);
                }
                else
                {
                    // Check if there are any available waiting area tables.
                    var waitingAreaTable = componentOnPlan.Find(t => t is Table && (t as Table).Available &&
                    (t as Table).TableSize >= group.GroupSize && (t as Table).OnWA);

                    // If there is no table on waiting area for the group place group in the lobby.
                    if (waitingAreaTable != null)
                    {
                        (waitingAreaTable as Table).SeatCustomersAtTable(group);
                    }
                    else if (!lobby.AddCustGroupToLobby(group))
                    {
                        return false;
                    }
                }

                return true;
            }

        }

        /* This method starts/resumes a simulation. The interval of the timer depends on the custFlow parameter.
         * After the interval elapses new custgroup will be generated. */
        public string StartSimulation(int custFlow, int lunchTime, int dinnerTime, int drinkTime, bool peakHour, bool runSimulation,
            TimeOfDay timeOfDay)
        {
            var tablesCount = componentOnPlan.Count(c => c is Table);

            if (tablesCount >= 1)
            {
                if (!pauseSim)
                {
                    customersSendAway = 0;
                    servedCustomers = 0;

                    stopWatch.Reset();

                    totalTimer.AutoReset = true;
                    totalTimer.Elapsed += TotalTimer_Elapsed;

                    secondsTimer.AutoReset = true;
                    secondsTimer.Interval = 1000;
                    secondsTimer.Elapsed += SecondsTimer_Elapsed;
                }
                else
                {
                    var takenTables = componentOnPlan.FindAll(t => t is Table && !(t as Table).Available && !(t as Table).OnWA);
                    var waitingTables = componentOnPlan.FindAll(t => t is Table && !(t as Table).Available && (t as Table).OnWA);

                    for (int i = 0; i < takenTables.Count; i++)
                    {
                        (takenTables[i] as Table).Customers.StartEating();
                    }

                    for (int i = 0; i < waitingTables.Count; i++)
                    {
                        (waitingTables[i] as Table).Customers.Wait();
                    }

                    foreach (var custGroup in lobby.GetGroupsInLobby())
                    {
                        custGroup.Wait();
                    }
                }

                if (!peakHour)
                {
                    totalTimer.Interval = custFlow * 1000;
                }
                else
                {
                    totalTimer.Interval = 500;
                }

                lunchDuration = lunchTime;
                dinnerDuration = dinnerTime;
                drinkDuration = drinkTime;

                stopWatch.Start();
                totalTimer.Start();
                secondsTimer.Start();

                return null;
            }
            else
            {
                return "Could not start simulation. You have to place at least one table on the plan!";
            }
        }

        private void SecondsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {

            var allWaitingTables = componentOnPlan.FindAll(t => t is Table && (t as Table).OnWA && !(t as Table).Available);
            var tableAvailability = componentOnPlan.Count(t => t is Table && (t as Table).Available && !(t as Table).OnWA);

            if ((lobby.Size != 0 || allWaitingTables.Count != 0) && tableAvailability != 0)
            {
                // Waiting tables are with priority.
                foreach (var table in allWaitingTables)
                {
                    var availableTable = componentOnPlan.Find(t => t is Table && (t as Table).Available &&
                    (t as Table).TableSize >= (table as Table).Customers.GroupSize && !(t as Table).OnWA);

                    if (availableTable != null)
                    {
                        (availableTable as Table).SeatCustomersAtTable((table as Table).Customers);
                        customerList.Add((table as Table).Customers);
                        (table as Table).Customers = null;
                        (table as Table).Available = true;
                    }
                }

                if (lobby.Size != 0)
                {
                    var lobbyGroups = lobby.GetGroupsInLobby();

                    for (int j = 0; j < lobbyGroups.Count; j++)
                    {
                        var availableTable = componentOnPlan.Find(t => t is Table && (t as Table).Available &&
                        (t as Table).TableSize >= lobbyGroups[j].GroupSize && !(t as Table).OnWA);

                        if (availableTable != null)
                        {
                            (availableTable as Table).SeatCustomersAtTable(lobbyGroups[j]);
                            customerList.Add(lobbyGroups[j]);
                            lobby.RemoveCustGroupFromLobby(lobbyGroups[j]);
                        }
                    }
                }

            }
        }

        // This is executed everytime the timer interval elapses.
        private void TotalTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var generatedGroup = GenerateGroup(dinnerDuration, lunchDuration, drinkDuration);

            if (!CheckForFreeTable(generatedGroup))
            {
                customersSendAway += generatedGroup.GroupSize;
            }
        }

        public void StopPauseSimulation(bool pause)
        {
            totalTimer.Stop();
            stopWatch.Stop();

            foreach (var customer in customerList)
            {
                if (customer != null)
                    customer.ClearTimers();
            }

            pauseSim = true;

            if (!pause)
            {
                customerList.Clear();
                lobby.ClearLobby();

                var tables = componentOnPlan.FindAll(table => table is Table && !(table as Table).Available);
                var bars = componentOnPlan.FindAll(bar => bar is Bar && !(bar as Bar).Available);

                for (int i = 0; i < tables.Count; i++)
                {
                    (tables[i] as Table).ClearTable();
                }
                for (int i = 0; i < bars.Count; i++)
                {
                    (bars[i] as Bar).ClearBar();
                }

                pauseSim = false;
            }
        }


        public void QuitWaiting(CustomerGroup group)
        {
            var inLobby = lobby.GetGroupsInLobby().Find(gr => gr.Equals(group));
            var atWaitingTable = componentOnPlan.Find(t => t is Table && (t as Table).OnWA &&
            (t as Table).Customers != null && (t as Table).Customers.Equals(group));

            if (inLobby != null)
            {
                customersSendAway += inLobby.GroupSize;
                lobby.RemoveCustGroupFromLobby(inLobby);
            }
            else if (atWaitingTable != null)
            {
                customersSendAway += (atWaitingTable as Table).Customers.GroupSize;
                (atWaitingTable as Table).ClearTable();
            }
        }

        public void FinishEating(CustomerGroup group)
        {
            var table = componentOnPlan.Find(t => t is Table && !(t as Table).Available && !(t as Table).OnWA &&
            (t as Table).Customers.Equals(group));

            if (table != null && (table as Table).Customers != null && customerList.Count > 0)
            {
                customerList.Remove(group);
                servedCustomers += (table as Table).Customers.GroupSize;
                (table as Table).ClearTable();
            }
        }
        public void FinishDrinking(CustomerGroup group)
        {
            var bar = componentOnPlan.Find(c => c is Bar && !(c as Bar).Available &&
            (c as Bar).Customers.Equals(group));

            if (bar != null && (bar as Bar).Customers != null && customerList.Count > 0)
            {
                customerList.Remove(group);
                servedCustomers += (bar as Bar).Customers.GroupSize;
                (bar as Bar).ClearBar();
            }
        }

        public List<CustomerGroup> LobbyCustomers()
        {
            return lobby.GetGroupsInLobby();
        }

        public TimeSpan GetSimulationRunTime()
        {
            return stopWatch.Elapsed;
        }

        // Save stats to a text file.
        public string ExportStatistics()
        {
            return String.Format("Served customers: {0}{1}Customers sent away: {2}{3}Time simulation ran: {4}",
                servedCustomers, Environment.NewLine, customersSendAway, Environment.NewLine,
                GetSimulationRunTime().ToString(@"hh\:mm\:ss"));
        }

        public string SaveSimulation(string filename)
        {
            Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write);

            try
            {
                IFormatter formatter = new BinaryFormatter();

                RestaurantPlan plan = RestaurantPlan.Instance;
                formatter.Serialize(stream, plan);

                return "Simulation successfully saved!";
            }
            catch (SerializationException ex)
            {
                return String.Format("Serialization failed: {0}", ex.Message);
            }
            finally
            {
                stream.Close();
            }
        }

        public string LoadSimulation(string filename)
        {
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            try
            {
                IFormatter formatter = new BinaryFormatter();

                RestaurantPlan save = (RestaurantPlan) formatter.Deserialize(stream);

                // Clear all the lists
                customerList.Clear();
                componentOnPlan.Clear();
                lobby.ClearLobby();

                // Populate all the lists with the saved objects
                LoadComponents(save.componentOnPlan);

                var groupArea = save.componentOnPlan.FirstOrDefault(c => c is GroupArea);
                var gaTables = save.componentOnPlan.FindAll(t => t is Table && (t as Table).OnGA);
                LoadGroupAreaTables(groupArea, gaTables);

                var sma = save.componentOnPlan.FirstOrDefault(c => c is SmokeArea);
                var smaTables = save.componentOnPlan.FindAll(t => t is Table && (t as Table).OnSA);
                LoadSmokingAreaTables(sma, smaTables);

                var wa = save.componentOnPlan.FirstOrDefault(c => c is WaitingArea);
                var waTables = save.componentOnPlan.FindAll(t => t is Table && (t as Table).OnWA);
                LoadWaitingAreaTables(wa, waTables);

                return "Simulation loaded successfully";
            }
            catch (SerializationException ex)
            {
                return String.Format("Deserialization failed: {1}", ex.Message);
            }
            finally
            {
                stream.Close();
            }
        }

        // Assigns back the timers and stopwatch to the RestaurantPlan instance.
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            stopWatch = new Stopwatch();
            totalTimer = new Timer();
            secondsTimer = new Timer();
        }

        // Method for loading all the components from a saved file.
        private void LoadComponents(List<Component> allComponents)
        {
            for (int i = 0; i < allComponents.Count; i++)
            {
                componentOnPlan.Add(allComponents[i]);
            }
        }

        // Loads the tables in the group area.
        private void LoadGroupAreaTables(Component ga, List<Component> tables)
        {
            if (ga != null)
            {
                (ga as GroupArea).LoadTableList(tables);
            }
        }

        // Loads the tables in the smoking area.
        private void LoadSmokingAreaTables(Component sma, List<Component> tables)
        {
            if (sma != null)
            {
                (sma as SmokeArea).LoadTableList(tables);
            }
        }

        // Loads the tables in the waiting area.
        private void LoadWaitingAreaTables(Component wa, List<Component> tables)
        {
            if (wa != null)
            {
                (wa as WaitingArea).LoadTableList(tables);
            }
        }
    }
}
