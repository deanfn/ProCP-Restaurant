using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    class Bar
    {
        private int id;
        private static int count;
        public int size;
        public bool available;

        /// <summary>
        /// Creates new Bar object
        /// </summary>
        /// <param name="size"></param>
        public Bar(int size)
        {
            this.size = size;
            this.id = count;
            this.available = true;
            count++;
        }
    }
}
