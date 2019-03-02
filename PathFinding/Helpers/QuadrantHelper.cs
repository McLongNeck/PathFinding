using PathFinding.Models.Enums;
using System;

namespace PathFinding.Helpers
{
    public static class QuadrantHelper
    {
        public static Quadrant Opposite(Quadrant quadrant)
        {
            switch (quadrant)
            {
                case Quadrant.East:
                    return Quadrant.West;
                case Quadrant.South:
                    return Quadrant.North;
                case Quadrant.West:
                    return Quadrant.East;
                case Quadrant.North:
                    return Quadrant.South;
                default:
                    throw new Exception("This is some shit.");
            }
        }

        public static Quadrant ConstrainQuadrant(int quadrant)
        {
            var quadrantCount = Enum.GetNames(typeof(Quadrant)).Length;

            while (quadrant >= quadrantCount)
            {
                quadrant -= quadrantCount;
            }

            return (Quadrant)quadrant;
        }

        public static Quadrant GetQuadrant(double angle)
        {
            angle = AngleHelper.ContraintAngle(angle);

            var quad = (int)Math.Round((angle / 90));
            var result = ConstrainQuadrant(quad);

            return result;
        }

        /// <summary>
        /// Will get the second quadrant based on angle. So when angle points NNE, first choice would be North, since it's closest. 
        /// When excluding North, East will become your next best option.
        /// </summary>
        public static Quadrant GetSecondQuadrant(double angle, Quadrant firstQuadrant)
        {
            angle = AngleHelper.ContraintAngle(angle);

            switch (firstQuadrant)
            {
                case Quadrant.East:
                    return angle >= 0 && angle < 90 ? Quadrant.South : Quadrant.North;
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
    }
}
