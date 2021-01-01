using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Domain.Models
{
    public class MatchPlayer
    {
        public int Id { get; set; }

        public bool IsHome { get; set; }
        public bool IsSubstitute { get; set; }

        public int MatchId { get; set; }
        public int PlayerId { get; set; }
    }
}
