using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    class CustomerGroup
    {
        private static int id = 0;

        /* How large is the group. Groups can be minimum of 1 person
         * and maximum of 16 people. */
        public int Size { get; set; }

        // ID of the group
        public int GroupID { get; set; }

        public CustomerGroup(int size)
        {
            id++;
            GroupID = id;
            Size = size;
        }
    }
}
