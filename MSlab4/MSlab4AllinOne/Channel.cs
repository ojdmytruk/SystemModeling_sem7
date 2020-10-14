using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab4AllinOne
{
    public class Channel
    {
        public string name { get; set; }
        public double timeFree { get; set; }
        public bool isFree { get; set; }


        public Channel(string name, double timeFree, bool isFree)
        {
            this.name = name;
            this.timeFree = timeFree;
            this.isFree = isFree;
        }
    }
}
