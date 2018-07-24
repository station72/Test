﻿using System;
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
            //ТУТ ДРУГОЙ ДЕФИС
            var teams = rawEventTitle.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
            if (teams.Length != 2)
            {
                throw new ArgumentException("Teams length = " + teams.Length, nameof(rawEventTitle));
            }

            var result = new List<TokenizedTeam>();
            foreach (var teamName in teams)
            {
                var tokenizedTeam = TokenizeTeam(teamName);
                tokenizedTeam.RawTeamTitle = teamName;
                result.Add(tokenizedTeam);
            }

            return result;
        }

        //move to base method
        private TokenizedTeam TokenizeTeam(string teamName)
        {
            var tokenizedTeam = new TokenizedTeam();

            var underToken = GetUnderToken(teamName);
            if (underToken.Token != null)
            {
                tokenizedTeam.Tokens.Add(underToken.Token);
                teamName = underToken.RemainingTitle;
            }

            var unitedToken = GetUnitedToken(teamName);
            if (unitedToken.Token != null)
            {
                tokenizedTeam.Tokens.Add(unitedToken.Token);
                teamName = unitedToken.RemainingTitle;
            }

            tokenizedTeam.Title = teamName;

            return tokenizedTeam;
        }

        ////move to base method
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
                result.Token = TokenizerHelper.GetUnderToken(underMatchYears);
                result.RemainingTitle = regex.Replace(teamName, string.Empty).Trim();
            }
            else
            {
                result.RemainingTitle = teamName;
            }

            return result;
        }

        private TokenResult GetUnitedToken(string teamName)
        {
            var inTheMid = " Юнайтед ";
            var inTheEnd = " Юнайтед";

            if (!(teamName.Contains(inTheMid) || teamName.EndsWith(inTheEnd)))
                return new TokenResult
                {
                    Token = null,
                    RemainingTitle = teamName
                };

            var result = new TokenResult();
            result.Token = TokenizerHelper.GetUnitedToken();
            result.RemainingTitle = teamName.Replace(inTheMid, string.Empty)
                                       .Replace(inTheEnd, string.Empty)
                                       .Trim();

            return result;
        }
    }
}
