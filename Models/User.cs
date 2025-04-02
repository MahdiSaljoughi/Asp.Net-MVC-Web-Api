using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MvcApi.Models;

[Index(nameof(Phone), IsUnique = true)]
[Index(nameof(UserName), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key] public Guid Id { get; set; }

    [StringLength(30)] public string? UserName { get; set; }

    [StringLength(30)] public string? FirstName { get; set; }

    [StringLength(30)] public string? LastName { get; set; }

    [StringLength(100)] [EmailAddress] public string? Email { get; set; }

    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 11 digits.")]
    [StringLength(11)]
    public string Phone { get; set; } = null!;

    [StringLength(500)] public string? Address { get; set; }

    [Required] [StringLength(20)] public string Role { get; set; } = "Customer";
    
    // [Required] public int RoleId { get; set; }
    // [ForeignKey("RoleId")] [BindNever] public virtual Role Role { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}