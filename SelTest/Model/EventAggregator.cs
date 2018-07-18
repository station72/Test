using System.Collections.Generic;

namespace SelTest.Model
{
    class EventAggregator
    {
        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public Sport Sport { get; set; }

        public List<SportEvent> SportEvents { get; set; }

        public List<Fork> Forks { get; set; }

        public EventAggregator()
        {
            SportEvents = new List<SportEvent>();
        }
    }
}
