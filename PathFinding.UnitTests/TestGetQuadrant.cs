using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Helpers;
using PathFinding.Models;
using PathFinding.Models.Enums;

namespace PathFinding.UnitTests
{
    [TestClass]
    public class TestGetQuadrant
    {
        private readonly short[,] grid;

        public TestGetQuadrant()
        {
            grid = GridBuilder.InitGrid();
        }

        [TestMethod]
        public void GetQuadrantNorth()
        {
            var start = GridBuilder.SetStart(new Position(1, 1), grid);
            var goal = GridBuilder.SetGoal(new Position(1, 0), grid);
            var angle = AngleHelper.GetAngle(start, goal);

            var result = QuadrantHelper.GetQuadrant(angle);

            Assert.AreEqual(Quadrant.North, result);
        }

        [TestMethod]
        public void GetQuadrantEast()
        {
            var start = GridBuilder.SetStart(new Position(1, 1), grid);
            var goal = GridBuilder.SetGoal(new Position(2, 1), grid);
            var angle = AngleHelper.GetAngle(start, goal);

            var result = QuadrantHelper.GetQuadrant(angle);

            Assert.AreEqual(Quadrant.East, result);
        }

        [TestMethod]
        public void GetQuadrantSouth()
        {
            var start = GridBuilder.SetStart(new Position(0, 0), grid);
            var goal = GridBuilder.SetGoal(new Position(0, 1), grid);
            var angle = AngleHelper.GetAngle(start, goal);

            var result = QuadrantHelper.GetQuadrant(angle);

            Assert.AreEqual(Quadrant.South, result);
        }

        [TestMethod]
        public void GetQuadrantWest()
        {
            var start = GridBuilder.SetStart(new Position(1, 1), grid);
            var goal = GridBuilder.SetGoal(new Position(0, 1), grid);
            var angle = AngleHelper.GetAngle(start, goal);

            var result = QuadrantHelper.GetQuadrant(angle);

            Assert.AreEqual(Quadrant.West, result);
        }

        [TestMethod]
        public void GetQuadrantEdge()
        {
            var start = GridBuilder.SetStart(new Position(2, 2), grid);
            var goal = GridBuilder.SetGoal(new Position(1, 1), grid);
            var angle = AngleHelper.GetAngle(start, goal);

            var result = QuadrantHelper.GetQuadrant(angle);

            Assert.AreEqual(Quadrant.West, result);
        }

        [TestMethod]
        public void GetQuadrantNorthEdge()
        {
            var start = GridBuilder.SetStart(new Position(1, 3), grid);
            var goal = GridBuilder.SetGoal(new Position(0, 0), grid);
            var angle = AngleHelper.GetAngle(start, goal);

            var result = QuadrantHelper.GetQuadrant(angle);

            Assert.AreEqual(Quadrant.North, result);
        }

        [TestMethod]
        public void GetQuadrantNorthEastEdge()
        {
            var start = GridBuilder.SetStart(new Position(1, 1), grid);
            var goal = GridBuilder.SetGoal(new Position(3, 0), grid);
            var angle = AngleHelper.GetAngle(start, goal);

            var result = QuadrantHelper.GetQuadrant(angle);

            Assert.AreEqual(Quadrant.East, result);
        }
    }
}
