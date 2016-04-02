using HDT.Plugins.EndGame.Archetype;
using Hearthstone_Deck_Tracker.Stats;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HDT.Plugins.EndGame.Tests
{
	[TestClass]
	public class ArchetypeCardTest
	{
		[TestMethod]
		public void DefaultConstructorHasNullProps()
		{
			var card = new ArchetypeCard();
			Assert.AreEqual(0, card.Count);
			Assert.IsNull(card.Id);
		}

		[TestMethod]
		public void ParamConstructorAssignsProps()
		{
			var card = new ArchetypeCard("OG_123", 2);
			Assert.AreEqual(2, card.Count);
			Assert.AreEqual("OG_123", card.Id);
		}

		[TestMethod]
		public void EqualityOverrideCastable()
		{
			ArchetypeCard card = new ArchetypeCard("OG_123", 2);
			object same = new ArchetypeCard("OG_123", 2);
			object other = new ArchetypeCard("AT_128", 2);
			Assert.IsTrue(card.Equals(same));
			Assert.IsFalse(card.Equals(other));
		}

		[TestMethod]
		public void EqualityOverrideNotCastable()
		{
			ArchetypeCard card = new ArchetypeCard("OG_123", 2);
			object other = "a string";
			object none = null;
			Assert.IsFalse(card.Equals(other));
			Assert.IsFalse(card.Equals(none));
		}

		[TestMethod]
		public void EqualitySameType()
		{
			var card = new ArchetypeCard("OG_123", 2);
			var same = new ArchetypeCard("OG_123", 2);
			var other = new ArchetypeCard("AT_128", 2);
			Assert.IsTrue(card.Equals(same));
			Assert.IsFalse(card.Equals(other));
		}

		[TestMethod]
		public void EqualityTrackedCard()
		{
			var card = new ArchetypeCard("OG_123", 2);
			var same = new TrackedCard("OG_123", 2);
			var other = new TrackedCard("AT_128", 2);
			Assert.IsTrue(card.Equals(same));
			Assert.IsFalse(card.Equals(other));
		}

		[TestMethod]
		public void TestHashCode()
		{
			var card = new ArchetypeCard("OG_123", 2);
			var same = new ArchetypeCard("OG_123", 2);
			Assert.AreEqual(card.GetHashCode(), same.GetHashCode());
		}

		[TestMethod]
		public void TestToString()
		{
			var card = new ArchetypeCard("OG_123", 2);
			Assert.AreEqual("OG_123 x2", card.ToString());
		}
	}
}