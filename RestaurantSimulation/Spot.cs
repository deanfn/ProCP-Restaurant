using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RestaurantSimulation
{
    [Serializable]
    class Spot
    {
        public bool Free { get; set; }

        public Point Coordinates { get; set; }

        public Spot(Point loc)
        {
            Free = true;
            Coordinates = loc;
        }
    }
}
