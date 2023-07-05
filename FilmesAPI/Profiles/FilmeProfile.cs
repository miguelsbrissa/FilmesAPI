using AutoMapper;
using FilmesAPI.Data.DTOs;

namespace FilmesAPI.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile() { 
            CreateMap<FilmeDto, Filme>();
            CreateMap<Filme, FilmeDto>();
            CreateMap<Filme, ReadFilmeDto>();
        }
    }
}
