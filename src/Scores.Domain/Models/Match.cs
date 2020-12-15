﻿using System;

namespace Scores.Domain.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime KickOff { get; set; }

        public int HomeTeamId { get; set; }
        public Club HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public Club AwayTeam { get; set; }
    }
}