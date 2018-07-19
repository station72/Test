using SelTest.Tokenizers;
using Xunit;

namespace UnitTests
{
    public class FonbetTokenizerTests
    {
        //Калуга U17 — Ротор - Волгоград U17
        //Калуга до 17 - Ротор до 17       MARATHON
        [Fact]
        public void Tokenize_Under()
        {
            var tokenizer = new FonbetTokenizer();
            var tokenizedTeams = tokenizer.Tokenize("Калуга U17 — Ротор - Волгоград U17");
            Assert.Equal(2, tokenizedTeams.Count);

            Assert.Equal("Калуга", tokenizedTeams[0].Title);
            Assert.Equal(TokenizerHelper.GetUnderDescription("17"), tokenizedTeams[0].Description);

            Assert.Equal("Ротор - Волгоград", tokenizedTeams[1].Title);
            Assert.Equal(TokenizerHelper.GetUnderDescription("17") , tokenizedTeams[1].Description);
        }
    }
}
