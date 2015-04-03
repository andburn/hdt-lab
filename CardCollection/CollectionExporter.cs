using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndBurn.HDT.Plugins.CardCollection
{
    public class CollectionExporter
    {
        public static string[] GetCardList(string filepath = null)
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

        private static string[] GetCardListFromFile(string filepath)
        {
            return null;
        }

        private static string[] GetCardListFromDB()
        {
            return null;
        }
    }
}
