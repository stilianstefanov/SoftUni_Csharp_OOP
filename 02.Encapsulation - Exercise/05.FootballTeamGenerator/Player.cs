using System;
using System.Collections.Generic;
using System.Text;

namespace FootballTeamGenerator
{
    public class Player
    {
        private string name;
        private Stats stats;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Name = name;
            stats = new Stats(endurance, sprint, dribble, passing, shooting);
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameCannotBeNullOrWhiteSpace));
                }
                name = value;
            }
        }

        public double AvarageStats
        {
            get
            {
                return (stats.Endurance + stats.Passing + stats.Shooting + stats.Sprint + stats.Dribble) / 5.0;
            }
        }
    }
}
