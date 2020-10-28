using System;
using System.Collections.Generic;
using System.Text;

namespace MS_lab8
{
    public class Arc
    {
        public T nextTransition { get; set; }
        public P plaseFrom { get; set; }
        public P placeTo { get; set; }
        public int n { get; set; }
        public string name { get; set; }

        public Arc(string name, P next, int n)
        {
            this.name = name;
            placeTo = next;
            this.n = n;
        }

        public Arc(string name, P previoustP, T nextT, int n)
        {
            this.name = name;
            nextTransition = nextT;
            plaseFrom = previoustP;
            this.n = n;
        }
    }
}
