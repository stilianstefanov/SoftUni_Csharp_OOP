using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    public class Mission
    {
        private string state;

		public Mission(string codeName, string state)
		{
			CodeName = codeName;
			State = state;
		}
        public string CodeName { get; private set; }
	

		public string State
		{
			get { return state; }
			private set 
			{
				if (value != "inProgress" && value != "Finished")
				{
					throw new ArgumentException();
                }		
				state = value;
			}
		}

		public void CompleteMission()
		{
			this.State = "Finished";
        }

		public override string ToString()
		{
			return $"Code Name: {CodeName} State: {State}";

        }
	}
}
