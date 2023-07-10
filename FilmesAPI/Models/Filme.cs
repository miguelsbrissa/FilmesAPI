using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Filme
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório!")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O título é obrigatório!")]
    [MaxLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "O título é obrigatório!")]
    [MaxLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres")]
    public string Diretor { get; set; }

    [Required(ErrorMessage = "O título é obrigatório!")]
    [Range(60, 600, ErrorMessage = "A duração do filme deve ter entre 60 e 600 minutos")]
    public int Duracao { get; set; }
}
