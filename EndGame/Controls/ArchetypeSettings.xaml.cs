using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HDT.Plugins.EndGame.Archetype;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;

namespace HDT.Plugins.EndGame.Controls
{
	/// <summary>
	/// Interaction logic for ArchetypeSettings.xaml
	/// </summary>
	public partial class ArchetypeSettings : UserControl
	{
		private ArchetypeManager _manager;

		public ArchetypeSettings()
		{
			InitializeComponent();
			_manager = new ArchetypeManager();
			_manager.LoadDecks();
			LoadArchetype();
		}

		private void LoadArchetype()
		{
			DeckClassFilterSelection.ItemsSource = Enum.GetValues(typeof(PlayerClass));
			DeckFormatFilterSelection.ItemsSource = Enum.GetValues(typeof(Format));
			DeckClassSelection.ItemsSource = Enum.GetValues(typeof(PlayerClass));
			DeckFormatSelection.ItemsSource = Enum.GetValues(typeof(Format));
			DeckFormatFilterSelection.SelectedItem = Format.All;
			DeckList.ItemsSource = _manager.Decks;
			DataContext = _manager.Decks.First();
		}

		private void DeckClassFilterSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void DeckFormatFilterSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void DeckClassSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void DeckFormatSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void DeckList_SelectionChanged(object sender, RoutedEventArgs e)
		{
			DataContext = (ArchetypeDeck)DeckList.SelectedItem;
		}
	}
}