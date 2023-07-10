using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs;

public class CinemaDto
{
    [Required(ErrorMessage = "O campo de nome é obrigatório!")]
    public string Nome { get; set; }
}
