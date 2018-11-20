using System;
using System.Collections.Generic;
using System.Text;

namespace service_plan_core
{
    public class Train_obj
    {
        public int cap = 150;
        public int remain_cap = 150;
        public int[] passenger = new int[20];
        public Train_obj()
        { }
        public Train_obj(int c)
        {
            cap = c;
            remain_cap = cap;
        }
        public void getOn(int amount, int i){
            this.remain_cap -= amount;
            passenger[i] += amount;
        }
        public void getOff(int i){
            this.remain_cap += passenger[i];
            passenger[i] = 0;
        }
    }
}
