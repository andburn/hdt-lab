using System.Collections.Generic;
using System.Linq;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Stats;

namespace HDT.Plugins.EndGame.Archetype
{
	public class ArchetypeManager
	{
		private List<ArchetypeDeck> _archetypes;
		private List<ArchetypeStyle> _styles;

		// TODO make prop defs consistent
		public List<ArchetypeDeck> Decks
		{
			get { return _archetypes; }
		}

		public ArchetypeManager()
		{
			_archetypes = new List<ArchetypeDeck>();
			_styles = new List<ArchetypeStyle>() {
				ArchetypeStyles.AGGRO,
				ArchetypeStyles.COMBO,
				ArchetypeStyles.CONTROL,
				ArchetypeStyles.MIDRANGE
			};
		}

		public void Add(ArchetypeDeck deck)
		{
			if (!_archetypes.Contains(deck))
			{
				_archetypes.Add(deck);
			}
		}

		public bool Remove(ArchetypeDeck deck)
		{
			return _archetypes.Remove(deck);
		}

		public List<ArchetypeDeck> Find(GameStats game)
		{
			var deck = new PlayedDeck(game.OpponentHero, game.Format ?? Format.All, game.Turns, game.OpponentCards);
			return Find(deck);

			//foreach (var archetype in _archetypes)
			//{
			//	// store and sort? get largest?
			//	var s = deck.Similarity(archetype);
			//	if (s > similarity)
			//	{
			//		similarity = s;
			//		match = archetype;
			//	}
			//}
		}

		public List<ArchetypeDeck> Find(PlayedDeck deck)
		{
			return _archetypes.OrderByDescending(x => deck.Similarity(x)).ToList();
		}
	}
}