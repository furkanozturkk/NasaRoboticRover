using System;
using System.Linq;
using System.Collections.Generic;
namespace NasaRoboticRover
{
    public static class Validators
    {

        public static Location MapCoordinateParser(string cordinates)
        {
            var parameters = cordinates.Split(' ').ToList();
            if (parameters.Count != 2)
            {
                throw new Exception("Harita Kordinatı Hatalı => '" + cordinates + "'");
            }
            bool isParsedX = int.TryParse(parameters[0], out int x);
            bool isParsedY = int.TryParse(parameters[1], out int y);

            bool isNumbersNotNegative = (x >= 0 && y >= 0);
            return isNumbersNotNegative && isParsedX && isParsedY ? new Location(x, y) : throw new Exception("Harita Kordinatı Hatalı => '" + cordinates + "'");
        }
        public static Tuple<Location, Direction> LocationParser(string locationString)
        {
            var parameters = locationString.Split(' ').ToList();
            if (parameters.Count != 3)
            {
                throw new Exception("Araç Konumu Hatalı => " + locationString);
            }
            bool isParsedX = int.TryParse(parameters[0], out int x);
            bool isParsedY = int.TryParse(parameters[1], out int y);
            bool isParsedDirection = Enum.TryParse(parameters[2].ToUpper(), out Direction direction);

            bool isNumbersNotNegative = (x >= 0 && y >= 0);
            var result = isNumbersNotNegative && isParsedX && isParsedY && isParsedDirection ? Tuple.Create(new Location(x, y), direction) : throw new Exception("Araç Konumu Hatalı => " + locationString);
            return result;
        }
        public static bool ActionValidator(string roverAction)
        {
            var actions = roverAction.ToCharArray().ToList();
            var validActions = new List<string>() { "L", "R", "M" };
            bool isValid = true;
            actions.ForEach(action =>
            {
                if (!validActions.Contains(action.ToString().ToUpper()))
                {
                    isValid = false;
                    return;
                }
            });
            return isValid;
        }
        public static bool MapOverflowValidator(Map map, IVehicle vehicle)
        {
            var condition = !(vehicle.Location.X > map.Width || vehicle.Location.Y > map.Height || vehicle.Location.X < 0 || vehicle.Location.Y < 0);

            return condition;
        }
    }

}