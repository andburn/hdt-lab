using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HDT.Plugins.EndGame.Archetype;
using HDT.Plugins.EndGame.Enums;

namespace HDT.Plugins.EndGame.Controls
{
	public partial class ArchetypeSettings : UserControl
	{
		private ArchetypeManager _manager;

		public ArchetypeSettings()
		{
			InitializeComponent();
			_manager = ArchetypeManager.Instance;
			_manager.LoadDecks();
			LoadArchetype();
		}

		private void LoadArchetype()
		{
			DeckClassFilterSelection.ItemsSource = Enum.GetValues(typeof(PlayerClass));
			DeckFormatFilterSelection.ItemsSource = Enum.GetValues(typeof(GameFormat));
			DeckFormatFilterSelection.SelectedItem = GameFormat.ANY;
			DeckList.ItemsSource = _manager.Decks;
			DeckList.SelectedIndex = 0;
			var deck = _manager.Decks.FirstOrDefault();
			if (deck != null)
				ArchetypeDeck.DataContext = new ArchetypeDeckViewModel(deck);
		}

		private void DeckList_SelectionChanged(object sender, RoutedEventArgs e)
		{
			ListBox box = sender as ListBox;
			var deck = _manager.GetDeck(box.SelectedItem);
			if (deck != null)
				ArchetypeDeck.DataContext = new ArchetypeDeckViewModel(deck);
		}

		private void ButtonNew_Click(object sender, RoutedEventArgs e)
		{
			_manager.AddDeck(new ArchetypeDeck());
		}
	}
}