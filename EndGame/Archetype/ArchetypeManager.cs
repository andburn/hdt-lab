using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hearthstone_Deck_Tracker.Stats;
using Newtonsoft.Json;

namespace HDT.Plugins.EndGame.Archetype
{
	public class ArchetypeManager
	{
		private const string DECKS_FILE = @".\deck.json";

		private static ArchetypeManager _instance;

		public static ArchetypeManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new ArchetypeManager();
				return _instance;
			}
		}

		private List<ArchetypeDeck> _archetypes;

		public List<ArchetypeDeck> Decks
		{
			get { return _archetypes; }
		}

		private List<ArchetypeStyle> _defaultStyles;
		private List<ArchetypeStyle> _customStyles;

		public List<ArchetypeStyle> Styles
		{
			get
			{
				var styles = new List<ArchetypeStyle>(_defaultStyles);
				styles.AddRange(_customStyles);
				return styles;
			}
		}

		private ArchetypeManager()
		{
			_archetypes = new List<ArchetypeDeck>();
			_defaultStyles = new List<ArchetypeStyle>() {
				ArchetypeStyles.AGGRO,
				ArchetypeStyles.COMBO,
				ArchetypeStyles.CONTROL,
				ArchetypeStyles.MIDRANGE
			};
			_customStyles = new List<ArchetypeStyle>();
		}

		public void Reset()
		{
			_archetypes.Clear();
			_defaultStyles = new List<ArchetypeStyle>() {
				ArchetypeStyles.AGGRO,
				ArchetypeStyles.COMBO,
				ArchetypeStyles.CONTROL,
				ArchetypeStyles.MIDRANGE
			};
			_customStyles.Clear();
		}

		// TODO some error handling on file IO
		public void LoadDecks(string file = null)
		{
			var decks = JsonConvert.DeserializeObject<List<ArchetypeDeck>>(
				File.ReadAllText(file ?? DECKS_FILE));
			Decks.Clear();
			foreach (var d in decks)
				AddDeck(d);
		}

		// TODO some error handling on file IO
		public void SaveDecks(string file = null)
		{
			File.WriteAllText(file ?? DECKS_FILE, JsonConvert.SerializeObject(_archetypes));
		}

		public void AddDeck(ArchetypeDeck deck)
		{
			// Skip duplicate decks
			if (!_archetypes.Any(x => x.Matches(deck)))
			{
				_archetypes.Add(deck);
			}
		}

		public void RemoveDeck(ArchetypeDeck deck)
		{
			// Remove actual deck
			var success = _archetypes.Remove(deck);
			// else remove matching decks
			if (!success)
				_archetypes.RemoveAll(x => x.Matches(deck));
		}

		// TODO necessary
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

		public void AddStyle(ArchetypeStyle style)
		{
			if (!_customStyles.Contains(style))
				_customStyles.Add(style);
		}

		public void RemoveStyle(ArchetypeStyle style)
		{
			_customStyles.Remove(style);
		}
	}
}