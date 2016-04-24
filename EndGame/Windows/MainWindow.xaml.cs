using System.Collections.Generic;
using System.Linq;
using HDT.Plugins.EndGame.Archetype;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Stats;
using Hearthstone_Deck_Tracker.Utility.Extensions;

namespace HDT.Plugins.EndGame.Windows
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		internal void SetOpponentDeck(List<TrackedCard> cards)
		{
			var deck = new Hearthstone_Deck_Tracker.Hearthstone.Deck();
			foreach (var c in cards)
			{
				var existing = deck.Cards.FirstOrDefault(x => x.Id == c.Id);
				if (existing != null)
				{
					existing.Count++;
					continue;
				}
				var card = Database.GetCardFromId(c.Id);
				card.Count = c.Count;
				deck.Cards.Add(card);
				if (string.IsNullOrEmpty(deck.Class) && !string.IsNullOrEmpty(card.PlayerClass))
					deck.Class = card.PlayerClass;
			}
			//SetDeck(deck, showImportButton);
			//_deck = deck;
			PlayedDeck.Items.Clear();
			foreach (var card in deck.Cards.ToSortedCardList())
				PlayedDeck.Items.Add(card);
			Helper.SortCardCollection(PlayedDeck.Items, false);
		}

		internal void SetArchetypeDeck(List<ArchetypeDeck> decks)
		{
			if (decks.Count > 0)
			{
				var cards = decks.First().Cards;
				var deck = new Hearthstone_Deck_Tracker.Hearthstone.Deck();
				foreach (var c in cards)
				{
					var existing = deck.Cards.FirstOrDefault(x => x.Id == c.Id);
					if (existing != null)
					{
						existing.Count++;
						continue;
					}
					var card = Database.GetCardFromId(c.Id);
					card.Count = c.Count;
					deck.Cards.Add(card);
					if (string.IsNullOrEmpty(deck.Class) && !string.IsNullOrEmpty(card.PlayerClass))
						deck.Class = card.PlayerClass;
				}
				//SetDeck(deck, showImportButton);
				//_deck = deck;
				ArchetypeDeck.Items.Clear();
				foreach (var card in deck.Cards.ToSortedCardList())
					ArchetypeDeck.Items.Add(card);
				Helper.SortCardCollection(ArchetypeDeck.Items, false);
			}
		}
	}
}