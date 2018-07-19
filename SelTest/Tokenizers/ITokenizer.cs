using SelTest.Tokenizers.Model;
using System.Collections.Generic;

namespace SelTest.Tokenizers
{
    internal interface ITokenizer
    {
        List<TokenizedTeam> Tokenize(string rawEventTitle);
    }
}
