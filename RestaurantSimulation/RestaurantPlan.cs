using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantSimulation
{
    class RestaurantPlan
    {
        private List<Component> componentOnPlan = new List<Component>();

        public bool AddComponent(Component c)
        {
            foreach (Component com in componentOnPlan)
            {
                if ((c.x == com.x && c.y == com.y))
                {
                    MessageBox.Show("Please Select Free Spot!");
                    return false;
                }
            }

            this.componentOnPlan.Add(c);
            return true;
        }
    }
}
