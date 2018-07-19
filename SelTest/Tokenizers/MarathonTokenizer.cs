using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SelTest.Tokenizers.Model;

namespace SelTest.Tokenizers
{
    public class MarathonTokenizer : ITokenizer
    {
        //move to base method
        public List<TokenizedTeam> Tokenize(string rawEventTitle)
        {
            if (string.IsNullOrWhiteSpace(rawEventTitle))
            {
                throw new ArgumentNullException(nameof(rawEventTitle));
            }

            var teams = rawEventTitle.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
            if (teams.Length != 2)
            {
                throw new ArgumentException("teams.Length = " + teams.Length + "; rawEventTitle = " + rawEventTitle);
            }

            var result = new List<TokenizedTeam>();
            foreach (var teamName in teams)
            {
                var tokenizedTeam = TokenizeTeam(teamName);
                result.Add(tokenizedTeam);
            }

            return result;
        }

        //move to base method
        private TokenizedTeam TokenizeTeam(string teamName)
        {
            var description = string.Empty;
            var title = teamName;

            var underToken = GetUnderToken(teamName);
            if (!string.IsNullOrWhiteSpace(underToken.Token))
            {
                description += TokenizerHelper.GetUnderDescription(underToken.Token);
                title = underToken.OtherText;
            }

            var result = new TokenizedTeam
            {
                Description = description,
                Title = title
            };

            return result;
        }

        //move to base method
        private TokenResult GetUnderToken(string teamName)
        {
            var result = new TokenResult();

            var regex = new Regex(@" до (\d{1,2})");
            var matches = regex.Matches(teamName);
            if (matches.Count > 1)
            {
                throw new ArgumentException("matches.Count = " + matches.Count + "; teamName = " + teamName);
            }
            if (matches.Count == 1)
            {
                var match = matches[0];
                //there other index
                var underMatchYears = match.Value.Substring(4);
                result.Token = underMatchYears;
                result.OtherText = regex.Replace(teamName, string.Empty).Trim();
            }
            else
            {
                result.OtherText = teamName;
            }

            return result;
        }
    }
}
