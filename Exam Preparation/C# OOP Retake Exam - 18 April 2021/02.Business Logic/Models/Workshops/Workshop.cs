namespace Easter.Models.Workshops
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Bunnies.Contracts;
    using Eggs.Contracts;
    using Models.Dyes.Contracts;
   

    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            List<IDye> dyes = bunny.Dyes.ToList();

            while (!egg.IsDone() && bunny.Energy > 0 && dyes.Any())
            {
                IDye currentDye = dyes.First();

                while (!currentDye.IsFinished() && bunny.Energy > 0 && !egg.IsDone())
                {
                    currentDye.Use();
                    bunny.Work();
                    egg.GetColored();
                }

                if (currentDye.IsFinished())
                    dyes.Remove(currentDye);
            }
        }
    }
}
