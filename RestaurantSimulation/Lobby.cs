using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    sealed class Lobby
    {
        private static readonly Lobby instance = new Lobby();
        private readonly int lobbyCapacity = 6;
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

        public bool AddCustGroupToLobby(CustomerGroup group)
        {
            if (Size < lobbyCapacity && (Size + group.GroupSize) < lobbyCapacity)
            {
                customers.Add(group);
                Size += group.GroupSize;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
