using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab4AllinOne
{
    public class DriverBankSimulation
    {
        public DriverBankSimulation()
        {

        }

        public void DriverBankSimulationMain()
        {
            CreateDriver c = new CreateDriver(0.5, "CREATOR", "Exponential", 0.1);
            ProcessDriver p1 = new ProcessDriver(0.3, "CASHBOX1", "Exponential", 2, 1);
            ProcessDriver p2 = new ProcessDriver(0.3, "CASHBOX2", "Exponential", 2, 1);
            ProcessDriver d = new ProcessDriver(0.3, "GO OUT", "Exponential", 3, 1);
            p1.nextProcesses = new List<ProcessDriver> { p2, d };
            p2.nextProcesses = new List<ProcessDriver> { p1, d };
            p1.otherProcess = p2;
            p2.otherProcess = p1;
            p1.state = 1;
            p2.state = 1;
            c.tnext = 0.5;
            p1.queue = 2;
            p2.queue = 2;
            c.nextElements_ = new List<ProcessDriver> { p1, p2 };
            List<Element> list = new List<Element> { c, p1, p2, d };
            DriverBankModel model = new DriverBankModel(list);
            model.simulate(500.0);
            model.ReturnResult();
        }
    }
}
