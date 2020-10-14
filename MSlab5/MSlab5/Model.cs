using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace MSlab5
{
    public class Model
    {
        private List<Element> list;
        double tnext = 0.0;
        int @event;
        double tcurr = 0.0;
        public ulong ellapsed { get; set; }

        public Model(List<Element> elements)
        {
            list = elements;
        }

        public void simulate(double timeModeling)
        {
            ulong begin = (ulong)System.Diagnostics.Stopwatch.GetTimestamp() / TimeSpan.TicksPerMillisecond;
            while (tcurr < timeModeling)
            {
                tnext = double.MaxValue;
                foreach (Element e in list)
                {
                    if (e.Tnext() < tnext)
                    {
                        tnext = e.Tnext();
                        @event = e.GetId();
                    }
                }
                //Console.WriteLine("\nIt's time for event in " + _elements[_event].GetName() + ", time = " + _tnext);
                foreach (Element e in list)
                {
                    e.statistics(tnext - tcurr);
                }
                tcurr = tnext;
                foreach (Element e in list)
                {
                    e.Tcurr(tcurr);
                }
                foreach (Element e in list)
                {
                    if (e.Tnext() == tcurr)
                    {
                        e.outAct(0);
                    }
                }
            }
            ellapsed = (ulong)System.Diagnostics.Stopwatch.GetTimestamp() / TimeSpan.TicksPerMillisecond - begin;
            //printResult();
            //accuracy();
        }

        //public void PrintInfo()
        //{
        //    foreach (Element e in _elements)
        //    {
        //        e.PrintInfo();
        //    }
        //}

        //public void printResult()
        //{
        //    Console.WriteLine("\n-------------RESULTS-------------");
        //    foreach (Element e in _elements)
        //    {
        //        e.PrintResult();
        //    }
        //}
        
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

                    Console.WriteLine("{0,10}|{1,22}|{2,22}|{3,22}|{4,22}", p.Name(), (Double)p.GetMeanQueue() / p.Tcurr(), theoreticalAverageQueue[i], (Double)p.GetMeanState() / p.Tcurr(), theoreticalWorkload[i]);
                    i++;
                }
            }
            Console.WriteLine("---------------------------MESSUREMENT ERRORS---------------------------");
            Console.WriteLine("{0,10}|{1,22}|{2,22}", "name", "queue accuracy", "workload accuracy");
            i = 0;
            double qAccuracy;
            double wlAccuracy;
            foreach (var e in list)
            {
                if (e.GetType() == typeof(Process))
                {
                    Process p = (Process)e;
                    qAccuracy = Math.Abs(p.GetMeanQueue() / p.Tcurr() - theoreticalAverageQueue[i]);
                    wlAccuracy = Math.Abs(p.GetMeanState() / p.Tcurr() - theoreticalWorkload[i]);
                    Console.WriteLine("{0,10}|{1,22}|{2,22}", p.Name(), qAccuracy, wlAccuracy);
                    i++;
                }
            }
        }
    }
}
