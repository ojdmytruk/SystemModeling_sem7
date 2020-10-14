using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab4AllinOne
{
    public class Element
    {
        public string name { get; set; }
        public double tnext { get; set; }
        public double delayAvg { get; set; }
        protected double delayDev { get; set; }
        public string distribution { get; set; }
        public int quantity { get; set; }
        public double tcurr { get; set; }
        public int state { get; set; }
        public Element nextElement { get; set; }
        public List<Element> nextElements { get; set; }
        public bool isNext { get; set; }
        private static int nextId;
        public int id { get; set; }

        public Element()
        {
            tnext = 0.0;
            delayAvg = 1.0;
            distribution = "Exponential";
            tcurr = tnext;
            state = 0;
            id = nextId;
            nextId++;
            this.name = name;
        }
        public Element(double delay, string name, string distribution, double devDelay)
        {
            this.name = name;
            tnext = 0.0;
            delayAvg = delay;
            delayDev = devDelay;
            this.distribution = distribution;
            tcurr = tnext;
            state = 0;
            id = nextId;
            nextId++;
            isNext = true;
            nextElements = new List<Element>();
            this.name = name;
        }

        public double Delay()
        {
            double delay = 0.00;
            switch (distribution)
            {
                case "Exponential":
                    delay = Distributions.exp(delayAvg);
                    break;
                case "Uniform":
                    delay = Distributions.uniform(delayAvg, delayDev);
                    break;
                case "Normal":
                    delay = Distributions.norm(delayAvg, delayDev);
                    break;
                case "Erlang":
                    delay = Distributions.erlang(delayAvg, delayDev);
                    break;
                case "":
                    delay = delayAvg;
                    break;
            }

            return delay;
        }

        public virtual void inAct(int i)
        {

        }

        public virtual void outAct()
        {
            quantity++;
        }

        public void printResult()
        {
            Console.WriteLine($"{name}: quantity = {quantity}");
        }

        public virtual void printInfo()
        {
            Console.WriteLine($"{name}: state = {state} quantity = {quantity} tnext = {tnext}");
        }

        public virtual void statistics(double delta)
        {

        }
    }
}