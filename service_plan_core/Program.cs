using System;
using System.Collections;
using System.Collections.Generic;

namespace service_plan_core
{
    public static class Program
    {
        static void Main(string[] args)
        {
         //   int[,] passeng ;
            int[,] outbound_demand = new int[5, 5];
            int[,] inbound_demand = new int[5, 5];
            List<Service> outbound_services = new List<Service>();
            List<int[]> backward = new List<int[]>();
            Train_obj train = new Train_obj(200);
            int[] service = { 1, 1, 1, 1, 1 };
            int[] service2 = { 1, 0, 1, 1, 1 };
            int[] service3 = { 1, 0, 0, 0, 1 };
            int[] service4 = { 0, 1, 1, 1, 1 };
            int[] service5 = { 1, 0, 0, 1, 1 };
            int[] service6 = { 1, 0, 1, 0, 1 };
            int[] service7 = { 0, 1, 1, 0, 1 };

            Service aService;

            aService = new Service("aaa",service);
            //add service to list
            outbound_services.Add(aService);
            outbound_services[0].show(); 
            aService = new Service("3_station_outbound", service2);
            outbound_services.Add(aService);
            aService = new Service("2_station_outbound", service3);
            outbound_services.Add(aService);
            aService = new Service("4_station_outbound_start_at_1", service4);
            outbound_services.Add(aService);
            aService = new Service("3_station_outbound 0 1 0", service5);
            outbound_services.Add(aService);
            aService = new Service("3_station_outbound_start_at_1", service6);
            outbound_services.Add(aService);
            aService = new Service("3_station_outbound", service7);
            outbound_services.Add(aService);
           


            //add demand to be time frame demand

            TF_Demand passeng_demand = new TF_Demand(720,5);
            outbound_demand = passeng_demand.getOutbound_demand(0);
            inbound_demand = passeng_demand.getInbound_demand(0);
            for (int i = 0; i < passeng_demand.getTF_amount(); i++)
            {
                Console.WriteLine("This is all station demand . at : " + i);
                Service_algo.showarray(passeng_demand.demand[i]);
            }
            Service_algo.orchestrator_of_service(outbound_demand, train, outbound_services);
    
            Console.WriteLine("This is LAST demand . ");
        }
    }

}
