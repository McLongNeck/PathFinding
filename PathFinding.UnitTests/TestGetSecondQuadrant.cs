using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Helpers;
using PathFinding.Models.Enums;

namespace PathFinding.UnitTests
{
    [TestClass]
    public class TestGetSecondQuadrant
    {
        /* Happy Flow Cases */
        [TestMethod]
        public void GetSecondQuadrantNNE()
        {
            var result = QuadrantHelper.GetSecondQuadrant(280, Quadrant.North);

            Assert.AreEqual(Quadrant.East, result);
        }

        [TestMethod]
        public void GetSecondQuadrantNNW()
        {
            var result = QuadrantHelper.GetSecondQuadrant(260, Quadrant.North);

            Assert.AreEqual(Quadrant.West, result);
        }

        [TestMethod]
        public void GetSecondQuadrantEEN()
        {
            var result = QuadrantHelper.GetSecondQuadrant(350, Quadrant.East);

            Assert.AreEqual(Quadrant.North, result);
        }

        [TestMethod]
        public void GetSecondQuadrantEES()
        {
            var result = QuadrantHelper.GetSecondQuadrant(10, Quadrant.East);

            Assert.AreEqual(Quadrant.South, result);
        }

        [TestMethod]
        public void GetSecondQuadrantSSE()
        {
            var result = QuadrantHelper.GetSecondQuadrant(80, Quadrant.South);

            Assert.AreEqual(Quadrant.East, result);
        }

        [TestMethod]
        public void GetSecondQuadrantSSW()
        {
            var result = QuadrantHelper.GetSecondQuadrant(100, Quadrant.South);

            Assert.AreEqual(Quadrant.West, result);
        }

        [TestMethod]
        public void GetSecondQuadrantWWS()
        {
            var result = QuadrantHelper.GetSecondQuadrant(170, Quadrant.West);

            Assert.AreEqual(Quadrant.South, result);
        }

        [TestMethod]
        public void GetSecondQuadrantWWN()
        {
            var result = QuadrantHelper.GetSecondQuadrant(190, Quadrant.West);

            Assert.AreEqual(Quadrant.North, result);
        }

        /* Edge Cases */
        [TestMethod]
        public void GetSecondQuadrantNNEPlusFullCircle()
        {
            var result = QuadrantHelper.GetSecondQuadrant(280 + 360, Quadrant.North);

            Assert.AreEqual(Quadrant.East, result);
        }

        [TestMethod]
        public void GetSecondQuadrantNNEMinusFullCircle()
        {
            var result = QuadrantHelper.GetSecondQuadrant(280 - 360, Quadrant.North);

            Assert.AreEqual(Quadrant.East, result);
        }
    }
}
