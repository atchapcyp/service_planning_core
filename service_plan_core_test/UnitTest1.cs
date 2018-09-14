using System;
using Xunit;
using service_plan_core;

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

        int Add(int x, int y) {
            return x + y;
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
            int[] service = { 1, 1, 1, 0, 1 };

            int[,] expected =
            {
                { 0,  0,  0, 10, 0 },
                { 10, 0,  0, 10, 0 },
                { 10, 10, 0, 10, 0 },
                { 10, 10, 10, 0, 10 },
                { 10, 10, 10, 10, 0 } };
            Service_algo.Train_a_b_c_d_e(demand, train, service);
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
            int[] service = { 1, 1, 1, 1, 1 };

            int[,] expected =
            {
                { 0,  0,  0, 0, 0 },
                { 10, 0,  7, 7, 7 },
                { 10, 10, 0, 3, 3 },
                { 10, 10, 10, 0, 0 },
                { 10, 10, 10, 10, 0 } };
            Service_algo.Train_a_b_c_d_e(demand, train, service);
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
