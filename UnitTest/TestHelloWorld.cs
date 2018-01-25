using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using HelloWorld;
namespace UnitTest
{
    public class TestHelloWorld
    {
        Hello hello = new Hello();
        [Fact]
        public void TestIfReturnsHelloWorld()
        {
            Assert.Equal("Hello World", hello.World());
        }

    }
}
