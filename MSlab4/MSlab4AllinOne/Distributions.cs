using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab4AllinOne
{
	public class Distributions
	{      

        public static double exp(double timeMean)
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

        public static double uniform(double timeMin, double timeMax)
        {
            Random random = new Random();
            double a = 0;
            while (a == 0)
            {
                a = random.NextDouble();
            }
            a = timeMin + a * (timeMax - timeMin);

            return a;
        }

        public static double norm(double timeMean, double timeDeviation)
        {
            double a;
            Random random = new Random();
            double u1 = 1.0 - random.NextDouble();
            double u2 = 1.0 - random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            a = timeMean + timeDeviation * randStdNormal;

            return a;
        }

        public static double erlang(double timeMean, double timeDeviation)
        {
            double a = -1 / timeDeviation;
            double[] R = new double[] { 0.43, 0.80, 0.29, 0.67, 0.19, 0.96, 0.02, 0.73, 0.50, 0.33, 0.14, 0.71 };
            double r = 1;
            for (int i = 0; i < (int)timeMean; i++)
            {
                r *= R[i];
            }
            a *= Math.Log(r);
            return a;
        }
    }
}
