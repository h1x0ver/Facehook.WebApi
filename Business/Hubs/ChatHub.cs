using Facehook.DAL.Context;
using Facehook.Entity.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Facehook.Business.Hubs;

[Authorize]
public class ChatHub : Hub<IChatClient>
{
    private static readonly List<AppUser> _clients = new List<AppUser>();
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContext;
    public ChatHub(AppDbContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }
    public override async Task OnConnectedAsync()
    {
        var name = Context?.User?.Identity?.Name;
        var user = await _context.Users.Where(u => u.UserName == name).FirstOrDefaultAsync();
        _context.Update(user);
        _context.SaveChanges();
        if (!_clients.Any(u => u.Firstname == name))
        {
            _clients.Add(user);
        }
        await Clients.All.GetClients(_clients);
        await Clients.All.GetClient(user);
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var name = Context?.User?.Identity?.Name;
        var user = _context.Users.Where(u => u.UserName == name).FirstOrDefault();
        _context?.Update(user);
        _context?.SaveChanges();
        _clients.RemoveAll(u => u.UserName == name);
        await Clients.All.GetClients(_clients);
        await Clients.All.GetClient(user);
    }

}
