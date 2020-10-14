using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab4AllinOne
{
    public class ClinicCreate : Element
    {
        public List<Patient> patients { get; set; }
        public Patient patient { get; set; }

        public ClinicCreate(double delay, string name, string distribution, double devDelay = 0) : base(delay, name, distribution, devDelay)
        {
            patients = new List<Patient>();
            patient = new Patient(0, 0, 0);
        }


        public override void outAct()
        {
            base.outAct();
            int index = 0;
            if (patients.Count > 0)
            {
                index = patient.randomProbability(patients) - 1;
                foreach (var t in patients)
                {
                    if (index + 1 == t.id)
                    {
                        t.quantity++;
                    }
                }
            }

            tnext = tcurr + Delay();

            nextElement.delayAvg = patients[index].avgRegistration;
            nextElement.inAct(patients[index].id);
        }
    }
}
