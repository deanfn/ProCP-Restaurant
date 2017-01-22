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
        private Random r = new Random();
        [NonSerialized]
        private Timer t, wait;
        private static int id = 0;
        // Fields for the lunch and dinner times.
        private int lunchTime;
        private int dinnerTime;
        private int drinkTime;

        public int ID { get; set; }
        public int GroupSize { get; set; }

        public mealType? MealT
        {
            get { return meal; }
        }

        public CustomerGroup(int dinnerT, int lunchT, int drinkT)
        {
            id++;
            /* When a group of customers come to the restaurant it will be assigned an ID,
             * if there is available table that ID will be replaced with the table ID, if not
             * the ID will remain until a table becomes available. */
            this.ID = id;
            this.meal = DinnerOrLunch(meal);
            this.GroupSize = GenerateGroupSize(meal);
            lunchTime = lunchT;
            dinnerTime = dinnerT;
            drinkTime = drinkT;

            t = new Timer(SetInterval(meal, dinnerT, lunchT, drinkT));
            t.Elapsed += OnTimedEvent;

            // Timer for waiting for available table;
            wait = new Timer();
            wait.Elapsed += Leave;
        }
        public int GenerateGroupSize(mealType? m)
        {

            int size = 0;
            if (m != mealType.drinks)
            {
                /* If the number is less or equal to 5 the group will be with size 1 to 4 people,
                 * else the group will be larger, up to 16 people. */
                if (r.Next(1, 11) <= 5)
                {
                    return size = r.Next(1, 5);
                }
                else
                {
                    return size = r.Next(5, 17);
                }
            }
            else
            {
                return size = r.Next(1, 5);
            }

        }

        public int SetInterval(mealType? m, int dinnerT, int lunchT, int drinkT)
        {
            int interval = 0;
            switch (m)
            {
                case mealType.drinks:
                    return interval = drinkT * 1000;
                case mealType.lunch:
                    return interval = lunchT * 1000;
                case mealType.dinner:
                    return interval = dinnerT * 1000;
            }
            return 0;
        }

        public mealType? DinnerOrLunch(mealType? m)
        {
            if (RestaurantPlan.Instance.TimeOfDay == TimeOfDay.afternoon)
            {
                m = r.Next(1, 6) <= 4 ? mealType.lunch : mealType.drinks;
            }
            else
            {
                m = r.Next(1, 20) <= 13 ? mealType.dinner : mealType.drinks;
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
            if (meal != mealType.drinks)
            {
                RestaurantPlan.Instance.FinishEating(this);
            }
            else
                RestaurantPlan.Instance.FinishDrinking(this);

        }

        // Waiting time is randomly generated and is between 15 and 24 seconds.
        public void Wait()
        {
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
            t = new Timer(SetInterval(meal, dinnerTime, lunchTime, drinkTime));
            t.Elapsed += OnTimedEvent;

            wait = new Timer();
            wait.Elapsed += Leave;
        }

    }
}
