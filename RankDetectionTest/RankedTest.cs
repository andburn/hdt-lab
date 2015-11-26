using Hearthstone.Ranked;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace RankDetectionTest
{
    [TestClass]
    public class RankedTest
    {
        [TestMethod]
        public void RankedGame_IsSuccessful()
        {
            var result = RankDetection.Match(new Bitmap(@"data\Ranked_14_14_Hi.png"));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void RankedGame_IsCorrect()
        {
            var result = RankDetection.Match(new Bitmap(@"data\Ranked_14_14_Hi.png"));
            Assert.AreEqual(14, result.Player);
        }
    }
}
