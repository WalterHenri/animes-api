using Animes.Application.DTOs;
using Animes.Application.Features.Directors.Commands;
using Animes.Domain.Entities;
using AutoMapper;

namespace Animes.Application.Mappers;

public class DirectorProfile : Profile
{
    public DirectorProfile()
    {
        CreateMap<Director, DirectorDto>().ReverseMap();
        CreateMap<CreateDirectorCommand, Director>();
        CreateMap<UpdateDirectorCommand, Director>();
    }
}