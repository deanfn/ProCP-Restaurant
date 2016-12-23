using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RestaurantSimulation
{
    enum mealType { lunch, dinner, drinks };

    class CustomerGroup
    {
        private mealType? meal = null;
        private Timer t;
        private static int id = 0;

        public int ID { get; set; }
        public int GroupSize { get; set; }

        public CustomerGroup(int gSize, int dinnerT, int lunchT)
        {
            id++;
            /* When a group of customers come to the restaurant it will be assigned an ID,
             * if there is available table that ID will be replaced with the table ID, if not
             * the ID will remain until a table becomes available. */
            this.ID = id;
            this.GroupSize = gSize;
            this.meal = DinnerOrLunch(meal);
            t = new Timer(SetInterval(meal,dinnerT,lunchT));
            t.Start();
            t.Enabled = true;
            t.Elapsed += OnTimedEvent;          
        }

        public int SetInterval(mealType? m, int dinnerT, int lunchT)
        {
            int interval = 0;
            if (m == mealType.drinks || m == mealType.dinner)
            {
                return interval = dinnerT * 1000;
            }
            else return interval = lunchT * 1000;
        }

        public mealType? DinnerOrLunch(mealType? m)
        {
            Random r = new Random();

            switch(r.Next(1,4))
            {
                case 1:
                    return m = mealType.dinner;
                case 2:
                    return m = mealType.lunch;
                case 3:
                    return m = mealType.drinks;
            }
            return m;
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //Implementation for what happens, when timer reaches it's interval.
            /*TO DO*/
            t.Stop();
            
        }

    }
}
