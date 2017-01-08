using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.Serialization;

namespace RestaurantSimulation
{
    enum mealType { lunch, dinner, drinks };

    [Serializable]
    class CustomerGroup
    {
        private mealType? meal = null;

        [NonSerialized]
        private Timer t, wait;
        private static int id = 0;

        // Fields for the lunch and dinner times.
        private int lunchTime;
        private int dinnerTime;

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
            lunchTime = lunchT;
            dinnerTime = dinnerT;

            t = new Timer(SetInterval(meal,dinnerT,lunchT));
            t.Elapsed += OnTimedEvent;

            // Timer for waiting for available table;
            wait = new Timer();
            wait.Elapsed += Leave;
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
            t.Dispose();
            RestaurantPlan.Instance.FinishEating(this);
        }

        // Waiting time is randomly generated and is between 15 and 24 seconds.
        public void Wait()
        {
            Random rand = new Random();

            wait.Interval = 10 * 1000;
            wait.Start();
        }

        public void StopWaiting()
        {
            wait.Stop();
            wait.Dispose();
        }

        public void Leave(object sender, ElapsedEventArgs e)
        {
            StopWaiting();
            RestaurantPlan.Instance.QuitWaiting(this);
        }

        public void ClearTimers()
        {
            t.Stop();
            wait.Stop();
        }

        // Assign the timers objects after deserialization.
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            t = new Timer(SetInterval(meal, dinnerTime, lunchTime));
            t.Elapsed += OnTimedEvent;

            wait = new Timer();
            wait.Elapsed += Leave;
        }

    }
}
