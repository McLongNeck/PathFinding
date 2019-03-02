using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Helpers;
using PathFinding.Models;
using PathFinding.ViewModels;

namespace PathFinding.UnitTests
{
    [TestClass]
    public class TestCanMove
    {
        private readonly short[,] grid;
        private Navigator navigator;

        public TestCanMove()
        {
            grid = GridBuilder.InitGrid();
            navigator = new Navigator(grid);
        }

        [TestMethod]
        public void OutOfBounds()
        {
            Assert.AreEqual(navigator.CanMove(new GridNode(0, 0), Quadrant.North), false);
            Assert.AreEqual(navigator.CanMove(new GridNode(GridBuilder.GridSize, GridBuilder.GridSize), Quadrant.East), false);
            Assert.AreEqual(navigator.CanMove(new GridNode(GridBuilder.GridSize, GridBuilder.GridSize), Quadrant.South), false);
            Assert.AreEqual(navigator.CanMove(new GridNode(0, 0), Quadrant.West), false);
        }

        [TestMethod]
        public void InsideBounds()
        {
            Assert.AreEqual(navigator.CanMove(new GridNode(2, 2), Quadrant.North), true);
            Assert.AreEqual(navigator.CanMove(new GridNode(2, 2), Quadrant.East), true);
            Assert.AreEqual(navigator.CanMove(new GridNode(2, 2), Quadrant.South), true);
            Assert.AreEqual(navigator.CanMove(new GridNode(2, 2), Quadrant.East), true);
        }

        [TestMethod]
        public void MoveOnBlock()
        {
            GridBuilder.SetBlock(new Position(2, 2), grid);
            var navigator = new Navigator(grid);

            Assert.AreEqual(navigator.CanMove(new GridNode(2, 3), Quadrant.North), false);
            Assert.AreEqual(navigator.CanMove(new GridNode(1, 2), Quadrant.East), false);
            Assert.AreEqual(navigator.CanMove(new GridNode(2, 1), Quadrant.South), false);
            Assert.AreEqual(navigator.CanMove(new GridNode(3, 2), Quadrant.West), false);
        }
    }
}
