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
        private Timer t, wait;
        private static int id = 0;

        public int ID { get; set; }
        public int GroupSize { get; set; }

        private RestaurantPlan restaurantPlan;
        {
            id++;
            /* When a group of customers come to the restaurant it will be assigned an ID,
             * if there is available table that ID will be replaced with the table ID, if not
             * the ID will remain until a table becomes available. */
            this.ID = id;
            this.GroupSize = gSize;
            this.meal = DinnerOrLunch(meal);
            t = new Timer(SetInterval(meal,dinnerT,lunchT));
            t.Elapsed += OnTimedEvent;

            // Timer for waiting for available table;
            wait = new Timer();
            t.Elapsed += Leave;
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

        public void StartEating()
        {
            t.Start();
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            t.Stop();
            restaurantPlan.FinishEating(this);
            //RestaurantPlan.Instance.FinishEating(this);
        }

        // Waiting time is randomly generated and is between 15 and 24 seconds.
        public void Wait()
        {
            Random rand = new Random();

            wait.Interval = rand.Next(15, 25) * 1000;
            wait.Start();
        }

        public void StopWaiting()
        {
            wait.Stop();
        }

        public void Leave(object sender, ElapsedEventArgs e)
        {
            StopWaiting();
            restaurantPlan.QuitWaiting(this);
            //RestaurantPlan.Instance.QuitWaiting(this);
        }

    }
}
