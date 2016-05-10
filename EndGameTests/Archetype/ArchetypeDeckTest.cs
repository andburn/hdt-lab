﻿using System.Collections.Generic;
using HDT.Plugins.EndGame.Archetype;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HDT.Plugins.EndGame.Tests.Archetype
{
	[TestClass]
	public class ArchetypeDeckTest
	{
		[TestMethod]
		public void DefaultConstructorHasDefaultProps()
		{
			var deck = new ArchetypeDeck();
			Assert.IsNull(deck.Name);
			Assert.IsNull(deck.Style);
			Assert.AreEqual(PlayerClass.WARRIOR, deck.Klass);
			Assert.AreEqual(Format.All, deck.Format);
			Assert.IsNull(deck.Cards);
		}

		[TestMethod]
		public void ParamConstructorAssignsProps()
		{
			var deck = new ArchetypeDeck(
				"Control Warrior",
				PlayerClass.WARRIOR,
				GameFormat.STANDARD,
				ArchetypeStyles.CONTROL,
				new List<Card>()
			);
			Assert.AreEqual("Control Warrior", deck.Name);
			Assert.AreEqual(ArchetypeStyles.CONTROL, deck.Style);
			Assert.AreEqual(PlayerClass.WARRIOR, deck.Klass);
			Assert.AreEqual(Format.Standard, deck.Format);
			Assert.IsTrue(deck.Cards.Count == 0);
		}

		[TestMethod]
		public void TestToString()
		{
			var deck = new ArchetypeDeck(
				"Control Warrior",
				PlayerClass.WARRIOR,
				GameFormat.STANDARD,
				ArchetypeStyles.CONTROL,
				new List<Card>()
			);
			Assert.AreEqual("Control Warrior", deck.ToString());
		}

		[TestMethod]
		public void TestToNoteString()
		{
			var deck = new ArchetypeDeck(
				"Control Warrior",
				PlayerClass.WARRIOR,
				GameFormat.STANDARD,
				ArchetypeStyles.CONTROL,
				new List<Card>()
			);
			Assert.AreEqual("Control Warrior : WARRIOR.CONTROL", deck.ToNoteString());
		}
	}
}