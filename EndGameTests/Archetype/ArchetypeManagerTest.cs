using System.Collections.Generic;
using System.IO;
using System.Linq;
using HDT.Plugins.EndGame.Archetype;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Stats;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HDT.Plugins.EndGame.Tests.Archetype
{
	[TestClass]
	public class ArchetypeManagerTest
	{
		private ArchetypeManager _manager;

		private string _json = "[{\"Name\":\"One\",\"Style\":{\"Name\":\"Control\",\"Style\":0},\"Klass\":4,\"Format\":1,\"Cards\":[{\"Id\":\"AB_123\",\"Count\":1},{\"Id\":\"AB_124\",\"Count\":1},{\"Id\":\"AB_125\",\"Count\":1}]}]";

		private List<ArchetypeDeck> _decks = new List<ArchetypeDeck>() {
			new ArchetypeDeck("One", PlayerClass.HUNTER, Format.Standard, ArchetypeStyles.CONTROL,
				new List<Card>() {
					new SingleCard("AB_123"),
					new SingleCard("AB_124"),
					new SingleCard("AB_125")
				})
		};

		[TestInitialize]
		public void Setup()
		{
			_manager = new ArchetypeManager();
		}

		[TestMethod]
		public void AddDeck()
		{
			Assert.AreEqual(0, _manager.Decks.Count);
			_manager.Add(new ArchetypeDeck());
			Assert.AreEqual(1, _manager.Decks.Count);
		}

		[TestMethod]
		public void DontAddDuplicateDeck()
		{
			_manager.Add(new ArchetypeDeck(
				"Face ",
				PlayerClass.HUNTER,
				Format.Standard,
				ArchetypeStyles.AGGRO,
				new List<Card>() {
					new SingleCard("AB_123"),
					new SingleCard("AB_456")
				}
			));
			_manager.Add(new ArchetypeDeck(
				"Face ",
				PlayerClass.HUNTER,
				Format.Standard,
				ArchetypeStyles.AGGRO,
				new List<Card>() {
					new SingleCard("AB_123"),
					new SingleCard("AB_456")
				}
			));
			Assert.AreEqual(1, _manager.Decks.Count);
		}

		[TestMethod]
		public void RemoveDeck()
		{
			_manager.Add(new ArchetypeDeck(
				"Face ",
				PlayerClass.HUNTER,
				Format.Standard,
				ArchetypeStyles.AGGRO,
				new List<Card>() {
					new SingleCard("AB_123"),
					new SingleCard("AB_456")
				}
			));
			_manager.Add(new ArchetypeDeck(
				"rAmp",
				PlayerClass.DRUID,
				Format.Wild,
				ArchetypeStyles.COMBO,
				new List<Card>() {
					new SingleCard("AB_987"),
					new SingleCard("AB_321")
				}
			));
			Assert.AreEqual(2, _manager.Decks.Count);
			_manager.Remove(new ArchetypeDeck(
				"rAmp",
				PlayerClass.DRUID,
				Format.Wild,
				ArchetypeStyles.COMBO,
				new List<Card>() {
					new SingleCard("AB_987"),
					new SingleCard("AB_321")
				}
			));
			Assert.AreEqual(1, _manager.Decks.Count);
		}

		[TestMethod]
		public void FindBestMatch()
		{
			_manager.Add(new ArchetypeDeck(
				"Face ",
				PlayerClass.HUNTER,
				Format.Standard,
				ArchetypeStyles.AGGRO,
				new List<Card>() {
					new SingleCard("AB_123"),
					new SingleCard("AB_456")
				}
			));
			_manager.Add(new ArchetypeDeck(
				"rAmp",
				PlayerClass.DRUID,
				Format.Wild,
				ArchetypeStyles.COMBO,
				new List<Card>() {
					new SingleCard("AB_987"),
					new SingleCard("AB_321")
				}
			));
			var deck = new PlayedDeck("Druid", Format.Wild, 5, new List<TrackedCard>() {
				new TrackedCard("OG_129", 1),
				new TrackedCard("AT_387", 1),
				new TrackedCard("AB_321", 1),
				new TrackedCard("FP1_298", 1)
			});
			Assert.AreEqual("rAmp", _manager.Find(deck).First().Name);
		}

		// TODO: Improve these tests, a lot!

		[TestMethod]
		public void LoadArcheyptesFromFile()
		{
			_manager.LoadDecks("data/decks.json");
			Assert.AreEqual(1, _manager.Decks.Count);
			Assert.AreEqual(_decks[0].Name, _manager.Decks[0].Name);
			Assert.AreEqual(_decks[0].Klass, _manager.Decks[0].Klass);
		}

		[TestMethod]
		public void SaveArcheyptesFromFile()
		{
			_manager.Add(_decks.First());
			_manager.SaveDecks("data/saved.json");
			var read = File.ReadAllText("data/saved.json");
			Assert.AreEqual(_json, read);
		}
	}
}