using System.Collections.Generic;
using System.Linq;

namespace SelTest.Model
{
    class EventAggregatorContainer
    {
        private static EventAggregatorContainer _instance;
        private static object _lock = new object();

        internal static EventAggregatorContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new EventAggregatorContainer();
                        }
                    }
                }
                return _instance;
            }
        }

        private List<EventAggregator> _EventAggregators { get; }

        public EventAggregator GetEventAggregator(string id)
        {
            return _EventAggregators.FirstOrDefault(u => u.Id == id);
        }

        private EventAggregatorContainer()
        {
            _EventAggregators = new List<EventAggregator>();
        }

        private Dictionary<Bookmaker, List<RecognizedSportEvent>> _bookmakerEvents
            = new Dictionary<Bookmaker, List<RecognizedSportEvent>>
            {
                {
                    Bookmaker.Fonbet, new List<RecognizedSportEvent>()
                },
                {
                    Bookmaker.Marathon, new List<RecognizedSportEvent>()
                }
            };

        private void AddEvent(RecognizedSportEvent recSportEvent)
        {
            var list = _bookmakerEvents[recSportEvent.SportEvent.Bookmaker];
            var oldVersion = list.FirstOrDefault(u => u.SportEvent.Id == recSportEvent.SportEvent.Id);
            if (oldVersion != null)
            {
                list.Remove(oldVersion);
            }

            list.Add(recSportEvent);
        }

        public void AddEvents(IEnumerable<SportEvent> events)
        {
            foreach (var sportEvent in events)
            {
                var recEvent = new SportEventTokenizeManager().Tokenize(sportEvent);
                AddEvent(recEvent);
            }

            Match();
        }

        private void Match()
        {
            var aCol = _bookmakerEvents[Bookmaker.Fonbet];
            var bCol = _bookmakerEvents[Bookmaker.Marathon];

            if (aCol.Count == 0 || bCol.Count == 0)
            {
                return;
            }

            foreach (var a in aCol)
            {
                var match = new SportEventMatch();
                foreach (var b in bCol)
                {
                    if (!EventNameHelper.HaveSameTokens(a.Team1.Tokens, b.Team1.Tokens) 
                     || !EventNameHelper.HaveSameTokens(a.Team2.Tokens, b.Team2.Tokens))
                    {
                        continue;
                    }

                    var distanceTeam1 = EventNameHelper.Levenshtein(a.Team1.Title, b.Team1.Title);
                    var distanceTeam2 = EventNameHelper.Levenshtein(a.Team2.Title, b.Team2.Title);

                    if (distanceTeam1 == 0 || distanceTeam2 == 0)
                    {
                        match.RecognizedSportEvent = b;
                        match.DistanceOfTeam1 = distanceTeam1;
                        match.DistanceOfTeam2 = distanceTeam2;
                        break;
                    }
                }

                if (match.RecognizedSportEvent == null)
                {
                    continue;
                }

                AddToEventAggregator(a, match.RecognizedSportEvent);
            }
        }

        void AddToEventAggregator(RecognizedSportEvent recSportEvent1, RecognizedSportEvent recSportEvent2)
        {
            var id = EventNameHelper.GetEventId(recSportEvent1.SportEvent);
            var aggregator = _EventAggregators.FirstOrDefault(u => u.Id == id);
            if (aggregator != null)
            {
                aggregator.RecognizedSportEvents.RemoveAll(
                    u => u.SportEvent.Bookmaker == recSportEvent1.SportEvent.Bookmaker
                      || u.SportEvent.Bookmaker == recSportEvent2.SportEvent.Bookmaker);

                aggregator.RecognizedSportEvents.Add(recSportEvent1);
                aggregator.RecognizedSportEvents.Add(recSportEvent2);

                return;
            }

            var newAggregator = new EventAggregator(id)
            {
                Sport = Sport.Soccer,
                RecognizedSportEvents = new List<RecognizedSportEvent>
                {
                    recSportEvent1,
                    recSportEvent2
                },
                Team1 = recSportEvent1.Team1.Title,
                Team2 = recSportEvent1.Team2.Title,
            };

            _EventAggregators.Add(newAggregator);
        }
    }
}
