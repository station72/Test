using System.Collections.Generic;

namespace SelTest.Model
{
    class EventAggregator
    {
        public string Id { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public Sport Sport { get; set; }

        public List<RecognizedSportEvent> RecognizedSportEvents { get; set; }

        public List<Fork> Forks { get; set; }

        public EventAggregator(string id)
        {
            Id = id;
            RecognizedSportEvents = new List<RecognizedSportEvent>();
        }
    }
}
