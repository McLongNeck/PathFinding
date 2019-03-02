using PathFinding.Helpers;
using System.Collections.Generic;
using System.Linq;
using static PathFinding.Helpers.GridBuilder;

namespace PathFinding.Models
{
    public class Navigator
    {
        public Position StartPos;
        public Position GoalPos;
        public bool PathFound { get; private set; } = false;

        public List<Position> Path { get; private set; } = new List<Position>();
        public short[,] Grid { get; private set; }
        public short GridSize { get; private set; } = 5;

        private short MaxStepsBack { get; set; } = 3;
        private short TimesSteppedBack { get; set; } = 0;

        public Navigator(short[,] grid)
        {
            Grid = grid;
        }

        public Navigator(short[,] grid, short gridSize, short maxStepsBack)
        {
            Grid = grid;
            GridSize = gridSize;
            MaxStepsBack = maxStepsBack;
        }

        public List<Position> Go()
        {
            var curNode = new GridNode();

            curNode.CurrentPos = StartPos;
            Path.Clear();
            Path.Add(curNode.CurrentPos);
            PathFound = false;

            GetNextPos(curNode);

            return Path;
        }

        private bool GetNextPos(GridNode currentNode)
        {
            var quad = GridHelper.GetBestAvailableDirection(currentNode, this);

            if (quad.HasValue)
            {
                currentNode.PreviousPos = currentNode.CurrentPos;
                currentNode.CurrentPos.Move(quad.Value);
                Path.Add(currentNode.CurrentPos);

                if (currentNode.CurrentPos.Equals(GoalPos))
                {
                    PathFound = true;
                    return true;
                }
                else
                {
                    return GetNextPos(currentNode);
                }
            }
            else
            {
                if (Path.Count > 1 && TimesSteppedBack < MaxStepsBack)
                {
                    TimesSteppedBack++;

                    SetDeadEnd(Path.Last(), Grid);
                    Path.Remove(Path.Last());

                    var LastIndex = Path.IndexOf(Path.Last());
                    currentNode.CurrentPos = Path[LastIndex];
                    currentNode.PreviousPos = LastIndex - 1 >= 0 ? Path[LastIndex - 1] : currentNode.CurrentPos;

                    return GetNextPos(currentNode);
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
