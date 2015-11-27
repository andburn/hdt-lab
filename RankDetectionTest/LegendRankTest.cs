using System;
using System.Drawing;
using Hearthstone.Ranked;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RankDetectionTest
{
	[TestClass]
	public class LegendRankTest
	{
		private static RankResult _result;

		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			_result = RankDetection.Match(new Bitmap(@"data\LegendGame.png"));
		}

		[TestMethod]
		public void LegendRank_IsSuccessful()
		{
			Assert.IsTrue(_result.Success);
		}

		[TestMethod]
		public void LegendRank_IsCorrect()
		{
			Assert.AreEqual(0, _result.Player);
			Assert.AreEqual(0, _result.Opponent);
		}

		[TestMethod]
		public void LegendRankCroppedA()
		{
			var result = RankDetection.FindBest(new Bitmap(@"data\LegendA.png"));
			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void LegendRankCroppedB()
		{
			var result = RankDetection.FindBest(new Bitmap(@"data\LegendB.png"));
			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void LegendRankCroppedC()
		{
			var result = RankDetection.FindBest(new Bitmap(@"data\LegendC.png"));
			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void LegendRankCroppedD()
		{
			var result = RankDetection.FindBest(new Bitmap(@"data\LegendD.png"));
			Assert.AreEqual(0, result);
		}
	}
}
