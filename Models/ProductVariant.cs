using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApi.Models;

public class ProductVariant
{
    [Key] public int Id { get; set; }

    public string HexCode { get; set; } = null!;
    
    public string ColorName { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    public int Price { get; set; }
    
    public float? Discount { get; set; }
    
    public int? PriceAfterDiscount { get; set; }
    
    public int ProductId { get; set; }
    [ForeignKey("ProductId")] public Product Product { get; set; } = null!;
    
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}