using SelTest;
using SelTest.Model;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class ForkHelperTests
    {
        [Fact]
        public void ForksCount()
        {
            var se1 = new SportEvent
            {

            };

            var se2 = new SportEvent
            {

            };

            var forks = ForkHelper.GetFork(se1, se2);
            Assert.Equal(4, forks.Count);

        }
    }
}
