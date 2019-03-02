using PathFinding.Models.Enums;
using System;

namespace PathFinding.Helpers
{
    public static class QuadrantHelper
    {
        public static Quadrant GetQuadrant(double angle)
        {
            var res = Math.Round((angle / 90));

            if (res > (int)Quadrant.MAX)
            {
                res -= (int)Quadrant.MAX + 1;
            }

            return (Quadrant)res;
        }

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
    }
}
