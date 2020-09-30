using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab3
{
    public class Distributions
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
		public static double Unif(double timeMin, double timeMax)
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

        public static double Norm(double timeMean, double timeDeviation)
        {
            double a;
            Random r = new Random();
			double u1 = 1.0 - r.NextDouble(); 
			double u2 = 1.0 - r.NextDouble();
			double nextGaussian = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
			a = timeMean + timeDeviation * nextGaussian;
            return a;
        }

    }
}
