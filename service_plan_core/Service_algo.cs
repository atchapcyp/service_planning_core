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

        public static void Train_a_b_c_d_e(TF_Demand demands, Train_obj train, Service aService, int timeframe)
        {
            int[,] actual_getoff = new int[5, 5];
            int get_off_next_station = 0;
            int i, j, k, next_station_index = -1;
            for (i = 0; i < 5; i++)
            {
                if (aService.stop_station[i] == 0)
                {
                    continue;
                }
                for (int a = 4; a > i; a--)
                {
                    if (aService.stop_station[a] == 1)
                    {
                        next_station_index = a;
                    }
                }

                int demand_at_station = 0; 
                Console.WriteLine("Remainning Seat : " + train.remain_cap);
                get_off_next_station = train.passenger[i];
                Console.WriteLine("Number of getting off passenger at station " + i + " = " + get_off_next_station);
                train.getOff(i);
                Console.WriteLine("Remainning Seat after get off : " + train.remain_cap);
                for (k = i + 1; k < 5; k++) // sum of demand at station i
                {
                    if (aService.stop_station[k] == 0) { continue; }
                    demand_at_station += demands.cal_demand[i, k];
                    Console.WriteLine("Demand at station " + i + " to station " + k + " is " + demands.cal_demand[i, k]);
                }
                Console.WriteLine("All of Demand at station " + i + " is " + demand_at_station);
                if (demand_at_station < train.remain_cap)
                {
                    for (j = i + 1; j < 5; j++)
                    {
                        if (aService.stop_station[j] == 0) { continue; }
                        train.getOn(demands.cal_demand[i, j], j);

                        demands.cal_demand[i, j] = 0;
                        clear_remain_demand(demands, timeframe, i, j);
                    }
                }
                else
                {
                    double ratio = 1.0 * train.remain_cap / demand_at_station;
                    demand_at_station = 0;
                    int fill_demand;
                    for (j = i + 1; j < 5; j++)
                    {
                        if (aService.stop_station[j] == 0) { continue; }

                        fill_demand = (int)(demands.cal_demand[i, j] * ratio);


                        demand_at_station += fill_demand;

                    }
                    int remain_cap = train.remain_cap;
                    remain_cap -= demand_at_station;
                    for (j = i + 1; j < 5; j++)
                    {
                        if (aService.stop_station[j] == 0) { continue; }
                        Console.WriteLine("..............Debug train remainning seat  " + train.remain_cap);
                        Console.WriteLine("..............Debug Demand at station      " + demands.cal_demand[i,j]);
                        Console.WriteLine("..............Debug ratio      " + ratio);
                        fill_demand = (int)(demands.cal_demand[i, j] * ratio);

                        Console.WriteLine("..............Debug fill_demand  " + fill_demand);
                        actual_getoff[i, j] = fill_demand;
                        if (remain_cap > 0 && demands.cal_demand[i, j] > fill_demand)
                        {
                            demands.cal_demand[i, j] -= (fill_demand + 1);
                            train.getOn(fill_demand + 1, j);
                            update_remain_demand(demands, timeframe, fill_demand + 1, i, j);
                            remain_cap -= 1;
                        }
                        else
                        {
                            demands.cal_demand[i, j] -= fill_demand;
                            train.getOn(fill_demand, j);
                            update_remain_demand(demands, timeframe, fill_demand, i, j);
                        }
                        demand_at_station += fill_demand;

                    }
                    Console.WriteLine("..............train remainning seat AFTER  " + train.remain_cap);
                }

            }
        }

        public static void update_remain_demand(TF_Demand demands,int timeframe,int fill_demand,int i,int j){
            if( demands.carry_matrix[i,j]==-1){
                demands.demand[timeframe][i, j] -= fill_demand;
            }
            else {
                for (int k = demands.carry_matrix[i, j]; k <= timeframe;k++){
                    if (demands.demand[k][i, j] < fill_demand) {
                        fill_demand -= demands.demand[k][i, j];
                        demands.demand[k][i, j] = 0;
                    }
                    else{
                        demands.demand[k][i, j] -= fill_demand;
                        return;
                    }
                }
            }
        }
        public static void clear_remain_demand(TF_Demand demands, int timeframe, int i, int j)
        {
            if (demands.carry_matrix[i, j] == -1)
            {
                demands.demand[timeframe][i, j] = 0;
            }
            else
            {
                for (int k = demands.carry_matrix[i, j]; k <= timeframe; k++)
                {
                        demands.demand[k][i, j] = 0;
                 
                }
            }
        }

        public static Boolean isDemandEmpty_with_service(int[,] demand,Service services)
        {
            for (int i = 0; i < 5; i++)
            {
                if (services.stop_station[i] == 0)
                    continue;
                else
                {
                    for (int j = i + 1; j < 5; j++)
                    {   if (services.stop_station[j] == 0)
                            continue;
                        if (demand[i, j] != 0)
                            return false;
                    }
                }
            }
            return true;
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
        static public void all_service_n_time(int[,] outbound_demand,Train_obj train,List<Service> services){
            int indexOfMaxUtilize;
            float most_utilize=0;
            while (!isDemandEmpty(outbound_demand)){
                for (int i = 0; i < services.Count;i++){
                    float service_util = Utilize_service(outbound_demand, train, services[i]);
                    float max_util = max_utilize_of_service(train.cap, services[i]);
                    float util_percent = service_util / max_util * 100;
                    if (util_percent > most_utilize){
                        indexOfMaxUtilize = i;
                    }
                }

            }
        }

        static public (int,float) index_of_most_utilize_service(int[,] outbound_demand,Train_obj train,List<Service> services)
        {
            int counter = 0;
            int index_of_max_util=0;
            float util_percent=0;
            for (int i = 0; i < services.Count; i++)
            {
                services[i].show();
                    Console.WriteLine("----- ROUND " + ++counter + " ----- ");
                    showarray(outbound_demand);
                    float service_util = Utilize_service(outbound_demand, train, services[i]);
                    float max_util = max_utilize_of_service(train.cap, services[i]);
                    float new_util_percent = service_util / max_util * 100;
                    Console.WriteLine("This service utilize : " + service_util);
                    Console.WriteLine("MAX service utilize : " + max_util);
                    Console.WriteLine("Percent service utilize : " + new_util_percent);
                if (Math.Abs(100-new_util_percent)<=float.Epsilon) {//float compare
                    Console.WriteLine("FLOAT CHECK EQUAL 100");
                    return (i,100); 
                }
                if(new_util_percent>util_percent){
                    util_percent = new_util_percent;
                    index_of_max_util = i;
                }
            }

            return (index_of_max_util,util_percent);
        }

        static public void actual_run(TF_Demand demands,Train_obj train,Service service,int timeframe){
            Console.WriteLine("ACTUAL_RUN . ");
            Console.WriteLine(service.service_id);
            Train_a_b_c_d_e(demands, train, service,timeframe);
            Console.WriteLine("This is remainning demand . ");
            showarray(demands.cal_demand);
            Console.WriteLine("This is CURRENT demand . ");
            showarray(demands.demand[timeframe]);
            Console.WriteLine("------------------ ");
        }

        static public void orchestrator_of_service(TF_Demand outbound_demand,Train_obj train,List<Service> services){
            for (int i = 0; i < outbound_demand.demand.Count; i++)
            {
                outbound_demand.get_demand(i);
                while (!isDemandEmpty(outbound_demand.demand[i]))
                {

                    float p;
                    int s = 0;
                    (s, p) = index_of_most_utilize_service(outbound_demand.cal_demand, train, services);
                    Console.WriteLine("\n\nthis is CARRY_matrix \n\n\n");
                    showarray(outbound_demand.carry_matrix);
                    Console.WriteLine("this is PERCENT " + p);
                    if (p <= 60)
                    {
                        for (int out_loop = 0; out_loop < outbound_demand.dimension;out_loop++){
                            for (int in_loop = out_loop+1 ; in_loop <outbound_demand.dimension;in_loop++){
                                if (outbound_demand.demand[i][out_loop,in_loop] > 0)
                                {
                                    outbound_demand.carry_matrix[out_loop, in_loop] = i;
                                }
                            }
                        }
                        break;
                    }
                    Console.WriteLine("----IN ACTUAL ---- " +"\n\n");
                    actual_run(outbound_demand, train, services[s],i);
                    Console.WriteLine("----OUT ACTUAL ---- " + "\n\n");
                }
                Console.WriteLine("----END--OF--orchestrate----LOOP--- "+i+"\n\n\n\n\n");
            }
            Console.WriteLine("----END--OF--orchestrate-------- ");
        }

        static public void test_orchestrator_of_service(TF_Demand outbound_demand, Train_obj train, List<Service> services)
        {
            for (int i = 0; i < outbound_demand.demand.Count; i++)
            {
                outbound_demand.get_demand(i);
                while (!isDemandEmpty(outbound_demand.demand[i]))
                {

                    float p;
                    int s = 0;
                    (s, p) = index_of_most_utilize_service(outbound_demand.cal_demand, train, services);
                    Console.WriteLine("\n\nthis is CARRY_matrix \n\n\n");
                    showarray(outbound_demand.carry_matrix);
                    Console.WriteLine("this is PERCENT " + p);
                    if (p <= 60)
                    {
                        for (int out_loop = 0; out_loop < outbound_demand.dimension; out_loop++)
                        {
                            for (int in_loop = out_loop + 1; in_loop < outbound_demand.dimension; in_loop++)
                            {
                                if (outbound_demand.demand[i][out_loop, in_loop] > 0)
                                {
                                    outbound_demand.carry_matrix[out_loop, in_loop] = i;
                                }
                            }
                        }
                        break;
                    }
                    Console.WriteLine("----IN ACTUAL ---- " + "\n\n");
                    actual_run(outbound_demand, train, services[s], i);
                    Console.WriteLine("----OUT ACTUAL ---- " + "\n\n");
                    break;
                }
                Console.WriteLine("----END--OF--orchestrate----LOOP--- " + i + "\n\n\n\n\n");
            }
            Console.WriteLine("----END--OF--orchestrate-------- ");
        }


        public static float cal_utilize_percent(int[,] demand,Train_obj train,Service service){
            float a = Utilize_service(demand, train, service);
            float b = max_utilize_of_service(train.cap, service);
            return a / b * 100;
        }

        public static int cal_all_service_util(int[,] demand,Train_obj train,List<Service> services){
            int most_util_index=-1;
            float most_percent=-1;
            for (int i = 0; i < services.Count;i++){
                float temp = cal_utilize_percent(demand, train, services[i]);
                if (temp > most_percent) {
                    most_percent = temp;
                    most_util_index = i;
                }
            }
            return most_util_index;
        }

        public static float max_utilize_of_service(int train_cap,Service service){

            int source=0,destination=0;
            for (int i = 0; i < service.getLength();i++){
                if(service.stop_station[i]==1){
                    source = i;
                    break;
                }
            }
            for (int i = service.getLength()-1; i > 0;i--){
                if(service.stop_station[i]==1){
                    destination = i;
                    break;
                }
            }
            return train_cap*Station.getDistance(source,destination);
        }

        //Cal_remain_seat returns utilization (sum of passenger*distance)
        public static float Utilize_service(int[,] demand, Train_obj train, Service service)
        {
            int[,] actual_getoff = new int[5, 5];
            int get_off_next_station = 0;
            int i, j, k,next_station_index=0;
            float train_util=0;
            int[,] cal_demand = (int[,])demand.Clone();
            for (i = 0; i < 5; i++)
            {
                if (service.stop_station[i] == 0)
                {   
                    continue;
                }
                for (int a = 4; a > i;a--){
                    if (service.stop_station[a]==1){
                        next_station_index = a;
                    }
                }

                int demand_at_station = 0;
                get_off_next_station = sum_get_off(i);



                train.remain_cap += get_off_next_station;

                get_off_next_station = 0;
                for (k = i + 1; k < 5; k++) // sum of demand at station i
                {
                    if (service.stop_station[k] == 0) { continue; }
                    demand_at_station += cal_demand[i, k];

                }

                if (demand_at_station < train.remain_cap)
                {   
                    train.remain_cap -= demand_at_station;
                    for (j = i + 1; j < 5; j++)
                    {
                        if (service.stop_station[j] == 0) { continue; }
                        actual_getoff[i, j] = cal_demand[i, j];
                        cal_demand[i, j] = 0;
                    }
                }
                else
                {

                    double ratio = 1.0 * train.remain_cap / demand_at_station;
                    demand_at_station = 0;
                    for (j = i + 1; j < 5; j++)
                    {
                        if (service.stop_station[j] == 0) { continue; }

                        int fill_demand = (int)(cal_demand[i, j] * ratio);
                    
                        actual_getoff[i, j] = fill_demand;
                        cal_demand[i, j] -= fill_demand;
                        demand_at_station += fill_demand;

                    }

                    train.remain_cap -= demand_at_station;

                    int round_up_count = train.remain_cap;
                    for (j = next_station_index ; j < round_up_count+next_station_index; j++)
                    {
                        actual_getoff[i, j]+=1;
                        cal_demand[i, j] -= 1;
                        demand_at_station++;
                        train.remain_cap--;
                    }

                }
                Console.WriteLine("CALCULATE UTIL--- Traincap : "+train.cap );
                Console.WriteLine("CALCULATE UTIL--- Remaincap : " + train.remain_cap);
                Console.WriteLine("CALCULATE UTIL--- StationDistance : " + Station.arr_distance[i, next_station_index]);
                train_util += (train.cap - train.remain_cap) * Station.arr_distance[i, next_station_index];
                Console.WriteLine("Train_util : " + train_util);

            }

            int sum_get_off(int station)
            {
                if (station == 0) { return 0; }
                int l;
                int sum = 0;
                for (l = 0; l < 5; l++)
                {
                    Console.WriteLine("in_sum get off BEFORE : " + sum + " station : " + station + " l : " +l);
                    sum += actual_getoff[l, station];
                   
                }
                return sum;
            }
           
            return train_util;
        }
    }
}
