using AutoMapper;
using Facehook.Business.DTO_s.User;
using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.Entity.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Facehook.Business.Repositories;

public class UserRepository : IUserService
{
    private readonly IUserDal _userDal;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _hostEnvironment;

    public UserRepository(IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment hostEnvironment,
        IUserDal userDal)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _userDal = userDal;
    }

    public async Task<UserGetDTO> Get(int id)
    {
        throw new ArgumentNullException();
    }

    public Task<List<UserGetDTO>> GetAll()
    {
        var users = _userDal.GetAll();
        return users;
    }
}
