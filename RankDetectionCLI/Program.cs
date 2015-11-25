using Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }            
        }
    }
}
