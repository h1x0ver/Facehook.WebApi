using AutoMapper;
using Facehook.Business.DTO_s;
using Facehook.Business.DTO_s.Comment;
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
        CreateMap<CommentCreateDTO, Comment>();
        CreateMap<Comment, CommentGetDTO>();

        CreateMap<Post, PostGetDto>()
            .ForMember(c => c.ImageName, c => c.Ignore())
            .ForMember(c => c.LikeCount, c => c.MapFrom(c => c.Likes!.Count))
            .ForMember(c => c.CommentCount, c => c.MapFrom(c => c.Comments!.Count))
            .ForMember(c => c.ImageName, c => c.MapFrom(c => c.Images!.Select(c => c.Name)));


        CreateMap<PostCreateDTO, Post>();
        CreateMap<StoryCreateDTO, Story>();
        CreateMap<Story, StoryGetDTO>()
            .ForMember(c => c.ImageName, c => c.Ignore())
            .ForMember(c => c.ImageName, c => c.MapFrom(c => c.Images!.Select(c => c.Name)));

        CreateMap<AppUser, UserGetDTO>()
             .ForMember(c => c.ProfileImage, c => c.MapFrom(c => c.ProfileImage!.Name));


        CreateMap<AppUser, FriendSuggestionDTO>();
        CreateMap<AppUser, UserProfileDTO>()
            .ForMember(c => c.ProfileImage, c => c.Ignore())
            .ForMember(c => c.PostCount, c => c.Ignore())
            .ForMember(c => c.FriendCount, c => c.Ignore())
            .ForMember(c => c.Status, c => c.Ignore());
    }
}
