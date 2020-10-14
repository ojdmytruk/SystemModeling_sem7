using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab4AllinOne
{
    public class Model
    {
        public List<Element> list = new List<Element>();
        public double tnext { get; set; }
        public double tcurr { get; set; }
        int @event { get; set; }


        public Model(List<Element> elements)
        {
            list = elements;
            tnext = 0.00;
            @event = 0;
            tcurr = tnext;
        }

        public void simulate(double timeModeling)
        {
            int prev = -1;

            while (tcurr < timeModeling)
            {
                tnext = Double.MaxValue;

                foreach (Element e in list)
                {
                    if (e.tnext < tnext)
                    {
                        tnext = e.tnext;
                        @event = list.IndexOf(e);
                    }
                }
                List<Channel> channels = new List<Channel>();
                List<Channel> outChannels = new List<Channel>();
                if (list[@event].GetType() == typeof(Process) && list[@event].name != "DISPOSE")
                {
                    Process p = (Process)list[@event];
                    p.channelInAct();
                }
                foreach (var e in list)
                {
                    if (e.GetType() == typeof(Process) && e.name != "DISPOSE")
                    {
                        Process p = (Process)e;
                        outChannels = p.channelOutAct(tnext);
                        foreach (var o in outChannels)
                        {
                            channels.Add(o);
                        }

                    }
                }
                channels = channels.OrderBy(x => x.timeFree).ToList();
                foreach (var c in channels)
                {
                    Console.WriteLine("{0} is free at t={1}", c.name, c.timeFree);
                }
                if (!(prev == 0 && @event == 0))
                    Console.WriteLine("Time fot event in {0} t={1}", list[@event].name, tnext);
                foreach (Element e in list)
                {
                    e.statistics(tnext - tcurr);
                }
                tcurr = tnext;

                foreach (Element e in list)
                {
                    e.tcurr = tcurr;
                }
                list[@event].outAct();

                foreach (Element e in list)
                {
                    if (e.tnext == tcurr)
                    {
                        e.outAct();
                    }
                }
                //PrintInfo();
                prev = @event;
            }
            PrintResult();
        }
               

        public void PrintInfo()
        {
            foreach (Element e in list)
            {
                e.printInfo();
            }
        }

        public void PrintResult()
        {
            Console.WriteLine("\n-------------RESULTS-------------");
            Console.WriteLine("{0,12}|{1,22}|{2,4}|{3,22}|{4,22}|{5,22}|{6,10}|{7,22}", "name", "avgQ", "maxQ", "f prob", "avgWait", "avgProc", "workload", "avgWL");
            foreach (Element e in list)
            {                
                if (e.GetType() == typeof(Process) && e.name != "DISPOSE")
                {

                    Process p = (Process)e;
                    p.avgWait = p.queueAvg / p.quantity;
                    p.queueAvg /= tcurr;
                    p.avgTimeProcess /= p.quantity;
                    p.probability = p.failure / (double)(p.quantity + p.failure);
                    if (p.avgWorkload / p.tcurr < 0) p.avgWorkload = 0;
                    else
                        p.avgWorkload /= p.tcurr;
                    Console.WriteLine("{0,12}|{1,22}|{2,4}|{3,22}|{4,22}|{5,22}|{6,10}|{7,22}", p.name, p.queueAvg, p.maxQueueCurrent, p.probability, p.avgWait, p.avgTimeProcess, p.workload, p.avgWorkload);
                    
                }
            }
            foreach (Element e in list)
            {
                e.printResult();
            }
        }

        public void accuracy()
        {
            double[] theoreticalAverageQueue = new double[] { 1.786, 0.003, 0.004, 0.00001 };
            double[] theoreticalWorkload = new double[] { 0.714, 0.054, 0.062, 0.036 };
            
            int i = 0;
            Console.WriteLine("{0,10}|{1,22}|{2,22}|{3,22}|{4,22}", "name", "avgQ", "theoQ", "awgWL", "theoWL");
            foreach (var e in list)
            {
                if (e.GetType() == typeof(Process))
                {
                    Process p = (Process)e;
                    
                    Console.WriteLine("{0,10}|{1,22}|{2,22}|{3,22}|{4,22}", p.name, p.queueAvg, theoreticalAverageQueue[i], p.avgWorkload, theoreticalWorkload[i]);
                    i++;
                }
                
            }

        }
     
    }
}
