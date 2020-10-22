using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MS_lab7
{
    public class Model
    {
        public List<P> positions = new List<P>();
        public List<T> transitions = new List<T>();
        public List<T> nextTransitions = new List<T>();
        Random random = new Random();

        public Model(List<P> positions, List<T> transitions)
        {
            this.positions = positions;
            this.transitions = transitions;
        }

        public void simulate(int iterations, bool ver)
        {
            int iterator = 0;
            while (iterator < iterations)
            {
                if (ver)
                {
                    Console.WriteLine();
                    Console.WriteLine("iteration {0}", iterator + 1);
                    Console.WriteLine("-----------------------------------------");
                }                
                foreach (var p in positions)
                {
                    p.markersStatistic();
                }
                foreach (var t in transitions)
                {
                    if (t.transitionPossibility(positions))
                    {
                        nextTransitions.Add(t);
                    }
                }
                foreach (var t in nextTransitions)
                {
                    t.choiceProbability = (Double)1 / nextTransitions.Count();
                }
                double r = random.NextDouble();
                for (int i=0; i<nextTransitions.Count; i++)
                {
                    if (r < nextTransitions[i].choiceProbability)
                    {
                        positions = nextTransitions[i].performTransaction(positions, ver);
                        break;
                    }
                    else
                        r -= nextTransitions[i].choiceProbability;
                }
                nextTransitions.Clear();
                iterator++;
            }
            if (ver)
                Console.WriteLine("------------------------Verification-----------------------");
            printStatistics(iterations);
        }

        public void printStatistics(int iterations)
        {            
            Console.WriteLine();
            Console.WriteLine("--------------------Statistics--------------------");
            Console.WriteLine("{0,26}|{1,4}|{2,4}|{3,22}", "name", "min", "max", "average");
            foreach (var p in positions)
            {
                p.markersAvarage /= iterations;
                Console.WriteLine("{0,26}|{1,4}|{2,4}|{3,22}", p.name, p.markersMin, p.markersMax, p.markersAvarage);
            }
        }
    }
}
