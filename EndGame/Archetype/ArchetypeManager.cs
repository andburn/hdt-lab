using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Stats;
using Newtonsoft.Json;

namespace HDT.Plugins.EndGame.Archetype
{
	public class ArchetypeManager
	{
		private const string DECKS_FILE = @"E:\Dump\deck.json";

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

		public void LoadDecks(string file = null)
		{
			var decks = JsonConvert.DeserializeObject<List<ArchetypeDeck>>(
				File.ReadAllText(file ?? DECKS_FILE));
			foreach (var d in decks)
				Add(d);
		}

		public void SaveDecks(string file = null)
		{
			File.WriteAllText(file ?? DECKS_FILE, JsonConvert.SerializeObject(_archetypes));
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
		}

		public List<ArchetypeDeck> Find(PlayedDeck deck)
		{
			return _archetypes.OrderByDescending(x => deck.Similarity(x)).ToList();
		}
	}
}