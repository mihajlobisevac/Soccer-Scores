namespace Scores.Domain.Entities
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
