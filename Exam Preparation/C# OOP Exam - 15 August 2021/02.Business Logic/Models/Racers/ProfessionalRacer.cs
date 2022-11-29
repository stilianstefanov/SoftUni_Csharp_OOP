namespace CarRacing.Models.Racers
{
    using Cars.Contracts;

    public class ProfessionalRacer : Racer
    {
        private const int InitialDrivingExperience = 30;
        private const string Initialbehavior = "strict";

        public ProfessionalRacer(string username, ICar car)
            : base(username, Initialbehavior, InitialDrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();

            DrivingExperience += 10;
        }
    }
}
