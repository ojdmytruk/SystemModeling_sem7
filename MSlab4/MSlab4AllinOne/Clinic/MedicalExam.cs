using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab4AllinOne
{
    public class MedicalExam : ClinicProcess
    {
        public MedicalExam(double delay, double startDelay, string name, string distribution, int maxParallel) : base(delay, startDelay, name, distribution, maxParallel)
        {
        }

        public override void outAct()
        {
            quantity += 1;
            tnext = Double.MaxValue;
            int patient = 0;
            if (states.Count != 0)
            {
                patient = states[0];
                states.RemoveAt(0);
            }
            if (queue.Count != 0)
            {
                patient = queue[0];
                queue.RemoveAt(0);
                states.Add(patient);
            }
            if (patient != 0)
            {
                ClinicProcess nextProcess;

                if (patient == 2)
                {
                    nextProcess = nextProcesses[0];
                    nextProcess.inAct(1);
                    Console.WriteLine("From {0} will go to {1} , t={2}", name, nextProcess.name, nextProcess.tnext);
                }
                else if (patient == 3)
                {
                    nextProcess = nextProcesses[1];
                    nextProcess.inAct(1);
                    Console.WriteLine("From {0} will go to {1} , t={2}", name, nextProcess.name, nextProcess.tnext);
                }
                tnext = tcurr + Delay();
            }
        }

    }
}
