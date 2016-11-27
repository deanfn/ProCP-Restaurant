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
                
                if ((c.X == com.X && c.Y == com.Y) || c.X + 1 == com.X && c.Y == com.Y || c.X - 1 == com.X && c.Y == com.Y || c.X == com.X && c.Y + 1 == com.Y ||
                    c.X == com.X && c.Y - 1 == com.Y || c.X + 1 == com.X && c.Y + 1 == com.Y || c.X -1 == com.X && c.Y - 1 == com.Y || c.X + 1 == com.X && c.Y - 1 == com.Y || c.X - 1 == com.X && c.Y + 1 == com.Y)
                {

                    if ((com is GroupArea || com is SmokeArea) && c is Bar)
                    {
                        MessageBox.Show("The bar can be placed only outside of special areas!");
                        return false;
                    }

                    else if (c.X == com.X && c.Y == com.Y && com is GroupArea && c is Table)
                    {
                        if ((com as GroupArea).AddTable(c))
                        {
                            break;
                        }
                    }

                    else if (c.X == com.X && c.Y == com.Y && com is SmokeArea && c is Table)
                    {
                        if ((com as SmokeArea).AddTable(c))
                        {
                            break;
                        }
                    }

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
                if(c.X == x && c.Y == y)
                {
                    return c;
                }
            }
            return null;
        }
    }
}
