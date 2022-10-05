using Microsoft.AspNetCore.Http;

namespace Facehook.Business.DTO_s.User;

public class ProfilePhotoDTO
{
    public IFormFile? ImageFile { get; set; }
}
