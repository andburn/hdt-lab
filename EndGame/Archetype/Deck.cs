using System.Collections.Generic;
using System.Linq;
using HDT.Plugins.EndGame.Enums;

namespace HDT.Plugins.EndGame.Archetype
{
	public abstract class Deck
	{
		public PlayerClass Klass { get; set; }
		public GameFormat Format { get; set; }
		public List<Card> Cards { get; set; }

		public Deck()
		{
		}

		public Deck(PlayerClass klass, GameFormat format, List<Card> cards)
			: this()
		{
			Klass = klass;
			Format = format;
			Cards = cards;
		}

		public Deck(string klass, GameFormat format, List<Card> cards)
			: this()
		{
			Klass = Utils.KlassFromString(klass);
			Format = format;
			Cards = cards;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Deck d = obj as Deck;
			if (d == null)
			{
				return false;
			}

			Cards.Sort();
			d.Cards.Sort();

			return Klass == d.Klass && Format == d.Format
				&& Cards.SequenceEqual(d.Cards);
		}

		public override int GetHashCode()
		{
			// TODO hash code can't include cards, any change add/remove gives different hash!
			// now will create large buckets, add uuid maybe
			return (int)Klass ^ (int)Format;
		}
	}
}