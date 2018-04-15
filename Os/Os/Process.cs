using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os
{
    class Process
    {
       public int size;
        public int starttime;
        public int duration;
        public int responsetime;
        public int fixedduration;
        public int state; //       0.Ready   1.Running    3.Finished
        public Process(int s, int d)
        {

            size = s;
            duration = d;
            state = 0;
            fixedduration = d;
        }
    }
}
