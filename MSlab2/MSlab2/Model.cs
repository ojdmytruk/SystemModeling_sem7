using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab2
{
    public class Model
    {
        private double tnext;
        private double tcurr;
        private double t0, t1;
        private double delayCreate, delayProcess;
        private int numCreate, numProcess, failure;
        private int state, maxqueue, queue;
        private int nextEvent;

        private double probability;
        private double averageDevice;
        private double waitTime;
        private double Ravg, Qavg, Lavg;

        int i;


        public Model(double delay0, double delay1)
        {
            delayCreate = delay0;
            delayProcess = delay1;
            tnext = 0.0;
            tcurr = tnext;
            t0 = tcurr; t1 = Double.MaxValue;
            maxqueue = 0;
            averageDevice = 0;
            waitTime = 0;
        }

        public Model(double delay0, double delay1, int maxQ, int iteration)
        {
            delayCreate = delay0;
            delayProcess = delay1;
            tnext = 0.0;
            tcurr = tnext;
            t0 = tcurr; t1 = Double.MaxValue;
            maxqueue = maxQ;
            averageDevice = 0;
            waitTime = 0;
            i = iteration;
        }

        public void simulate(double timeModeling)
        {
            while (tcurr < timeModeling)
            {
                tnext = t0;
                nextEvent = 0;

                if (t1 < tnext)
                {
                    tnext = t1;
                    nextEvent = 1;
                }
                averageDevice += (tnext - tcurr) * state;
                waitTime += (tnext - tcurr) * queue;
                
                tcurr = tnext;
                switch (nextEvent)
                {
                    case 0:
                        event0();
                        break;
                    case 1:
                        event1();
                        break;
                }    
                   
                if (i==0)
                    printInfo();
            }
            if (i == 0)
            {
                Console.WriteLine();
                Console.WriteLine("VERIFICATION");
                Console.WriteLine("{0,6}|{1,6}|{2,4}|{3,7}|{4,9}|{5,7}|{6,22}|{7,22}|{8,22}|{9,22}", "delay0", "delay1", "maxQ", "created", "processed", "failure", "Ravg", "Qavg", "Lavg", "probability");
            }
            Ravg = averageDevice / tnext; //середнє завантаження
            Qavg = waitTime / numProcess; //середній час очікування
            Lavg = waitTime / tnext;//середня довжина черги
            probability = Convert.ToDouble(failure) / Convert.ToDouble(numCreate);//ймовірність відмови
            Console.WriteLine("{0,6}|{1,6}|{2,4}|{3,7}|{4,9}|{5,7}|{6,22}|{7,22}|{8,22}|{9,22}", delayCreate, delayProcess, maxqueue, numCreate, numProcess, failure, Ravg, Qavg, Lavg, probability);
            
        }

       
        public void printInfo()
        {
            Console.WriteLine(" t= " + tcurr + " state = " + state + " queue = " + queue);
        }

        //Подія "надходження вимоги до СМО"
        public void event0()
        {
            t0 = tcurr + getDelayOfCreate();
            numCreate++;
            if (state == 0)
            {
                state = 1; 
                t1 = tcurr + getDelayOfProcess();
                
            }
            else
            {
                if (queue < maxqueue)
                {
                    queue++;
                }    
                    
                else
                    failure++;
            }

        }

        ////Подія "закінчилась обробка вимоги в каналі СМО"
        public void event1()
        {
            t1 = Double.MaxValue;
            state = 0;
            if (queue > 0)
            {
                queue--;
                state = 1;
                
                t1 = tcurr + getDelayOfProcess();
            }
            numProcess++;
        }


        private double getDelayOfCreate()
        {
            return Distribution.Exp(delayCreate);
        }

        private double getDelayOfProcess()
        {
            return Distribution.Exp(delayProcess);
        }


    }
}
