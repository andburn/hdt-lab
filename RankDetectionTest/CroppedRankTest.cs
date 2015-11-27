using System.Drawing;
using Hearthstone.Ranked;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RankDetectionTest
{
	[TestClass]
	public class CroppedRankTest
	{
		[TestMethod]
		public void Rank01()
		{
			Assert.IsTrue(TestRank(1));
		}

		[TestMethod]
		public void Rank02()
		{
			Assert.IsTrue(TestRank(2));
		}

		[TestMethod]
		public void Rank03()
		{
			Assert.IsTrue(TestRank(3));
		}

		[TestMethod]
		public void Rank04()
		{
			Assert.IsTrue(TestRank(4));
		}

		[TestMethod]
		public void Rank05()
		{
			Assert.IsTrue(TestRank(5));
		}

		[TestMethod]
		public void Rank06()
		{
			Assert.IsTrue(TestRank(6));
		}

		[TestMethod]
		public void Rank07()
		{
			Assert.IsTrue(TestRank(7));
		}

		[TestMethod]
		public void Rank08()
		{
			Assert.IsTrue(TestRank(8));
		}

		[TestMethod]
		public void Rank09()
		{
			Assert.IsTrue(TestRank(9));
		}

		[TestMethod]
		public void Rank10()
		{
			Assert.IsTrue(TestRank(10));
		}

		[TestMethod]
		public void Rank11()
		{
			Assert.IsTrue(TestRank(11));
		}

		[TestMethod]
		public void Rank12()
		{
			Assert.IsTrue(TestRank(12));
		}

		[TestMethod]
		public void Rank13()
		{
			Assert.IsTrue(TestRank(13));
		}

		[TestMethod]
		public void Rank14()
		{
			Assert.IsTrue(TestRank(14));
		}

		[TestMethod]
		public void Rank15()
		{
			Assert.IsTrue(TestRank(15));
		}

		[TestMethod]
		public void Rank16()
		{
			Assert.IsTrue(TestRank(16));
		}

		[TestMethod]
		public void Rank17()
		{
			Assert.IsTrue(TestRank(17));
		}

		[TestMethod]
		public void Rank18()
		{
			Assert.IsTrue(TestRank(18));
		}

		[TestMethod]
		public void Rank19()
		{
			Assert.IsTrue(TestRank(19));
		}

		[TestMethod]
		public void Rank20()
		{
			Assert.IsTrue(TestRank(20));
		}

		[TestMethod]
		public void Rank21()
		{
			Assert.IsTrue(TestRank(21));
		}

		[TestMethod]
		public void Rank22()
		{
			Assert.IsTrue(TestRank(22));
		}

		[TestMethod]
		public void Rank23()
		{
			Assert.IsTrue(TestRank(23));
		}

		[TestMethod]
		public void Rank24()
		{
			Assert.IsTrue(TestRank(24));
		}

		[TestMethod]
		public void Rank25()
		{
			Assert.IsTrue(TestRank(25));
		}

		// Helper Methods

		private bool TestRank(int rank)
		{
			return rank == RankDetection.FindBest(new Bitmap(@".\data\" + rank + ".png"));
        }
	}
}
