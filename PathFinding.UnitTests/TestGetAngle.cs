using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Helpers;
using PathFinding.Models;

namespace PathFinding.UnitTests
{
    [TestClass]
    public class TestGetAngle
    {
        [TestMethod]
        public void GetAngleUp()
        {
            var result = AngleHelper.GetAngle(new Position(1, 1), new Position(1, 0));

            Assert.AreEqual(270, result);
        }

        [TestMethod]
        public void GetAngleRight()
        {
            var result = AngleHelper.GetAngle(new Position(1, 1), new Position(2, 1));

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAngleDown()
        {
            var result = AngleHelper.GetAngle(new Position(0, 0), new Position(0, 1));

            Assert.AreEqual(90, result);
        }

        [TestMethod]
        public void GetAngleLeft()
        {
            var result = AngleHelper.GetAngle(new Position(1, 1), new Position(0, 1));

            Assert.AreEqual(180, result);
        }
    }
}
