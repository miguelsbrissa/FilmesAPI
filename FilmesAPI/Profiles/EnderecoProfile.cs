using AutoMapper;
using FilmesApi.Models;
using FilmesAPI.Data.DTOs;

namespace FilmesAPI.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<EnderecoDto, Endereco>();
        CreateMap<Endereco, EnderecoDto>();
        CreateMap<Endereco, ReadEnderecoDto>();
    }
}
