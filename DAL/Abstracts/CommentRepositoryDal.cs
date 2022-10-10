using Facehook.Core.EFRepository;
using Facehook.DAL.Context;
using Facehook.Entity.Entites;

namespace Facehook.DAL.Abstracts;

public class CommentRepositoryDal : EFEntityRepositoryBase<Comment, AppDbContext>, ICommentDal
{
    public CommentRepositoryDal(AppDbContext context) : base(context) { }
}