using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MvcApi.Dto;

[Index(nameof(UserName), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class UserUpdataDto
{
    [StringLength(30)] public string? UserName { get; set; }

    [StringLength(30)] public string? FirstName { get; set; }

    [StringLength(30)] public string? LastName { get; set; }

    [StringLength(100)] [EmailAddress] public string? Email { get; set; }

    [StringLength(500)] public string? Address { get; set; }

    [StringLength(20)] public string? Role { get; set; }
}