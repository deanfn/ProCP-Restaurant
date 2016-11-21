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
                if ((c.x1 == com.x1 && c.y1 == com.y1) || (c.x1 == com.x2 && c.y1 == com.y2) || (c.x2 == com.x1 && c.y2 == com.y1))
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
