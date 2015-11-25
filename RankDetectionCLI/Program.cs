using Hearthstone;
using Hearthstone.Ranked;
using System.Drawing;

namespace RankDetectionCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                if (args[0].ToLower() == "create")
                {
                    RankTemplate.Create(args[1], args[2]);
                }
                else if (args[0].ToLower() == "match")
                {
                    RankDetection.SetTemplateLocation(args[2]);
                    var match = RankDetection.Match(new Bitmap(args[1]));
                    System.Console.WriteLine(match);
                }
            }            
        }
    }
}
