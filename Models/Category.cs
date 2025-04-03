using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApi.Models;

public class Category
{
    [Key] public int Id { get; set; }

    public string Title { get; set; } = null!;
    
    public string TitleEnglish { get; set; } = null!;

    public string Url { get; set; } = null!;
    
    public Guid UserId { get; set; }
    [ForeignKey("UserId")] public User User { get; set; } = null!;
    
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}