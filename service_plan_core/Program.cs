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
            List<Service> forward = new List<Service>();
            List<int[]> backward = new List<int[]>();
            List<int[,]> demand_timeframe = new List<int[,]>();
            Train_obj train = new Train_obj(200);
            int[] service = { 1, 1, 1, 1, 1 };
            int[] service2 = { 1, 0, 1, 0, 1 };
            int[] service3 = { 1, 0, 0, 0, 1 };

            Service aService;
            //int[, ] temp_demand;
           
           /* while (true)
            {
                if (!remain()) break;
                     service = createService(outbound_demand);
                aService = new Service("aaa", service);
                Service_algo.Train_a_b_c_d_e(outbound_demand, train, forward[0])
            }
            service = createService(outbound_demand);*/
            aService = new Service("aaa",service);
            //add service to list
            forward.Add(aService);
            forward[0].show(); 
            aService = new Service("3_station_outbound", service2);
            forward.Add(aService);
            aService = new Service("2_station_outbound", service3);
            forward.Add(aService);

            //add demand to be time frame demand

            TF_Demand passeng_demand = new TF_Demand(120,5);
            outbound_demand = split5x5_to(passeng_demand.demand[0], 'O');
            inbound_demand = split5x5_to(passeng_demand.demand[0], 'I');
            for (int i = 0; i < passeng_demand.demand.Count; i++)
            {

                Console.WriteLine("This is all station demand . at : " + i);
                Service_algo.showarray(passeng_demand.demand[i]);
            }
            Service_algo.one_service_n_time(outbound_demand, train, service);
      
            Console.WriteLine("This is LAST demand . ");


            //unused
            //ArrayList myArryList = new ArrayList();
            //myArryList.Add(service)
            //int[,] service1 =new int[2,5] { { 1, 1, 1, 1, 1 }, { 1, 0, 1, 0, 1 } };

        }


        public static int[,] split5x5_to(int[,] fullmatrix, char direction)
        {
            int[,] halfmatrix=new int[5,5] ;

            if (direction=='O'){

                for (int i = 0; i<5;i++){
                    for (int j = 0; j < 5;j++){
                        if (i<j){
                            halfmatrix[i,j] = fullmatrix[i,j];
                        }
                    }
                }
            }
            if (direction=='I'){
                for (int i = 0; i < 5;i++){
                    for (int j = 0; j < 5;j++){

                        if (i > j)
                        {
                            int k = 4 - i;
                            int l = 4 - j;
                            halfmatrix[k, l] = fullmatrix[i, j];
                        }
                        else{
                            halfmatrix[j, i] = 0;
                        }
                    }
                }
            }

            return halfmatrix;
        }
    }

}
