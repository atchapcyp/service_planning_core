using System;
namespace service_plan_core
{
    public class Station
    {
        public static int[,] arr_distance = {
                { 0, 30, 60, 85, 110},
                {30,0,30,55,80},
                {60,30,0,25,50},
                {85,55,25,0,25},
                {110,80,50,25,0}};

        public Station()
        {

        }
    }
}
