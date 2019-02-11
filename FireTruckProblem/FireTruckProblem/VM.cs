using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
// Final Project, Dec 4/18
namespace FireTruckProblem
{
    class VM : INotifyPropertyChanged
    {
        #region Properties
        public const string END_CASE = "0 0";        
        private BindingList<string> output = new BindingList<string>();
        public BindingList<string> Output 
        {
            get { return output; }
            set { output = value; NotifyChanged(); }
        }
        private string selectedFileName = "";
        public string SelectedFileName
        {
            get { return selectedFileName; }
            set { selectedFileName = value; NotifyChanged(); }
        }
        private List<FireRoute> fireRoutes = new List<FireRoute>();
        public List<FireRoute> FireRoutes
        {
            get { return fireRoutes; }
            set { fireRoutes = value; NotifyChanged(); }
        }
        #endregion
        #region Logic
        // Method to create output. Clear binding list at beginning so we don't see "Case 1:" multiple times. 
        public void GenerateOutput()
        {
            Output = new BindingList<string>();
            for (int i = 0; i < FireRoutes.Count; i++)
            {               
                Output.Add($"Case {i + 1}:");
                FireRoutes[i].Routes.ForEach(r => Output.Add(r));              
                Output.Add($"There are {FireRoutes[i].Routes.Count} routes from the firestation to streetcorner {FireRoutes[i].End}.");
                if(FireRoutes[i].Routes.Count>0)
                    Output.Add($"The shortest route is: {FireRoutes[i].Routes[0]}");
            }            
        }       
        // method to select file for import
        public void SelectFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                SelectedFileName = openFileDialog.FileName;
        }
        // method imports file and converts content into list of FireRoutes, bound to FireRoutes property
        public bool ImportFile()
        {
            try
            {
                string[] importLines = File.ReadAllLines(SelectedFileName);
                List<int[]> fireGrid = new List<int[]>();
                int endPosition = 0;
                for (int i = 0; i < importLines.Length; i++)
                {
                    // Case when one element: this is our end position
                    if (importLines[i].Split(' ').Length == 1)
                        endPosition = int.Parse(importLines[i]);
                    // if we aren't at the end case, add each line to a list of arrays
                    else if (importLines[i] != END_CASE)
                    {
                        int[] lineInt = new int[2];
                        string[] lineString = importLines[i].Split(' ');
                        for (int j = 0; j < lineString.Length; j++)
                        {
                            lineInt[j] = int.Parse(lineString[j].Trim());
                        }
                        fireGrid.Add(lineInt);
                    }
                    // once we're at the end case, create a new FireRoute object and add it to our list of FireRoutes
                    else
                    {
                        FireRoutes.Add(new FireRoute(endPosition, fireGrid));
                        fireGrid = new List<int[]>();
                    }
                }
                return false;
            }
            catch
            {
                return true;
            }
        }
        // method saves fire routes to text file on Desktop
        public void SaveRoutes()
        {
            string fileText = "";
            for (int j = 0; j < Output.Count; j++)
                fileText += Output[j] + Environment.NewLine;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string appPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            string dataPath = Path.Combine(appPath, "Prog8010");
            string filePath = Path.Combine(dataPath, $"Routes.txt");
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);
            File.WriteAllText(filePath, fileText);
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