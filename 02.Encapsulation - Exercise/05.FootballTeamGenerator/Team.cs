using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballTeamGenerator
{
    public class Team
    {
        private string name;
        private List<Player> players;

        public Team(string name)
        {
            Name = name;
            players = new List<Player>();
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
        }
        public double Rating
        {
            get
            {
                if (players.Count == 0)
                {
                    return 0;
                }
                return Math.Round(players.Select(p => p.AvarageStats).Average());
            }
        }

        public void AddPlayer(Player player) => players.Add(player);
        
        public void RemovePlayer(string playerName)
        {
            if (!players.Any(p => p.Name == playerName))
            {
                throw new ArgumentException($"Player {playerName} is not in {this.Name} team.");
            }
            players.Remove(players.Find(p => p.Name == playerName));
        }
    }
}
