using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeamGenerator
{
    public class StartUp
    {
        private static List<Team> teams;

        static void Main(string[] args)
        {
            teams = new List<Team>();

            RunEngine();
        }

        private static void RunEngine()
        {
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = input.Split(";", StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    string command = tokens[0];
                    string teamName = tokens[1];
                    switch (command)
                    {
                        case "Team":
                            teams.Add(new Team(teamName));
                            break;
                        case "Add":
                            AddPlayer(tokens, teamName);
                            break;
                        case "Remove":
                            RemovePlayer(tokens, teamName);
                            break;
                        case "Rating":
                            Rating(tokens, teamName);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void Rating(string[] tokens, string teamName)
        {
            var team = teams.FirstOrDefault(t => t.Name == tokens[1]);
            if (team == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistingTeamMessage, teamName));
            }
            Console.WriteLine(team);
        }

        private static void RemovePlayer(string[] tokens, string teamName)
        {
            var team = teams.FirstOrDefault(t => t.Name == tokens[1]);
            string playerName = tokens[2];
            if (team == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistingTeamMessage, teamName));
            }

            team.RemovePlayer(playerName);
        }

        private static void AddPlayer(string[] tokens, string teamName)
        {
            var team = teams.FirstOrDefault(t => t.Name == tokens[1]);
            if (team == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistingTeamMessage, teamName));
            }
            var newPlayer = new Player(tokens[2], int.Parse(tokens[3]), int.Parse(tokens[4]),
                int.Parse(tokens[5]), int.Parse(tokens[6]), int.Parse(tokens[7]));

            team.AddPlayer(newPlayer);
        }
    }
}
