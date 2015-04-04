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
            try
            {
                string[] file = File.ReadAllLines(filepath);
                foreach (var line in file)
                {
                    cards.Add(Game.GetCardFromName(line));
                }
            }
            catch (Exception e)
            {
                Logger.Write("Exception: " + e.Message, "GetCardListFromFile");
            }
            return cards;
        }

        private static List<Card> GetCardListFromDB()
        {
            return Game.GetActualCards();
        }
    }

    public class CardCount
    {
        public int Standard { get; set; }
        public int Golden { get; set; }

        public CardCount()
        {
            Standard = 0;
            Golden = 0;
        }

        public CardCount(int standard, int golden)
        {
            Standard = standard;
            Golden = golden;
        }
    }
}
