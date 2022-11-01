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
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameCannotBeNullOrWhiteSpace);
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
            var playerToRemove = players.FirstOrDefault(p => p.Name == playerName);

            if (playerToRemove == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.MissingPlayerMessage,
                    playerName, this.Name));
            }
            players.Remove(playerToRemove);
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.Rating}";
        }
    }
}
