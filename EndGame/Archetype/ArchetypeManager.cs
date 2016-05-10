using System.Collections.Generic;
using System.IO;
using System.Linq;
using HDT.Plugins.EndGame.Enums;
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
			_archetypes = new List<ArchetypeDeck>() {
				new ArchetypeDeck("Sample A", Enums.PlayerClass.PALADIN, GameFormat.ANY, ArchetypeStyles.MIDRANGE, new List<Card>() {
					new SingleCard("EX1_008"), new SingleCard("EX1_360"), new SingleCard("EX1_382")
				}),
				new ArchetypeDeck("Sample B", Enums.PlayerClass.HUNTER, GameFormat.ANY, ArchetypeStyles.AGGRO, new List<Card>() {
					new SingleCard("CS2_124"), new SingleCard("EX1_538"), new SingleCard("EX1_539")
				})
			};
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
			Decks.Clear();
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

		public ArchetypeDeck Get(string name)
		{
			return _archetypes.FirstOrDefault(x => x.Name == name);
		}

		public List<ArchetypeDeck> Find(GameStats game)
		{
			var deck = new PlayedDeck(game.OpponentHero, game.Format, game.Turns, game.OpponentCards);
			return Find(deck);
		}

		public List<ArchetypeDeck> Find(PlayedDeck deck)
		{
			// TODO remove any 0 or even below a threshold?
			return _archetypes.OrderByDescending(x => deck.Similarity(x)).ToList();
		}
	}
}