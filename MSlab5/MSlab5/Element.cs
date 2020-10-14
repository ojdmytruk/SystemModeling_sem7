using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab5
{
    public class Element
    {
        private static int nextId = 0;
        public Random Rand;
        private int id;
        private string name;
        private double tcurr;
        private double tnext = 0.0;
        private double deleyAvg = 1.0;
        private double delayDev = 1.0;
        private string distribution = "";
        private int quantity = 0;
        private List<Element> nextElements = new List<Element>();
        private List<double> probabilities = new List<double>();
        private List<double> probId = new List<double>();

        public Element()
        {
            id = nextId++;
            name = "element_" + id;
            Rand = new Random();
        }

        public Element(string name)
        {
            id = nextId++;
            this.name = name;
            Rand = new Random();
        }

        public Element(string name, string distribution, double avgDelay)
        {
            id = nextId++;
            this.name = name;
            Rand = new Random();
            this.distribution = distribution;
            deleyAvg = avgDelay;

        }

        public Element(string name, double tnext) : this(name)
        {

            this.tnext = tnext;
        }

        public double Delay
        {
            get
            {
                double delay;

                switch (distribution)
                {
                    case "exp":
                        {
                            delay = Distributions.exp(deleyAvg);
                            break;
                        }
                    case "norm":
                        {
                            delay = Distributions.norm(deleyAvg, delayDev);
                            break;
                        }
                    case "unif":
                        {
                            delay = Distributions.uniform(deleyAvg, delayDev);
                            break;
                        }
                    case "erl":
                        {
                            delay = Distributions.erlang(deleyAvg, delayDev);
                            break;
                        }
                    default:
                        {
                            delay = deleyAvg;
                            break;
                        }
                }
                return delay;
            }
           
        }


        public int Quantity()
        {
            return quantity;
        }

        public virtual double Tcurr()
        {
            return tcurr;
        }

        public void Tcurr(double tcurr)
        {
            this.tcurr = tcurr;
        }

        public void NextElement(Element nextElement)
        {
            NextElement(nextElement, 1.0);
        }

        public void NextElement(Element nextElement, double probabilityIndex)
        {
            nextElements.Add(nextElement);
            probId.Add(probabilityIndex);
            double indexSum = probId.Select(p => p).Sum();
            probabilities = probId.Select(x => x / indexSum).ToList();
        }

        public virtual void inAct(int nextElement)
        {

        }

        public virtual void outAct(int element)
        {
            quantity++;
            Element nextElement;
            nextElement = ChooseNextElement();
            if (nextElement != null)
            {
                nextElement.inAct(element);
            }
        }

        public double Tnext()
        {
            return tnext;
        }

        public void Tnext(double tnext)
        {
            this.tnext = tnext;
        }

        public int GetId()
        {
            return id;
        }

        //public virtual void PrintResult()
        //{
        //    Console.WriteLine(name + " quantity: " + quantity + "\t");
        //}

        //public virtual void PrintInfo()
        //{
        //    Console.WriteLine(name + " quantity: " + quantity + "\t");
        //    Console.WriteLine("tnext: " + tnext + "\t");
        //}

        public string Name()
        {
            return name;
        }

        public virtual void statistics(double delta)
        {

        }

        public Element ChooseNextElement()
        {
            double rand = Rand.NextDouble();
            Element nextElement = null;

            for (int i = 0; i < probabilities.Count; i++)
            {
                if (rand < probabilities[i])
                {
                    nextElement = nextElements[i];
                    break;
                }
                else
                {
                    rand -= probabilities[i];
                }
            }
            return nextElement;
        }

    }
}
