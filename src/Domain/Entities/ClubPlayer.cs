namespace SoccerScores.Domain.Entities
{
    public class ClubPlayer
    {
        public int Id { get; set; }

        public int ShirtNumber { get; set; }

        public Club Club { get; set; }
        public Player Player { get; set; }
    }
}
