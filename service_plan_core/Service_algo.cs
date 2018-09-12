using System;
using System.Collections.Generic;
using System.Text;

namespace service_plan_core
{
    class Service_algo
    {

        public const int A = 0;
        public const int B = 1;
        public const int C = 2;
        public const int D = 3;
        public const int E = 4;
        static public void make5x5(int[,] passeng_num)
        {

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    passeng_num[i, j] = GetRandomNumber(1000, 5000);

                    if (i == j)
                    {
                        passeng_num[i, j] = 0;
                    }
                }

            }


        }
        private static readonly Random getrandom = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }
        public static void showarray(int[,] passeng_num)
        {
            //show 5x5 array
            int rowLength = passeng_num.GetLength(0);
            int colLength = passeng_num.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", passeng_num[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.ReadLine();
        }

        public static void train_a_b_c_d_e(int[,] demand, Train_obj train)
        {
            int[,] actual_getoff = new int[5, 5];
            int get_off_next_station = 0;
            int i, j, k;
            for (i = 0; i < 5; i++)
            {
                int demand_at_station = 0;

                Console.WriteLine("Remainning Seat : " + train.remain_cap);
                get_off_next_station = sum_get_off(actual_getoff, i);
                Console.WriteLine("Number of getting off passenger at station " + i + " = " + get_off_next_station);
                train.remain_cap += get_off_next_station;
                Console.WriteLine("Remainning Seat after get off : " + train.remain_cap);
                get_off_next_station = 0;
                for (k = i + 1; k < 5; k++) // sum of demand at station i
                {
                    demand_at_station += demand[i, k];
                    Console.WriteLine("Demand at station " + i + " to station " + k + " is " + demand[i, k]);
                }
                Console.WriteLine("All of Demand at station " + i + " is " + demand_at_station);
                if (demand_at_station < train.remain_cap)
                {
                    train.remain_cap -= demand_at_station;
                    for (j = i + 1; j < 5; j++)
                    {
                        actual_getoff[i, j] = demand[i, j];
                        demand[i, j] = 0;
                    }
                }
                else
                {
                    double ratio = 1.0 * train.remain_cap / demand_at_station;
                    demand_at_station = 0;
                    for (j = i + 1; j < 5; j++)
                    {
                        Console.WriteLine("..............Debug train remainning seat  " + train.remain_cap);
                        Console.WriteLine("..............Debug Demand at station      " + demand_at_station);
                        Console.WriteLine("..............Debug ratio      " + ratio);
                        int fill_demand = (int)(demand[i, j] * ratio);
                        Console.WriteLine("..............Debug fill_demand  " + fill_demand);
                        actual_getoff[i, j] = fill_demand;
                        demand[i, j] -= fill_demand;
                        demand_at_station += fill_demand;
                        // if  remain_cap> 0 .....
                        // when demand is over cap
                    }
                    train.remain_cap -= demand_at_station;
                    Console.WriteLine("..............train remainning seat  " + train.remain_cap);
                }
            }


            int sum_get_off(int[,] getoff, int station)
            {
                int l;
                int sum = 0;
                for (l = 0; l < 5; l++)
                {
                    sum += getoff[l, station];
                }
                return sum;
            }
        }
    }

}
