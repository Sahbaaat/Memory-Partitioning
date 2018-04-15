using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os
{
    
    class Program
    {
        static int timer2 = 0;
        static int timer1 = 0;
        static Partition x;

        static void Main(string[] args)
        {

            List<Process> process1 = new List<Process>();
            List<Partition> mainmemory1 = new List<Partition>();

            List<Process> process2 = new List<Process>();
            List<Partition2> mainmemory2 = new List<Partition2>();
            Queue<Process> queue2 = new Queue<Process>();
            

            
            for(int i=0;i<5;i++)
            {
                Partition2 p = new Partition2(2 ^ i);
                mainmemory2.Add(p);
            }

            for (int i = 0; i < 5; i++)
            {
                Partition p = new Partition(2 ^ i);
                mainmemory1.Add(p);
            }
         //Adding 8 prossess
            Process pro1= new Process(2, 5);
            Process pro2 = new Process(4, 4);
            Process pro3 = new Process(10, 10);
            Process pro4 = new Process(5, 6);
            Process pro5 = new Process(1, 3);
            Process pro6 = new Process(2, 20);
            Process pro7 = new Process(8, 3);
            Process pro8 = new Process(16, 3);

            process1.Add(pro1);
            process1.Add(pro2);
            process1.Add(pro3);
            process1.Add(pro4);
            process1.Add(pro5);
            process1.Add(pro6);
            process1.Add(pro7);
            process1.Add(pro8);

            //Adding 8 processes
            Process pr1 = new Process(2, 5);
            Process pr2 = new Process(4, 4);
            Process pr3 = new Process(10, 10);
            Process pr4 = new Process(5, 6);
            Process pr5 = new Process(1, 3);
            Process pr6 = new Process(2, 20);
            Process pr7 = new Process(8, 3);
            Process pr8 = new Process(16, 3);

            process2.Add(pr1);
            process2.Add(pr2);
            process2.Add(pr3);
            process2.Add(pr4);
            process2.Add(pr5);
            process2.Add(pr6);
            process2.Add(pr7);
            process2.Add(pro8);


            foreach (Process p in process2)
                queue2.Enqueue(p);

            //Initiallizing memory1 and run the processes in memory1
            Initialize1(mainmemory1, process1);
            Run1(mainmemory1, process1);

            //Initiallizing memory2 and run the processes in memory2
            Initialize2(mainmemory2, queue2);
            Run2(mainmemory2, queue2);

            //calculating throuput for memory1 and memory2
            float throuput1 = timer1 / process1.Count;
            float throuput2 = timer2 / process2.Count;
            
            Console.WriteLine("1 queue for all partitions Response time ---> {0}", Calculate_Responetime(process1));
            Console.WriteLine("1 queue for all partitions Throuhput ---> {0}", throuput1 );
            Console.WriteLine("1 queue for each partition Response time ---> {0}", Calculate_Responetime(process2));
            Console.WriteLine("1 queue for each partition Throuhput ---> {0}", throuput2);
            Console.ReadKey();
        }

        //initialize the memory1 partiotions with processes in the queue
        static public void Initialize2(List<Partition2> mainmemory, Queue<Process> queue)
        {
            int n = queue.Count;
            Queue<Process> copied_queue2=new Queue<Process>();
            foreach(Process p in queue)
                copied_queue2.Enqueue(p);

            Process new_pro;

               for (int j = 0; j < n; j++)
            {
                bool added = false;
                new_pro = copied_queue2.Dequeue();
                
                if (new_pro.state == 0)
                {
                    foreach (Partition2 part in mainmemory)
                    {
                        if (part.size >= new_pro.size && part.isfull == false)
                        {
                            queue.Dequeue();
                            part.ongoing = new_pro;
                            new_pro.starttime = timer2;
                            part.isfull = true;
                            new_pro.state = 1;
                            added = true;
                        }
                        if (added)
                            break;
                    }
                }
            }
        }

        //fill the memory with first-fit policy
        static public void First_fit(List<Partition2> mainmemory, Queue<Process> queue)
        {
            
            Process new_pro;
            Queue<Process> copied_queue2 = new Queue<Process>();
            foreach (Process p in queue)
                copied_queue2.Enqueue(p);

            int n = queue.Count;
            for (int j = 0; j < n; j++)
            {
                bool added = false;
                new_pro = copied_queue2.Dequeue();

                if (new_pro.state == 0)
                {
                    foreach (Partition2 part in mainmemory)
                    {
                        if (part.size >= new_pro.size && part.isfull == false)
                        {
                            queue.Dequeue();
                            part.ongoing = new_pro;
                            new_pro.starttime = timer2;
                            part.isfull = true;
                            new_pro.state = 1;
                            added = true;
                        }
                        if (added)
                            break;
                    }
                }
            }
        }

        static public void Run2(List<Partition2> mainmemory, Queue<Process> queue)
        {

            while (!Memory2IsEmpty(mainmemory))
            {
                timer2++;
                foreach (Partition2 partition in mainmemory)
                {
                    if (partition.isfull)
                    {
                        if (partition.ongoing.duration > 0)
                        {
                            partition.ongoing.duration--;

                        }
                        else
                        {
                            partition.isfull = false;
                            partition.ongoing.state = 2;
                            First_fit(mainmemory, queue);
                        }
                    }
                    else
                    {
                        First_fit(mainmemory, queue);
                    }

                }
            }

        }

        //checks if any process is runing in memory2
        static public bool Memory2IsEmpty(List<Partition2> mainmemory)
        {
            bool empty = true;
            foreach (Partition2 partition in mainmemory)
                if (partition.isfull)
                    empty = false;
            return empty;
        }

        //checks if any process is runing in memory2
        static public bool Memory1IsEmpty(List<Partition> mainmemory)
            {
            bool empty = true;
            foreach (Partition partition in mainmemory)
                if (partition.isfull)
                    empty = false;
            return empty;
        }
        //initialize the memory2 partiotions with processes in the list
        static public void Initialize1(List<Partition>mainmemory, List<Process> pros) {
            foreach (Process p in pros)
            {
                bool added = false;
                foreach (Partition part in mainmemory)
                {
                    if (part.size == p.size &&part.isfull==false)
                    {
                        part.ongoing = p;
                        p.starttime = 0;
                        p.state = 0;
                        added = true;
                        part.isfull = true;
                        break;
                    }
                }
                if (!added)
                {
                    int internalfragmentation = 0;
                    int min = p.size;
                    foreach (Partition part in mainmemory)
                    {

                        internalfragmentation = part.size - p.size;
                        if (min >= internalfragmentation)
                        {
                            min = internalfragmentation;
                            x = part;
                        }
                    }
                    x.proceses.Enqueue(p);
                    p.state = 0;
                    p.starttime = 0;
                    added = true;
                }
            }
        }

        //place the process which is in the partition's queue of processes in to the partition
        static public void AddToPartition(Partition partition)
        {
            Process p = partition.proceses.Dequeue();
            p.starttime = timer1;
            p.state = 1;
            partition.ongoing = p;
            partition.isfull = true;

        }

        //running the processes in memory1
        static public void Run1(List<Partition> mainmemory, List<Process> pros)
        { 
            while(!Memory1IsEmpty(mainmemory))
            {
                timer1++;
                foreach(Partition partition in mainmemory)
                {
                    if (partition.isfull)
                    {
                        if (partition.ongoing.duration > 0)
                        {
                            partition.ongoing.duration--;
                        }
                        else
                        {
                            partition.isfull = false;
                            partition.ongoing.state = 2;
                            if (partition.proceses.Count > 0)
                                AddToPartition(partition);
                        }
                    }
                    else
                    {
                        if(partition.proceses.Count>0)
                            AddToPartition(partition);
                    }
                }
            }
        }

        //Calculates the response time for processes
        static public float Calculate_Responetime(List<Process> processes)
        {
            float avg = 0;
            foreach (Process p in processes)
            {    
                p.responsetime = p.starttime + p.fixedduration;
                avg += p.responsetime;
               // Console.WriteLine(p.starttime);
               // Console.WriteLine(p.fixedduration);   
            }
            avg = avg / processes.Count();
            return avg;
        }
    }
}
