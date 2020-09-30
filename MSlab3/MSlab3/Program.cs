using System;
using System.Collections.Generic;

namespace MSlab3
{
    class Program
    {
		public static void Main(string[] args)
		{
            Random random = new Random();

            double createDelay = random.Next(1, 4) + random.NextDouble();
            double delayP1 = random.Next(1, 4) + random.NextDouble();
            double delayP2 = random.Next(1, 4) + random.NextDouble();
            double delayP3 = random.Next(1, 4) + random.NextDouble();
            double delayP4 = random.Next(1, 4) + random.NextDouble();

            int devicesP1 = random.Next(1, 5);
            int devicesP2 = random.Next(1, 5);
            int devicesP3 = random.Next(1, 5);
            int devicesP4 = random.Next(1, 5);

            int maxQ1 = random.Next(1, 5);
            int maxQ2 = random.Next(1, 5);
            int maxQ3 = random.Next(1, 5);
            int maxQ4 = random.Next(1, 5);

            double newDelayP4 = delayP4;

            for (int i = 0; i < 5; i++)
            {               

                Create c = new Create(createDelay, 0);

                Process p1 = new Process(delayP1, devicesP1, 1);
                Process p2 = new Process(delayP2, devicesP2, 2);
                Process p3 = new Process(delayP3, devicesP3, 3);
                Process p4 = new Process(newDelayP4, devicesP4, 4);
                Dispose d = new Dispose();
                c.NextElement = p1;

                p1.Maxqueue = maxQ1;
                p2.Maxqueue = maxQ2;
                p3.Maxqueue = maxQ3;
                p4.Maxqueue = maxQ4;
                c.Name = "CREATOR";
                p1.Name = "PROCESSOR1";
                p2.Name = "PROCESSOR2";
                p3.Name = "PROCESSOR3";
                p4.Name = "PROCESSOR4";
                d.Name = "DESPOSE";
                c.GetDistribution = "exp";
                p1.GetDistribution = "exp";
                p2.GetDistribution = "exp";
                p3.GetDistribution = "exp";
                p4.GetDistribution = "exp";

                List<Element> elementList = new List<Element>();
                elementList.Add(c);
                elementList.Add(p1);
                elementList.Add(p2);
                elementList.Add(p3);
                elementList.Add(p4);
                elementList.Add(d);

                Model model = new Model(elementList);
                model.simulate(600, i);

                newDelayP4 /= 2;
            }

        }

	}
}
