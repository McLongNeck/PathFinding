using PathFinding.Models;

namespace PathFinding.Helpers
{
    public static class AngleHelper
    {
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

    }
}
