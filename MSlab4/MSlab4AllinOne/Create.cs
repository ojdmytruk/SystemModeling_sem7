using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab4AllinOne
{
    public class Create : Element
    {
        public Create(double delay, string name, string distribution, double devDelay = 0) : base(delay, name, distribution, devDelay)
        {
        }
        public override void outAct()
        {
            base.outAct();
            tnext = tcurr + Delay();
            nextElement.inAct(1);
        }
    }
}
