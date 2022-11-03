using MilitaryElite.Models;
using MilitaryElite.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilitaryElite.Core
{
    public class Engine : IEngine
    {
        private List<ISolder> soliders;
        private List<IPrivate> privates;

        public void Run()
        {
            soliders = new List<ISolder>();
            privates = new List<IPrivate>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string soliderType = tokens[0];
                string id = tokens[1];
                string firstName = tokens[2];
                string lastName = tokens[3];

                switch (soliderType)
                {
                    case "Private":
                        {
                            decimal salary = decimal.Parse(tokens[4]);
                            var newPrivate = new Private(id, firstName, lastName, salary);
                            soliders.Add(newPrivate);
                            privates.Add(newPrivate);
                        }
                        break;
                    case "LieutenantGeneral":
                        AddLeutenantGeneral(tokens);
                        break;
                    case "Engineer":
                        AddEngineer(tokens);
                        break;
                    case "Commando":
                        AddCommando(tokens);
                        break;
                    case "Spy":
                        int codeNumber = int.Parse(tokens[4]);
                        soliders.Add(new Spy(id, firstName, lastName, codeNumber));
                        break;
                }
            }
            foreach (var solider in soliders)
            {
                Console.WriteLine(solider.ToString());
            }
        }

        private void AddCommando(string[] tokens)
        {
            string id = tokens[1];
            string firstName = tokens[2];
            string lastName = tokens[3];
            decimal salary = decimal.Parse(tokens[4]);
            string corps = tokens[5];

            List<Mission> missions = new List<Mission>();
            for (int i = 6; i < tokens.Length; i += 2)
            {
                string missionCode = tokens[i];
                string missionState = tokens[i + 1];
                try
                {
                    missions.Add(new Mission(missionCode, missionState));
                }
                catch (Exception e)
                { }                                  
            }

            try
            {
                soliders.Add(new Commando(id, firstName, lastName, salary, corps, missions));
            }
            catch (Exception e)
            { }
        }

        private void AddEngineer(string[] tokens)
        {

            string id = tokens[1];
            string firstName = tokens[2];
            string lastName = tokens[3];
            decimal salary = decimal.Parse(tokens[4]);
            string corps = tokens[5];

            List<Repair> repairs = new List<Repair>();
            for (int i = 6; i < tokens.Length; i += 2)
            {
                string repairName = tokens[i];
                int hoursWorked = int.Parse(tokens[i + 1]);

                repairs.Add(new Repair(repairName, hoursWorked));
            }

            try
            {
                soliders.Add(new Engineer(id, firstName, lastName, salary, corps, repairs));
            }
            catch (Exception e)
            { }                           
        }

        private void AddLeutenantGeneral(string[] tokens)
        {
            string id = tokens[1];
            string firstName = tokens[2];
            string lastName = tokens[3];
            decimal salary = decimal.Parse(tokens[4]);

            List<IPrivate> privatesToAdd = new List<IPrivate>();
            for (int i = 5; i < tokens.Length; i++)
            {
                privatesToAdd.Add(privates.Find(s => s.Id == tokens[i]));
            }

            soliders.Add(new LieutenantGeneral(id, firstName, lastName, salary, privatesToAdd));
        }
    }
}
