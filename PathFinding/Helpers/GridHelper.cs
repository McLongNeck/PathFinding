using PathFinding.Models;
using PathFinding.Models.Enums;

namespace PathFinding.Helpers
{
    public static class GridHelper
    {
        public static Quadrant? GetBestAvailableDirection(GridNode currentPos, Navigator navigator)
        {
            Quadrant firstQuadrant, SecondQuadrant, ThirdQuadrant, FourthQuadrant;
            var angleToGoal = AngleHelper.GetAngle(currentPos.CurrentPos, navigator.GoalPos);

            /* Can we move directly towards the goal? */
            if (CanMove(navigator, currentPos, firstQuadrant = QuadrantHelper.GetQuadrant(angleToGoal)))
            {
                return firstQuadrant;
            }

            /* Can we move to the secondary quadrant? */
            if (CanMove(navigator, currentPos, SecondQuadrant = QuadrantHelper.GetSecondQuadrant(angleToGoal, firstQuadrant)))
            {
                return SecondQuadrant;
            }

            /* Can we move to the thrid best quadrant? */
            if (CanMove(navigator, currentPos, ThirdQuadrant = QuadrantHelper.Opposite(SecondQuadrant)))
            {
                return ThirdQuadrant;
            }

            /* Guess it's worth a shot to see if we can move to the fourth quadrant? */
            if (CanMove(navigator, currentPos, FourthQuadrant = QuadrantHelper.Opposite(firstQuadrant)))
            {
                return FourthQuadrant;
            }

            return null;
        }

        public static bool CanMove(Navigator navigator, GridNode fromPos, Quadrant direction)
        {
            bool canDo = true;
            short toX = fromPos.CurrentPos.X;
            short toY = fromPos.CurrentPos.Y;

            switch (direction)
            {
                case Quadrant.North:
                    toY--;
                    break;
                case Quadrant.East:
                    toX++;
                    break;
                case Quadrant.South:
                    toY++;
                    break;
                case Quadrant.West:
                    toX--;
                    break;
                default:
                    break;
            }

            canDo = !navigator.Path.Contains(new Position(toX, toY));
            canDo = canDo && toX >= 0 && toY >= 0 && toX < navigator.GridSize && toY < navigator.GridSize;
            canDo = canDo && navigator.Grid[toX, toY] != (short)NodeState.Block;
            canDo = canDo && navigator.Grid[toX, toY] != (short)NodeState.DeadEnd;

            return canDo;
        }
    }
}
