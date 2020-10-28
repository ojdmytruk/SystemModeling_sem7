using System;
using System.Collections.Generic;

namespace MS_lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            int key = 0;

            while (key != 4)
            {
                Console.WriteLine();
                Console.WriteLine("Choose simulation:");
                Console.WriteLine("1. Task1");
                Console.WriteLine("2. Task2");
                Console.WriteLine("3. Task3");
                Console.WriteLine("4. Exit");
                key = Convert.ToInt32(Console.ReadLine());
                switch (key)
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                }
            }
        }

        public static void Task1()
        {
            T genA = new T("Generate messege A");
            T queryAB = new T("Query A-B");
            T replyBA = new T("Reply B-A");
            T sendAB = new T("Send A-B");
            T getInB = new T("Get in B");
            T informGetInB = new T("Successful get in B");
            //
            T genB = new T("Generate messege B");
            T queryBA = new T("Query B-A");
            T replyAB = new T("Reply A-B");
            T sendBA = new T("Send B-A");
            T getInA = new T("Get in A");
            T informGetInA = new T("Successful get in A");

            P indicator = new P("Indicator", 1);
            P incomingA = new P("IncomingA", 1);
            P generatedA = new P("GeneratedA", 0);
            P requestedA = new P("RequestedA", 0);
            P admitedA = new P("AdmitedA", 0);
            P sentA = new P("SentA", 0);
            P gotB = new P("GotB", 0);
            P allGotB = new P("All got in B", 0);
            //
            P incomingB = new P("IncomingB", 1);
            P generatedB = new P("GeneratedB", 0);
            P requestedB = new P("RequestedB", 0);
            P admitedB = new P("AdmitedB", 0);
            P sentB = new P("SentB", 0);
            P gotA = new P("GotA", 0);
            P allGotA = new P("All got in A", 0);

            Arc indicator_replyBA = new Arc("ind-rba", indicator, replyBA, 1);
            Arc incomingA_genA = new Arc("inA-genA", incomingA, genA, 1);
            Arc genA_incomingA = new Arc("genA-inA", incomingA, 1);
            Arc genA_generatedA = new Arc("genA-gendA", generatedA, 1);
            Arc generatedA_queryAB = new Arc("gendA-qab", generatedA, queryAB, 1);
            Arc queryAB_requestedA = new Arc("qab-reqA", requestedA, 1);
            Arc requestedA_replyBA = new Arc("reqA-rba", requestedA, replyBA, 1);
            Arc replyBA_admitedA = new Arc("rba-admA", admitedA, 1);
            Arc admitedA_sendAB = new Arc("admA-sendAB", admitedA, sendAB, 1);
            Arc sendAB_sentA = new Arc("sendAB-sentA", sentA, 1);
            Arc sentA_getInB = new Arc("sentA-getinB", sentA, getInB, 1);
            Arc getInB_gotB = new Arc("getinB_gotB", gotB, 1);
            Arc gotB_infGotInB = new Arc("gotB_infgB", gotB, informGetInB, 1);
            Arc infGotInB_indicator = new Arc("infgB_ind", indicator, 1);
            Arc infGotInB_allGotB = new Arc("infgB_allGotB", allGotB, 1);
            //
            Arc indicator_replyAB = new Arc("ind-rab", indicator, replyAB, 1);
            Arc incomingB_genB = new Arc("inB-genB", incomingB, genB, 1);
            Arc genB_incomingB = new Arc("genB-inB", incomingB, 1);
            Arc genB_generatedB = new Arc("genB-gendB", generatedB, 1);
            Arc generatedB_queryBA = new Arc("gendB-qba", generatedB, queryBA, 1);
            Arc queryBA_requestedB = new Arc("qba-reqB", requestedB, 1);
            Arc requestedB_replyAB = new Arc("reqB-rab", requestedB, replyAB, 1);
            Arc replyAB_admitedB = new Arc("rab-admB", admitedB, 1);
            Arc admitedB_sendBA = new Arc("admB-sendBA", admitedB, sendBA, 1);
            Arc sendBA_sentB = new Arc("sendBA-sentB", sentB, 1);
            Arc sentB_getInA = new Arc("sentB-getinA", sentB, getInA, 1);
            Arc getInA_gotA = new Arc("getinA_gotA", gotA, 1);
            Arc gotA_infGotInA = new Arc("gotA_infgA", gotA, informGetInA, 1);
            Arc infGotInA_indicator = new Arc("infgA_ind", indicator, 1);
            Arc infGotInA_allGotA = new Arc("infgA_allGotA", allGotA, 1);

            genA.arcsIn.Add(incomingA_genA);
            genA.arcsOut.Add(genA_generatedA);
            genA.arcsOut.Add(genA_incomingA);
            queryAB.arcsIn.Add(generatedA_queryAB);
            queryAB.arcsOut.Add(queryAB_requestedA);
            replyBA.arcsIn.Add(requestedA_replyBA);
            replyBA.arcsIn.Add(indicator_replyBA);
            replyBA.arcsOut.Add(replyBA_admitedA);
            sendAB.arcsIn.Add(admitedA_sendAB);
            sendAB.arcsOut.Add(sendAB_sentA);
            getInB.arcsIn.Add(sentA_getInB);
            getInB.arcsOut.Add(getInB_gotB);
            informGetInB.arcsIn.Add(gotB_infGotInB);
            informGetInB.arcsOut.Add(infGotInB_allGotB);
            informGetInB.arcsOut.Add(infGotInB_indicator);
            //
            genB.arcsIn.Add(incomingB_genB);
            genB.arcsOut.Add(genB_generatedB);
            genB.arcsOut.Add(genB_incomingB);
            queryBA.arcsIn.Add(generatedB_queryBA);
            queryBA.arcsOut.Add(queryBA_requestedB);
            replyAB.arcsIn.Add(requestedB_replyAB);
            replyAB.arcsIn.Add(indicator_replyAB);
            replyAB.arcsOut.Add(replyAB_admitedB);
            sendBA.arcsIn.Add(admitedB_sendBA);
            sendBA.arcsOut.Add(sendBA_sentB);
            getInA.arcsIn.Add(sentB_getInA);
            getInA.arcsOut.Add(getInA_gotA);
            informGetInA.arcsIn.Add(gotA_infGotInA);
            informGetInA.arcsOut.Add(infGotInA_allGotA);
            informGetInA.arcsOut.Add(infGotInA_indicator);

            List<P> places = new List<P>() { incomingA, generatedA, requestedA, admitedA, sentA, gotB, allGotB, indicator,
                                             incomingB, generatedB, requestedB, admitedB, sentB, gotA, allGotA};
            List<T> transitions = new List<T>() { genA, queryAB, replyBA, sendAB, getInB, informGetInB,
                                                  genB, queryBA, replyAB, sendBA, getInA, informGetInA};
            Model task1 = new Model(places, transitions);
            task1.simulate(100);
            Console.WriteLine();
            Console.WriteLine("Got in B amount: {0}", allGotB.markersCount);
            Console.WriteLine("Got in A amount: {0}", allGotA.markersCount);
        }
        public static void Task2()
        {
            int n;
            Console.WriteLine("Enter buffer max:");
            n = Convert.ToInt32(Console.ReadLine());

            T processor = new T("Processor");
            T consumer = new T("Consumer");

            P incoming = new P("Incoming", 1);
            P buffer = new P("Buffer", 0);
            P stopProcessorRule = new P("Free in buffer", n);
            P consumedNumber = new P("Consumed", 0);

            Arc incoming_processor = new Arc("inc-proc", incoming, processor, 1);
            Arc processor_buffer = new Arc("put", buffer, 1);
            Arc processor_incoming = new Arc("proc-inc", incoming, 1);
            Arc buffer_consumer = new Arc("take", buffer, consumer, 1);
            Arc consumer_stoprule = new Arc("cons-spr", stopProcessorRule, 1);
            Arc stoprule_processor = new Arc("spr-proc", stopProcessorRule, processor, 1);
            Arc consumer_consumed = new Arc("cons-count", consumedNumber, 1);

            processor.arcsIn.Add(incoming_processor);
            processor.arcsIn.Add(stoprule_processor);
            processor.arcsOut.Add(processor_buffer);
            processor.arcsOut.Add(processor_incoming);
            consumer.arcsIn.Add(buffer_consumer);
            consumer.arcsOut.Add(consumer_stoprule);
            consumer.arcsOut.Add(consumer_consumed);

            List<P> places = new List<P>() { incoming, buffer, stopProcessorRule, consumedNumber};
            List<T> transitions = new List<T>() { processor, consumer };

            Model task2 = new Model(places, transitions);
            task2.simulate(100);
            Console.WriteLine();
            Console.WriteLine("Average markers in buffer: {0}", buffer.markersAvarage.ToString());
        }

        public static void Task3()
        {
            int n;
            Console.WriteLine("Enter resources number (>3):");
            n = Convert.ToInt32(Console.ReadLine());

            T type_1_create = new T("Create type 1");
            T type_1_process = new T("Process type 1");
            T type_2_create = new T("Create type 2");
            T type_2_process = new T("Process type 2");
            T type_3_create = new T("Create type 3");
            T type_3_process = new T("Process type 3");

            P resources = new P("Resources", n);
            P incoming1 = new P("Incoming t1", 1);
            P incoming2 = new P("Incoming t2", 1);
            P incoming3 = new P("Incoming t3", 1);
            P created1 = new P("Created t1", 0);
            P created2 = new P("Created t2", 0);
            P created3 = new P("Created t3", 0);
            P processed1 = new P("Processed t1", 0);
            P processed2 = new P("Processed t2", 0);
            P processed3 = new P("Processed t3", 0);

            Arc incoming1_create1 = new Arc("inc1-cr1", incoming1, type_1_create, 1);
            Arc create1_incoming1 = new Arc("cr1-inc1", incoming1, 1);
            Arc create1_created1 = new Arc("cr1-crd1", created1, 1);
            Arc created1_process1 = new Arc("crd1-pr1", created1, type_1_process, 1);
            Arc resources_process1 = new Arc("r-pr1", resources, type_1_process, n);
            Arc process1_resources = new Arc("pr1-r", resources, n);
            Arc process1_processed1 = new Arc("pr1-prd1", processed1, 1);
            //
            Arc incoming2_create2 = new Arc("inc2-cr2", incoming2, type_2_create, 1);
            Arc create2_incoming2 = new Arc("cr2-inc2", incoming2, 1);
            Arc create2_created2 = new Arc("cr2-crd2", created2, 1);
            Arc created2_process2 = new Arc("crd2-pr2", created2, type_2_process, 1);
            Arc resources_process2 = new Arc("r-pr2", resources, type_2_process, n/3);
            Arc process2_resources = new Arc("pr2-r", resources, n/3);
            Arc process2_processed2 = new Arc("pr2-prd2", processed2, 1);
            //
            Arc incoming3_create3 = new Arc("inc3-cr3", incoming3, type_3_create, 1);
            Arc create3_incoming3 = new Arc("cr3-inc3", incoming3, 1);
            Arc create3_created3 = new Arc("cr3-crd3", created3, 1);
            Arc created3_process3 = new Arc("crd3-pr3", created3, type_3_process, 1);
            Arc resources_process3 = new Arc("r-pr3", resources, type_3_process, n/2);
            Arc process3_resources = new Arc("pr3-r", resources, n/2);
            Arc process3_processed3 = new Arc("pr3-prd3", processed3, 1);

            type_1_create.arcsIn.Add(incoming1_create1);
            type_1_create.arcsOut.Add(create1_incoming1);
            type_1_create.arcsOut.Add(create1_created1);
            type_1_process.arcsIn.Add(created1_process1);
            type_1_process.arcsIn.Add(resources_process1);
            type_1_process.arcsOut.Add(process1_processed1);
            type_1_process.arcsOut.Add(process1_resources);

            type_2_create.arcsIn.Add(incoming2_create2);
            type_2_create.arcsOut.Add(create2_incoming2);
            type_2_create.arcsOut.Add(create2_created2);
            type_2_process.arcsIn.Add(created2_process2);
            type_2_process.arcsIn.Add(resources_process2);
            type_2_process.arcsOut.Add(process2_processed2);
            type_2_process.arcsOut.Add(process2_resources);

            type_3_create.arcsIn.Add(incoming3_create3);
            type_3_create.arcsOut.Add(create3_incoming3);
            type_3_create.arcsOut.Add(create3_created3);
            type_3_process.arcsIn.Add(created3_process3);
            type_3_process.arcsIn.Add(resources_process3);
            type_3_process.arcsOut.Add(process3_processed3);
            type_3_process.arcsOut.Add(process3_resources);

            List<P> places = new List<P>() { resources, incoming1, incoming2, incoming3,
                                                        created1, created2, created3,
                                                        processed1, processed2, processed3};
            List<T> transitions = new List<T>() { type_1_create, type_1_process, type_2_create, type_2_process, type_3_create, type_3_process };
            Model task3 = new Model(places, transitions);
            task3.simulate(100);
            Console.WriteLine();
            int allProcessed = processed1.markersCount + processed2.markersCount + processed3.markersCount;
            Console.WriteLine("Processed amount: {0}", allProcessed);
            Console.WriteLine("{0,4}|{1,10}|{2,10}", "Type", "Processed", "% of all");
            Console.WriteLine("{0,4}|{1,10}|{2,10}", "1", processed1.markersCount, (((Double)processed1.markersCount/allProcessed)*100).ToString());
            Console.WriteLine("{0,4}|{1,10}|{2,10}", "2", processed2.markersCount, (((Double)processed2.markersCount / allProcessed) * 100).ToString());
            Console.WriteLine("{0,4}|{1,10}|{2,10}", "3", processed3.markersCount, (((Double)processed3.markersCount / allProcessed) * 100).ToString());
        }
    }
}
