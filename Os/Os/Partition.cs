using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os
{
    class Partition
    {
        public int size;
        public bool isfull;
        public Queue<Process> proceses;
        public Process ongoing;
        public Partition(int s)
        {
            size = s;
            isfull = false;
            proceses = new Queue<Process>();
            ongoing = null;
    }
    }
}
