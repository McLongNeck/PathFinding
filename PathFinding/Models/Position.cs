using PathFinding.Models.Enums;
using System.Diagnostics;

namespace PathFinding.Models
{
    [DebuggerDisplay("{X}, {Y}")]
    public struct Position
    {
        public short X;
        public short Y;

        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        public void Move(Quadrant direction)
        {
            switch (direction)
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
    }
}
