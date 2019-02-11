using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Date: Nov 20/18
namespace PaySorter
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int HourlyPayRate { get; set; }

        // Default constructor
        public Employee()
        {
        }

        // Constructor with inputs
        public Employee(int EmployeeID, string Name, string Position, int PayRate)
        {
            this.EmployeeID= EmployeeID;
            this.Name = Name;
            this.Position = Position;
            this.HourlyPayRate = PayRate;
        }
        // ToString override
        public override string ToString() => $"{Name}:  ${HourlyPayRate}/hour";
    }
}
