using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scores.Domain.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        [JsonProperty("alpha3Code")]
        public string NameCode { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Flag { get; set; }

        public bool Deactivated { get; set; }
    }
}
