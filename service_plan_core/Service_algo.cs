using System;
using System.Collections.Generic;
using System.Text;

namespace service_plan_core
{
    public static class Service_algo
    {

        public const int A = 0;
        public const int B = 1;
        public const int C = 2;
        public const int D = 3;
        public const int E = 4;
        static public void Make5x5(int[,] passeng_num)
        {

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    passeng_num[i, j] = GetRandomNumber(10, 100);

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
                    Console.Write(string.Format("{0}\t ", passeng_num[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.ReadLine();
        }

        public static void Train_a_b_c_d_e(int[,] demand, Train_obj train,Service aService)
        {
            int[,] actual_getoff = new int[5, 5];
            int get_off_next_station = 0;
            int i, j, k;
                        for (i = 0; i < 5; i++) { 
                if (aService.stop_station[i]==0){
                    continue;
               }

                int demand_at_station = 0;

                Console.WriteLine("Remainning Seat : " + train.remain_cap);
                get_off_next_station = sum_get_off(i);
                Console.WriteLine("Number of getting off passenger at station " + i + " = " + get_off_next_station);
                train.remain_cap += get_off_next_station;
                Console.WriteLine("Remainning Seat after get off : " + train.remain_cap);
                get_off_next_station = 0;
                for (k = i + 1; k < 5; k++) // sum of demand at station i
                { if (aService.stop_station[k] == 0) { continue; }
                    demand_at_station += demand[i, k];
                    Console.WriteLine("Demand at station " + i + " to station " + k + " is " + demand[i, k]);
                }
                Console.WriteLine("All of Demand at station " + i + " is " + demand_at_station);
                if (demand_at_station < train.remain_cap)
                {
                    train.remain_cap -= demand_at_station;
                    for (j = i + 1; j < 5; j++)
                    {
                        if (aService.stop_station[j] == 0) { continue; }
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
                        if (aService.stop_station[j] == 0) { continue; }
                        Console.WriteLine("..............Debug train remainning seat  " + train.remain_cap);
                        Console.WriteLine("..............Debug Demand at station      " + demand_at_station);
                        Console.WriteLine("..............Debug ratio      " + ratio);
                        int fill_demand = (int)(demand[i, j] * ratio);
                        Console.WriteLine("..............Debug fill_demand  " + fill_demand);
                        actual_getoff[i, j] = fill_demand;
                        demand[i, j] -= fill_demand;
                        demand_at_station += fill_demand;

                    }

                    train.remain_cap -= demand_at_station;
                    Console.WriteLine("..............train remainning seat BEFORE " + train.remain_cap);
                    int round_up_count = train.remain_cap;
                    for (j = i+1; j <= round_up_count+i; j++)
                    {
                        Console.WriteLine("..............ROUND UP AT : " + i +j);
                        actual_getoff[i, j]++;
                        demand[i, j] -= 1;
                        demand_at_station++;
                        train.remain_cap--;
                    }
                      Console.WriteLine("..............train remainning seat AFTER  " + train.remain_cap);
                }
            }


            int sum_get_off(int station)
            {   if (station == 0) return 0; 
                    int l;
                    int sum = 0;
                    for (l = 0; l < 5; l++)
                    {
                        sum += actual_getoff[l, station];
                    }
                    return sum;
            }
        }

        public static Boolean isDemandEmpty(int[,] demand)
        {
            foreach (int value in demand)
            {
                if (value != 0)
                {
                    return false;
                }
            }
            return true;
        }

        static public void fixedValue_5x5(int[,] passeng_num,int num)
        {

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    passeng_num[i, j] = num;

                    if (i == j)
                    {
                        passeng_num[i, j] = 0;
                    }
                }

            }
        }

        static public int one_service_n_time(int[,] half_demand,Train_obj train, int[] service)
        {
            int counter = 0;
            List<Service> forward = new List<Service>();
            Service aService;
            aService = new Service("All_station", service);
            forward.Add(aService);
            while (!Service_algo.isDemandEmpty(half_demand))
            {
                Console.WriteLine("----- ROUND " + ++counter + " ----- ");
                Service_algo.showarray(half_demand);
                Service_algo.Train_a_b_c_d_e(half_demand, train, forward[0]);
                Console.WriteLine("This is remainning demand . ");
                Service_algo.showarray(half_demand);
                Console.WriteLine("------------------ ");
                
            }
            return counter;
        }

    }

}
