using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab3
{
	public class Element
	{
		private string name;
		private double tnext;
		private double delayMean, delayDev;
		private string distribution;
		private int quantity;
		private double tcurr;
		private int state;
		private Element nextElement;
		private static int nextId = 0;
		private int id;

		private List<int> help1 = new List<int>();
		private List<int> help2 = new List<int>();
		private List<int> help3 = new List<int>();
		private List<int> help4 = new List<int>();
		//private List<Element> followingElements = new List<Element>();


		public Element()
		{
			tnext = 0.0;
			delayMean = 1.0;
			distribution = "exp";
			tcurr = tnext;
			state = 0;
			nextElement = null;
			id = nextId;
			nextId++;
			name = "element" + id;

			help1.Add(2);
			help1.Add(3);
			help2.Add(1);
			help2.Add(3);
			help2.Add(5);
			help3.Add(1);
			help3.Add(2);
			help3.Add(4);
			help4.Add(1);
			help4.Add(3);
			help4.Add(5);

		}
		public Element(double delay)
		{
			name = "anonymus";
			tnext = 0.0;
			delayMean = delay;
			distribution = "";
			tcurr = tnext;
			state = 0;
			nextElement = null;
			id = nextId;
			nextId++;
			name = "element" + id;

			help1.Add(2);
			help1.Add(3);
			help2.Add(1);
			help2.Add(3);
			help2.Add(5);
			help3.Add(1);
			help3.Add(2);
			help3.Add(4);
			help4.Add(1);
			help4.Add(3);
			help4.Add(5);
		}
		public Element(string nameOfElement, double delay)
		{
			name = nameOfElement;
			tnext = 0.0;
			delayMean = delay;
			distribution = "exp";
			tcurr = tnext;
			state = 0;
			nextElement = null;
			id = nextId;
			nextId++;
			name = "element" + id;

			help1.Add(2);
			help1.Add(3);
			help2.Add(1);
			help2.Add(3);
			help2.Add(5);
			help3.Add(1);
			help3.Add(2);
			help3.Add(4);
			help4.Add(1);
			help4.Add(3);
			help4.Add(5);
		}

		public virtual double Delay
		{
			get
			{
				double delay = DelayMean;
				if ("exp".Equals(GetDistribution, StringComparison.OrdinalIgnoreCase))
				{
                    delay = Distributions.Exp(DelayMean);
				}
				else
				{
					if ("norm".Equals(GetDistribution, StringComparison.OrdinalIgnoreCase))
					{
						delay = Distributions.Norm(DelayMean, DelayDev);
					}
					else
					{
						if ("unif".Equals(GetDistribution, StringComparison.OrdinalIgnoreCase))
						{
							delay = Distributions.Unif(DelayMean, DelayDev);
						}
						else
						{
							if ("".Equals(GetDistribution, StringComparison.OrdinalIgnoreCase))
							{
								delay = DelayMean;
							}
						}
					}
				}
				return delay;
			}
		}




		public virtual double DelayDev
		{
			get
			{
				return delayDev;
			}
			set
			{
				this.delayDev = value;
			}
		}


		public virtual string GetDistribution
		{
			get
			{
				return distribution;
			}
			set
			{
				this.distribution = value;
			}
		}



		public virtual int Quantity
		{
			get
			{
				return quantity;
			}
            set
            {
				this.quantity = value;
            }
		}

		public virtual double Tcurr
		{
			get
			{
				return tcurr;
			}
			set
			{
				this.tcurr = value;
			}
		}


		public virtual int State
		{
			get
			{
				return state;
			}
			set
			{
				this.state = value;
			}
		}


		public virtual Element NextElement
		{
			get
			{
				return nextElement;
			}
			set
			{
				this.nextElement = value;
			}
		}


		public virtual void inAct(int tasks)
		{

		}

		public virtual void outAct(List<Element> followingElements, int iterator)
		{
			quantity ++;
		}

		public virtual double Tnext
		{
			get
			{
				return tnext;
			}
			set
			{
				this.tnext = value;
			}
		}



		public virtual double DelayMean
		{
			get
			{
				return delayMean;
			}
			set
			{
				this.delayMean = value;
			}
		}


		public virtual int Id
		{
			get
			{
				return id;
			}
			set
			{
				this.id = value;
			}
		}

		public virtual void printResult()
		{
			Console.WriteLine(Name + "  quantity = " + quantity);
		}

		public virtual void printInfo()
		{
			Console.WriteLine(Name + " state= " + state + " quantity = " + quantity + " tnext= " + tnext);
		}

		public virtual string Name
		{
			get
			{
				return name;
			}
			set
			{
				this.name = value;
			}
		}


		public virtual void doStatistics(double delta)
		{

		}

		public virtual void SetNextElement(int index, List<Element> list)
		{
			Random random = new Random();
			if (index == 0) this.NextElement = list[1];
			else if (index == 1)
			{
				int nextEvent = help1.OrderBy(x => random.Next()).First();
				this.NextElement = list[nextEvent];
			}
			else if (index == 2)
			{
				int nextEvent = help2.OrderBy(x => random.Next()).First();
				this.NextElement = list[nextEvent];
			}
			else if (index == 3)
			{
				int nextEvent = help3.OrderBy(x => random.Next()).First();
				this.NextElement = list[nextEvent];
			}
			else if (index == 4)
			{
				int nextEvent = help4.OrderBy(x => random.Next()).First();
				this.NextElement = list[nextEvent];
			}
			else if (index == 5) this.NextElement = list[0];
		}

	}
}