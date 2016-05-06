using System.Collections.ObjectModel;
using System.Linq;
using HDT.Plugins.EndGame.Archetype;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Utility.Logging;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.EndGame.Controls
{
	public class ArchetypeDeckViewModel
	{
		private ArchetypeDeck _deck;
		private ObservableCollection<HDTCard> _cards;

		public ArchetypeDeckViewModel(ArchetypeDeck deck)
		{
			_deck = deck;
			_cards = new ObservableCollection<HDTCard>(_deck.Cards.Select(x => new HDTCard(HearthDb.Cards.Collectible[x.Id])));
			_cards.CollectionChanged += CardsUpdated;
		}

		private void CardsUpdated(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			Log.Info("Cards Updated: " + Cards.Count);
		}

		public string Name
		{
			get { return _deck.Name; }
			set { _deck.Name = value; }
		}

		public PlayerClass Klass
		{
			get { return _deck.Klass; }
			set { _deck.Klass = value; }
		}

		public Format Format
		{
			get { return _deck.Format; }
			set { _deck.Format = value; }
		}

		public ObservableCollection<HDTCard> Cards
		{
			get
			{
				return _cards;
			}
		}

		public void AddCard(HDTCard card)
		{
			if (!_cards.Contains(card))
				_cards.Add(card);
		}

		public void RemoveCard(HDTCard card)
		{
			_cards.Remove(card);
		}
	}
}