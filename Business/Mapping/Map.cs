using AutoMapper;
using Facehook.Business.DTO_s;
using Facehook.Business.DTO_s.Story;
using Facehook.Business.DTO_s.User;
using Facehook.Entity.DTO.Post;
using Facehook.Entity.Entites;
using Facehook.Entity.Identity;

namespace Facehook.Business.Mapping;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Post, PostGetDto>()
            .ForMember(c => c.ImageName, c => c.Ignore());
        CreateMap<PostCreateDTO, Post>();
        CreateMap<StoryCreateDTO, Story>();
        CreateMap<Story, StoryGetDTO>();
        CreateMap<AppUser, UserGetDTO>();
    }
}
