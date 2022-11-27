namespace Gym.Models.Athletes
{
    using Utilities.Messages;
    using System;

    public class Boxer : Athlete
    {
        public Boxer(string fullName, string motivation, int numberOfMedals)
             : base(fullName, motivation, numberOfMedals, 60)
        {
        }

        public override void Exercise()
        {
            Stamina += 15;

            if (Stamina > 100)
            {
                Stamina = 100;

                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
        }
    }
}
