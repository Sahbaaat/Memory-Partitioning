using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os
{
    class Partition2
    {
        public int size;
        public bool isfull;
        public Process ongoing;
        public Partition2(int s)
        {
            size = s;
            isfull = false;
            ongoing = null;
        }
    }
}
