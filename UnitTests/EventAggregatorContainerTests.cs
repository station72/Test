using SelTest;
using SelTest.Model;
using Xunit;

namespace UnitTests
{
    public class EventAggregatorContainerTests
    {
        [Fact]
        public void Test()
        {
            var container = EventAggregatorContainer.Instance;
            var sportEvent1 = new SportEvent
            {
                Bookmaker = Bookmaker.Fonbet,
                TitleOrigin = "Калуга U17 — Ротор-Волгоград U17",
                Id = "123123"
            };

            var sportEvent2 = new SportEvent
            {
                Bookmaker = Bookmaker.Marathon,
                TitleOrigin = "Калуга до 17 - Ротор до 17",
                Id = "444555"
            };

            container.AddEvents(new SportEvent[] { sportEvent1 });
            container.AddEvents(new SportEvent[] { sportEvent2 });

            var aggregator = container.GetEventAggregator(EventNameHelper.GetEventId(sportEvent1));
            Assert.NotNull(aggregator);

            Assert.Equal("Калуга", aggregator.Team1);
            Assert.Equal("Ротор-Волгоград", aggregator.Team2);
        }
    }
}
