using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs;

public class EnderecoDto
{
    [Required(ErrorMessage = "O campo de logradouro é obrigatório!")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O campo de numero é obrigatório!")]
    public int Numero { get; set; }
}
