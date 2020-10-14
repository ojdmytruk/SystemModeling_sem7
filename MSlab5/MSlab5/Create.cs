using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab5
{
    public class Create : Element
    {
        public Create(string name) : base(name)
        {

        }

        public Create(string name, string distribution, double avgDelay) : base(name, distribution, avgDelay)
        {

        }

        public Create(string name, double tnext) : base(name, tnext)
        {

        }

        public override void outAct(int element)
        {
            base.outAct(element);
            Tnext(Tcurr() + Delay);
        }

        //public override void PrintInfo()
        //{
        //    base.PrintInfo();
        //    Console.WriteLine();
        //}


        //public override void PrintResult()
        //{
        //    base.PrintResult();
        //    Console.WriteLine();
        //}
        
    }
}
