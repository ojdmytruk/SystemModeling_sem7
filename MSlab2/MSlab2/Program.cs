using System;

namespace MSlab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            double delay0 = random.Next(1, 5);
            double delay1 = random.Next(1, 5);
            int maxQ = random.Next(1, 5);

            double newDelay0 = delay0;
            double newDelay1 = delay1;
            int newMaxQ = maxQ;

            for (int i=0; i<9; i++)
            {
                Model model = new Model(newDelay0, delay1, maxQ, i);
                model.simulate(1000);
                newDelay0 *= 2;
            }

            Console.WriteLine("VERIFICATION");
            Console.WriteLine("{0,6}|{1,6}|{2,4}|{3,7}|{4,9}|{5,7}|{6,22}|{7,22}|{8,22}|{9,22}", "delay0", "delay1", "maxQ", "created", "processed", "failure", "Ravg", "Qavg", "Lavg", "probability");
            for (int i = 1; i < 10; i++)
            {
                Model model = new Model(delay0, newDelay1, maxQ, i);
                model.simulate(1000);
                newDelay1 *= 2;
            }

            Console.WriteLine("VERIFICATION");
            Console.WriteLine("{0,6}|{1,6}|{2,4}|{3,7}|{4,9}|{5,7}|{6,22}|{7,22}|{8,22}|{9,22}", "delay0", "delay1", "maxQ", "created", "processed", "failure", "Ravg", "Qavg", "Lavg", "probability");
            for (int i = 1; i < 10; i++)
            {
                Model model = new Model(delay0, newDelay1, newMaxQ, i);
                model.simulate(1000);
                newMaxQ *= 2;
            }
        }
    }
}
