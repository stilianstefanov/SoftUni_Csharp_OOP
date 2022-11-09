using System;
using System.Collections.Generic;

namespace _06.Money_Transactions
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] accountsArgs = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, double> accounts = GetAccounts(accountsArgs);

            
            string commandArgs;
            while ((commandArgs = Console.ReadLine()) != "End")
            {
                try
                {
                    PorocessComand(accounts, commandArgs);
                }
                catch(ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                finally
                {
                   Console.WriteLine("Enter another command");
                }
            }
        }

        private static void PorocessComand(Dictionary<string, double> accounts, string commandArgs)
        {
            string[] commandTokens = commandArgs.Split();
            string command = commandTokens[0];
            string accId = commandTokens[1];
            double value = double.Parse(commandTokens[2]);

            if (!accounts.ContainsKey(accId))
            {
                throw new ArgumentException("Invalid account!");
            }

            switch (command)
            {
                case "Deposit":
                    accounts[accId] += value;
                    break;
                case "Withdraw":
                    {
                        if (value > accounts[accId])
                        {
                            throw new ArgumentException("Insufficient balance!");
                        }
                        accounts[accId] -= value;
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid command!");                    
            }

            Console.WriteLine($"Account {accId} has new balance: {accounts[accId]:f2}");
        }

        private static Dictionary<string, double> GetAccounts(string[] accountsArgs)
        {
            var accounts = new Dictionary<string, double>();

            for (int i = 0; i < accountsArgs.Length; i++)
            {
                string[] curAccArgs = accountsArgs[i].Split("-");
                string accID = curAccArgs[0];
                double accSum = double.Parse(curAccArgs[1]);

                accounts.Add(accID, accSum);
            }

            return accounts;
        }
    }
}
