using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hearthstone.Ranked;
using System.Drawing;

namespace RankDetectionTest
{
    [TestClass]
    public class NonRankedTest
    {
        [TestMethod]
        public void NonRankedGame_IsNotSuccessful()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsGameAi.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void MenuA_IsNotSuccessful()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsMenuA.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void MenuB_IsNotSuccessful()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsMenuB.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void NonRankedFriendsList_IsNotSuccessful()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsGameAiFriends.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void RankedFriendsList_IsNotSuccessful()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsGameRankedFriends.png"));
            Assert.IsFalse(result.Success);
        }

    }
}
