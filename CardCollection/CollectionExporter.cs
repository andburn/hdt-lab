using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hearthstone_Deck_Tracker.Hearthstone;
using System.IO;

namespace AndBurn.HDT.Plugins.CardCollection
{
    public class CollectionExporter
    {
        public static List<Card> GetCardList(string filepath = null)
        {
            if (filepath == null)
            {
                return GetCardListFromDB();
            }
            else
            {
                return GetCardListFromFile(filepath);
            }
        }

        private static List<Card> GetCardListFromFile(string filepath)
        {
            List<Card> cards = new List<Card>();
            // TODO: File IO checks
            string[] file = File.ReadAllLines(filepath);
            foreach (var line in file)
            {
                // TODO: check for unknown card?
                cards.Add(Game.GetCardFromName(line));
            }
            return cards;
        }

        private static List<Card> GetCardListFromDB()
        {
            return Game.GetActualCards();
        }
    }
}
