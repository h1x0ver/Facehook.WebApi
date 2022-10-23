namespace Facehook.Business.DTO_s.User;

public class PasswordChangeDto
{
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
}
