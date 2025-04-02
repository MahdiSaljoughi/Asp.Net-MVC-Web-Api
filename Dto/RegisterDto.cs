using System.ComponentModel.DataAnnotations;

namespace MvcApi.Dto;

public class RegisterDto
{
    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 11 digits.")]
    [StringLength(11)]
    public string Phone { get; set; } = string.Empty;
}