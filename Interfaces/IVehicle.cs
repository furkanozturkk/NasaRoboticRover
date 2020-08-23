namespace NasaRoboticRover{
    public interface IVehicle{
        void Move();
        void Rotate(Turn turn);
        Location Location{get;set;}
        Direction Direction{get;set;}
        string GetCoordinate();
    }
}