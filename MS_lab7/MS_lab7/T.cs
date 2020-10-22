using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace MS_lab7
{
    public class T
    {
        public List<Arc> arcsIn = new List<Arc>();
        public List<Arc> arcsOut = new List<Arc>();
        public string name = "";
        public double choiceProbability = 0;

        public T(string name)
        {
            this.name = name;
        }

        public bool transitionPossibility(List<P> positions)
        {
            bool f = true;
            List<string> fromPositionsNames = arcsIn.Select(x => x.plaseFrom.name).ToList();
            List<P> connectedPositions = positions.Where(x => fromPositionsNames.Contains(x.name) == true).ToList();
            for (int i = 0; i<connectedPositions.Count; i++)
            {
                if (connectedPositions[i].markersCount<arcsIn[i].n)
                {
                    f = false;
                    break;
                }    
            }
            return f;
        }

        public List<P> performTransaction(List<P> positions, bool ver)
        {            
            if (ver)
            {
                Console.WriteLine();
                Console.WriteLine("Transition {0}: ", name);
                Console.WriteLine("Takes markers from: ");
            }            
            foreach (var a in arcsIn)
            {
                positions.Where(x => x.name == a.plaseFrom.name).FirstOrDefault().markersCount -= 1;
                if (ver)
                    Console.Write("{0} ", a.plaseFrom.name);
            }
            if (ver)
            {
                Console.WriteLine();
                Console.Write("Sends markers to: ");
            }            
            foreach (var a in arcsOut)
            {
                positions.Where(x => x.name == a.placeTo.name).FirstOrDefault().markersCount += 1;
                if (ver)
                    Console.Write("{0} ", a.placeTo.name);
            }
            if (ver)
                Console.WriteLine();
            return positions;
        }

    }
}
