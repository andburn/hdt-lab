using System;
using System.Drawing;
using Hearthstone.Ranked;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RankDetectionTest
{
	// Tests to verify correct rank medal locations for player and oppoenent.
	// Possible change to player rank/label location increase in ~4 pixels,
	// at 768 height. Probably from LOE patch 4.0.0.10833.

	[TestClass]
	public class ResolutionTest
	{
		// Testes for current captures (November 2015) - should all pass

		[TestMethod]
		public void Height_1080()
		{
			Assert.IsTrue(TestCapture("1080"));
		}

		[TestMethod]
		public void Height_1050()
		{
			Assert.IsTrue(TestCapture("1050"));
		}

		[TestMethod]
		public void Height_1020()
		{
			Assert.IsTrue(TestCapture("1020"));
		}

		[TestMethod]
		public void Height_987()
		{
			Assert.IsTrue(TestCapture("987", 4, 4));
		}

		[TestMethod]
		public void Height_900()
		{
			Assert.IsTrue(TestCapture("900"));
		}

		[TestMethod]
		public void Height_864()
		{
			Assert.IsTrue(TestCapture("864"));
		}

		[TestMethod]
		public void Height_768()
		{
			Assert.IsTrue(TestCapture("768"));
		}

		[TestMethod]
		public void Height_708()
		{
			Assert.IsTrue(TestCapture("708"));
		}

		[TestMethod]
		public void Height_672()
		{
			Assert.IsTrue(TestCapture("672"));
		}

		// Tests for older captures (in 2015) - should all fail

		[TestMethod]
		public void Height_Oct_1080()
		{
			Assert.IsFalse(TestCapture("Oct_1080", 11, 11));
		}

		[TestMethod]
		public void Height_Jun_1080()
		{
			Assert.IsFalse(TestCapture("Jun_1080", 14, 14));
		}

		[TestMethod]
		public void Height_Jul_768()
		{
			Assert.IsFalse(TestCapture("Jul_768", 15, 15));
		}

		// Helper Methods

		private bool TestCapture(string height, int ply = 8, int opp = 8)
		{
			var result = RankDetection.Match(new Bitmap(@"data\Height_" + height + ".png"));
			return result.Player == ply && result.Opponent == opp;
		}

	}
}
