using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Helpers;
using PathFinding.Models;
using System.Collections.Generic;
using System.Linq;

namespace PathFinding.UnitTests
{
    [TestClass]
    public class TestGetPath
    {
        private readonly short[,] grid;
        private Navigator navigator;

        public TestGetPath()
        {
            grid = GridBuilder.InitGrid();
            navigator = new Navigator(grid);
        }

        [TestMethod]
        public void GetDirectPath()
        {
            var start = GridBuilder.SetStart(new Position(1, 1), grid);
            var goal = GridBuilder.SetGoal(new Position(3, 1), grid);
            navigator.StartPos = start;
            navigator.GoalPos = goal;

            var expectedPath = new List<Position>();
            expectedPath.Add(new Position(1, 1));
            expectedPath.Add(new Position(2, 1));
            expectedPath.Add(new Position(3, 1));

            var path = navigator.Go();

            Assert.AreEqual(expectedPath.SequenceEqual(path), true);
        }

        [TestMethod]
        public void GetPathDirectDiagonal()
        {
            var start = GridBuilder.SetStart(new Position(0, 0), grid);
            var goal = GridBuilder.SetGoal(new Position(4, 4), grid);
            navigator.StartPos = start;
            navigator.GoalPos = goal;

            var expectedPath = new List<Position>();
            expectedPath.Add(new Position(0, 0));
            expectedPath.Add(new Position(1, 0));
            expectedPath.Add(new Position(1, 1));
            expectedPath.Add(new Position(2, 1));
            expectedPath.Add(new Position(2, 2));
            expectedPath.Add(new Position(3, 2));
            expectedPath.Add(new Position(3, 3));
            expectedPath.Add(new Position(4, 3));
            expectedPath.Add(new Position(4, 4));

            var path = navigator.Go();

            Assert.AreEqual(expectedPath.SequenceEqual(path), true);
        }

        [TestMethod]
        public void GetPathSimpleBlock()
        {
            var start = GridBuilder.SetStart(new Position(1, 1), grid);
            var block = GridBuilder.SetBlock(new Position(2, 1), grid);
            var goal = GridBuilder.SetGoal(new Position(3, 1), grid);

            var navigator = new Navigator(grid);
            navigator.StartPos = start;
            navigator.GoalPos = goal;

            var expectedPath = new List<Position>();
            expectedPath.Add(new Position(1, 1));
            expectedPath.Add(new Position(1, 2));
            expectedPath.Add(new Position(2, 2));
            expectedPath.Add(new Position(3, 2));
            expectedPath.Add(new Position(3, 1));

            var path = navigator.Go();

            Assert.AreEqual(expectedPath.SequenceEqual(path), true);
        }

        [TestMethod]
        public void GetPathSimpleWall()
        {
            var start = GridBuilder.SetStart(new Position(0, 0), grid);
            var goal = GridBuilder.SetGoal(new Position(3, 0), grid);

            GridBuilder.SetBlock(new Position(2, 0), grid);
            GridBuilder.SetBlock(new Position(2, 1), grid);

            var navigator = new Navigator(grid);
            navigator.StartPos = start;
            navigator.GoalPos = goal;

            var expectedPath = new List<Position>();
            expectedPath.Add(new Position(0, 0));
            expectedPath.Add(new Position(1, 0));
            expectedPath.Add(new Position(1, 1));
            expectedPath.Add(new Position(1, 2));
            expectedPath.Add(new Position(2, 2));
            expectedPath.Add(new Position(3, 2));
            expectedPath.Add(new Position(3, 1));
            expectedPath.Add(new Position(3, 0));

            var path = navigator.Go();

            Assert.AreEqual(expectedPath.SequenceEqual(path), true);
        }

        [TestMethod]
        public void GetPathBigWall()
        {
            var start = GridBuilder.SetStart(new Position(0, 0), grid);
            var goal = GridBuilder.SetGoal(new Position(3, 0), grid);

            GridBuilder.SetBlock(new Position(2, 0), grid);
            GridBuilder.SetBlock(new Position(2, 1), grid);
            GridBuilder.SetBlock(new Position(2, 2), grid);

            var navigator = new Navigator(grid);
            navigator.StartPos = start;
            navigator.GoalPos = goal;

            var expectedPath = new List<Position>();
            expectedPath.Add(new Position(0, 0));
            expectedPath.Add(new Position(1, 0));
            expectedPath.Add(new Position(1, 1));
            expectedPath.Add(new Position(1, 2));
            expectedPath.Add(new Position(1, 3));
            expectedPath.Add(new Position(2, 3));
            expectedPath.Add(new Position(3, 3));
            expectedPath.Add(new Position(3, 2));
            expectedPath.Add(new Position(3, 1));
            expectedPath.Add(new Position(3, 0));

            var path = navigator.Go();

            Assert.AreEqual(expectedPath.SequenceEqual(path), true);
        }

        [TestMethod]
        public void GetPathCorner()
        {
            var start = GridBuilder.SetStart(new Position(0, 0), grid);
            var goal = GridBuilder.SetGoal(new Position(4, 0), grid);

            GridBuilder.SetBlock(new Position(3, 0), grid);
            GridBuilder.SetBlock(new Position(2, 1), grid);

            var navigator = new Navigator(grid);
            navigator.StartPos = start;
            navigator.GoalPos = goal;

            var expectedPath = new List<Position>();
            expectedPath.Add(new Position(0, 0));
            expectedPath.Add(new Position(1, 0));
            expectedPath.Add(new Position(1, 1));
            expectedPath.Add(new Position(1, 2));
            expectedPath.Add(new Position(2, 2));
            expectedPath.Add(new Position(3, 2));
            expectedPath.Add(new Position(3, 1));
            expectedPath.Add(new Position(4, 1));
            expectedPath.Add(new Position(4, 0));

            var path = navigator.Go();

            Assert.AreEqual(expectedPath.SequenceEqual(path), true);
        }

        [TestMethod]
        public void GetPathDeepCorner()
        {
            var start = GridBuilder.SetStart(new Position(0, 0), grid);
            var goal = GridBuilder.SetGoal(new Position(3, 0), grid);

            GridBuilder.SetBlock(new Position(1, 1), grid);
            GridBuilder.SetBlock(new Position(1, 2), grid);

            GridBuilder.SetBlock(new Position(2, 0), grid);

            GridBuilder.SetBlock(new Position(3, 1), grid);
            GridBuilder.SetBlock(new Position(3, 2), grid);

            var navigator = new Navigator(grid);
            navigator.StartPos = start;
            navigator.GoalPos = goal;

            var expectedPath = new List<Position>();
            expectedPath.Add(new Position(0, 0));
            expectedPath.Add(new Position(0, 1));
            expectedPath.Add(new Position(0, 2));
            expectedPath.Add(new Position(0, 3));
            expectedPath.Add(new Position(1, 3));
            expectedPath.Add(new Position(2, 3));
            expectedPath.Add(new Position(3, 3));
            expectedPath.Add(new Position(4, 3));
            expectedPath.Add(new Position(4, 2));
            expectedPath.Add(new Position(4, 1));
            expectedPath.Add(new Position(4, 0));
            expectedPath.Add(new Position(3, 0));

            var path = navigator.Go();

            Assert.AreEqual(expectedPath.SequenceEqual(path), true);
        }
    }
}
