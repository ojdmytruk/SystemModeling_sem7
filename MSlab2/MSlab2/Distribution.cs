using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab2
{
    class Distribution
    {
        public static double Exp(double timeMean)
        {
            Random random = new Random();

            double a = 0;
            while (a == 0)
            {
                a = random.NextDouble();
            }
            a = -timeMean * Math.Log(a);

            return a;
        }

    }
}
