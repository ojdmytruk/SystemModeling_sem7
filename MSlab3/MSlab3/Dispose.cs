using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab3
{
    public class Dispose : Element
    {
        public Dispose() 
        {

        }

        public override void inAct(int tasks)
        {
            base.inAct(1);
            Tnext = base.Tcurr;            
        }

        public override void outAct(List<Element> followingElements, int iterator)
        {
            base.outAct(followingElements, iterator);
            base.Tnext = base.Tcurr + base.Delay;
        }
    }
}
