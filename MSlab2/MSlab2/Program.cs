using System;

namespace MSlab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            for (int i=0; i<9; i++)
            {
                double delay0 = random.Next(1, 5);
                double delay1 = random.Next(1, 5);
                int maxQ = random.Next(1, 5);                           

                Model model = new Model(delay0, delay1, maxQ, i);
                model.simulate(1000);
            }
            
            

        }
    }
}
