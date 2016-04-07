using System;
using System.Collections.Generic;
using System.Linq;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Stats;

namespace HDT.Plugins.EndGame.Archetype
{
	public class ArchetypeManager
	{
		private List<Deck> _archetypes;

		public ArchetypeManager()
		{
			_archetypes = new List<Deck>();
			// load archetypes from ...
		}

		public void Add(Deck deck)
		{
		}

		public void Remove(Deck deck)
		{
		}

		public List<Deck> Filter(PlayerClass klass, Format? format)
		{
			var fmt = format ?? Format.All;
			if (fmt == Format.All)
			{
				return _archetypes.Where(x => x.Klass == klass).ToList();
			}
			else
			{
				return _archetypes.Where(x => x.Klass == klass && x.Format == fmt).ToList();
			}
		}

		public List<Deck> Find(GameStats game)
		{
			List<TrackedCard> deck = game.OpponentCards;
			var klass = (PlayerClass)Enum.Parse(typeof(PlayerClass), game.OpponentHero);
			var achetypes = Filter(klass, game.Format);

			return new List<Deck>();
		}
	}
}