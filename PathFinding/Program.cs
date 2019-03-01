using PathFinding.Helper;
using System;

namespace PathFinding
{
    internal class Program
    {
        private static void Main(string[] args) 
        {
            var grid = GridBuilder.InitGrid();
            var start = GridBuilder.SetStart(new Position(0, 0), grid);
            var goal = GridBuilder.SetGoal(new Position(3, 0), grid);

            GridBuilder.SetBlock(new Position(1, 1), grid);
            GridBuilder.SetBlock(new Position(1, 2), grid);

            GridBuilder.SetBlock(new Position(2, 0), grid);

            GridBuilder.SetBlock(new Position(3, 1), grid);
            GridBuilder.SetBlock(new Position(3, 2), grid);

            var navigator = new GridNavigator(grid);
            navigator.StartPos = start;
            navigator.GoalPos = goal;

            var path = navigator.Go();

            GridDrawer.DrawGrid(grid);
            GridDrawer.DrawPath(grid, path, navigator);
            GridDrawer.DrawSymbols(grid, path);
            Console.ReadKey();
        }
    }
}
