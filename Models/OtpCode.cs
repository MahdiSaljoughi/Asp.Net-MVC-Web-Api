using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MvcApi.Models;

[Index(nameof(Phone), IsUnique = true)]
[Index(nameof(Code), IsUnique = true)]
public class OtpCode
{
    [Key] public int Id { get; set; }
    
    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 11 digits.")]
    [StringLength(11)]
    public string Phone { get; set; } = null!;
    
    [Required]
    [StringLength(4)]
    public int Code { get; set; }
    
    public DateTime CreatedAt { get; set; }
}