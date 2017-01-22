using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    [Serializable]
    class SpecialArea : Component
    {
        // List of spots
        public List<Spot> Spots { get; set; }

        public SpecialArea(Point coordinates) : base(coordinates)
        {
            Spots = new List<Spot>();
            Spots.Add(new Spot(new Point(coordinates.X / 40, coordinates.Y / 40)));
            Spots.Add(new Spot(new Point(coordinates.X / 40 + 1, coordinates.Y / 40)));
            Spots.Add(new Spot(new Point(coordinates.X / 40 + 1, coordinates.Y / 40 + 1)));
            Spots.Add(new Spot(new Point(coordinates.X / 40, coordinates.Y / 40 + 1)));
        }

        // No need to write implementation.
        public override void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }

        // This method is used to add tables to the static lists of the special area classes
        public virtual bool AddTable(Component c)
        {
            foreach (var spot in Spots)
            {
                if (spot.Coordinates.X == c.X && spot.Coordinates.Y == c.Y && spot.Free)
                {
                    spot.Free = false;
                    return true;
                }
            }

            return false;
        }

        /* Clears the static list in the child classes and populates
         * the list with previously saved tables.
         * It is up to the child classes to provide implementation */
        public virtual void LoadTableList(List<Component> tables)
        {

        }

        public override int GetSize()
        {
            throw new NotImplementedException();
        }
    }
}
