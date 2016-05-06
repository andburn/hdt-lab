﻿using System;
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
			DeckFormatFilterSelection.SelectedItem = Format.All;
			DeckList.ItemsSource = _manager.Decks;
			DeckList.SelectedIndex = 0;
			ArchetypeDeck.DataContext = new ArchetypeDeckViewModel((ArchetypeDeck)_manager.Decks.First());
		}

		private void DeckList_SelectionChanged(object sender, RoutedEventArgs e)
		{
			ArchetypeDeck.DataContext = new ArchetypeDeckViewModel((ArchetypeDeck)DeckList.SelectedItem);
		}
	}
}