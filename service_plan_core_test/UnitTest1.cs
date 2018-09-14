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

        Assert.Equal(4,Add(2,2));
        }

        [Fact]
        public void FallingTest()
        {
            Assert.Equal(5,Add(2,2));
        }

        int Add(int x,int y){
            return x+y;
        }




        
    }
}
