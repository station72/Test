using SelTest;
using SelTest.Model;
using Xunit;

namespace UnitTests
{
    public class ForkHelperTests
    {
        //1 vs x2
        [Fact]
        public void Win1VSwin2OrDraw_No_Fork()
        {
            var se1 = new SportEvent
            {
                Win1 = 2.5f
            };

            var se2 = new SportEvent
            {
                Win2OrDraw = 1
            };

            var forks = ForkHelper.GetFork(se1, se2);
            Assert.Empty(forks);
        }

        [Fact]
        public void Win1VSwin2OrDraw_Fork()
        {
            var se1 = new SportEvent
            {
                Win1 = 3f
            };

            var se2 = new SportEvent
            {
                Win2OrDraw = 3f
            };

            var forks = ForkHelper.GetFork(se1, se2);
            Assert.Single(forks);

            var fork = forks[0];
            Assert.Same(se1, fork.SportEvent1);
            Assert.Equal(nameof(SportEvent.Win1), fork.SportEvent1Field);

            Assert.Same(se2, fork.SportEvent2);
            Assert.Equal(nameof(SportEvent.Win2OrDraw), fork.SportEvent2Field);
        }

        //1x vs 2
        [Fact]
        public void Win1OrDrawVSwin2_No_Fork()
        {
            var se1 = new SportEvent
            {
                Win1OrDraw = 2f
            };

            var se2 = new SportEvent
            {
                Win2 = 2f
            };

            var forks = ForkHelper.GetFork(se1, se2);
            Assert.Empty(forks);
        }

        [Fact]
        public void Win1OrDrawVSWin2_Fork()
        {
            var se1 = new SportEvent
            {
                Win1OrDraw = 4f
            };

            var se2 = new SportEvent
            {
                Win2 = 4f
            };

            var forks = ForkHelper.GetFork(se1, se2);
            Assert.Single(forks);

            var fork = forks[0];
            Assert.Same(se1, fork.SportEvent1);
            Assert.Equal(nameof(SportEvent.Win1OrDraw), fork.SportEvent1Field);

            Assert.Same(se2, fork.SportEvent2);
            Assert.Equal(nameof(SportEvent.Win2), fork.SportEvent2Field);
        }

        //x2 vs 1
        [Fact]
        public void Win2OrDrawVSwin1_No_Fork()
        {
            var se1 = new SportEvent
            {
                Win2OrDraw = 0.5f
            };

            var se2 = new SportEvent
            {
                Win1 = 0.5f
            };

            var forks = ForkHelper.GetFork(se1, se2);
            Assert.Empty(forks);
        }

        [Fact]
        public void Win2OrDrawVSwin1_Fork()
        {
            var se1 = new SportEvent
            {
                Win2OrDraw = 4f
            };

            var se2 = new SportEvent
            {
                Win1 = 4f
            };

            var forks = ForkHelper.GetFork(se1, se2);
            Assert.Single(forks);

            var fork = forks[0];

            Assert.Same(se1, fork.SportEvent1);
            Assert.Equal(nameof(SportEvent.Win2OrDraw), fork.SportEvent1Field);
            Assert.Same(se2, fork.SportEvent2);
            Assert.Equal(nameof(SportEvent.Win1), fork.SportEvent2Field);
        }

        //2 vs 1x
        [Fact]
        public void Win2VSwin1OrDraw_No_Fork()
        {
            var se1 = new SportEvent
            {
                Win2 = 2f
            };

            var se2 = new SportEvent
            {
                Win1OrDraw = 2f
            };

            var forks = ForkHelper.GetFork(se1, se2);
            Assert.Empty(forks);
        }

        [Fact]
        public void Win2VSwin1OrDraw_Fork()
        {
            var se1 = new SportEvent
            {
                Win2 = 4f
            };

            var se2 = new SportEvent
            {
                Win1OrDraw = 2f
            };

            var forks = ForkHelper.GetFork(se1, se2);
            Assert.Single(forks);

            var fork = forks[0];
            Assert.Same(se1, fork.SportEvent1);
            Assert.Equal(nameof(SportEvent.Win2), fork.SportEvent1Field);
            Assert.Same(se2, fork.SportEvent2);
            Assert.Equal(nameof(SportEvent.Win1OrDraw), fork.SportEvent2Field);
        }
    }
}
