using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] tokens = command.Split(";", StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    switch (tokens[0])
                    {
                        case "Team":
                            teams.Add(new Team(tokens[1]));
                            break;
                        case "Add":
                            var team = teams.FirstOrDefault(t => t.Name == tokens[1]);
                            if (team == null)
                            {
                                Console.WriteLine($"Team {tokens[1]} does not exist.");
                            }
                            else
                            {
                                var newPlayer = new Player(tokens[2], int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]), int.Parse(tokens[7]));
                                team.AddPlayer(newPlayer);
                            }
                            break;
                        case "Remove":
                            var teamRemove = teams.FirstOrDefault(t => t.Name == tokens[1]);
                            if (teamRemove == null)
                            {
                                Console.WriteLine($"Team {tokens[1]} does not exist.");
                            }
                            else
                            {
                                teamRemove.RemovePlayer(tokens[2]);
                            }
                            break;
                        case "Rating":
                            var teamRating = teams.FirstOrDefault(t => t.Name == tokens[1]);
                            if (teamRating == null)
                            {
                                Console.WriteLine($"Team {tokens[1]} does not exist.");
                            }
                            else
                            {
                                Console.WriteLine($"{teamRating.Name} - {teamRating.Rating}");
                            }
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
