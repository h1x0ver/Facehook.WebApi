using Facehook.Core.EFRepository;
using Facehook.DAL.Abstracts;
using Facehook.DAL.Context;
using Facehook.Entity.Identity;

namespace Facehook.DAL.Implementations;

public class UserRepositoryDal : EFEntityRepositoryBase<AppUser, AppDbContext>, IUserDal
{
    public UserRepositoryDal(AppDbContext context) : base(context)   { }
}
