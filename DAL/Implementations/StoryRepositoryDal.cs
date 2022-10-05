using Facehook.Core.EFRepository;
using Facehook.DAL.Abstracts;
using Facehook.DAL.Context;
using Facehook.Entity.Entites;

namespace Facehook.DAL.Implementations;

public class StoryRepositoryDal : EFEntityRepositoryBase<Story, AppDbContext>, IStoryDal
{
    public StoryRepositoryDal(AppDbContext context) : base(context) { }
}
