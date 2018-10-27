using System;
namespace service_plan_core
{
    public class Service

    {
        public string service_id;
        public int[] stop_station;

        public Service(string id,int[] stop_station)
        {
            service_id = id;
            this.stop_station= stop_station ;

        }
        public void show(){
            Console.WriteLine(this.service_id);

            foreach(int i in this.stop_station)
                Console.Write(i + " ");
         
            Console.WriteLine();
        }

        public int getLength(){
            return this.stop_station.Length;
        }

       
    }
}
