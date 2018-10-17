using System;
using System.Collections.Generic;
namespace service_plan_core
{
    public class TF_Demand
    {
        
        public int interval;
        public List<int[,]> demand=new List<int[,]>();
        public TF_Demand(int dimension){
            int[,] subdemand = new int[dimension,dimension]; 
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    subdemand[i, j] = GetRandomNumber(10, 100);

                    if (i == j)
                    {
                        subdemand[i, j] = 0;
                    }
                }

            }
            demand.Add(subdemand);
        }

        public TF_Demand(int timeframe_interval, int dimension) // minute
        {
            int m = 24 * 60 / timeframe_interval;
            for (int k = 0; k < m; k++)
            {
                int[,] subdemand = new int[dimension, dimension];
                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        subdemand[i, j] = GetRandomNumber(100, 100);

                        if (i == j)
                        {
                            subdemand[i, j] = 0;
                        }
                    }

                }
                demand.Add(subdemand);
            }
        }

        public TF_Demand(int day,int timeframe_interval, int dimension) // minute
        {
            int m = 24 * 60 *day/ timeframe_interval;
            for (int k = 0; k < m; k++)
            {
                int[,] subdemand = new int[dimension, dimension];
                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        subdemand[i, j] = GetRandomNumber(10, 100);

                        if (i == j)
                        {
                            subdemand[i, j] = 0;
                        }
                    }

                }
                demand.Add(subdemand);
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


    }
}
