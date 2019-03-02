using PathFinding.Models;
using PathFinding.Models.Enums;

namespace PathFinding.Helpers
{
    public static class GridHelper
    {
        public static Quadrant? GetBestAvailableDirection(GridNode currentPos, Navigator navigator)
        {
            Quadrant result;
            var angleToGoal = AngleHelper.GetAngle(currentPos.CurrentPos, navigator.GoalPos);

            /* Can we move directly towards the goal? */
            if (CanMove(navigator, currentPos, result = QuadrantHelper.GetQuadrant(angleToGoal)))
            {
                return result;
            }

            /* Can we move to the secondary quadrant? */
            if (CanMove(navigator, currentPos, result = QuadrantHelper.GetSecondQuadrant(angleToGoal, result)))
            {
                return result;
            }

            /* Can we move to the thrid quadrant? */
            if (CanMove(navigator, currentPos, result = QuadrantHelper.GetQuadrant(angleToGoal + 180)))
            {
                return result;
            }

            /* Guess it's worth a shot to see if we can move to the fourth quadrant? */
            if (CanMove(navigator, currentPos, result = QuadrantHelper.GetSecondQuadrant(angleToGoal + 180, result)))
            {
                return result;
            }

            return null;
        }

        public static bool CanMove(Navigator navigator, GridNode fromPos, Quadrant direction)
        {
            bool CanDo = true;

            switch (direction)
            {
                case Quadrant.North:
                    CanDo = !navigator.Path.Contains(new Position(fromPos.CurrentPos.X, (short)(fromPos.CurrentPos.Y - 1)));
                    CanDo = CanDo && fromPos.CurrentPos.Y - 1 >= 0;
                    CanDo = CanDo && navigator.Grid[fromPos.CurrentPos.X, fromPos.CurrentPos.Y - 1] != (short)NodeState.Block;
                    CanDo = CanDo && navigator.Grid[fromPos.CurrentPos.X, fromPos.CurrentPos.Y - 1] != (short)NodeState.DeadEnd;
                    break;
                case Quadrant.East:
                    CanDo = !navigator.Path.Contains(new Position((short)(fromPos.CurrentPos.X + 1), fromPos.CurrentPos.Y));
                    CanDo = CanDo && fromPos.CurrentPos.X + 1 < navigator.GridSize;
                    CanDo = CanDo && navigator.Grid[fromPos.CurrentPos.X + 1, fromPos.CurrentPos.Y] != (short)NodeState.Block;
                    CanDo = CanDo && navigator.Grid[fromPos.CurrentPos.X + 1, fromPos.CurrentPos.Y] != (short)NodeState.DeadEnd;
                    break;
                case Quadrant.South:
                    CanDo = !navigator.Path.Contains(new Position(fromPos.CurrentPos.X, (short)(fromPos.CurrentPos.Y + 1)));
                    CanDo = CanDo && fromPos.CurrentPos.Y + 1 < navigator.GridSize;
                    CanDo = CanDo && navigator.Grid[fromPos.CurrentPos.X, fromPos.CurrentPos.Y + 1] != (short)NodeState.Block;
                    CanDo = CanDo && navigator.Grid[fromPos.CurrentPos.X, fromPos.CurrentPos.Y + 1] != (short)NodeState.DeadEnd;
                    break;
                case Quadrant.West:
                    CanDo = !navigator.Path.Contains(new Position((short)(fromPos.CurrentPos.X - 1), fromPos.CurrentPos.Y));
                    CanDo = CanDo && fromPos.CurrentPos.X - 1 >= 0;
                    CanDo = CanDo && navigator.Grid[fromPos.CurrentPos.X - 1, fromPos.CurrentPos.Y] != (short)NodeState.Block;
                    CanDo = CanDo && navigator.Grid[fromPos.CurrentPos.X - 1, fromPos.CurrentPos.Y] != (short)NodeState.DeadEnd;
                    break;
                default:
                    break;
            }

            return CanDo;
        }
    }
}
