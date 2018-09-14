using System;
using System.Collections;
using System.Collections.Generic;

namespace service_plan_core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initiate Service Planning .net core");
            Console.WriteLine("Test 1st editing and commit");
            Console.WriteLine("Test 2nd editting and commit on macbook ");

            int[,] passeng = new int[5, 5];
            List<int[]> myList = new List<int[]>();
            Train_obj train = new Train_obj(5000);
            int[] service = { 1, 1, 1, 1, 1 };
            int[] service2 = { 1, 0, 1, 0, 1 };
        
            myList.Add(service);
            myList.Add(service2);

            Service_algo.Make5x5(passeng);
            Service_algo.showarray(passeng);
            Console.WriteLine("------------------ ");
            Service_algo.Train_a_b_c_d_e(passeng, train,myList[0]); 
            Service_algo.showarray(passeng);
            Service_algo.Train_a_b_c_d_e(passeng, train,myList[1]);
            Console.WriteLine("------------------ ");
            Service_algo.train_x_y(passeng, train, 0, 3);
            Service_algo.showarray(passeng);


            //unused
            //ArrayList myArryList = new ArrayList();
            //myArryList.Add(service)
            //int[,] service1 =new int[2,5] { { 1, 1, 1, 1, 1 }, { 1, 0, 1, 0, 1 } };

        }
    }
}
