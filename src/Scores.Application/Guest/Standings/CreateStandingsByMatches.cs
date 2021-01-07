using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scores.Application.Guest.Standings
{
    [Service]
    public class CreateStandingsByMatches
    {
        public class ClubViewModel
        {
            public string Name { get; set; }
            public int Played { get; set; }
            public int Wins { get; set; }
            public int Draws { get; set; }
            public int Losses { get; set; }
            public int Points { get; set; }
        }

        public IEnumerable<ClubViewModel> Do(List<GetMatchById.Response> Matches)
        {
            var Clubs = new List<ClubViewModel>();

            foreach (var match in Matches)
            {
                if (!Clubs.Any(x => x.Name == match.HomeTeam.Name))
                {
                    var WDL = WinDrawLoss.Calculate(match.HomeTeam.Id, Matches);

                    Clubs.Add(new ClubViewModel
                    {
                        Name = match.HomeTeam.Name,
                        Played = WDL.Played,
                        Wins = WDL.Wins,
                        Draws = WDL.Draws,
                        Losses = WDL.Losses,
                        Points = WDL.Points,
                    });
                }

                if (!Clubs.Any(x => x.Name == match.AwayTeam.Name))
                {
                    var WDL = WinDrawLoss.Calculate(match.AwayTeam.Id, Matches);

                    Clubs.Add(new ClubViewModel
                    {
                        Name = match.AwayTeam.Name,
                        Played = WDL.Played,
                        Wins = WDL.Wins,
                        Draws = WDL.Draws,
                        Losses = WDL.Losses,
                        Points = WDL.Points,
                    });
                }
            }

            return Clubs;
        }

        public class WinDrawLoss
        {
            public int Played { get; set; }
            public int Wins { get; set; }
            public int Draws { get; set; }
            public int Losses { get; set; }
            public int Points { get; set; }

            public static WinDrawLoss Calculate(int clubId, List<GetMatchById.Response> matches)
            {
                var winDrawLoss = new WinDrawLoss();

                winDrawLoss.Played = matches.FindAll(x => x.HomeTeam.Id == clubId || x.AwayTeam.Id == clubId).Count;

                winDrawLoss.Wins = matches.FindAll(x => x.HomeTeam.Id == clubId &&
                    x.Events.Any(y => y.Class == "FT" && y.HomeScore > y.AwayScore)).Count;
                winDrawLoss.Wins += matches.FindAll(x => x.AwayTeam.Id == clubId &&
                    x.Events.Any(y => y.Class == "FT" && y.HomeScore < y.AwayScore)).Count;

                winDrawLoss.Draws = matches.FindAll(x => x.HomeTeam.Id == clubId &&
                    x.Events.Any(y => y.Class == "FT" && y.HomeScore == y.AwayScore)).Count;
                winDrawLoss.Draws += matches.FindAll(x => x.AwayTeam.Id == clubId &&
                    x.Events.Any(y => y.Class == "FT" && y.HomeScore == y.AwayScore)).Count;

                winDrawLoss.Losses = matches.FindAll(x => x.HomeTeam.Id == clubId &&
                    x.Events.Any(y => y.Class == "FT" && y.HomeScore < y.AwayScore)).Count;
                winDrawLoss.Losses += matches.FindAll(x => x.AwayTeam.Id == clubId &&
                    x.Events.Any(y => y.Class == "FT" && y.HomeScore > y.AwayScore)).Count;

                winDrawLoss.Points = (3 * winDrawLoss.Wins) + winDrawLoss.Draws;

                return winDrawLoss;
            }
        }
    }
}
 