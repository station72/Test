using SelTest.Tokenizers.Model;

namespace SelTest.Model
{
    class RecognizedSportEvent
    {
        public TokenizedTeam Team1 { get; set; }

        public TokenizedTeam Team2 { get; set; }

        public SportEvent SportEvent { get; set; }
    }
}
