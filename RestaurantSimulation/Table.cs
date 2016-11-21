using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    class Table
    {
        public int id;
        public int size;
        public bool available;
        public bool merging;
        private static int count = 0;
        
        /// <summary>
        /// Creates new Table object, with unique ID
        /// </summary>
        /// <param name="size"></param>
        /// <param name="avail"></param>
        /// <param name="merg"></param>
        public Table(int size, bool merg)
        {
            this.size = size;
            this.available = true;
            this.merging = merg;
            this.id = count;
            count++;
        }

        /// <summary>
        /// Returns Table objects size and ID
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "size: " + size + ", id: " + id;
        }
    }
}
