using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hearthstone_Deck_Tracker.Plugins;

namespace AndBurn.HDT.Plugins.CardCollection
{
    public class CardCollectionPlugin : IPlugin
    {
        public string Name
        {
            get { return "CardCollection"; }
        }

        public string Description
        {
            get { return "Creates a list of the cards that you have in your collection."; }
        }

        public string ButtonText
        {
            get { return "Run"; }
        }

        public string Author
        {
            get { return "andburn"; }
        }

        public Version Version
        {
            get { return new Version(0, 1, 0); }
        }

        public void OnLoad()
        {
            // Nothing for now
        }

        public void OnUnload()
        {
            // Nothing for now
        }

        public void OnUpdate()
        {
            // Nothing for now
        }

        public void OnButtonPress()
        {
            // open dialog with the details, ok and cancel buttons
        }

    }
}
