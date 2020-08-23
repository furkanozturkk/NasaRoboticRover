using System;
using System.Linq;
namespace NasaRoboticRover
{

    public class Rover : IVehicle
    {
        public Direction Direction { get; set; }
        public Location Location { get; set; }
        public void Move()
        {
            int x = 0, y = 0;
            switch (this.Direction)
            {
                case Direction.N:
                    {
                        y++;
                        break;
                    }
                case Direction.W:
                    {
                        x--;
                        break;
                    }
                case Direction.S:
                    {
                        y--;
                        break;
                    }
                case Direction.E:
                    {
                        x++;
                        break;
                    }
                default: throw new Exception("Geçersiz Hareket. => '" + this.Direction + "'");
            }
            this.Location.X += x;
            this.Location.Y += y;
        }

        public string GetCoordinate()
        {
            return $"{this.Location.X} {this.Location.Y} {this.Direction.ToString()}";
        }
        public void Rotate(Turn turn)
        {
            switch (turn)
            {
                case Turn.L:
                    {
                        this.Direction = (Direction)(((int)Direction + 1) % 4);
                        break;
                    }
                case Turn.R:
                    {
                        this.Direction = (Direction)(((int)Direction == 0 ? 3 : (int)Direction - 1) % 4);   
                        break;
                    }
                default: throw new Exception("Geçersiz Yön. => '" + turn + "'");
            }
        }
    }

}
