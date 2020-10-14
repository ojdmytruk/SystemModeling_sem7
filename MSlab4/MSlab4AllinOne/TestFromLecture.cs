using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab4AllinOne
{
    public class TestFromLecture
    {
        public TestFromLecture()
        {

        }

        public void TestFromLectureMain()
        {
            Create c = new Create(2.0, "CREATOR", "Uniform");
            Process p1 = new Process(0.6, "PROCESSOR1", "Uniform", 2, 1);
            Process p2 = new Process(0.3, "PROCESSOR2", "Uniform", 2, 1);
            Process p3 = new Process(0.4, "PROCESSOR3", "Uniform", 2, 1);
            Process p4 = new Process(0.1, "PROCESSOR4", "Uniform", 2, 2);

            c.nextElement = p1;
            p1.probOfChoice = new List<Double> { 0.15, 0.13, 0.3 };
            p1.nextProcesses = new List<Process> { p2, p3, p4 };            
            p2.nextProcesses = new List<Process> { p1 };
            p3.nextProcesses = new List<Process> { p1 };
            p4.nextProcesses = new List<Process> { p1 };
            List<Element> elementsList = new List<Element> { c, p1, p2, p3, p4 };
            Model model = new Model(elementsList);
            model.simulate(10000.0);
            model.accuracy();
            
            
        }
    }
}
