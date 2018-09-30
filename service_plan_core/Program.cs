using System;
using System.Collections;
using System.Collections.Generic;

namespace service_plan_core
{
    public static class Program
    {
        static void Main(string[] args)
        {
            int[,] passeng = new int[5, 5];
            int[,] outbound_demand = new int[5, 5];
            int[,] inbound_demand = new int[5, 5];
            List<Service> forward = new List<Service>();
            List<int[]> backward = new List<int[]>();
            Train_obj train = new Train_obj(20);
            int[] service = { 1, 1, 1, 1, 1 };
            int[] service2 = { 1, 0, 1, 0, 1 };
            int[] service3 = { 1, 0, 0, 0, 1 };



            Service aService;
           
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



            //backward.Add(opservice);
            //backward.Add(opservice2);
            //backward.Add(opservice3);


            Service_algo.fixedValue_5x5(passeng,10);
            outbound_demand = split5x5_to(passeng, 'O');
            inbound_demand = split5x5_to(passeng, 'I');
            Console.WriteLine("This is all station demand . ");
            Service_algo.showarray(passeng);

            /* while (!Service_algo.isDemandEmpty(outbound_demand))
             {   
                 Console.WriteLine("-----ROUND"+counter+"----- ");
                 Service_algo.showarray(outbound_demand);
                 Service_algo.Train_a_b_c_d_e(outbound_demand, train, forward[0]);
                 Console.WriteLine("This is remainning demand . ");
                 Service_algo.showarray(outbound_demand);
                 Console.WriteLine("------------------ ");
                 counter++;
             } */
            Service_algo.one_service_n_time(outbound_demand, train, service2);

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
