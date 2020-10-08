using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Models
{
    public class Cargo
    {
        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string Descricao { get; set; }
    }
}
