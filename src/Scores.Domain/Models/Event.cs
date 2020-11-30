using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scores.Domain.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public int HomeScore { get; set; }

        [Required]
        public int AwayScore { get; set; }

        [Required]
        public int Minute { get; set; }

        [Required]
        public bool IsHome { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Class { get; set; }

        public Match Match { get; set; }
        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }
    }
}
