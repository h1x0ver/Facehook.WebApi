
using Facehook.Business.Base;
using Facehook.Entity.DTO.Post;
using Facehook.Entity.Entites;

namespace Facehook.Business.Services;
public interface IPostService : IBaseService<PostGetDto, PostCreateDTO, PostUpdateDTO> { }
