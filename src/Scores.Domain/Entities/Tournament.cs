namespace Scores.Domain.Entities
{
    public class Tournament
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool HasGroupStage { get; set; }

        public int CountryId { get; set; }
    }
}
