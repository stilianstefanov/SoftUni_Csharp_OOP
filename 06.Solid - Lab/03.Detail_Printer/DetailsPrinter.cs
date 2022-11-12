using P03.Detail_Printer;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03.DetailPrinter
{
    public class DetailsPrinter
    {
        private IList<IEmoloyee> employees;

        public DetailsPrinter(IList<IEmoloyee> employees)
        {
            this.employees = employees;
        }

        public void PrintDetails()
        {
            foreach (IEmoloyee employee in this.employees)
            {
                employee.Print();
            }
        }       
    }
}
