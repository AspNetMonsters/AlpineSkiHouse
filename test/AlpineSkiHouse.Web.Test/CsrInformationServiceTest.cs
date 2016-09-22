using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AlpineSkiHouse.Web.Tests
{
    public class SimpleTest
    {
        [Fact]
        public void SimpleFact()
        {
            var result = Math.Pow(2, 2);
            Assert.Equal(4, result);
        }

        [Theory]
        [InlineData(2, 4)]
        [InlineData(4, 8)]
        [InlineData(8, 16)]
        [InlineData(32, 64)]
        public void SimpleTheory(int amount, int expected)
        {
            var result = Math.Pow(amount, 2);            
            Assert.Equal(expected, result);
        }
        
    }
}
