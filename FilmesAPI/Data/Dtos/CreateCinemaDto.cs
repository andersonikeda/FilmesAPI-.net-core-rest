﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class CreateCinemaDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatorio")]
        public string Nome { get; set; }
        public int EnderecoID { get; set;}
    }
}
