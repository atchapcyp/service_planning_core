using System;

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
            Train_obj train = new Train_obj(300);        
            Service_algo.make5x5(passeng);
            Service_algo.showarray(passeng);
            Console.WriteLine("------------------ ");
            Service_algo.train_a_b_c_d_e(passeng, train);
            Service_algo.showarray(passeng);
            Console.WriteLine("------------------ ");
            Service_algo.train_a_b_c_d_e(passeng, train);
            Service_algo.showarray(passeng);
            Console.WriteLine("------------------ ");
            Service_algo.train_a_b_c_d_e(passeng, train);
            Service_algo.showarray(passeng);
            Console.WriteLine("------------------ ");
            Service_algo.train_a_b_c_d_e(passeng, train);
            Service_algo.showarray(passeng);
        }
    }
}
