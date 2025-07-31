using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class ReadCinemaDto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public ReadEnderecoDto Endereco { get; set; }
    }
}
