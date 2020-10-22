using System;
using System.Collections.Generic;

namespace MS_lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i=0; i<3; i++)
            {
                P p1 = new P("[p1 - enter]", 1);
                P p2 = new P("[p2 - resource queue]", i);
                P p3 = new P("[p3 - resource is free]", 1);
                P p4 = new P("[p4 - took resource]", 0);
                P p5 = new P("[p5 - exit queue]", 0);
                P p6 = new P("[p6 - amount exit1]", 0);
                P p7 = new P("[p7 - amount exit2]", 0);

                T t1 = new T("[income]");
                T t2 = new T("[take resource]");
                T t3 = new T("[free resource]");
                T t4 = new T("[exit1]");
                T t5 = new T("[exit2]");

                Arc a1 = new Arc("[in income]",  p1, t1, 1);
                Arc a2 = new Arc("[out income(pos 1)]", p1, 1);
                Arc a3 = new Arc("[out income(pos 2))]", p2, 1);
                Arc a4 = new Arc("[in take resourse(pos 2)]",  p2, t2, 1);
                Arc a5 = new Arc("[in take resourse(pos 3)]",  p3, t2, 1);
                Arc a6 = new Arc("[out take resourse]", p4, 1);
                Arc a7 = new Arc("[in free resourse]",  p4, t3, 1);
                Arc a8 = new Arc("[out free resource(pos 5)]", p5, 1);
                Arc a9 = new Arc("[out free resource(pos 3)]", p3, 1);
                Arc a10 = new Arc("[in exit1]",  p5, t4, 1);
                Arc a11 = new Arc("[in exit2]",  p5, t5, 1);
                Arc a12 = new Arc("[out exit1]", p6, 1);
                Arc a13 = new Arc("[out exit2]", p7, 1);

                t1.arcsIn.Add(a1);
                t1.arcsOut.Add(a2);
                t1.arcsOut.Add(a3);
                t2.arcsIn.Add(a4);
                t2.arcsIn.Add(a5);
                t2.arcsOut.Add(a6);
                t3.arcsIn.Add(a7);
                t3.arcsOut.Add(a8);
                t3.arcsOut.Add(a9);
                t4.arcsIn.Add(a10);
                t4.arcsOut.Add(a12);
                t5.arcsIn.Add(a11);
                t5.arcsOut.Add(a13);

                List<P> positions = new List<P>() { p1, p2, p3, p4, p5, p6, p7 };
                foreach (var p in positions)
                {
                    Console.WriteLine("Start number of markers in {0}: {1}", p.name, p.markersCount);
                }
                List<T> transactions = new List<T>() { t1, t2, t3, t4, t5 };
                Model model = new Model(positions, transactions);
                if (i == 0)
                    model.simulate(100, true);
                else
                    model.simulate(100, false);
                Console.WriteLine("-----------------------------------------------------------");

            }
        }
    }
}
