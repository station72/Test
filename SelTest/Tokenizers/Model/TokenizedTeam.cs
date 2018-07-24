using System.Collections.Generic;

namespace SelTest.Tokenizers.Model
{
    public class TokenizedTeam
    {
        public string Title { get; set; }

        public string RawTeamTitle { get; set; }

        public HashSet<string> Tokens { get; set; } = new HashSet<string>();
    }
}
