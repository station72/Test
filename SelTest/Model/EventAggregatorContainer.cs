using System;
using System.Collections.Generic;
using System.Linq;

namespace SelTest.Model
{
    class EventAggregatorContainer
    {
        private static EventAggregatorContainer _instance;

        //TODO: add lock
        internal static EventAggregatorContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventAggregatorContainer();
                }

                return _instance;
            }
        }

        internal List<EventAggregator> EventAggregators { get; }

        internal EventAggregatorContainer()
        {
            EventAggregators = new List<EventAggregator>();
        }

        internal EventAggregator SearchAggregator(string team1, string team2)
        {
            var result = EventAggregators
                .Where(u => (u.Team1 == team1 && u.Team2 == team2) || (u.Team1 == team2 && u.Team2 == team1)).ToArray();
            if (result.Length > 1)
            {
                throw new Exception("length = " + result.Length);
            }
            return result.FirstOrDefault();
        }

        internal void AddEvent(string team1, string team2, SportEvent sportEvent)
        {
            var aggregator = SearchAggregator(team1, team2);
            if (aggregator != null)
            {
                var existsEvent = aggregator.SportEvents
                    .RemoveAll(u => u.Id == sportEvent.Id && u.BookMaker == sportEvent.BookMaker);

                aggregator.SportEvents.Add(sportEvent);

                return;
            }

            var newEventAggregator = new EventAggregator
            {
                Sport = Sport.Soccer,
                Team1 = team1,
                Team2 = team2
            };

            newEventAggregator.SportEvents.Add(sportEvent);
            EventAggregators.Add(newEventAggregator);
        }
    }
}
