using AutoMapper;
using Facehook.Entity.DTO.Post;
using Facehook.Entity.Entites;

namespace Facehook.Business.Mapping;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Post, PostGetDto>()
            .ForMember(c => c.ImageName, c => c.Ignore());
    }
}
