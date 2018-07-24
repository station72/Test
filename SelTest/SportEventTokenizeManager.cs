using SelTest.Model;
using SelTest.Tokenizers;
using System;

namespace SelTest
{
    internal class SportEventTokenizeManager
    {
        public RecognizedSportEvent Tokenize(SportEvent sportEvent)
        {
            var tokenizer = GetTokenizer(sportEvent.Bookmaker);
            var teams = tokenizer.Tokenize(sportEvent.TitleOrigin);
            return new RecognizedSportEvent
            {
                Team1 = teams[0],
                Team2 = teams[1],
                SportEvent = sportEvent
            };
        }

        private ITokenizer GetTokenizer(Bookmaker bookmaker)
        {
            switch (bookmaker)
            {
                case Bookmaker.Fonbet:
                    return new FonbetTokenizer();
                case Bookmaker.Marathon:
                    return new MarathonTokenizer();
                default:
                    throw new NotImplementedException("bookmaker is not implemented " + bookmaker.ToString());
            }
        }
    }
}
