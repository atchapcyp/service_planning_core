using System;
using Xunit;
using service_plan_core;
using System.Collections;
using System.Collections.Generic;

namespace service_plan_core_test
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {

            Assert.Equal(4, Add(2, 2));
        }
        public static int a = 10;
        [Theory]
        [InlineData(5, 5)]
        [InlineData(10, 10)]
        public void FallingTest(int x, int y)
        {
            double expected = x + y;

            Assert.Equal(expected, Add(x, y));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
        [Fact]
        public void Test_isDemandEmpty_With_Service_01111()
        {
            int[] service = { 0, 1, 1, 1, 1 };
            int[,] demand = {
            { 0, 10, 10,10, 10 },
            { 0, 0,0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0,0, 0, 0 } };

            bool actual = Service_algo.isDemandEmpty_with_service(demand,service);
            Assert.True(actual);
        }

        [Fact]
        public void Test_isDemandEmpty_With_Service_10101()
        {
            int[] service = { 1, 0, 1, 0, 1 };
            int[,] demand = {
            { 0, 10, 0, 10, 0 },
            { 0, 0, 10, 10, 10 },
            { 0, 0, 0, 10, 0 },
            { 0, 0, 0, 0, 10 },
            { 0, 0,0, 0, 0 } };

            bool actual = Service_algo.isDemandEmpty_with_service(demand,service);
            Assert.True(actual);
        }


        [Fact]
        public void Test_isDemandEmpty_Must_be_False()
        {
            int[,] demand = {
            { 0, 0, 0, 0, 0 },
            { 0, 0,0, 0, 0 },
            { 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0,0, 0, 0 } };

            bool actual = Service_algo.isDemandEmpty(demand);
            Assert.False(actual);
        }

        [Fact]
        public void Test_isDemandEmpty_Must_be_True()
        {
            int[,] demand = {
            { 0, 0, 0, 0, 0 },
            { 0, 0,0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0,0, 0, 0 } };

            bool actual = Service_algo.isDemandEmpty(demand);
            Assert.True(actual);
        }
        

        [Fact]
        public void Test_split_matrix_to_inbound()
        {
            int[,] fullmatrix = {
            { 00, 01, 02, 03, 04 },
            { 10, 11, 12, 13, 14 },
            { 20, 21, 22, 23, 24 },
            { 30, 31, 32, 33, 34 },
            { 40, 41, 42, 43, 44 } };

            int[,] actual = Program.split5x5_to(fullmatrix, 'I');
            int[,] expected = {
            { 0, 43, 42, 41, 40 },
            { 0, 0 , 32, 31, 30 },
            { 0, 0 , 0 , 21, 20 },
            { 0, 0 , 0 , 0,  10 },
            { 0, 0 , 0 , 0,  0 } };

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.Equal(expected[i, j], actual[i, j]);
                }
            }
        }

        [Fact]
        public void Test_split_matrix_to_outbound()
        {
            int[,] fullmatrix = {
            { 00, 01, 02, 03, 04 },
            { 10, 11, 12, 13, 14 },
            { 20, 21, 22, 23, 24 },
            { 30, 10, 32, 33, 34 },
            { 40, 41, 42, 43, 44 } };

            int[,] actual = Program.split5x5_to(fullmatrix, 'O');
            int[,] expected = {
             { 0, 01, 02, 03, 04 },
            { 0, 0, 12, 13, 14 },
            { 0, 0, 0, 23, 24 },
            { 0, 0, 0, 0, 34 },
            { 0, 0, 0, 0, 0 } };

            Assert.Equal(expected, actual);
        }
    

        [Fact]
        public void Train_service_3_station()
        {
            int[,] demand = {
            { 0, 10, 10, 10, 10 },
            { 10, 0, 10, 10, 10 },
            { 10, 10, 0, 10, 10 },
            { 10, 10, 10, 0, 10 },
            { 10, 10, 10, 10, 0 } };

            Train_obj train = new Train_obj(10);
            List<Service> forward = new List<Service>();
            int[] service_3_station = { 1, 0, 1, 0, 1 };
            Service testService = new Service("3_station_outbound", service_3_station);
            forward.Add(testService);

            int[,] expected =
            {
                { 0,  10,  5, 10, 5 },
                { 10, 0,  10, 10, 10 },
                { 10, 10, 0, 10, 5 },
                { 10, 10, 10, 0, 10 },
                { 10, 10, 10, 10, 0 } };
            Service_algo.Train_a_b_c_d_e(demand, train, forward[0]);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.Equal(expected[i, j], demand[i, j]);
                }
            }
            Assert.Equal(expected, demand);

        }

        [Fact]
        public void Train_service_3_station_no_overcap()
        {
            int[,] demand = {
            { 0, 10, 10, 10, 10 },
            { 10, 0, 10, 10, 10 },
            { 10, 10, 0, 10, 10 },
            { 10, 10, 10, 0, 10 },
            { 10, 10, 10, 10, 0 } };

            Train_obj train = new Train_obj(30);
            List<Service> forward = new List<Service>();
            int[] service_3_station = { 1, 0, 1, 0, 1 };
            Service testService = new Service("All_station_outbound", service_3_station);
            forward.Add(testService);

            int[,] expected =
            {
                { 0,  10,  0, 10, 0 },
                { 10, 0,  10, 10, 10 },
                { 10, 10, 0, 10, 0 },
                { 10, 10, 10, 0, 10 },
                { 10, 10, 10, 10, 0 } };
            Service_algo.Train_a_b_c_d_e(demand, train, forward[0]);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.Equal(expected[i, j], demand[i, j]);
                }
            }
            Assert.Equal(expected, demand);

        }
        [Fact]
        public void Train_service_4_station()
        {
            int[,] demand = {
            { 0, 10, 10, 10, 10 },
            { 10, 0, 10, 10, 10 },
            { 10, 10, 0, 10, 10 },
            { 10, 10, 10, 0, 10 },
            { 10, 10, 10, 10, 0 } };

            Train_obj train = new Train_obj(30);
            int[] service_4_station = { 1, 1, 1, 0, 1 };
            List<Service> forward = new List<Service>();
            Service testService = new Service("All_station_outbound", service_4_station);
            forward.Add(testService);

            int[,] expected =
            {
                { 0,  0,  0, 10, 0 },
                { 10, 0,  5, 10, 5 },
                { 10, 10, 0, 10, 0 },
                { 10, 10, 10, 0, 10 },
                { 10, 10, 10, 10, 0 } };
            Service_algo.Train_a_b_c_d_e(demand, train, forward[0]);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.Equal(expected[i, j], demand[i, j]);
                }
            }
            Assert.Equal(expected, demand);

        }

        [Fact]
        public void Train_service_4_station_no_overcap()
        {
            int[,] demand = {
            { 0, 10, 10, 10, 10 },
            { 10, 0, 10, 10, 10 },
            { 10, 10, 0, 10, 10 },
            { 10, 10, 10, 0, 10 },
            { 10, 10, 10, 10, 0 } };

            Train_obj train = new Train_obj(40);
            int[] service_4_station = { 1, 1, 1, 0, 1 };
            List<Service> forward = new List<Service>();
            Service testService = new Service("All_station_outbound", service_4_station);
            forward.Add(testService);

            int[,] expected =
            {
                { 0,  0,  0, 10, 0 },
                { 10, 0,  0, 10, 0 },
                { 10, 10, 0, 10, 0 },
                { 10, 10, 10, 0, 10 },
                { 10, 10, 10, 10, 0 } };
            Service_algo.Train_a_b_c_d_e(demand, train, forward[0]);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.Equal(expected[i, j], demand[i, j]);
                }
            }
            Assert.Equal(expected, demand);

        }


        [Fact]
        public void Train_service_all_station()
        {
            int[,] demand = {
            { 0, 10, 10, 10, 10 },
            { 10, 0, 10, 10, 10 },
            { 10, 10, 0, 10, 10 },
            { 10, 10, 10, 0, 10 },
            { 10, 10, 10, 10, 0 } };

            Train_obj train = new Train_obj(40);
            int[] service_all_station = { 1, 1, 1, 1, 1 };
            List<Service> forward = new List<Service>();
            Service testService = new Service("All_station_outbound", service_all_station);
            forward.Add(testService);

            int[,] expected =
            {
                { 0,  0,  0, 0, 0 },
                { 10, 0,  6, 7, 7 },
                { 10, 10, 0, 3, 3 },
                { 10, 10, 10, 0, 0 },
                { 10, 10, 10, 10, 0 } };
            Service_algo.Train_a_b_c_d_e(demand, train, forward[0]);
            for (int i=0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.Equal(expected[i, j], demand[i, j]);
                }
            }
            Assert.Equal(expected, demand);

        }




        
    }
}
