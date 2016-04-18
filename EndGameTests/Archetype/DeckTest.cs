using System.Collections.Generic;
using HDT.Plugins.EndGame.Archetype;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Stats;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HDT.Plugins.EndGame.Tests.Archetype
{
	[TestClass]
	public class DeckTest
	{
		#region Additional test attributes

		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//

		#endregion Additional test attributes

		[TestMethod]
		public void Similarity1()
		{
			Deck control = new Deck(
				"Control Warrior",
				PlayerClass.WARRIOR,
				ArchetypeStyles.CONTROL,
				new List<Card>() {
					new SingleCard("OG_123"),
					new SingleCard("OG_789"),
					new SingleCard("OG_456"),
				}
			);

			PlayedDeck opponent = new PlayedDeck("Warrior", Format.Standard, 7,
				new List<TrackedCard>() {
					new TrackedCard("OG_123", 2),
					new TrackedCard("OG_124", 1)
				}
			);

			Assert.AreEqual(0.33, opponent.Similarity(control));
		}

		[TestMethod]
		public void Similarity3()
		{
			Deck control = new Deck(
				"Control Warrior",
				PlayerClass.WARRIOR,
				ArchetypeStyles.CONTROL,
				new List<Card>() {
					new SingleCard("OG_123"),
					new SingleCard("OG_789")
				}
			);

			PlayedDeck opponent = new PlayedDeck("Warrior", Format.Standard, 7,
				new List<TrackedCard>() {
					new TrackedCard("OG_123", 2),
					new TrackedCard("OG_124", 1)
				}
			);

			Assert.AreEqual(0.5, opponent.Similarity(control));
		}

		[TestMethod]
		public void Similarity2()
		{
			List<Card> cards = new List<Card>() {
				new SingleCard("OG_123")
			};
			Deck control = new Deck(
				"Control Warrior",
				PlayerClass.WARRIOR,
				ArchetypeStyles.CONTROL,
				cards);

			List<TrackedCard> tracked = new List<TrackedCard>() {
				new TrackedCard("OG_123", 2),
				new TrackedCard("OG_124", 1)
			};
			PlayedDeck opponent = new PlayedDeck("Warrior", Format.Standard, 7, tracked);

			Assert.AreEqual(1.0, opponent.Similarity(control));
		}
	}
}