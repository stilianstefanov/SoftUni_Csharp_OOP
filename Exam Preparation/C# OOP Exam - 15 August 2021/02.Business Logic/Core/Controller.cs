namespace CarRacing.Core
{
    using System;
    using System.Text;
    using System.Linq;

    using Models.Cars.Contracts;
    using Models.Racers.Contracts;
    using Repositories.Contracts;
    using Contracts;
    using Models.Maps.Contracts;
    using Models.Cars;
    using Utilities.Messages;
    using Models.Racers;
    using Repositories;
    using Models.Maps;

    public class Controller : IController
    {
        private IRepository<ICar> cars;
        private IRepository<IRacer> racers;
        private IMap map;

        public Controller()
        {
            cars = new CarRepository();
            racers= new RacerRepository();
            map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar newCar = null;
            switch (type)
            {
                case "SuperCar":
                    newCar = new SuperCar(make, model, VIN, horsePower);
                    break;
                case "TunedCar":
                    newCar = new TunedCar(make, model, VIN, horsePower);
                    break;
                default:
                    throw new ArgumentException(ExceptionMessages.InvalidCarType);                   
            }

            cars.Add(newCar);

            return string.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            var carToRacer = cars.FindBy(carVIN);
            if (carToRacer == null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }

            IRacer newRacer = null;
            switch (type)
            {
                case "ProfessionalRacer":
                    newRacer = new ProfessionalRacer(username, carToRacer);
                    break;
                case "StreetRacer":
                    newRacer = new StreetRacer(username, carToRacer);
                    break;
                default:
                    throw new ArgumentException(ExceptionMessages.InvalidRacerType);                    
            }

            racers.Add(newRacer);

            return string.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            var racerOne = racers.FindBy(racerOneUsername);
            if (racerOne == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            }

            var racerTwo = racers.FindBy(racerTwoUsername);
            if (racerTwo == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            }

            return map.StartRace(racerOne, racerTwo);
        }

        public string Report()
        {
            var sb = new StringBuilder();

            foreach (var racer in racers.Models.OrderByDescending(r => r.DrivingExperience).ThenBy(r => r.Username))
            {
                sb.AppendLine(racer.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
