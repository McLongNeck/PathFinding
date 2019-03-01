namespace PathFinding.Helper
{
    public static class GridBuilder
    {
        public enum BoxState
        {
            Empty = 0,
            Start = 1,
            Goal = 2,
            Block = 3,
            DeadEnd = 4
        };

        public const int GridSize = 5;

        public static int[,] InitGrid()
        {
            var result = new int[GridSize, GridSize];

            for (int y = 0; y < GridBuilder.GridSize; y++)
            {
                for (int x = 0; x < GridBuilder.GridSize; x++)
                {
                    result[x, y] = 0;
                }
            }

            return result;
        }

        public static Position SetStart(Position pos, int[,] grid)
        {
            grid[pos.X, pos.Y] = (int)BoxState.Start;
            
            return pos;
        }

        public static Position SetGoal(Position pos, int[,] grid)
        {
            grid[pos.X, pos.Y] = (int)BoxState.Goal;

            return pos;
        }

        public static Position SetBlock(Position pos, int[,] grid)
        {
            grid[pos.X, pos.Y] = (int)BoxState.Block;

            return pos;
        }

        public static Position SetDeadEnd(Position pos, int[,] grid)
        {
            grid[pos.X, pos.Y] = (int)BoxState.DeadEnd;

            return pos;
        }
    }
}
