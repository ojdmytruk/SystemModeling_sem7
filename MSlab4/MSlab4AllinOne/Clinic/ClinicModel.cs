using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab4AllinOne
{
    public class ClinicModel
    {
        public List<Element> elementList { get; set; }
        public double tcurr { get; set; }
        public double tnext { get; set; }
        public int @event { get; set; }

        

        public ClinicModel(List<Element> list)
        {
            elementList = list;
            tcurr = 0.0;
            tnext = 0.0;
        }

        public void simulate(double timeModeling)
        {
            int prev = -1;
            while (tcurr < timeModeling)
            {
                tnext = Double.MaxValue;
                foreach (var e in elementList)
                {
                    if (e.tnext < tnext)
                    {
                        tnext = e.tnext;
                        @event = elementList.IndexOf(e);
                    }
                }
                List<Channel> channels = new List<Channel>();
                List<Channel> outChannels = new List<Channel>();
                if ((elementList[@event].GetType() == typeof(ClinicProcess) || elementList[@event] is ClinicProcess == true) && elementList[@event].name != "EXIT")
                {
                    ClinicProcess p = (ClinicProcess)elementList[@event];
                    p.channelInAct();
                }
                foreach (var e in elementList)
                {
                    if ((e.GetType() == typeof(ClinicProcess) || e is ClinicProcess == true) && e.name != "EXIT")
                    {
                        ClinicProcess p = (ClinicProcess)e;
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
                    Console.WriteLine();
                    Console.WriteLine("{0} is free at t={1}", c.name, c.timeFree);
                    Console.WriteLine();
                }
                if (!(prev == 0 && @event == 0))
                    Console.WriteLine("Time fot event in {0} t={1}", elementList[@event].name, tnext);
                foreach (var e in elementList)
                {
                    if (e.GetType() == typeof(Department))
                    {
                        Department d = (Department)e;
                        if (d.states.Count > 0)
                        {
                            d.stsatistics(tnext - tcurr, d.states[0]);
                        }
                        else
                        {
                            d.stsatistics(tnext - tcurr, 0);
                        }
                    }
                    else
                    {
                        e.statistics(tnext - tcurr);
                    }
                }
                tcurr = tnext;
                foreach (var e in elementList)
                {
                    e.tcurr = tcurr;
                }
                //Get(event).OutAct();
                elementList[@event].outAct();
                foreach (var e in elementList)
                {
                    if (e.tnext == tcurr)
                    {
                        e.outAct();
                    }
                }
                //PrintInfo();
            }
            PrintResult();

        }

       
        public void PrintInfo()
        {
            foreach (var e in elementList)
            {
                e.printInfo();
            }
        }

        public void PrintResult()
        {
            Console.WriteLine("---------------------RESULTS-----------------------");
            int patients = 0;
            double tWaiting = 0;
            double timeBetweenLab = 0;
            List<double> types = new List<double> { 0, 0, 0 };
            List<int> quantities = new List<int> { 0, 0, 0 };
            Console.WriteLine("{0,20}|{1,15}|{2,10}|{3,22}|{4,22}", "name", "maxParallel", "quantity", "avgQ", "workload");
            foreach (var e in elementList)
            {
                patients += e.quantity;
                if (e.GetType() == typeof(ClinicCreate))
                {
                    ClinicCreate c = (ClinicCreate)e;
                    for (int i = 0; i < c.patients.Count; i++)
                    {
                        quantities[i] = c.patients[i].quantity;
                    }

                }
                
                if (e.GetType() == typeof(ClinicProcess))
                {
                    ClinicProcess process = new ClinicProcess();
                    process = (ClinicProcess)e;
                    patients += process.quantity;
                    tWaiting += process.waitTime;
                    foreach (var t in process.patients)
                    {
                        types[t.id - 1] += t.waitTime;
                        quantities[t.id - 1] += t.quantity;
                    }
                    double average = process.queueAvg / process.tcurr;
                    double workload = process.avgWorkload / process.tcurr;
                    Console.WriteLine("{0,20}|{1,15}|{2,10}|{3,22}|{4,22}", process.name, process.maxParallel, process.quantity, average, workload);
                    
                }
                if (e.GetType() == typeof(Department))
                {
                    Department doctor = (Department)e;
                    tWaiting += doctor.waitTime;
                    foreach (var t in doctor.patients)
                    {
                        types[t.id - 1] += t.waitTime;
                        quantities[t.id - 1] += t.quantity;
                    }
                    double average = doctor.queueAvg / doctor.tcurr;
                    double workload = doctor.avgWorkload / doctor.tcurr;
                    Console.WriteLine("{0,20}|{1,15}|{2,10}|{3,22}|{4,22}", doctor.name, doctor.maxParallel, doctor.quantity, average, workload);
                    timeBetweenLab = doctor.delaySum / doctor.toExam;
                    double AverageTime = tWaiting / patients;
                    for (int i = 0; i < types.Count; i++)
                    {
                        types[i] /= quantities[i];
                        Console.WriteLine($"Average time in the hospital of type {i + 1} is {types[i]}");
                    }
                    Console.WriteLine($"Average time in the hospital is {AverageTime}");
                    Console.WriteLine($"Avg trip from dep to lab duration is {timeBetweenLab}");

                }
            }
            
        }
    }
}
