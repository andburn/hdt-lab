using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone
{
    public class RankDetection
    {
        public Result Process()
        {
            return new Result();
        }

        public class Result
        {
            public int Player { get; private set; }
            public int Opponent { get; private set; }
            public bool Success { get; private set; }
        }
    }
}
