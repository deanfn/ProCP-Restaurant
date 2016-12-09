using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    class SpecialAreas : Component
    {
        // Boolean array to check if there are free spots in the group area
        public bool[] FreeSpots { get; set; }

        // Coordinates for every spot in the area. On click four group area spots are drawn
        public List<Point> Coordinates { get; set; }

        public SpecialAreas(Point coordinates) : base(coordinates)
        {
            FreeSpots = new bool[4];
            Coordinates = new List<Point>();

            Coordinates.Add(new Point(coordinates.X / 40, coordinates.Y / 40));
            Coordinates.Add(new Point(coordinates.X / 40 + 1, coordinates.Y / 40));
            Coordinates.Add(new Point(coordinates.X / 40 + 1, coordinates.Y / 40 + 1));
            Coordinates.Add(new Point(coordinates.X / 40, coordinates.Y / 40 + 1));

            for (int i = 0; i < FreeSpots.Length; i++)
            {
                FreeSpots[i] = true;
            }
        }

        // No need to write implementation.
        public override void Drawing(Graphics g)
        {
            throw new NotImplementedException();
        }

        // This method is used to add tables to the static lists of the special area classes
        public virtual bool AddTable(Component c)
        {
            for (int i = 0; i < FreeSpots.Length; i++)
            {
                if (Coordinates[i].X == c.X && Coordinates[i].Y == c.Y && FreeSpots[i])
                {
                    FreeSpots[i] = false;
                    return true;
                }
            }

            return false;
        }

        // Implementation is not necessary. Areas don't have IDs.
        public override void DecreaseCount()
        {
            throw new NotImplementedException();
        }

        // Implementation is not necessary. Areas don't have IDs.
        public override void DecreaseID()
        {
            throw new NotImplementedException();
        }

        public override int GetSize()
        {
            throw new NotImplementedException();
        }

        public override List<int> getXpointList()
        {
            throw new NotImplementedException();
        }

        public override List<int> getYpointList()
        {
            throw new NotImplementedException();
        }
    }
}
