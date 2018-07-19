using SelTest.Tokenizers;
using Xunit;

namespace UnitTests
{
    public class MarathonTokenizerTest
    {
        [Fact]
        public void Tokenize_Under()
        {
            var tokenizer = new MarathonTokenizer();
            var tokens = tokenizer.Tokenize("Калуга до 17 - Ротор до 17");
            Assert.Equal(2, tokens.Count);

            Assert.Equal("Калуга", tokens[0].Title);
            Assert.Equal(TokenizerHelper.GetUnderDescription("17"), tokens[0].Description);

            Assert.Equal("Ротор", tokens[1].Title);
            Assert.Equal(TokenizerHelper.GetUnderDescription("17"), tokens[1].Description);
        }
    }
}
