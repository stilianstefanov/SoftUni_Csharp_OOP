namespace CarRacing.Models.Maps
{
    using Racers.Contracts;
    using Utilities.Messages;
    using Contracts;    

    public class Map : IMap
    {       
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return OutputMessages.RaceCannotBeCompleted;
            }

            IRacer winningRacer = null;
            IRacer loserRacer = null;            

            if (racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                winningRacer = racerOne;
                loserRacer = racerTwo;                
            }
            else if (!racerOne.IsAvailable() && racerTwo.IsAvailable())
            {
                winningRacer = racerTwo;
                loserRacer = racerOne;                
            }

            if (winningRacer != null)
            {
                winningRacer.Race();
                return string.Format(OutputMessages.OneRacerIsNotAvailable, winningRacer.Username, loserRacer.Username);
            }

            double racerOneChances = racerOne.Car.HorsePower * racerOne.DrivingExperience * GetMultiplier(racerOne.RacingBehavior);
            double racerTwoChances = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * GetMultiplier(racerTwo.RacingBehavior);

            racerOne.Race();
            racerTwo.Race();

            if (racerOneChances > racerTwoChances)
            {
                winningRacer = racerOne;
            }
            else
            {
                winningRacer = racerTwo;
            }

            return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winningRacer.Username);
        }

        private double GetMultiplier(string racingBehavior)
        {
            if (racingBehavior == "strict")
            {
                return 1.2;
            }
            else
            {
                return 1.1;
            }
        }
    }
}
