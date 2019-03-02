using PathFinding.Models;
using PathFinding.Models.Enums;

namespace PathFinding.Helpers
{
    public static class GridBuilder
    {
        public const short GridSize = 5;

        public static short[,] InitGrid()
        {
            var result = new short[GridSize, GridSize];

            for (short y = 0; y < GridBuilder.GridSize; y++)
            {
                for (short x = 0; x < GridBuilder.GridSize; x++)
                {
                    result[x, y] = 0;
                }
            }

            return result;
        }

        public static Position SetStart(Position pos, short[,] grid)
        {
            grid[pos.X, pos.Y] = (short)NodeState.Start;

            return pos;
        }

        public static Position SetGoal(Position pos, short[,] grid)
        {
            grid[pos.X, pos.Y] = (short)NodeState.Goal;

            return pos;
        }

        public static Position SetBlock(Position pos, short[,] grid)
        {
            grid[pos.X, pos.Y] = (short)NodeState.Block;

            return pos;
        }

        public static Position SetDeadEnd(Position pos, short[,] grid)
        {
            grid[pos.X, pos.Y] = (short)NodeState.DeadEnd;

            return pos;
        }
    }
}
