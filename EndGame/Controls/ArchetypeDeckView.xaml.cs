using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HDT.Plugins.EndGame.Enums;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Utility.Logging;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.EndGame.Controls
{
	/// <summary>
	/// Interaction logic for ArchetypeDeckView.xaml
	/// </summary>
	public partial class ArchetypeDeckView : UserControl
	{
		private List<HDTCard> _collectibleCards;
		private CultureInfo _culture;

		public ArchetypeDeckView()
		{
			InitializeComponent();

			ComboBoxKlass.ItemsSource = Enum.GetValues(typeof(PlayerClass));
			ComboBoxFormat.ItemsSource = Enum.GetValues(typeof(Format));

			_culture = new CultureInfo(Config.Instance.SelectedLanguage.Insert(2, "-"));
			_collectibleCards = HearthDb.Cards.Collectible.Values.Select(c => new HDTCard(c)).ToList();
		}

		private void TextBoxCardSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			// TODO clear on reopen?? load
			var textBox = (TextBox)sender;
			if (textBox == null)
				return;
			if (string.IsNullOrEmpty(textBox.Text))
			{
				//SearchList.ItemsSource = new[] { "No Matches" };
				return;
			}
			// language dependent case-insensitivity
			// http://stackoverflow.com/questions/444798/case-insensitive-containsstring/15464440#15464440)
			var predictions = _collectibleCards.Where(x =>
				_culture.CompareInfo.IndexOf(x.LocalizedName, textBox.Text, CompareOptions.IgnoreCase) >= 0).ToList();
			if (predictions.Count <= 0)
			{
				//SearchList.ItemsSource = new[] { "No Matches" };
				return;
			}
			SearchList.ItemsSource = predictions;
			SearchList.SelectedIndex = 0;
			//var selectionStart = textBox.Text.Length - 1;
			//textBox.Text = prediction;
			//textBox.Select(selectionStart + 1, textBox.Text.Length - selectionStart);
		}

		private void TextBoxCardSearch_OnPreviewKeyDown(object sender, KeyEventArgs e)
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

					//case Key.Enter:
					//	var deck = (ArchetypeDeck)DeckList.SelectedItem;
					//	Log.Debug(deck.ToString());
					//	if (deck != null)
					//	{
					//		Log.Debug(SearchList.SelectedItem.ToString());
					//		if (SearchList.SelectedIndex >= 0)
					//		{
					//			var sc = new SingleCard(HearthDb.Cards.GetFromName((string)SearchList.SelectedItem, HearthDb.Enums.Language.enUS).Id);
					//			deck.Cards.Add(sc);
					//		}
					//	}
					//	break;
			}
			SearchList.ScrollIntoView(SearchList.SelectedItem);
		}

		private void ButtonAddCard_Click(object sender, RoutedEventArgs e)
		{
			var deck = (ArchetypeDeckViewModel)DataContext;
			if (deck != null)
			{
				Log.Info(SearchList.SelectedItem.ToString());
				if (SearchList.SelectedIndex >= 0)
				{
					deck.AddCard((HDTCard)SearchList.SelectedItem);
				}
			}
		}

		private void ButtonRemoveCard_Click(object sender, RoutedEventArgs e)
		{
			var deck = (ArchetypeDeckViewModel)DataContext;
			if (deck != null)
			{
				if (ListViewCards.SelectedIndex >= 0)
				{
					Log.Info(ListViewCards.SelectedItem.ToString());
					deck.RemoveCard((HDTCard)ListViewCards.SelectedItem);
				}
			}
		}
	}
}