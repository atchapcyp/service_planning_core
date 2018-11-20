using System;
using System.Collections;
using System.Collections.Generic;

namespace service_plan_core
{
    public static class Program
    {
        static void Main(string[] args)
        {
            String[] str1 = { "xxxx","YYY" };
           //int[,] unserve_demand;
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

            aService = new Service("All_station",service);
            //add service to list
            outbound_services.Add(aService);
            outbound_services[0].show(); 
            aService = new Service("3_station_outbound", service2);
            outbound_services.Add(aService);
            aService = new Service("4_station_outbound_start_at_1", service4);
            outbound_services.Add(aService);
            //aService = new Service("4_station_outbound_start_at_1", service4);
            //outbound_services.Add(aService);
            //aService = new Service("3_station_outbound 0 1 0", service5);
            //outbound_services.Add(aService);
            //aService = new Service("3_station_outbound_start_at_1", service6);
            //outbound_services.Add(aService);
            //aService = new Service("3_station_outbound", service7);
            //outbound_services.Add(aService);
           


            //add demand to be time frame demand

            TF_Demand passeng_demand = new TF_Demand(720,5);
            TF_Demand outbound_demand = passeng_demand.Gen_Outbound_demand();
            TF_Demand inbound_demand = passeng_demand.Gen_Inbound_demand();
            for (int i = 0; i < passeng_demand.getTF_amount(); i++)
            {
                Console.WriteLine("This is all station demand . at : " + i);
                Service_algo.showarray(passeng_demand.demand[i]);
            }

            Service_algo.orchestrator_of_service(outbound_demand, train, outbound_services);
            Console.WriteLine("This is OUTBOUND demand . ");
            Service_algo.showarray(outbound_demand.demand[0]);
            Console.WriteLine("This is INBOUND demand . ");
            Service_algo.showarray(inbound_demand.demand[0]);
            Console.WriteLine("This is OUTBOUND 1 demand . ");
            Service_algo.showarray(outbound_demand.demand[1]);
            Console.WriteLine("This is INBOUND 1 demand . ");
            Service_algo.showarray(inbound_demand.demand[1]);

            //Service_algo.showarray(unserve_demand);
            //passeng_demand.set_unserve(unserve_demand, 0);
          // Console.WriteLine("This is unserved demand in TF demand. ");
           // Service_algo.showarray(passeng_demand.unserve_demand[0]);
            Console.WriteLine("This is LAST demand . ");
            Service_algo.showarray(passeng_demand.getDemand(0));
            Console.WriteLine("This is carry matrix . ");
            Service_algo.showarray(outbound_demand.carry_matrix);
            Console.WriteLine("Sum");
            LogWriter log = new LogWriter(str1[0]+str1[1]);
        }
    }

}
