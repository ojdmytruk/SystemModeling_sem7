using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab3
{
	public class Model
	{

		private List<Element> list = new List<Element>();
		internal double tnext, tcurr;
		internal int @event;

        public int oldIndex;

		double avgQueue, avgWait, load, failureProbability;
        

		public Model(List<Element> elements)
		{
			list = elements;
			tnext = 0.0;
			@event = 0;
            tcurr = tnext;
			
		}

		int i = 0;

        public virtual void simulate(double time, int iterator)
        {
            while (tcurr < time)
            {
                list[0].inAct(1);
                list[i].SetNextElement(i, list);
                tnext = double.MaxValue;
                if (list[i].Tnext < tnext)
                {
                    tnext = list[i].Tnext;
                }
                else if (list[i] is Process)
                {
                    Process p = (Process)list[i];
                    p.Failure = p.Failure + 1;
                }
                if (list[i] is Process)
                {
                    Process process = (Process)list[i];
                    process.Queue += 1;
                    process.doStatistics(tnext - tcurr);
                }
                if (iterator == 0)
                {
                    Console.WriteLine("It's time for event in " + list[i].Name + ", time =   " + tnext + " , base state :" + list[i].State);
                }
                tcurr = tnext;
                foreach (Element e in list)
                {
                    e.Tcurr = tcurr;
                }

                list[i].outAct(list, iterator);
                i = list.IndexOf(list[i].NextElement);
            }
            if (iterator == 0)
            {
                Console.WriteLine("{0,10}|{1,22}|{2,9}|{3,22}|{4,22}|{5,22}|{6,22}", "name", "delay","failure", "Ravg", "Qavg", "Lavg", "probability");
            }

            StatisticsResults();
        }
        


        public virtual void printInfo()
		{
			foreach (Element e in list)
			{
				e.printInfo();
			}
		}

		public virtual void StatisticsResults()
        {
            foreach (Element e in list)
            {
				if (e is Process)
                {
					Process p = (Process)e;
					avgQueue = (Double)p.MeanQueue / tnext;
					avgWait = p.MeanQueue / p.Quantity;
                    load = p.Load / tnext;
                    failureProbability = p.Failure / (Double)(p.Quantity+p.Failure);
					Console.WriteLine("{0,10}|{1,22}|{2,9}|{3,22}|{4,22}|{5,22}|{6,22}", p.Name, p.DelayMean, p.Failure, load, avgQueue, avgWait, failureProbability);
										
                }
            }
        }

		public virtual void printResult()
		{
			Console.WriteLine("\n-------------RESULTS-------------");
			foreach (Element e in list)
			{
				e.printResult();
				if (e is Process)
				{
					Process p = (Process)e;
					Console.WriteLine("mean length of queue = " + p.MeanQueue / tcurr + "\nfailure probability  = " + p.Failure / (double)p.Quantity);
				}
			}
		}
    }

}
