using Facehook.Core.EFRepository;
using Facehook.DAL.Abstracts;
using Facehook.DAL.Context;
using Facehook.Entity.Entites;

namespace Facehook.DAL.Implementations;

public class ImageRepositoryDal : EFEntityRepositoryBase<Image,AppDbContext>, IImageDal
{
    public ImageRepositoryDal(AppDbContext context) : base(context) { }
}
