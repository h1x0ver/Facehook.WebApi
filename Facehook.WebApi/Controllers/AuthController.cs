using Facehook.Business.DTO_s.Authentication;
using Facehook.Entity.Identity;
using Facehook.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//Bu Controllerin icinin dolu olmasi ve business layere cixartmamagimin sebebi:
// Eyer ki men bunu business yare cixartsaydim men gerey bunu core layerde yazmali olcagdim, deriniye getsey iki dene databasem olacagdi buda mene indiki case de lazim deyl yeni bidene de controllerin ici dolu olsa nolarkiye, heyatdida zaur olmuyandada olmur nagayrag indi
namespace Facehook.WebApi.Controllers;
[Route("api/[controller]"), ApiController,Authorize]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;
    public AuthController(UserManager<AppUser> userManager,
                          SignInManager<AppUser> signInManager,
                          RoleManager<IdentityRole> roleManager,
                          IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _config = config;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO registerDTO)
    {
        AppUser appUser = new AppUser();
        appUser.Email = registerDTO.Email;
        appUser.Lastname = registerDTO.Lastname;
        appUser.Firstname = registerDTO.Firstname;
        appUser.Birthday = registerDTO.Birthday;
        appUser.UserName = registerDTO.Username;
        var result = await _userManager.CreateAsync(appUser, registerDTO.Password);
        if (!result.Succeeded)
        {
            string error = "";
            foreach (var item in result.Errors)
            {
                error += item.Description + "\n";
            }
            return StatusCode(StatusCodes.Status406NotAcceptable, new Response(4010, error));
        }
        return Ok();
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var user = await _userManager.FindByNameAsync(loginDTO.Username);
        if (user == null) return StatusCode(StatusCodes.Status405MethodNotAllowed, new Response(4005, "Username does not exist idiot"));
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
        if (!result.Succeeded) return StatusCode(StatusCodes.Status401Unauthorized, "Invalid Password retard...");
        var issuer = _config.GetSection("JWT:issuer").Value;
        var audience = _config.GetSection("JWT:audience").Value;
        var secretKey = _config.GetSection("JWT:secretKey").Value;
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("Firstname", user.Firstname),
            new Claim("Lastname", user.Lastname)
        };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(n => new Claim(ClaimTypes.Role, n)));
        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(secretKey));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        //jwt security token
        JwtSecurityToken securityToken = new(
            audience: audience,
            signingCredentials: signingCredentials,
            issuer: issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4).AddMinutes(2)
        );
        //jwt sec. tokeni stringe cevirirem burda:
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return Ok(token);
    }
}
