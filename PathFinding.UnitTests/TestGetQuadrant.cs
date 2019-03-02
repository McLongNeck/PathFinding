using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Helpers;
using PathFinding.Models.Enums;

namespace PathFinding.UnitTests
{
    [TestClass]
    public class TestGetQuadrant
    {
        /* Up, down, left, right */
        [TestMethod]
        public void GetQuadrantNorth()
        {
            var result = QuadrantHelper.GetQuadrant(270);

            Assert.AreEqual(Quadrant.North, result);
        }

        [TestMethod]
        public void GetQuadrantEast()
        {
            var result = QuadrantHelper.GetQuadrant(0);

            Assert.AreEqual(Quadrant.East, result);
        }

        [TestMethod]
        public void GetQuadrantSouth()
        {
            var result = QuadrantHelper.GetQuadrant(90);

            Assert.AreEqual(Quadrant.South, result);
        }

        [TestMethod]
        public void GetQuadrantWest()
        {
            var result = QuadrantHelper.GetQuadrant(180);

            Assert.AreEqual(Quadrant.West, result);
        }

        /* At and around 45 degree angle */
        [TestMethod]
        public void GetQuadrantEdge()
        {
            var result = QuadrantHelper.GetQuadrant(315);

            Assert.AreEqual(Quadrant.East, result);
        }

        [TestMethod]
        public void GetQuadrantEdgeMinusOne()
        {
            var result = QuadrantHelper.GetQuadrant(314);

            Assert.AreEqual(Quadrant.North, result);
        }

        [TestMethod]
        public void GetQuadrantEdgeAddOne()
        {
            var result = QuadrantHelper.GetQuadrant(316);

            Assert.AreEqual(Quadrant.East, result);
        }

        /* Edge cases */
        [TestMethod]
        public void GetQuadrantFullCircle()
        {
            var result = QuadrantHelper.GetQuadrant(360);

            Assert.AreEqual(Quadrant.East, result);
        }

        [TestMethod]
        public void GetQuadrantDoubleFullCircle()
        {
            var result = QuadrantHelper.GetQuadrant(720);

            Assert.AreEqual(Quadrant.East, result);
        }
    }
}
