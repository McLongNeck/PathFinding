using PathFinding.Models;
using PathFinding.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using static PathFinding.Helpers.GridBuilder;

namespace PathFinding.Helpers
{
    public class Navigator
    {
        public bool Success { get; private set; } = false;
        public Position StartPos;
        public Position GoalPos;

        private readonly short[,] Grid;
        private List<Position> Path = new List<Position>();
        private int MaxStepsBack { get; set; }
        private int TimesSteppedBack { get; set; } = 0;

        /* Put in helper class. */

        /// <summary>
        /// Will get the second quadrant based on angle. So when angle points NNE, first choice would be North, since it's closest. 
        /// When excluding North, East will become your next best option.
        /// </summary>
        public static Quadrant GetSecondQuadrant(double angle, Quadrant firstQuadrant)
        {
            switch (firstQuadrant)
            {
                case Quadrant.East:
                    return angle >= 0 ? Quadrant.South : Quadrant.North;
                case Quadrant.South:
                    return angle < 90 ? Quadrant.East : Quadrant.West;
                case Quadrant.West:
                    return angle < 180 ? Quadrant.South : Quadrant.North;
                case Quadrant.North:
                    return angle < 270 ? Quadrant.West : Quadrant.East;
                default:
                    throw new Exception("Unknown quadrant");
            }
        }

        public Quadrant? GetBestDirection(GridNode node)
        {
            Quadrant result;
            var angleToGoal = GetAngle(node.CurrentPos, GoalPos);

            /* Can we move directly towards the goal? */
            if (CanMove(node, result = GetQuadrant(angleToGoal)))
            {
                return result;
            }

            /* Can we move to the secondary quadrant? */
            if (CanMove(node, result = GetSecondQuadrant(angleToGoal, result)))
            {
                return result;
            }

            /* Can we move to the thrid quadrant? */
            if (CanMove(node, result = GetQuadrant(angleToGoal + 180)))
            {
                return result;
            }

            /* Guess it's worth a shot to see if we can move to the fourth quadrant? */
            if (CanMove(node, result = GetSecondQuadrant(angleToGoal + 180, result)))
            {
                return result;
            }

            return null;
        }

        public bool CanMove(GridNode node, Quadrant direction)
        {
            bool CanDo = true;

            switch (direction)
            {
                case Quadrant.North:
                    CanDo = !Path.Contains(new Position(node.CurrentPos.X, (short)(node.CurrentPos.Y - 1)));
                    CanDo = CanDo && node.CurrentPos.Y - 1 >= 0;
                    CanDo = CanDo && Grid[node.CurrentPos.X, node.CurrentPos.Y - 1] != (short)NodeState.Block;
                    CanDo = CanDo && Grid[node.CurrentPos.X, node.CurrentPos.Y - 1] != (short)NodeState.DeadEnd;
                    break;
                case Quadrant.East:
                    CanDo = !Path.Contains(new Position((short)(node.CurrentPos.X + 1), node.CurrentPos.Y));
                    CanDo = CanDo && node.CurrentPos.X + 1 < GridSize;
                    CanDo = CanDo && Grid[node.CurrentPos.X + 1, node.CurrentPos.Y] != (short)NodeState.Block;
                    CanDo = CanDo && Grid[node.CurrentPos.X + 1, node.CurrentPos.Y] != (short)NodeState.DeadEnd;
                    break;
                case Quadrant.South:
                    CanDo = !Path.Contains(new Position(node.CurrentPos.X, (short)(node.CurrentPos.Y + 1)));
                    CanDo = CanDo && node.CurrentPos.Y + 1 < GridSize;
                    CanDo = CanDo && Grid[node.CurrentPos.X, node.CurrentPos.Y + 1] != (short)NodeState.Block;
                    CanDo = CanDo && Grid[node.CurrentPos.X, node.CurrentPos.Y + 1] != (short)NodeState.DeadEnd;
                    break;
                case Quadrant.West:
                    CanDo = !Path.Contains(new Position((short)(node.CurrentPos.X - 1), node.CurrentPos.Y));
                    CanDo = CanDo && node.CurrentPos.X - 1 >= 0;
                    CanDo = CanDo && Grid[node.CurrentPos.X - 1, node.CurrentPos.Y] != (short)NodeState.Block;
                    CanDo = CanDo && Grid[node.CurrentPos.X - 1, node.CurrentPos.Y] != (short)NodeState.DeadEnd;
                    break;
                default:
                    break;
            }

            return CanDo;
        }

        public static double GetAngle(Position current, Position goal)
        {
            float xDiff = goal.X - current.X;
            float yDiff = goal.Y - current.Y;
            var result = System.Math.Atan2(yDiff, xDiff) * 180.0 / System.Math.PI;

            if (result < 0)
            {
                result += 360;
            }

            return result;
        }

        public static Quadrant GetQuadrant(double angle)
        {
            var res = Math.Round((angle / 90));

            if (res > (int)Quadrant.MAX)
            {
                res -= (int)Quadrant.MAX + 1;
            }

            return (Quadrant)res;
        }

        public Navigator(short[,] grid, short maxStepsBack = 3)
        {
            Grid = grid;
            MaxStepsBack = maxStepsBack;
        }

        public List<Position> Go()
        {
            var curNode = new GridNode();

            curNode.CurrentPos = StartPos;
            Path.Clear();
            Path.Add(curNode.CurrentPos);

            GetNextPos(curNode);

            return Path;
        }

        private bool GetNextPos(GridNode currentNode)
        {
            var quad = GetBestDirection(currentNode);

            if (quad.HasValue)
            {
                currentNode.PreviousPos = currentNode.CurrentPos;
                currentNode.CurrentPos.Move(quad.Value);
                Path.Add(currentNode.CurrentPos);

                if (currentNode.CurrentPos.Equals(GoalPos))
                {
                    Success = true;
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
