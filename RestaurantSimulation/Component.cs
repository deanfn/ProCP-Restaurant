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
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }

        public Component(Point p)
        {
            this.x1 = p.X / 40;
            this.y1 = p.Y / 40;
        }

        public virtual void Drawing(ref PictureBox pb)
        {

        }
    }
}
