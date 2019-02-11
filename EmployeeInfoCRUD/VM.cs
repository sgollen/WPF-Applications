using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// Date: Nov 20/18
namespace EmployeeInfo 
{
    public class VM : INotifyPropertyChanged
    {
        //get a reference to the database object that we can use repeatedly
        DB database = DB.GetInstance();

        #region Properties
        private int newEmployeeID = 0;
        public int NewEmployeeID
        {
            get { return newEmployeeID; }
            set { newEmployeeID = value; NotifyChanged(); }
        }
        private string newName = "";
        public string NewName
        {
            get { return newName; }
            set { newName = value; NotifyChanged(); }
        }
        private string newPosition = "";
        public string NewPosition
        {
            get { return newPosition; }
            set { newPosition = value; NotifyChanged(); }
        }
        private int newHourlyPayRate = 0;
        public int NewHourlyPayRate
        {
            get { return newHourlyPayRate; }
            set { newHourlyPayRate = value; NotifyChanged(); }
        }

        private BindingList<Employee> employees = new BindingList<Employee>() { };
        public BindingList<Employee> Employees
        {
            get { return employees; }
            set { employees = value; NotifyChanged(); }
        }
        private Employee selectedEmployee = null;
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set { selectedEmployee = value; NotifyChanged(); }
        }
        #endregion

        #region Logic
        public VM()
        {
            //get a list of persons from the database
            Employees = new BindingList<Employee>(database.Get(2));
        }

        // method to sort ascending
        public void SortAscending()
        {
            Employees = new BindingList<Employee>(database.Get(1));
        }

        // method to sort descending
        public void SortDescending()
        {
            Employees = new BindingList<Employee>(database.Get(0));
        }

        // method to Add new Employees
        public void AddEmployee()
        {
            Employee newEmployee = new Employee(NewEmployeeID, NewName, NewPosition, NewHourlyPayRate);
            database.Add(newEmployee);
            Employees = new BindingList<Employee>(database.Get(2));
        }

        // method to save employees
        public void SaveEmployees()
        {
            string fileText = "";
            for (int j = 0; j < Employees.Count; j++)
            {
                fileText += Employees[j];
                fileText += Environment.NewLine;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string appPath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            string dataPath = Path.Combine(appPath, "Prog8010");
            string filePath = Path.Combine(dataPath, $"Employee.txt");
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            File.WriteAllText(filePath, fileText);
        }

        // method to clears the add new employee form
        public void ClearForm()
        {
            NewEmployeeID = 0;
            NewName = "";
            NewPosition = "";
            NewHourlyPayRate = 0;
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
