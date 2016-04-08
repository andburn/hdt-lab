using Hearthstone.Ranked;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace RankDetectionTest
{
    [TestClass]
    public class RankedDetectionTest
    {
		private static RankResult _resultHi;
		private static RankResult _resultLo;

		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			_resultHi = RankDetection.Match(new Bitmap(@"data\Ranked_14_14_Hi.png"));
			_resultLo = RankDetection.Match(new Bitmap(@"data\Ranked_14_14_Lo.png"));
		}

        [TestMethod]
        public void RankedGame_IsSuccessful_Hi()
        {
            Assert.IsTrue(_resultHi.Success);
        }

		[TestMethod]
		public void RankedGame_IsSuccessful_Lo()
		{
			Assert.IsTrue(_resultLo.Success);
		}

		[TestMethod]
        public void RankedGame_PlayerIsCorrect_Hi()
        {
            Assert.AreEqual(14, _resultHi.Player);
		}

		[TestMethod]
		public void RankedGame_PlayerIsCorrect_Lo()
		{
			Assert.AreEqual(14, _resultLo.Player);
		}

		[TestMethod]
		public void RankedGame_OpponentIsCorrect_Hi()
		{
			Assert.AreEqual(14, _resultHi.Opponent);
		}

		[TestMethod]
		public void RankedGame_OpponentIsCorrect_Lo()
		{
			Assert.AreEqual(14, _resultLo.Opponent);
		}
	}
}
