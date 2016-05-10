using System.Collections.Generic;
using HDT.Plugins.EndGame.Archetype;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HDT.Plugins.EndGame.Tests.Archetype
{
	[TestClass]
	public class DeckTest
	{
		private class ConcreteDeck : Deck
		{
			public ConcreteDeck() : base()
			{
			}

			public ConcreteDeck(PlayerClass klass, GameFormat format, List<Card> cards)
				: base(klass, format, cards)
			{
			}
		}

		[TestMethod]
		public void DefaultConstructorHasDefaultProps()
		{
			var deck = new ConcreteDeck();

			Assert.AreEqual(PlayerClass.WARRIOR, deck.Klass);
			Assert.AreEqual(Format.All, deck.Format);
			Assert.IsNull(deck.Cards);
		}

		[TestMethod]
		public void ParamConstructorAssignsProps()
		{
			var cards = new List<Card>() {
				new Card("AB_123", 1),
				new Card("AB_456", 2)
			};
			var deck = new ConcreteDeck(PlayerClass.DRUID, GameFormat.STANDARD, cards);

			Assert.AreEqual(PlayerClass.DRUID, deck.Klass);
			Assert.AreEqual(Format.Standard, deck.Format);
			Assert.AreEqual(cards, deck.Cards);
		}

		[TestMethod]
		public void EqualByReference()
		{
			var cards = new List<Card>() {
				new Card("AB_123", 1),
				new Card("AB_456", 2)
			};
			var deck = new ConcreteDeck(PlayerClass.DRUID, GameFormat.STANDARD, cards);
			Assert.AreEqual(deck, new ConcreteDeck(PlayerClass.DRUID, GameFormat.STANDARD, cards));
		}

		[TestMethod]
		public void EqualByValue()
		{
			var deckA = new ConcreteDeck(PlayerClass.DRUID, GameFormat.STANDARD,
				new List<Card>() {
					new Card("AB_123", 1),
					new Card("AB_456", 2)
				}
			);
			var deckB = new ConcreteDeck(PlayerClass.DRUID, GameFormat.STANDARD,
				new List<Card>() {
					new Card("AB_123", 1),
					new Card("AB_456", 2)
				}
			);
			Assert.AreEqual(deckA, deckB);
		}
	}
}