using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApi.Models;

public class User
{
    [Key] public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [EmailAddress] public string? Email { get; set; }
    [Required, Phone] public string Phone { get; set; }
    public string? Address { get; set; }
    public int RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}