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

        public static int getDistance(int s,int d){
            int distance=0;
            if (s==d){
                return distance;
            }
            if (s<d){
                distance = arr_distance[0, d] - arr_distance[0, s];
            }else if (s>d){
                distance = arr_distance[4, d] - arr_distance[4, s];
            }
            return distance;
        }
    }
}
