using System;
using System.Collections;
using System.Collections.Generic;

namespace service_plan_core
{
    class Program
    {
        static void Main(string[] args)
        {
            //enum direction {a,b};
            Console.WriteLine("Initiate Service Planning .net core");
            Console.WriteLine("Test 1st editing and commit");
            Console.WriteLine("Test 2nd editting and commit on macbook ");

            int[,] passeng = new int[5, 5];
            List<int[]> forward = new List<int[]>();
            List<int[]> backward = new List<int[]>();
            Train_obj train = new Train_obj(7);
            int[] service = { 1, 1, 1, 1, 1 };
            int[] service2 = { 1, 1, 1, 0, 1 };
            int[] service3 = { 1, 0, 0, 0, 1 };

            int[] opservice = { 1, 1, 1, 1, 1 };
            int[] opservice2 = { 1, 1, 1, 0, 1 };
            int[] opservice3 = { 1, 0, 0, 0, 1 };
 
            //add service to list
            forward.Add(service);
            forward.Add(service2);
            forward.Add(service3);
            backward.Add(opservice);
            backward.Add(opservice2);
            backward.Add(opservice3);


            Service_algo.Make5x5(passeng);
            Console.WriteLine("This is all station demand . ");
            Service_algo.showarray(passeng);

            Console.WriteLine("------------------ ");
            Service_algo.Train_a_b_c_d_e(passeng, train,forward[1]); 
            Service_algo.showarray(passeng);
            Console.WriteLine("------------------ ");
            Service_algo.Train_a_b_c_d_e(passeng, train, forward[2]);
            //Service_algo.train_x_y(passeng, train, 0, 3);
            Service_algo.showarray(passeng);





            


            //unused
            //ArrayList myArryList = new ArrayList();
            //myArryList.Add(service)
            //int[,] service1 =new int[2,5] { { 1, 1, 1, 1, 1 }, { 1, 0, 1, 0, 1 } };

        }


        public static int[,] split5x5(int[,] fullmatrix, char direction)
        { int[,] a= {{ 0, 10, 10, 10, 10 },
            { 10, 0, 10, 10, 10 },
            { 10, 10, 0, 10, 10 },
            { 10, 10, 10, 0, 10 },
            { 10, 10, 10, 10, 0 }};

           // if direction


            return a;
        }
    }

}
