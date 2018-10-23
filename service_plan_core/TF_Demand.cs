﻿using System;
using System.Collections.Generic;
namespace service_plan_core
{
    public class TF_Demand
    {
        
        public int interval;
        public int dimension;
        public List<int[,]> demand=new List<int[,]>();
        public TF_Demand() { }
        public TF_Demand(int dimension){
            int[,] subdemand = new int[dimension,dimension];
            this.dimension = dimension;
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
            this.demand.Add(subdemand);
        }

        public TF_Demand(int timeframe_interval, int dimension) // minute
        {
            this.dimension = dimension;
            this.interval = timeframe_interval;
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
                this.demand.Add(subdemand);
            }
        }

        public TF_Demand(int day,int timeframe_interval, int dimension) // minute
        {
            this.dimension = dimension;
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
                this.demand.Add(subdemand);
            }
        }

        public int getTF_amount(){
            return this.demand.Count;
        }
        public int[,] getDemand(int i){
            return this.demand[i];
        }

        public int[,] getOutbound_demand(int n){
            int[,] fullmatrix = this.demand[n];
            int[,] halfmatrix = new int[5, 5];

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (i < j)
                        {
                            halfmatrix[i, j] = fullmatrix[i, j];
                        }
                    }
                }
            return halfmatrix;   
    }
        public int[,] getInbound_demand(int n)
        {
            int[,] fullmatrix = this.demand[n];
            int[,] halfmatrix = new int[5, 5];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {

                    if (i > j)
                    {
                        int k = 4 - i;
                        int l = 4 - j;
                        halfmatrix[k, l] = fullmatrix[i, j];
                    }
                    else
                    {
                        halfmatrix[j, i] = 0;
                    }
                }
            }
            return halfmatrix;
        }

        // dont forget to add unittest
        public void commit_update_demand(int[,] latest_demand,int i){ 
            if (demand.Count > i)
            {
                demand.RemoveAt(i);
                demand.Insert(i, latest_demand);
            }else{
               // throw Exception;
            }
        }
        public void sum_demand(int i){
            // throw error when i>dimension
            int[,] new_demand = new int[dimension, dimension];
            for (int j = 0; j < dimension;j++){
                for (int k = 0; k < dimension;k++){
                     demand[i+1][j, k]+=demand[i][j,k];
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


    }
}
