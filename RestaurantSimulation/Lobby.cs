using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    [Serializable]
    sealed class Lobby
    {
        private static readonly Lobby instance = new Lobby();
        private readonly int lobbyCapacity = 20;
        private List<CustomerGroup> customers;

        // Property to get the instance
        public static Lobby Instance
        {
            get
            {
                return instance;
            }
        }

        public int Size { get; set; }

        private Lobby()
        {
            customers = new List<CustomerGroup>();
        }

        // Static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Lobby()
        {
        }

        // Clears the lobby list. This method is used when a new simulation is starting.
        public void ClearLobby()
        {
            customers.Clear();
            Size = 0;
        }

        public bool AddCustGroupToLobby(CustomerGroup group)
        {
            if (Size < lobbyCapacity && (Size + group.GroupSize) < lobbyCapacity)
            {
                customers.Add(group);
                Size += group.GroupSize;
                group.Wait();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveCustGroupFromLobby(CustomerGroup group)
        {
            Size -= group.GroupSize;
            customers.Remove(group);
        }

        public List<CustomerGroup> GetGroupsInLobby()
        {
            return customers;
        }
    }
}
