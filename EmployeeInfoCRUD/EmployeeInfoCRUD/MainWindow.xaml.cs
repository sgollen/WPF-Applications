using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaySorter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DetailWindow dw = null;
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //Show details window on button click
        private void BtnShowDetails_Click(object sender, RoutedEventArgs e)
        {
            if (dw == null)
            {
                dw = new DetailWindow(vm)
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                dw.Closed += Dw_Closed;
                dw.Show();
            }
        }
        //On close, reset dw to null to remove window
        private void Dw_Closed(object sender, System.EventArgs e) => dw = null;

        //calls SortAscending medthod of the VM class which sorts the Employee list
        private void BtnAscending_Click(object sender, RoutedEventArgs e)
        {
            vm.SortAscending();
        }

        //calls SortDescending medthod of the VM class sorts the Employee list
        private void BtnDescending_Click(object sender, RoutedEventArgs e)
        {
            vm.SortDescending();
        }

        //calls SaveEmployees medthod of the VM class which saves the Employee list to a text file
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveEmployees();
        }

        //calls AddEmployee medthod of the VM class which adds a new employee to the database
        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            vm.AddEmployee();
        }

        //calls ClearForm medthod of the VM class which clears the new Employee form
        private void BtnClearForm_Click(object sender, RoutedEventArgs e)
        {
            vm.ClearForm();
        }
    }
}
