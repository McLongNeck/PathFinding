using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Helpers;
using PathFinding.Models;

namespace PathFinding.UnitTests
{
    [TestClass]
    public class TestGetAngle
    {
        private readonly short[,] grid;

        public TestGetAngle()
        {
            grid = GridBuilder.InitGrid();
        }

        [TestMethod]
        public void GetAngleUp()
        {
            var start = GridBuilder.SetStart(new Position(1, 1), grid);
            var goal = GridBuilder.SetGoal(new Position(1, 0), grid);

            var result = Navigator.GetAngle(start, goal);

            Assert.AreEqual(270, result);
        }

        [TestMethod]
        public void GetAngleRight()
        {
            var start = GridBuilder.SetStart(new Position(1, 1), grid);
            var goal = GridBuilder.SetGoal(new Position(2, 1), grid);

            var result = Navigator.GetAngle(start, goal);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAngleDown()
        {
            var start = GridBuilder.SetStart(new Position(0, 0), grid);
            var goal = GridBuilder.SetGoal(new Position(0, 1), grid);

            var result = Navigator.GetAngle(start, goal);

            Assert.AreEqual(90, result);
        }

        [TestMethod]
        public void GetAngleLeft()
        {
            var start = GridBuilder.SetStart(new Position(1, 1), grid);
            var goal = GridBuilder.SetGoal(new Position(0, 1), grid);

            var result = Navigator.GetAngle(start, goal);

            Assert.AreEqual(180, result);
        }
    }
}
