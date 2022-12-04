namespace EasterRaces.Core.Entities
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Cars.Contracts;
    using Models.Cars.Entities;
    using Models.Drivers.Entities;
    using Models.Races.Entities;
    using Repositories.Entities;
    using Utilities.Messages;

    public class ChampionshipController : IChampionshipController
    {
        private CarRepository cars;
        private DriverRepository drivers;
        private RaceRepository races;

        public ChampionshipController()
        {
            cars= new CarRepository();
            drivers= new DriverRepository();
            races= new RaceRepository();
        }

        public string AddCarToDriver(string driverName, string carModel)
        {
            var driverToAddCar = drivers.GetByName(driverName);
            var carToAdd = cars.GetByName(carModel);

            if (driverToAddCar == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            if (carToAdd == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));

            driverToAddCar.AddCar(carToAdd);
            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            var raceToAddDriver = races.GetByName(raceName);
            var driverToAdd = drivers.GetByName(driverName);

            if (raceToAddDriver == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            if (driverToAdd == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));

            raceToAddDriver.AddDriver(driverToAdd);
            return string.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if (cars.GetByName(model) != null)
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));

            ICar newCar = null;
            switch (type)
            {
                case "Muscle":
                    newCar = new MuscleCar(model, horsePower);
                    break;
                case "Sports":
                    newCar = new SportsCar(model, horsePower);
                    break;
            }

            cars.Add(newCar);
            return string.Format(OutputMessages.CarCreated, newCar.GetType().Name, model);
        }

        public string CreateDriver(string driverName)
        {
            if (drivers.GetByName(driverName) != null)
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));

            var newDriver = new Driver(driverName);
            drivers.Add(newDriver);

            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateRace(string name, int laps)
        {
            if (races.GetByName(name) != null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));

            var newRace = new Race(name, laps);
            races.Add(newRace);
            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string StartRace(string raceName)
        {
            var raceToExecute = races.GetByName(raceName);
            if (raceToExecute == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));

            if (raceToExecute.Drivers.Count < 3)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, 3));

            var orderedWinners = raceToExecute.Drivers
                .OrderByDescending(d => d.Car.CalculateRacePoints(raceToExecute.Laps))
                .Take(3)
                .ToList();

            races.Remove(raceToExecute);

            var sb = new StringBuilder();
            sb.AppendLine($"Driver {orderedWinners[0].Name} wins {raceName} race.");
            sb.AppendLine($"Driver {orderedWinners[1].Name} is second in {raceName} race.");
            sb.AppendLine($"Driver {orderedWinners[2].Name} is third in {raceName} race.");

            return sb.ToString().TrimEnd();
        }
    }
}
