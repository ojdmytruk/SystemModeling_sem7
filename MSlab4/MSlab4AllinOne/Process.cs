using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab4AllinOne
{
    public class Process : Element
    {
        private int queue { get; set; }
        public int maxQueue { get; set; }
        public int failure { get; set; }
        public double probability { get; set; }
        public double queueAvg { get; set; }
        public int maxQueueCurrent { get; set; }
        public double avgWait { get; set; }
        public double avgTimeProcess { get; set; }
        public double avgWorkload { get; set; }
        public double workload { get; set; }
        public int maxParallel { get; set; }
        public List<Process> nextProcesses { get; set; }
        public List<Double> probOfChoice { get; set; }
        public List<Channel> channels { get; set; }
        public int tasks { get; set; }
        public Random random = new Random();

        
        public Process()
        {
            nextProcesses = new List<Process>();
            queue = 0;
            queueAvg = 0.0;
            maxQueueCurrent = 0;
            avgWorkload = 0;
        }

        public Process(double delay, string name, string distribution, int maxQueueLength, int maxParallel, double devDelay = 0) : base(delay, name, distribution, devDelay)
        {
            nextProcesses = new List<Process>();
            queue = 0;
            maxQueue = maxQueueLength;
            queueAvg = 0.0;
            maxQueueCurrent = 0;
            avgWorkload = 0;
            this.maxParallel = maxParallel;
            probOfChoice = new List<Double>();
            channels = new List<Channel>();
            for (int i = 0; i < this.maxParallel; i++)
            {
                channels.Add(new Channel($"{base.name}->Channel{i + 1}", 0.0, true));
            }
        }

        public override void inAct(int numberOfTasks)
        {
            int freeDevices = maxParallel - state;
            tasks = numberOfTasks;
            if (numberOfTasks <= freeDevices && numberOfTasks > 0)
            {
                state += numberOfTasks;
                numberOfTasks = 0;
            }
            else if (numberOfTasks > freeDevices)
            {
                numberOfTasks -= freeDevices;
                state = maxParallel;
            }

            tnext = tcurr + Delay();
            if (numberOfTasks > 0)
            {
                int queueAvaluable = maxQueue - queue;
                if (numberOfTasks < queueAvaluable)
                {
                    queue += numberOfTasks;
                    numberOfTasks = 0;
                }
                else
                {
                    numberOfTasks -= queueAvaluable;
                    queue = maxQueue;
                }
            }

            if (numberOfTasks > 0)
            {
                failure += numberOfTasks;
            }
        }

        public override void outAct()
        {
            base.outAct();

            tnext = Double.MaxValue;
            state -= 1;
            if (queue > 0)
            {
                queue--;
                state += 1;
                tnext = tcurr + Delay();
            }

            if (nextProcesses.Count != 0 && state != -1)
            {

                if (probOfChoice.Count > 1)
                {
                    int index = randomProbability(probOfChoice);
                    Process nextProcess = nextProcesses[index];
                    if (nextProcess != null)
                        nextProcess.inAct(1);
                    Console.WriteLine("From {0} will go to {1} , t={2}", name, nextProcess.name, nextProcess.tnext);
                }
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
                        Console.WriteLine("{0} will be free at t = {1}", channels[i].name, channels[i].timeFree);
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
            queueAvg += queue * delta;
            avgTimeProcess += delta;
            avgWorkload += delta * state;
            if (queue > maxQueueCurrent)
            {
                maxQueueCurrent = queue;
            }
            if (workload < state)
            {
                workload = state;
            }

        }

        public int randomProbability(List<Double> probabilities)
        {
            Random random = new Random();
            var vers = new double[probabilities.Count];
            double sum = probabilities.Sum();
            vers[0] = probabilities[0] / sum;
            for (int i = 1; i < probabilities.Count - 1; i++)
            {
                vers[i] = probabilities[i] / sum + vers[i - 1];
            }
            vers[vers.Length - 1] = 1.0;
            double rnd = random.NextDouble();
            for (int i = 0; i < probabilities.Count; i++)
            {
                if (vers[i] > rnd)
                    return i;
            }
            return probabilities.Count;
        }
    }
}
