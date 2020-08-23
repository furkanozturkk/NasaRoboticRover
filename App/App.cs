using System;
using System.Collections.Generic;
using System.Linq;

namespace NasaRoboticRover
{
    public class App
    {
        private Map Map { get; set; }
        private List<IVehicle> Rovers { get; set; }
        public App()
        {
            Map = new Map();
            Rovers = new List<IVehicle>();
        }
        private IVehicle CurrentVehicle { get; set; }
        public void Init()
        {

            Console.WriteLine("Hoşgeldiniz.");
            Console.WriteLine("Lütfen Koordinatlar ve Yönler Arasında Birer Boşluk Bırakarak Giriniz.");
            Console.WriteLine("Girdiler Sonlanınca Cevap Almak İçin exit komutunu girmelisiniz.");
            var state = "getMapCoordinate";

            while (true)
            {
                try
                {
                    switch (state)
                    {
                        case "getMapCoordinate":
                            {
                                Console.WriteLine("Lütfen Haritanın Sağ Üst Konumunu Giriniz.");
                                var mapCordinates = Console.ReadLine();
                                if (mapCordinates.ToLower() == "exit")
                                {
                                    state = "exit";
                                    break;
                                }
                                var data = Validators.MapCoordinateParser(mapCordinates);
                                this.Map.Height = data.Y;
                                this.Map.Width = data.X;

                                state = "getRoverCoordinate";
                                break;
                            }
                        case "getRoverCoordinate":
                            {
                                Console.WriteLine("Lütfen Gezicinin Koordinat Bilgilerini ve Yönünü Giriniz.");
                                CurrentVehicle = new Rover();
                                var roverCoordinate = Console.ReadLine();
                                if (roverCoordinate.ToLower() == "exit")
                                {
                                    state = "exit";
                                    break;
                                }
                                var data = Validators.LocationParser(roverCoordinate);
                                CurrentVehicle.Location = data.Item1;
                                CurrentVehicle.Direction = data.Item2;
                                bool isValid = Validators.MapOverflowValidator(this.Map, CurrentVehicle);
                                if (!isValid)
                                {
                                    throw new Exception("Geçersiz Konum => '" + roverCoordinate + "'");
                                }
                                state = "getRoverActions";
                                break;
                            }
                        case "getRoverActions":
                            {
                                Console.WriteLine("Lütfen Gezicinin Hareket Bilgilerini Giriniz.");
                                var roverActions = Console.ReadLine();
                                if (roverActions.ToLower() == "exit")
                                {
                                    state = "exit";
                                    break;
                                }
                                if (Validators.ActionValidator(roverActions))
                                {
                                    this.RunAction(CurrentVehicle, roverActions);
                                    this.Rovers.Add(CurrentVehicle);
                                    state = "getRoverCoordinate";
                                }
                                else
                                {
                                    Console.WriteLine("Gezicinin Hareket Bilgilerini Hatalı. => '" + roverActions + "'");
                                }

                                break;
                            }
                        case "exit":
                            {
                                this.WriteResult();
                                return;
                            }
                        default: break;
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }




        }
        private void WriteResult()
        {
            this.Rovers.ForEach(rover =>
            {
                Console.WriteLine(rover.GetCoordinate());
            });
        }
        public void RunAction(IVehicle vehicle, string actionString)
        {
            var actions = actionString.ToCharArray().ToList();
            var temp = vehicle;
            actions.ForEach(action =>
            {
                switch (action.ToString().ToUpper())
                {
                    case "M":
                        {
                            vehicle.Move();
                            break;
                        }
                    case "L":
                    case "R":
                        {
                            bool isParsed = Enum.TryParse(action.ToString().ToUpper(), out Turn turn);
                            if (isParsed)
                                vehicle.Rotate(turn);
                            else
                                throw new Exception("Geçersiz Aksiyon => '" + action + "'");
                            break;
                        }
                    default: throw new Exception("Geçersiz Aksiyon => '" + action + "'");
                }
            });
            bool isValid = Validators.MapOverflowValidator(this.Map, vehicle);
            if (!isValid)
            {
                vehicle = temp;
                throw new Exception("Gezici Harita Dışına Çıktı.Geçersiz Aksiyon => '" + actionString + "'");
            }
        }
    }
}