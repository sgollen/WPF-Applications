using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Final Project, Dec 4/18
namespace FireTruckProblem
{
    public class FireRoute
    {
        public int End { get; set; }       
        public List<int[]> Grid { get; set; }
        public List<string> Routes { get; set; }
        public const string START = "1";
        // Default constructor
        public FireRoute()
        {
        }
        // Methods for constructor
        // Find current position
        public int FindCurrentPosition(string path)
        {
            return int.Parse(path.Split(' ')[path.Split(' ').Length - 1]);
        }
        // Did we make it to the end? if not, check each corner for possible next steps
        public void FindFireRoutes(string path, int end, List<int[]> grid)
        {
            int currentPosition = FindCurrentPosition(path);
            if (currentPosition == end)
                Routes.Add(path);
            else
                for (int i = 0; i < grid.Count; i++)
                    SearchRoutes(i, path, end, grid);
        }
        public void SearchRoutes(int i, string path, int end, List<int[]> grid)
        {
            int currentPosition = FindCurrentPosition(path);
            // Is our current position in this corner?
            if (grid[i][0] == currentPosition || grid[i][1] == currentPosition)
            {
                int nextPos = grid[i][0] == currentPosition ? grid[i][1] : grid[i][0];
                // Have we been to the other position already? if not, add it to the path and check if we're there yet. 
                if (!path.Contains($"{nextPos}"))
                {
                    path += $" {nextPos}";
                    FindFireRoutes(path, end, grid);
                }
            }
        }
        // Constructor
        public FireRoute(int end, List<int[]> grid)
        {
            End = end;
            Grid = grid;
            Routes = new List<string>();
            FindFireRoutes(START, End, Grid);
            Routes.Sort((x, y) => x.Split(' ').Length.CompareTo(y.Split(' ').Length));
        }
    }
}