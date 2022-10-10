using Facehook.Business.Base;
using Facehook.Business.DTO_s;
using Facehook.Business.DTO_s.Comment;

namespace Facehook.Business.Services;

public interface ICommentService : IBaseEntityForComments<CommentCreateDTO, CommentGetDTO> { }

