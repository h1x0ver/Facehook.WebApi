using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facehook.Business.DTO_s.Authentication;

public class RegisterDTO
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public DateTime Birthday { get; set; }
    public string? Password { get; set; }
    [Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; }
}
