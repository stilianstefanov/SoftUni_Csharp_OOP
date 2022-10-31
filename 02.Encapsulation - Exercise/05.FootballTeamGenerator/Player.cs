using System;
using System.Collections.Generic;
using System.Text;

namespace FootballTeamGenerator
{
    public class Player
    {
        private string name;

        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name,int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Name = name;
            Endurance = endurance;
            Sprint = sprint;
            Dribble = dribble;
            Passing = passing;
            Shooting = shooting;
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
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

        public double AvarageStats
        {
            get
            {
                return (endurance + passing + shooting + sprint + dribble) / 5.0;
            }
        }
        private bool ValidateStats(string statName, int value)
        {
            if (value >= 0 && value <= 100)
            {
                return true;
            }
            throw new ArgumentException($"{statName} should be between 0 and 100.");
        }
    }
}
