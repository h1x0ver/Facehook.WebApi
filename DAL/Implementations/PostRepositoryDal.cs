using Facehook.Core.EFRepository;
using Facehook.DAL.Abstracts;
using Facehook.DAL.Context;
using Facehook.Entity.Entites;

namespace Facehook.DAL.Implementations;

public class PostRepositoryDal : EFEntityRepositoryBase<Post, AppDbContext> , IPostDal 
{
    public PostRepositoryDal(AppDbContext context) : base(context)
    {}

}
