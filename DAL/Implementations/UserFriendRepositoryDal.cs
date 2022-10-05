using Facehook.Core.EFRepository;
using Facehook.DAL.Abstracts;
using Facehook.DAL.Context;
using Facehook.Entity.Entites;

namespace Facehook.DAL.Implementations;

public class UserFriendRepositoryDal : EFEntityRepositoryBase<UserFriend, AppDbContext>, IUserFriendDal
{
    public UserFriendRepositoryDal(AppDbContext context) : base(context) { }
}
