using System.Collections.Generic;
using System.Linq;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Stats;

namespace HDT.Plugins.EndGame.Archetype
{
	public class PlayedDeck
	{
		public PlayerClass Klass { get; set; }
		public Format Format { get; set; }
		public int Turns { get; set; }
		public List<Card> Cards { get; set; }

		public PlayedDeck()
		{
		}

		public PlayedDeck(string klass, Format format, int turns, List<TrackedCard> cards)
		{
			Klass = ConvertKlass(klass);
			Format = format;
			Turns = turns;
			Cards = cards.Select(x => new Card(x)).ToList();
		}

		public bool Matches(Deck deck)
		{
			return deck.Cards.All(c => this.Cards.Contains(c));
		}

		private PlayerClass ConvertKlass(string klass)
		{
			switch (klass.ToLowerInvariant())
			{
				case "druid":
					return PlayerClass.DRUID;

				case "hunter":
					return PlayerClass.HUNTER;

				case "mage":
					return PlayerClass.MAGE;

				case "paladin":
					return PlayerClass.PALADIN;

				case "priest":
					return PlayerClass.PRIEST;

				case "rogue":
					return PlayerClass.ROGUE;

				case "shaman":
					return PlayerClass.SHAMAN;

				case "warlock":
					return PlayerClass.WARLOCK;

				case "warrior":
				default:
					return PlayerClass.WARRIOR;
			}
		}
	}
}