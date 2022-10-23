using AutoMapper;
using Facehook.Business.DTO_s.Message;
using Facehook.Business.DTO_s.User;
using Facehook.Business.Extensions;
using Facehook.Business.Hubs;
using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.Entity.Entites;
using Facehook.Entity.Identity;
using Facehook.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Facehook.Business.Repositories;
public class MessageRepository : IMessageService
{
    private readonly IMessageDal _messageDal;
    private readonly IUserDal _userDal;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IHubContext<ChatHub, IChatClient> _hubContext;


    public MessageRepository(IMessageDal messageDal,
                             IHttpContextAccessor httpContext,
                             IHubContext<ChatHub, IChatClient> hubContext,
                             IMapper mapper,
                             IUserDal userDal)
    {
        _messageDal = messageDal;
        _httpContext = httpContext;
        _hubContext = hubContext;
        _mapper = mapper;
        _userDal = userDal;
    }

    public async Task<List<MessageGetDTO>> GetMessages(string username)
    {
        AppUser user = await _userDal.GetAsync(u => u.UserName == username);
        if (user is null)
        {
            throw new NotFoundException("User is not defined");
        }
        var loginUserId = _httpContext?.HttpContext?.User.GetUserId();
        List<Message> messagesDb = await _messageDal.GetAllAsync(m=>m.SenderDate,m=>(m.SendUserId == loginUserId && m.UserId == user.Id) || (m.SendUserId == user.Id && m.UserId == loginUserId),0,int.MaxValue, "SendUser"
            
            );
        return _mapper.Map<List<MessageGetDTO>>(messagesDb);
    }

    public async Task<MessageGetDTO> SendMessage(MessageDTO message)
    {
        var sendUser = await _userDal.GetAsync(u => u.Id == message.SendUserId);
        if (await _userDal.GetAsync(u => u.Id == message.SendUserId) is null)
            throw new NotFoundException("User is not defined");
        MessageGetDTO getMessage = new MessageGetDTO()
        {
            Content = message.Content,
            SenderDate = System.DateTime.Now,
            SendUser = _mapper.Map<UserGetDTO>(sendUser),
        };
        await _hubContext.Clients.User(message.SendUserId).ReceiveMessage(getMessage);
        Message newMessage = _mapper.Map<Message>(message);
        newMessage.UserId = _httpContext?.HttpContext?.User.GetUserId();
        await _messageDal.CreateAsync(newMessage);
        await _messageDal.SaveAsync();
        return getMessage;
    }
}
