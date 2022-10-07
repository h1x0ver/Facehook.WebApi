using Facehook.Core.EFRepository;
using Facehook.DAL.Abstracts;
using Facehook.DAL.Context;
using Facehook.Entity.Entites;

namespace Facehook.DAL.Implementations;

public class LikeRepositoryDal : EFEntityRepositoryBase<Like, AppDbContext>, ILikeDal
{
    public LikeRepositoryDal(AppDbContext context) : base(context) { }
}
