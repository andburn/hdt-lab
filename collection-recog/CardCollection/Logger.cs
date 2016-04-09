using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndBurn.HDT.Plugins.CardCollection
{
    public class Logger
    {
        private static string LogFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "CardCollection.log");

        public static void Write(string message, string title = "log")
        {            
            using (StreamWriter file = new StreamWriter(LogFile, true))
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                file.WriteLine("{0} [{1}] {2}", timestamp, title, message);    
            }
        }
    }
}
