namespace Formula1.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models;
    using Utilities;
    using Models.Contracts;
    using Repositories.Contracts;
    using Repositories;

    public class Controller : IController
    {
        private IRepository<IPilot> pilots;
        private IRepository<IRace> races;
        private IRepository<IFormulaOneCar> cars;

        public Controller()
        {
            pilots = new PilotRepository();
            races = new RaceRepository();
            cars = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            var pilot = pilots.FindByName(pilotName);
            if (pilot == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }
            if (pilot.Car != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }
            var car = cars.FindByName(carModel);
            if (car == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            pilot.AddCar(car);
            cars.Remove(car);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            var race = races.FindByName(raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            var pilot = pilots.FindByName(pilotFullName);
            if (pilot == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }
            if (!pilot.CanRace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }
            if (race.Pilots.Any(p => p.FullName == pilotFullName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            race.AddPilot(pilot);

            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (cars.Models.Any(p => p.Model == model))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }     

            IFormulaOneCar newCar = null;
            switch (type)
            {
                case "Williams":
                    newCar = new Williams(model, horsepower, engineDisplacement);
                    break;
                case "Ferrari":
                    newCar = new Ferrari(model, horsepower, engineDisplacement);
                    break;
                default:
                    throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }
            cars.Add(newCar);

            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreatePilot(string fullName)
        {
            if (pilots.Models.Any(p => p.FullName == fullName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }
            pilots.Add(new Pilot(fullName));

            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (races.Models.Any(c => c.RaceName == raceName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            races.Add(new Race(raceName, numberOfLaps));

            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string PilotReport()
        {
            var sb = new StringBuilder();

            foreach (var pilot in pilots.Models.OrderByDescending(p => p.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            var sb = new StringBuilder();

            foreach (var race in races.Models.Where(r => r.TookPlace))
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string StartRace(string raceName)
        {
            var race = races.FindByName(raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }
            if (race.TookPlace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            var orderedWinners = race.Pilots.OrderByDescending(p => p.Car.RaceScoreCalculator(race.NumberOfLaps))
                .Take(3)
                .ToList();

            orderedWinners[0].WinRace();
            race.TookPlace = true;

            var sb = new StringBuilder();
            sb.AppendLine($"Pilot {orderedWinners[0].FullName} wins the {raceName} race.");
            sb.AppendLine($"Pilot {orderedWinners[1].FullName} is second in the {raceName} race.");
            sb.AppendLine($"Pilot {orderedWinners[2].FullName} is third in the {raceName} race.");

            return sb.ToString().TrimEnd();
        }
    }
}
