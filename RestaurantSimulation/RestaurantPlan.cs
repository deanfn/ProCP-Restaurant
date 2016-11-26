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
                if ((c.x == com.x && c.y == com.y) || ((c.x * 40) + 40)/40 == com.x || ((c.x * 40) - 40) / 40 == com.x ||  ((c.y * 40) + 40) / 40 == com.y || ((c.y * 40) - 40) / 40 == com.y)
                {
                    MessageBox.Show("Please Select Free Spot!");
                    return false;
                }
            }

            this.componentOnPlan.Add(c);
            return true;
        }

        public Component GetComponent(int x, int y)
        {
            foreach (Component c in componentOnPlan)
            {
                if(c.x==x && c.y==y)
                {
                    return c;
                }
            }
            return null;
        }
    }
}
