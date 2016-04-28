using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HDT.Plugins.EndGame.Archetype;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Utility.Logging;

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
			DeckList.SelectedIndex = 0;
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

		private void CardNameBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			// TODO clear on reopen?? load
			var textBox = sender as TextBox;
			if (textBox == null)
				return;
			if (string.IsNullOrEmpty(textBox.Text))
			{
				SearchList.ItemsSource = new[] { "No Matches" };
				return;
			}
			// FIX for localization (http://stackoverflow.com/questions/444798/case-insensitive-containsstring/15464440#15464440)
			// and for contains rather than starts
			var cards = HearthDb.Cards.Collectible.Values.Select<HearthDb.Card, string>(x => x.GetLocName(HearthDb.Enums.Language.enUS)).ToList();
			var predictions = cards.Where(x => x.StartsWith(textBox.Text, StringComparison.InvariantCultureIgnoreCase)).ToList();
			if (predictions.Count <= 0)
			{
				SearchList.ItemsSource = new[] { "No Matches" };
				return;
			}
			SearchList.ItemsSource = predictions;
			SearchList.SelectedIndex = 0;
			//var selectionStart = textBox.Text.Length - 1;
			//textBox.Text = prediction;
			//textBox.Select(selectionStart + 1, textBox.Text.Length - selectionStart);
		}

		private void CardNameBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			var index = SearchList.SelectedIndex;
			switch (e.Key)
			{
				case Key.Down:
					if (index < SearchList.Items.Count - 1)
						SearchList.SelectedIndex += 1;
					break;

				case Key.Up:
					if (index > 0)
						SearchList.SelectedIndex -= 1;
					break;

				case Key.Enter:
					var deck = (ArchetypeDeck)DeckList.SelectedItem;
					Log.Debug(deck.ToString());
					if (deck != null)
					{
						Log.Debug(SearchList.SelectedItem.ToString());
						if (SearchList.SelectedIndex >= 0)
						{
							var sc = new SingleCard(HearthDb.Cards.GetFromName((string)SearchList.SelectedItem, HearthDb.Enums.Language.enUS).Id);
							deck.Cards.Add(sc);
						}
					}
					break;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new Hearthstone_Deck_Tracker.Windows.ArenaRewardDialog(new Hearthstone_Deck_Tracker.Hearthstone.Deck() { IsArenaDeck = true });
			dialog.Show();
		}

		private void Addbutton_Click(object sender, RoutedEventArgs e)
		{
			var deck = (ArchetypeDeck)DeckList.SelectedItem;
			Log.Info(deck.ToString());
			if (deck != null)
			{
				Log.Info(SearchList.SelectedItem.ToString());
				if (SearchList.SelectedIndex >= 0)
				{
					var sc = new SingleCard(HearthDb.Cards.GetFromName((string)SearchList.SelectedItem, HearthDb.Enums.Language.enUS).Id);
					Log.Info(sc.ToString());
					deck.Cards.Add(sc);
					DataContext = deck;
				}
			}
		}
	}
}