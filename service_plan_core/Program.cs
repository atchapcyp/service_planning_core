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
            Train_obj train = new Train_obj(200);
            int[] service1 = { 1, 1, 1, 1, 1 };
            int[] service2 = { 1, 1, 1, 0, 1 };
            int[] service3 = { 1, 0, 0, 0, 1 };



            Service aService = new Service("aaa",service1);
            //add service to list
            forward.Add(aService);
            forward[0].show();
            Service bService = new Service("4_station_outbound", service2);
            forward.Add(bService);
            Service cService = new Service("2_station_outbound", service3);
            forward.Add(cService);



            //backward.Add(opservice);
            //backward.Add(opservice2);
            //backward.Add(opservice3);


            Service_algo.Make5x5(passeng);
            outbound_demand = split5x5_to(passeng, 'O');
            inbound_demand = split5x5_to(passeng, 'I');
            Console.WriteLine("This is all station demand . ");
            Service_algo.showarray(passeng);
            Console.WriteLine("------------------ ");
            Service_algo.showarray(outbound_demand);
            Service_algo.Train_a_b_c_d_e(outbound_demand, train,forward[0]);
            Console.WriteLine("This is remainning demand . ");
            Service_algo.showarray(outbound_demand);
            Console.WriteLine("------------------ ");
            Service_algo.Train_a_b_c_d_e(outbound_demand, train, forward[2]);
            Console.WriteLine("This is remainning demand . ");
            Service_algo.showarray(outbound_demand);

            Console.WriteLine("------------------ ");

            //Service_algo.showarray(inbound_demand);
            //Service_algo.Train_a_b_c_d_e(inbound_demand, train, backward[1]);
            Console.WriteLine("This is remainning demand . ");
            Service_algo.showarray(inbound_demand);
            Console.WriteLine("------------------ ");



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
