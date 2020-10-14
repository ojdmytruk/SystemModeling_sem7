using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSlab4AllinOne
{
    public class CreateDriver : Element
    {
        public List<ProcessDriver> nextElements_ { get; set; }

        public CreateDriver(double delay, string name, string distribution, double devDelay = 0) : base(delay, name, distribution, devDelay)
        {
            nextElements_ = new List<ProcessDriver>();
        }
        public override void outAct()
        {
            base.outAct();
            tnext = tcurr + Delay();
            GetNextElement().inAct(1);
        }
        public ProcessDriver GetNextElement()
        {
            List<int> queue = new List<int>();
            foreach (var e in nextElements_)
            {
                queue.Add(e.queue);
            }
            int count = 0, minIndex = 0, min = 10000;
            for (int i = 1; i < queue.Count - 1; i++)
            {
                if (queue[i] < min)
                {
                    minIndex = 0;
                }
            }
            for (int i = 0; i < queue.Count - 1; i++)
            {
                if (queue[i] == queue[0])
                {
                    count++;
                }
            }            
            if (queue.Sum(x => x) == 0 || count == queue.Count)
            {
                return nextElements_[0];
            }
            else
            {
                return nextElements_[minIndex];
            }
            
        }

    }
}
