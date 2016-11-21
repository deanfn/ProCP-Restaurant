using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    class MergedTable
    {
        public int size;
        public List<Table> tablel;
        private static int count = 0;
        private int id;

        /// <summary>
        /// Creates MergedTable object from 2 Table objects
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        public MergedTable(Table t1, Table t2)
        {
            tablel = new List<Table>();
            tablel.Add(t1);
            tablel.Add(t2);
            getSize(this);
            this.id = count;
            count++;
        }

        /// <summary>
        /// Extends size of MergedTable object by adding another Table object
        /// </summary>
        /// <param name="mt1"></param>
        /// <param name="t1"></param>
        public MergedTable(MergedTable mt1, Table t1)
        {
            mt1.tablel.Add(t1);
            extendTable(mt1);
        }

        /// <summary>
        /// Calculates Merged Table size
        /// </summary>
        /// <param name="t1"></param>
        private void getSize(MergedTable t1)
        {
            int temp = 0;
            for (int i = 0; i <= t1.tablel.Count - 1; i++)
            {
                temp += t1.tablel[i].size;
            }
            size = temp;
        }

        /// <summary>
        /// Extends Merged Table object size
        /// </summary>
        /// <param name="mt1"></param>
        private void extendTable(MergedTable mt1)
        {
            int temp = 0;
            for (int i = 0; i <= mt1.tablel.Count - 1; i++)
            {
                temp += mt1.tablel[i].size;
            }
            if (temp <= 20)
            {
                size = temp;
            }
            else throw new OutOfRangeException("Table can't hold more than 20 seats.");
        }
    }
}
