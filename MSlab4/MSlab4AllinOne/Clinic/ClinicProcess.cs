using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab4AllinOne
{
    public class ClinicProcess : Element
    {
        public List<int> queue { get; set; }
        public List<int> states { get; set; }
        public int maxqueue { get; set; }
        public int failure { get; set; }
        public double probability { get; set; }
        public double queueAvg { get; set; }
        public int maxQueueCurrent { get; set; }
        public double avgWait { get; set; }
        public double avgTimeProcess { get; set; }
        public double avgWorkload { get; set; }
        public double workload { get; set; }
        public int maxParallel { get; set; }
        public List<ClinicProcess> nextProcesses { get; set; }
        public double waitTime { get; set; }
        public double waiting { get; set; }
        public double delaySum { get; set; }
        public double delayDevStart { get; set; }
        public List<Channel> channels { get; set; }
        public int tasks { get; set; }

        public List<Patient> patients { get; set; }
        public int patientType { get; set; }

        public Random random = new Random();
        
        public ClinicProcess()
        {
            nextProcesses = new List<ClinicProcess>();
            queueAvg = 0.0;
            maxQueueCurrent = 0;
            avgWorkload = 0;
            states = new List<int>();
            queue = new List<int>();
        }

        public ClinicProcess(double delay, double devDelay, string name, string distribution, int maxParallel) : base(delay, name, distribution, devDelay)
        {
            nextProcesses = new List<ClinicProcess>();
            maxqueue = 10000;
            queueAvg = 0.0;
            maxQueueCurrent = 0;
            avgWorkload = 0;
            this.maxParallel = maxParallel;
            base.delayDev = devDelay;
            states = new List<int>();
            queue = new List<int>();
            tasks = 1;
            channels = new List<Channel>();
            for (int i = 0; i < this.maxParallel; i++)
            {
                channels.Add(new Channel($"{base.name}->Channel{i + 1}", 0.0, true));
            }
            patients = new List<Patient>{
                new Patient(1, 0.5, 1/15),
                new Patient(2, 0.1, 1/40),
                new Patient(3, 0.4, 1/30)
            };
        }

        public override void inAct(int patient)
        {
            if (states.Count < maxParallel)
            {
                states.Add(patient);
            }
            else if (queue.Count < maxqueue)
            {
                queue.Add(patient);
            }
            tnext = tcurr + Delay();
        }


        public override void outAct()
        {
            quantity += 1;
            tnext = Double.MaxValue;
            int patientType = 0;
            if (states.Count > 0)
            {
                patientType = states[0];
                states.RemoveAt(0);
            }

            if (queue.Count > 0)
            {
                int patientTypeQueue = queue[0];
                queue.RemoveAt(0);
                states.Add(patientTypeQueue);
            }
            this.patientType = patientType;
            if (patientType != 0)
                patients[patientType - 1].quantity++;
            if (nextProcesses.Count != 0 && patientType != 0)
            {
                int index = random.Next(0, nextProcesses.Count);
                ClinicProcess nextProcess = nextProcesses[index];
                nextProcess.inAct(patientType);
                tnext = tcurr + Delay();
                Console.WriteLine("From {0} will go to {1} , t={2}", name, nextProcess.name, nextProcess.tnext);
            }
        }


        public List<Channel> channelOutAct(double t)
        {
            List<Channel> outChannels = new List<Channel>();
            channels = channels.OrderBy(x => x.timeFree).ToList();
            foreach (var c in channels)
            {
                if (channels.Count != 0)
                {
                    if (c.timeFree < t && c.isFree == false)
                    {
                        c.isFree = true;
                        outChannels.Add(c);
                    }
                }
            }
            return outChannels;
        }

        public void channelInAct()
        {
            int count = 0;
            int numberOfFreeDevices = maxParallel - state;
            if (tasks <= numberOfFreeDevices && tasks > 0)
            {

                for (int i = 0; i < channels.Count; i++)
                {
                    if (channels[i].isFree == true)
                    {
                        channels[i].timeFree = tcurr + Delay();
                        channels[i].isFree = false;
                        Console.WriteLine();
                        Console.WriteLine("{0} will be free at t = {1}", channels[i].name, channels[i].timeFree);
                        Console.WriteLine();
                        count++;
                    }
                    if (count == tasks)
                    {
                        break;
                    }
                }
                tasks = 0;
            }
            else if (tasks > numberOfFreeDevices)
            {
                for (int i = 0; i < channels.Count; i++)
                {
                    if (channels[i].isFree == true)
                    {
                        channels[i].timeFree = tcurr + Delay();
                        channels[i].isFree = false;
                        Console.WriteLine();
                        Console.WriteLine("{0} will be free at t = {1}", channels[i].name, channels[i].timeFree);
                        Console.WriteLine();
                        count++;
                    }
                }
                tasks -= numberOfFreeDevices;
            }
        }


        public override void printInfo()
        {
            base.printInfo();
            Console.WriteLine($"failure = {failure}");
        }
        public override void statistics(double delta)
        {
            queueAvg += (queue.Count * delta);
            avgTimeProcess += delta;
            avgWorkload += delta * states.Count;
            if (patientType != 0)
                patients[patientType - 1].waitTime += (queue.Count + states.Count) * delta;
            waitTime += (queue.Count + states.Count) * delta;
            delaySum += delta;
            waiting += queue.Count + states.Count;
            if (queue.Count > maxQueueCurrent)
            {
                maxQueueCurrent = queue.Count;
            }
            if (workload < states.Count)
            {
                workload = states.Count;
            }
        }
    }
}
