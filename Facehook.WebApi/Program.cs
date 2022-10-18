//using
using Facehook.Business.Mapping;
using Facehook.Business.Repositories;
using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.DAL.Context;
using Facehook.DAL.Implementations;
using Facehook.Entity.Identity;
using Facehook.WebApi.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

//createBuilder
var builder = WebApplication.CreateBuilder(args);


//SignalR
builder.Services.AddSignalR();

//newtonSoftJson
builder.Services.AddControllers()
      .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//cors error fix 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins",
        builder => builder.WithOrigins("http://localhost:3000").WithMethods("PUT", "DELETE", "GET"));
});


builder.Services.AddEndpointsApiExplorer();
//swagger
builder.Services.AddSwaggerGen();
//identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{

    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;

    options.User.RequireUniqueEmail = true;

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();


//AUTH and change Default .net identity
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidAudience = builder.Configuration.GetSection("JWT:audience").Value,
        ValidIssuer = builder.Configuration.GetSection("JWT:issuer").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:secretKey").Value))

    };
});


//mapper
builder.Services.AddMapperService();

//add scoped:
//2 add scoped for 1 entity.

//post post post post post post
builder.Services.AddScoped<IPostService, PostRepository>();
builder.Services.AddScoped<IPostDal, PostRepositoryDal>();

//savePost
builder.Services.AddScoped<ISavePostService, SavePostRepository>();
builder.Services.AddScoped<ISavePostDal, SavePostRepositoryDal>();

//PostLike
builder.Services.AddScoped<ILikeService, LikeRepository>();
builder.Services.AddScoped<ILikeDal, LikeRepositoryDal>();

//PostComment
builder.Services.AddScoped<ICommentService, CommentRepository>();
builder.Services.AddScoped<ICommentDal, CommentRepositoryDal>();



//user
builder.Services.AddScoped<IUserService, UserRepository>();
builder.Services.AddScoped<IUserDal, UserRepositoryDal>();

//User-Friend
builder.Services.AddScoped<IUserFriendService, UserFriendRepository>();
builder.Services.AddScoped<IUserFriendDal, UserFriendRepositoryDal>();

//image
builder.Services.AddScoped<IImageService, ImageRepository>();
builder.Services.AddScoped<IImageDal, ImageRepositoryDal>();


//story
builder.Services.AddScoped<IStoryService, StoryRepository>();
builder.Services.AddScoped<IStoryDal, StoryRepositoryDal>();


//MY...SQL connection.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

//cors error fix
app.UseCors(x => x
    .WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true)
);




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

//auth
app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "images")),
    RequestPath = "/img"
});



app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chat");
});
app.MapControllers();

//run
app.Run();
