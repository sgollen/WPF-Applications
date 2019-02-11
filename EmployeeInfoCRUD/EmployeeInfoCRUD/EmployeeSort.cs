using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Date: Nov 20/18
namespace PaySorter
{
    // define method to sort ascending
    public class EmployeeSortAscPayRate : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            var res = x.HourlyPayRate.CompareTo(y.HourlyPayRate);
            if (res == 0)
                res = x.Name.CompareTo(y.Name);

            return res;
        }
    }

    // define method to sort descending
    public class EmployeeSortDesPayRate : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            var res = (y.HourlyPayRate.CompareTo(x.HourlyPayRate));
            if (res == 0)
                res = y.EmployeeID.CompareTo(x.EmployeeID);

            return res;
        }
    }
}
