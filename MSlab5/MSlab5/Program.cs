using System;
using System.Collections.Generic;

namespace MSlab5
{
    class Program
    {
        static void Main(string[] args)
        {

            CheckAccuracy();
            Random random = new Random();
            double processorDelay = (Double)random.Next(1, 3);
            NModelSerial(4, 10000.0, processorDelay);
            Console.WriteLine();
            Console.WriteLine("Task4____________________________________________________________________");
            Console.WriteLine();
            TheoreticalEstimate(4, 10000.0, processorDelay);
            NewNModelSerial(4, 10000.0, processorDelay);
        }

        static void CheckAccuracy()
        {
            Console.WriteLine();
            Console.WriteLine("Task1____________________________________________________________________");
            Console.WriteLine();
            Create c = new Create("CREATOR", "exp", 2.0);
            Process p1 = new Process("PROCESSOR1", "exp", 0.6);
            Process p2 = new Process("PROCESSOR2", "exp", 0.3);
            Process p3 = new Process("PROCESSOR3", "exp", 0.4);
            Process p4 = new Process("PROCESSOR4", "exp", 0.1);
            c.NextElement(p1);
            p1.NextElement(null, 0.42);
            p1.NextElement(p2, 0.15);
            p1.NextElement(p3, 0.13);
            p1.NextElement(p4, 0.3);
            p2.NextElement(p1);
            p3.NextElement(p1);
            p4.NextElement(p1);
            p4.MaxParallel(2);

            List<Element> list = new List<Element>() { c, p1, p2, p3, p4 };
            Model model = new Model(list);
            model.simulate(10000.0);
            model.accuracy();
        }

        static void NModelSerial(int eventsNum, double timeModeling, double processorDelay)
        {
            Console.WriteLine();
            Console.WriteLine("Task2,Task3______________________________________________________________");
            Console.WriteLine();
            int n = eventsNum;            
            List<Element> listProcesses;
            
            string processName;
            Console.WriteLine("{0,2}|{1,22}", "N", "ms");
            for (int iS=0; iS<10; iS++)
            {
                listProcesses = new List<Element>(n);
                Create c = new Create("CREATOR", "exp", processorDelay);
                listProcesses.Add(c);
                for (int i=1; i<=n;i++)
                {
                    processName = "P" + i.ToString();
                    listProcesses.Add(new Process(processName, "exp", processorDelay));
                    listProcesses[i - 1].NextElement(listProcesses[i]);
                }
                Model model = new Model(listProcesses);
                model.simulate(timeModeling);
                Console.WriteLine("{0,2}|{1,22}", n, model.ellapsed.ToString());
                n += 2;
            }
        }

        static void TheoreticalEstimate(int eventsNum, double timeModeling, double processorDelay)
        {
            double estimate;
            Console.WriteLine("{0,2}|{1,22}", "N", "estimate");
            int n = eventsNum;
            for (int i=0; i<10; i++)
            {
                estimate = 2 * Math.Log(processorDelay) * timeModeling * (n + 1);
                Console.WriteLine("{0,2}|{1,22}", n, estimate);
                n++;
            }
        }

        static void NewNModelSerial(int eventsNum, double timeModeling, double processorDelay)
        {
            Console.WriteLine();
            Console.WriteLine("Task5____________________________________________________________________");
            Console.WriteLine();
            int n = eventsNum;
            List<Element> listProcesses;
            Console.WriteLine("{0,2}|{1,22}", "N", "seconds");
            for (int iS = 0; iS < 10; iS++)
            {
                listProcesses = new List<Element>(n);
                Create c = new Create("CREATOR", "exp", processorDelay);
                listProcesses.Add(c);
                for (int i = 1; i<=n; i++)
                {
                    listProcesses.Add(new Process("P", "exp", processorDelay));
                }
                for (int i=0; i<n-2; i++)
                {
                    listProcesses[i].NextElement(listProcesses[i + 1], 0.5);
                    listProcesses[i].NextElement(listProcesses[i + 2], 0.5);
                }
                listProcesses[n - 1].NextElement(null, 1.0);
                listProcesses[n].NextElement(null, 1.0);
                Model model = new Model(listProcesses);
                model.simulate(timeModeling);
                Console.WriteLine("{0,2}|{1,22}", n, model.ellapsed.ToString());
                n += 2;
            }
        }
    }
}
