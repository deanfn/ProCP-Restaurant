﻿using System;
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
        public int X { get; set; }
        public int Y { get; set; }

        public Component(Point p)
        {
            this.X = p.X / 40;
            this.Y = p.Y / 40;
        }

        public virtual void Drawing(ref PictureBox pb) { }
        public virtual void DecreaseCount() { }
        public virtual void DecreaseID() { }
        
        
    }
}
