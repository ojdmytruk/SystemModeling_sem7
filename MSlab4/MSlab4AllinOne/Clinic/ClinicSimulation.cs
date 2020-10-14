using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab4AllinOne
{
    public class ClinicSimulation
    {
        public ClinicSimulation()
        {

        }

        public void ClinicSimulationMain()
        {
            ClinicCreate c = new ClinicCreate(15, "CREATOR", "Exponential");            
            ClinicProcess p1 = new ClinicProcess(3, 8, "CHAMBERS", "Uniform", 3);
            ClinicProcess p0 = new ClinicProcess(2, 5, "GO TO DEP", "Uniform", 2/*Int16.MaxValue*/);
            ClinicProcess d = new ClinicProcess(3, 8, "EXIT", "Exponential", 3);
            ClinicProcess p2 = new ClinicProcess(3, 4.5, "REGISTRATION", "Erlang", 3);
            MedicalExam p3 = new MedicalExam(2, 4, "LABORATORY", "Erlang", 2);
            ClinicProcess p4 = new ClinicProcess(2, 5, "GO TO REGISTRATION", "Exponential", 3);
            Department p5 = new Department(2, 5, "FROM DEP", "Uniform", 2);

            c.nextElement = p0;
            c.patients = new List<Patient>{
                new Patient(1, 0.5, 1/15),
                new Patient(2, 0.1, 1/40),
                new Patient(3, 0.4, 1/30)
            };
            p0.nextProcesses = new List<ClinicProcess> { p5 };
            p5.nextProcesses = new List<ClinicProcess> { p1, p4 };
            p2.nextProcesses = new List<ClinicProcess> { p3 };
            p3.nextProcesses = new List<ClinicProcess> { p0, d };
            p4.nextProcesses = new List<ClinicProcess> { p2 };

            List<Element> elementsList = new List<Element> { c, p0, p1, p2, p3, p4, p5, d };
            ClinicModel model = new ClinicModel(elementsList);
            model.simulate(1000.0);
        }
    }
}
