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
// Final Project, Dec 4/18
namespace FireTruckProblem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }
        private void ImportAndRun_Click(object sender, RoutedEventArgs e)
        {            
            vm.SelectFile();
            if (vm.ImportFile())
            {
                MessageBox.Show("There was an error importing the file. Please check your format and try again");
                return;
            }
            vm.GenerateOutput();            
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveRoutes();
        }
    }
}
