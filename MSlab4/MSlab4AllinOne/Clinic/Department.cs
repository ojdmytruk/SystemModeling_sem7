using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab4AllinOne
{
    public class Department : ClinicProcess
    {
        public int toExam { get; set; }
        public List<Patient> patientTypesDoc { get; set; }
        public int patientTypeDoc { get; set; }

        public Department(double delay, double startDelay, string name, string distribution, int maxParallel) : base(delay, startDelay, name, distribution, maxParallel)
        {
            toExam = 0;
            patientTypesDoc = new List<Patient>();
            patientTypesDoc = new List<Patient>{
                new Patient(1, 0.5, 1/15),
                new Patient(2, 0.1, 1/40),
                new Patient(3, 0.4, 1/30)
            };
        }

        public override void outAct()
        {
            quantity += 1;
            tnext = Double.MaxValue;
            int patientType = 0;
            if (states.Count > 0)
            {
                for (int i = 0; i < states.Count; i++)
                {
                    if (states[i] == 1)
                    {
                        patientType = states[i];
                        states.RemoveAt(i);
                        break;
                    }
                }
                if (patientType == 0)
                {
                    patientType = states.First();
                    states.RemoveAt(0);
                }
            }
            if (queue.Count != 0)
            {
                patientType = queue[0];
                queue.RemoveAt(0);
                states.Add(patientType);
            }

            patientTypeDoc = patientType;
            if (patientType != 0)
                patientTypesDoc[patientType - 1].quantity++;
            if (patientType != 0)
            {
                ClinicProcess nextProcess;
                if (patientType == 1)
                {
                    nextProcess = nextProcesses[0];
                }
                else
                {
                    nextProcess = nextProcesses[1];
                    toExam += 1;
                }
                nextProcess.inAct(patientType);
                tnext = tcurr + Delay();
                Console.WriteLine($"IN FUTURE from {name} to {nextProcess.name} t = {nextProcess.tnext}");
            }
        }

        public void stsatistics(double delta, int State)
        {
            queueAvg += queue.Count * delta;
            avgTimeProcess += delta;
            avgWorkload += delta * states.Count;
            if (patientTypeDoc != 0)
                patients[patientTypeDoc - 1].waitTime += (queue.Count + states.Count) * delta;
            waitTime += (queue.Count + states.Count) * delta;

            waiting += queue.Count + states.Count;
            if (queue.Count > maxQueueCurrent)
            {
                maxQueueCurrent = queue.Count;
            }
            if (workload < states.Count)
            {
                workload = states.Count;
            }
            if (State != 0 && (State == 2 || State == 3))
            {
                delaySum += delta;
            }
        }
    }
}
