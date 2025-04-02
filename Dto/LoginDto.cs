using System.ComponentModel.DataAnnotations;

namespace MvcApi.Dto;

public class LoginDto
{
    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 11 digits.")]
    [StringLength(11)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [Range(1000, 9999, ErrorMessage = "Code must be exactly 4 digits.")]
    public int Code { get; set; }
}