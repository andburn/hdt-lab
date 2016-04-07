using System.Collections.Generic;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;

namespace HDT.Plugins.EndGame.Archetype
{
	public class Deck
	{
		public string Name { get; set; }
		public PlayerClass Klass { get; set; }
		public Format Format { get; set; }
		public ArchetypeStyle Style { get; set; }
		public List<Card> Cards { get; set; }

		public Deck()
		{
		}

		public Deck(string name, PlayerClass klass, ArchetypeStyle style, List<Card> cards)
		{
			Name = name;
			Klass = klass;
			Style = style;
			Cards = cards;
		}
	}
}