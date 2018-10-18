using System;
using System.Collections.Generic;
using System.Text;

namespace service_plan_core
{
    public class Train_obj
    {
        private int cap = 150;
        public int remain_cap = 150;
        public Train_obj()
        { }
        public Train_obj(int c)
        {
            cap = c;
            remain_cap = cap;
        }
    }
}
