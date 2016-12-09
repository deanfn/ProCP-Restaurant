using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSimulation
{
    abstract class Component
    {
        //Properties to Get X and Y Location
        public int X { get; set; }
        public int Y { get; set; }

        public Component(Point p)
        {
            this.X = p.X / 40;
            this.Y = p.Y / 40;
        }

        public abstract void Drawing(Graphics g);
        public abstract void DecreaseCount();
        public abstract void DecreaseID();
        public abstract int GetSize();
        public abstract List<int> getXpointList();
        public abstract List<int> getYpointList();
    }
}
