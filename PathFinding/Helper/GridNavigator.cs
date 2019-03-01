﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static PathFinding.Helper.GridBuilder;

namespace PathFinding.Helper
{
    public struct GridNode
    {
        public Position CurrentPos;
        public Position PreviousPos;

        public GridNode(int x, int y)
        {
            CurrentPos = new Position(x, y);
            PreviousPos = new Position(x, y);
        }
    }

    [DebuggerDisplay("{X}, {Y}")]
    public struct Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(Quadrant quad)
        {
            switch (quad)
            {
                case Quadrant.North:
                    Y--;
                    break;
                case Quadrant.East:
                    X++;
                    break;
                case Quadrant.South:
                    Y++;
                    break;
                case Quadrant.West:
                    X--;
                    break;
                default:
                    throw new System.Exception("Can't move in this direction.");
            }
        }

        public override bool Equals(object obj)
        {
            var _obj = (Position)obj;

            return this.X == _obj.X && this.Y == _obj.Y;
        }

        public int X;
        public int Y;
    }

    public enum Quadrant
    {
        East = 0,
        South = 1,
        West = 2,
        North = 3,

        MAX = North
    }


    public class GridNavigator
    {
        public bool Success { get; private set; } = false;
        public Position StartPos;
        public Position GoalPos;

        private readonly int[,] Grid;
        private List<Position> Path = new List<Position>();
        private int MaxStepsBack { get; set; }
        private int TimesSteppedBack { get; set; } = 0;

        private Quadrant GetSecondSide(double angle, Quadrant notQuadrant)
        {
            switch (notQuadrant)
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

            if (CanMove(node, result = GetQuadrant(angleToGoal)))
            {
                return result;
            }

            if (CanMove(node, result = GetSecondSide(angleToGoal, result)))
            {
                return result;
            }

            if (CanMove(node, result = GetQuadrant(angleToGoal + 180)))
            {
                return result;
            }

            if (CanMove(node, result = GetSecondSide(angleToGoal + 180, result)))
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
                    CanDo = !Path.Contains(new Position(node.CurrentPos.X, node.CurrentPos.Y - 1));
                    CanDo = CanDo && node.CurrentPos.Y - 1 >= 0;
                    CanDo = CanDo && Grid[node.CurrentPos.X, node.CurrentPos.Y - 1] != (int)BoxState.Block;
                    CanDo = CanDo && Grid[node.CurrentPos.X, node.CurrentPos.Y - 1] != (int)BoxState.DeadEnd;
                    break;
                case Quadrant.East:
                    CanDo = !Path.Contains(new Position(node.CurrentPos.X + 1, node.CurrentPos.Y));
                    CanDo = CanDo && node.CurrentPos.X + 1 < GridSize;
                    CanDo = CanDo && Grid[node.CurrentPos.X + 1, node.CurrentPos.Y] != (int)BoxState.Block;
                    CanDo = CanDo && Grid[node.CurrentPos.X + 1, node.CurrentPos.Y] != (int)BoxState.DeadEnd;
                    break;
                case Quadrant.South:
                    CanDo = !Path.Contains(new Position(node.CurrentPos.X, node.CurrentPos.Y + 1));
                    CanDo = CanDo && node.CurrentPos.Y + 1 < GridSize;
                    CanDo = CanDo && Grid[node.CurrentPos.X, node.CurrentPos.Y + 1] != (int)BoxState.Block;
                    CanDo = CanDo && Grid[node.CurrentPos.X, node.CurrentPos.Y + 1] != (int)BoxState.DeadEnd;
                    break;
                case Quadrant.West:
                    CanDo = !Path.Contains(new Position(node.CurrentPos.X - 1, node.CurrentPos.Y));
                    CanDo = CanDo && node.CurrentPos.X - 1 >= 0;
                    CanDo = CanDo && Grid[node.CurrentPos.X - 1, node.CurrentPos.Y] != (int)BoxState.Block;
                    CanDo = CanDo && Grid[node.CurrentPos.X - 1, node.CurrentPos.Y] != (int)BoxState.DeadEnd;
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

        public GridNavigator(int[,] grid, int maxStepsBack = 3)
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
