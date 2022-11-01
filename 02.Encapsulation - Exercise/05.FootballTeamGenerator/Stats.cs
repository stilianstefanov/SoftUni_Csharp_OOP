using System;
using System.Collections.Generic;
using System.Text;

namespace FootballTeamGenerator
{
    public class Stats
    {
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Stats(int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Endurance = endurance;
            Sprint = sprint;
            Dribble = dribble;
            Passing = passing;
            Shooting = shooting;
        }
        public int Endurance
        {
            get { return endurance; }
            private set
            {
                if (ValidateStats("Endurance", value))
                {
                    endurance = value;
                }
            }
        }
        public int Sprint
        {
            get { return sprint; }
            private set
            {
                if (ValidateStats("Sprint", value))
                {
                    sprint = value;
                }
            }
        }
        public int Dribble
        {
            get { return dribble; }
            private set
            {
                if (ValidateStats("Dribble", value))
                {
                    dribble = value;
                }
            }
        }
        public int Passing
        {
            get { return passing; }
            private set
            {
                if (ValidateStats("Passing", value))
                {
                    passing = value;
                }
            }
        }
        public int Shooting
        {
            get { return shooting; }
            private set
            {
                if (ValidateStats("Shooting", value))
                {
                    shooting = value;
                }
            }
        }

        private bool ValidateStats(string statName, int value)
        {
            if (value >= 0 && value <= 100)
            {
                return true;
            }
            throw new ArgumentException(string.Format(ExceptionMessages.InvalidStatMessage, statName));
        }
    }
}
