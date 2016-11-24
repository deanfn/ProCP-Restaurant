using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSimulation
{
    class Component
    {
        //Properties to Get X and Y Location
        public int x { get; set; }
        public int y { get; set; }

        public Component(Point p)
        {
            this.x = p.X / 40;
            this.y = p.Y / 40;
        }

        public virtual void Drawing(ref PictureBox pb)
        {

        }
    }
}
