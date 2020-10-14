using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab5
{
    public class Process : Element
    {
        private int maxqueue = int.MaxValue;
        private int failure = 0;
        private double avgQueue = 0.0;
        protected int state = 0;
        protected int maxParallel = 1;
        private double workload = 0.0;       
        protected List<int> nextElements = new List<int>();
        private List<int> queue = new List<int>();

        protected List<double> nextElTime = new List<double>();

        public Process(String name) : base(name)
        {

        }

        public Process(string name, string distribution, double avgDelay) : base(name, distribution, avgDelay)
        {

        }

        //public Process(String name, int state, List<double> tNextList) : base(name)
        //{
        //    this.state = state;
        //    _tNextList = tNextList;
        //    nextElements = new List<int>();
        //    for (int i = 0; i < _tNextList.Count; i++)
        //    {
        //        nextElements.Add(0);
        //    }
        //}

        public override void inAct(int elNext)
        {
            base.inAct(elNext);
            if (state < maxParallel)
            {
                double newTNext = Tcurr() + Delay;
                state++;
                nextElements.Add(elNext);
                nextElTime.Add(newTNext);
                if (Tnext() > newTNext)
                    Tnext(newTNext);
            }
            else
            {
                if (queue.Count < maxqueue)
                {
                    queue.Add(elNext);
                }
                else
                {
                    failure++;
                }
            }
        }

        public override void outAct(int element)
        {
            double newTNext = double.MaxValue;
            if (nextElTime.Count != 0)
            {
                List<int> elementsToAct = new List<int>();
                for (int i = 0; i < nextElTime.Count; i++)
                {
                    if (nextElTime[i] <= Tcurr())
                    {
                        base.outAct(nextElements[i]);
                        elementsToAct.Add(i);
                    }
                }
                for (int i = elementsToAct.Count - 1; i >= 0; i--)
                {
                    int elementToRemove = elementsToAct[i];
                    nextElTime.RemoveAt(elementToRemove);
                    nextElements.RemoveAt(elementToRemove);
                }
                for (int i = 0; i < nextElTime.Count; i++)
                {
                    double temp = nextElTime[i];
                    if (temp < newTNext)
                        newTNext = temp;
                }

            }
            Tnext(newTNext);
            state = nextElTime.Count;
            ProcessFromQueue();
        }


        public void MaxParallel(int parallel)
        {
            if (parallel > 0)
                maxParallel = parallel;
        }

        //public override void PrintInfo()
        //{
        //    base.PrintInfo();
        //    Console.WriteLine("state: " + _state + "\t");
        //    Console.WriteLine("queue: " + _queue.Count + " of " + _maxqueue + "\t");
        //    Console.WriteLine("failure: " + _failure + "\t");
        //    Console.WriteLine();
        //}

        //public override void PrintResult()
        //{
        //    base.PrintResult();
        //    Console.WriteLine("mean length of queue: " + _meanQueue / GetTcurr() + "\t");
        //    Console.WriteLine("mean state: " + _meanState / GetTcurr() + "\t");
        //    Console.WriteLine("failure probability: " + _failure / ((double)GetQuantity() + _failure) + "\t");
        //    Console.WriteLine();
        //}

        public override void statistics(double delta)
        {
            avgQueue = GetMeanQueue() + queue.Count * delta;
            workload = GetMeanState() + state * delta;
        }

        public double GetMeanQueue()
        {
            return avgQueue;
        }

        public double GetMeanState()
        {
            return workload;
        }

        public int GetFailure()
        {
            return failure;
        }

        protected virtual void ProcessFromQueue()
        {
            if (queue.Count > 0)
            {
                int element = queue[0];
                queue.RemoveAt(0);
                inAct(element);
            }
        }
        
    }
}
