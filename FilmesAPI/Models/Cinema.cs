using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatorio")]
        public string Nome { get; set; }
    }
}
