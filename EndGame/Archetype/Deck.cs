using System.Collections.Generic;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;

namespace HDT.Plugins.EndGame.Archetype
{
	public abstract class Deck
	{
		public PlayerClass Klass { get; set; }
		public Format Format { get; set; }
		public List<Card> Cards { get; set; }

		public Deck()
		{
		}

		public Deck(PlayerClass klass, Format format, List<Card> cards)
		{
			Klass = klass;
			Format = format;
			Cards = cards;
		}

		public Deck(string klass, Format format, List<Card> cards)
		{
			Klass = ConvertKlass(klass);
			Format = format;
			Cards = cards;
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