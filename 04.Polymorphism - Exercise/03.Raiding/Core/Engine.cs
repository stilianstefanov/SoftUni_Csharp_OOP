
namespace Raiding.Core
{
    using System.Collections.Generic;

    using Contracts;
    using IO.Contracts;
    using Models.Contracts;
    

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private List<IHero> heroes;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            heroes = new List<IHero>();
        }

        public void Run()
        {
            int reqValidHeroesCnt = int.Parse(reader.ReadLine());

            
            while (heroes.Count < reqValidHeroesCnt)
            {
                IHero hero = null;

                string name = reader.ReadLine();
                string type = reader.ReadLine();
                
                try
                {
                    hero = HeroFactory.CreateHero(name, type);
                    heroes.Add(hero);
                }
                catch (System.Exception e)
                {
                    writer.WriteLine(e.Message);
                }
            }

            int bossPower = int.Parse(reader.ReadLine());

            int totalTeamPower = 0;

            if (heroes.Count > 0)
            {
                foreach (IHero _hero in heroes)
                {
                    writer.WriteLine(_hero.CastAbility());
                    totalTeamPower += _hero.Power;
                }
            }

            if (totalTeamPower >= bossPower)
                writer.WriteLine("Victory!");
            else
                writer.WriteLine("Defeat...");
        }
    }
}
