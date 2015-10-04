using System;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.Plugins;

namespace RankDetection
{
	public class RankDetectionPlugin : IPlugin
	{
		public string Author
		{
			get { return "andburn"; }
		}

		public string ButtonText
		{
			get { return ""; }
		}

		public string Description
		{
			get { return "Rank Detection Test"; }
		}

		public MenuItem MenuItem
		{
			get { return null; }
		}

		public string Name
		{
			get { return "RankDetection"; }
		}

		public void OnButtonPress()
		{
		}

		public void OnLoad()
		{
			RankDetection.Load();
			Hearthstone_Deck_Tracker.API.GameEvents.OnGameStart.Add(RankDetection.Start);
			Hearthstone_Deck_Tracker.API.GameEvents.OnInMenu.Add(RankDetection.Reset);
		}

		public void OnUnload()
		{
			RankDetection.RemoveTextBlock();
		}

		public void OnUpdate()
		{
		}

		public Version Version
		{
			get { return new Version(0, 0, 1); }
		}
    }
}
