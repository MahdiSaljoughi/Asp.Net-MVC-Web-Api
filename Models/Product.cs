using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MvcApi.Models;

[Index(nameof(Title), IsUnique = true)]
[Index(nameof(Slug), IsUnique = true)]
public class Product
{
    [Key] public Guid Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Slug { get; set; }
    
    public Guid CreatedBy { get; set; }
    [ForeignKey("CreatedBy")]
    public User CreatedByUser { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}