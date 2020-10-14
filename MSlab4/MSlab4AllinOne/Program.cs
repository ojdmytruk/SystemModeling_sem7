using System;

namespace MSlab4AllinOne
{
    class Program
    {
        static void Main(string[] args)
        {
            int key=0;
            TestFromLecture test = new TestFromLecture();
            DriverBankSimulation bankSimulation = new DriverBankSimulation();
            ClinicSimulation clinicSimulation = new ClinicSimulation();

            while (key!=4)
            {
                Console.WriteLine();
                Console.WriteLine("Choose simulation:");
                Console.WriteLine("1. Test from lecture");
                Console.WriteLine("2. Bank for drivers");
                Console.WriteLine("3. Clinic");
                Console.WriteLine("4. Exit");
                key = Convert.ToInt32(Console.ReadLine());
                switch(key)
                {
                    case 1:                        
                        test.TestFromLectureMain();
                        break;
                    case 2:
                        bankSimulation.DriverBankSimulationMain();
                        break;
                    case 3:
                        clinicSimulation.ClinicSimulationMain();
                        break;
                }
            }                   
                       
            
        }
    }
}
