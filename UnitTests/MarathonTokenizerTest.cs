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
            var teams = tokenizer.Tokenize("Селангор Юнайтед до 21 - Селангор-Волгоград до 19");
            Assert.Equal(2, teams.Count);

            Assert.Equal("Селангор", teams[0].Title);
            Assert.Equal(2, teams[0].Tokens.Count);
            Assert.Contains(TokenizerHelper.GetUnderToken("21"), teams[0].Tokens);
            Assert.Contains(TokenizerHelper.GetUnitedToken(), teams[0].Tokens);

            Assert.Equal("Селангор-Волгоград", teams[1].Title);
            Assert.Single(teams[1].Tokens);
            Assert.Contains(TokenizerHelper.GetUnderToken("19"), teams[1].Tokens);
        }
    }
}
