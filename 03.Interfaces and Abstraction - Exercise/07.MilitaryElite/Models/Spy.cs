namespace MilitaryElite.Models
{
    using System;

    using Models.Contracts;
    

    public class Spy : Solider, ISpy
    {
        public Spy(string id, string firstName, string lastName, int codeNumber) : base(id, firstName, lastName)
        {
            CodeNumber = codeNumber;
        }

        public int CodeNumber { get; private set; }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName} Id: {Id}" + Environment.NewLine
                + $"Code Number: {CodeNumber}";
        }
    }
}
