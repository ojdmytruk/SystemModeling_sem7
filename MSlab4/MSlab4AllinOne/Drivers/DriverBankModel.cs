using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab4AllinOne
{
    public class DriverBankModel
    {
        public List<Element> list = new List<Element>();
        public double tnext { get; set; }
        public double tcurr { get; set; }
        int @event { get; set; }
        public List<Element> sortedList = new List<Element>();


        public DriverBankModel(List<Element> elements)
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
                if (list[@event].GetType() == typeof(ProcessDriver) && list[@event].name != "DISPOSE")
                {
                    ProcessDriver p = (ProcessDriver)list[@event];
                    p.channelInAct();
                }
                foreach (var e in list)
                {
                    if (e.GetType() == typeof(ProcessDriver) && e.name != "DISPOSE")
                    {
                        ProcessDriver p = (ProcessDriver)e;
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
                if (e.GetType() == typeof(ProcessDriver) && e.name != "DISPOSE")
                {
                    ProcessDriver p = (ProcessDriver)e;
                    p.avgWait = p.queueAvg / p.quantity;
                    p.queueAvg /= tcurr;
                    p.avgTimeProcess /= p.quantity;
                    p.probability = p.failure / (double)(p.quantity + p.failure);
                    p.avgWorkload /= tcurr;
                    Console.WriteLine("{0,12}|{1,22}|{2,4}|{3,22}|{4,22}|{5,22}|{6,10}|{7,22}", p.name, p.queueAvg, p.maxqueue, p.probability, p.avgWait, p.avgTimeProcess, p.workload, p.avgWorkload);
                    
                }
            }

        }

        public void ReturnResult()
        {
            double average, workload, waiting, outWindow, avgBank = 0, actAndQueueQuantity = 0;
            int totalQuantity = 0, failure = 0;
            Console.WriteLine("{0,10}|{1,22}|{2,8}|{3,9}|{4,22}|{5,22}|{6,22}|{7,10}", "name", "avgQ", "LaneCh", "failure", "bankTime", "workload", "inProc", "queue", "window");
            foreach (var e in list)
            {
                if (e.GetType() == typeof(ProcessDriver))
                {
                    ProcessDriver p = (ProcessDriver)e;
                    average = p.queueAvg;
                    if (p.avgWorkload < 0) workload = 1.0 + p.avgWorkload;
                    else 
                        workload = p.avgWorkload;
                    waiting = p.queueAvg / (p.quantity + p.failure);
                    outWindow = tcurr / p.quantity;
                    totalQuantity += p.quantity;
                    actAndQueueQuantity += p.taskNumber;
                    avgBank += average;
                    failure += p.failure;
                    if (e.name != "GO OUT")
                    {
                        Console.WriteLine("{0,10}|{1,22}|{2,8}|{3,9}|{4,22}|{5,22}|{6,10}|{7,10}", p.name, average, p.changeLane, p.failure, waiting, workload, p.state, p.queue, outWindow);

                    }
                }

            }
            foreach (var e in list)
            {
                e.printResult();
            }
        }
    }
}
