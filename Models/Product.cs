using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MvcApi.Models;

[Index(nameof(Title), IsUnique = true)]
[Index(nameof(EnglishTitle), IsUnique = true)]
[Index(nameof(Slug), IsUnique = true)]
public class Product
{
    [Key] public int Id { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string EnglishTitle { get; set; }

    [Required] public string Slug { get; set; }

    [Required] public string Description { get; set; }

    public bool IsShow { get; set; } = false;

    public bool IsFastDelivery { get; set; } = false;

    public bool IsOffer { get; set; } = false;

    public bool IsFake { get; set; } = false;

    [Required] public string Brand { get; set; }

    [Required] public string EnglishBrand { get; set; }

    public Guid UserId { get; set; }
    [ForeignKey("UserId")] public User User { get; set; } = null!;
    
    public List<ProductImage>? Images { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}