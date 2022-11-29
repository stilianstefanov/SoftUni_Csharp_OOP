namespace CarRacing.Models.Racers
{
    using Cars.Contracts;


    public class StreetRacer : Racer
    {
        private const int InitialDrivingExperience = 10;
        private const string Initialbehavior = "aggressive";

        public StreetRacer(string username, ICar car)
            : base(username, Initialbehavior, InitialDrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();

            DrivingExperience += 5;
        }
    }
}
