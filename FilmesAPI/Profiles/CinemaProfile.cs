using AutoMapper;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CinemaDto, Cinema>();
        CreateMap<Cinema, CinemaDto>();
        CreateMap<Cinema, ReadCinemaDto>();
    }
}
