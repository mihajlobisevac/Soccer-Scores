using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scores.Domain.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime KickOff { get; set; }

        public Club HomeTeam { get; set; }
        public Club AwayTeam { get; set; }
    }
}
