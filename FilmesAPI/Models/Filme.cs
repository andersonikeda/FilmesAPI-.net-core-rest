﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "O titulo do filme é obrigatório")]
        public string? Titulo { get; set; }
        [Required(ErrorMessage = "O gênero do filme é obrigatório")]
        [MaxLength(50, ErrorMessage = "O tamanho do campo genero não pode exceder 50 caracteres")]
        public string? Genero { get; set; }
        [Required]
        [Range(70, 600, ErrorMessage = "A duração deve estar entre 70 e 600 minutos")]
        public int Duracao { get; set; }
        public string? Diretor { get; set; }
        
    }
}
