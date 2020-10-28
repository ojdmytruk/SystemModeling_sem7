using System;
using System.Collections.Generic;
using System.Text;

namespace MS_lab8
{
    public class P
    {
        public int markersCount;
        public int markersMin = 0;
        public int markersMax = 0;
        public double markersAvarage = 0;
        public string name { get; set; }

        public P(string name, int markersCount)
        {
            this.name = name;
            this.markersCount = markersCount;
            if (markersMin < markersCount)
            {
                markersMin = markersCount;
            }
        }

        public void markersStatistic()
        {
            if (markersCount < markersMin)
                markersMin = markersCount;
            if (markersCount > markersMax)
                markersMax = markersCount;
            markersAvarage += markersCount;
        }
    }
}
