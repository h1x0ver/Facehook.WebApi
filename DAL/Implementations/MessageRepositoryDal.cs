using Facehook.Core.EFRepository;
using Facehook.DAL.Abstracts;
using Facehook.DAL.Context;
using Facehook.Entity.Entites;

namespace Facehook.DAL.Implementations;

public class MessageRepositoryDal : EFEntityRepositoryBase<Message, AppDbContext> , IMessageDal
{
    public MessageRepositoryDal(AppDbContext context) : base(context) { }
}
