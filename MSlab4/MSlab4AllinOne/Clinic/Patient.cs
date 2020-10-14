using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab4AllinOne
{
    public class Patient
    {
        public int id { get; set; }
        public double frequency { get; set; }
        public double avgRegistration { get; set; }
        public int quantity { get; set; }
        public double waitTime { get; set; }

        public Patient(int id, double frequency, double avgTimeOfRegistration)
        {
            this.id = id;
            this.frequency = frequency;
            avgRegistration = avgTimeOfRegistration;
            quantity = 0;
        }

        public int randomProbability(List<Patient> probabilities)
        {
            Random random = new Random();
            int x = 0;
            double a = random.NextDouble();
            probabilities = probabilities.OrderByDescending(x => x.frequency).ToList();
            double sum = probabilities.Sum(x => x.frequency);
            for (int i = 0; i < probabilities.Count; i++)
            {
                if (a < sum)
                {
                    x = probabilities[i].id;
                }
                sum -= probabilities[i].frequency;
            }
            return x;
        }
    }
}
