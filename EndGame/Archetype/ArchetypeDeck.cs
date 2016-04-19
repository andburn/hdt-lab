using System.Collections.Generic;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;

namespace HDT.Plugins.EndGame.Archetype
{
	public class ArchetypeDeck : Deck
	{
		public string Name { get; set; }
		public ArchetypeStyle Style { get; set; }

		public ArchetypeDeck() : base()
		{
		}

		public ArchetypeDeck(string name, PlayerClass klass, Format format, ArchetypeStyle style, List<Card> cards)
			: base(klass, format, cards)
		{
			Name = name;
			Style = style;
		}
	}
}