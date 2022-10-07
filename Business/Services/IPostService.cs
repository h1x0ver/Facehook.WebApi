
using Facehook.Business.Base;
using Facehook.Business.DTO_s.Post;
using Facehook.Entity.DTO.Post;
using Facehook.Entity.Entites;

namespace Facehook.Business.Services;
public interface IPostService : IBaseService<PostGetDto, PostCreateDTO, PostUpdateDTO, PostSaveDTO> { }
