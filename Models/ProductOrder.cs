using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApi.Models;

public class ProductOrder
{
    [Key] public int Id { get; set; }

    public int OrderId { get; set; }
    [ForeignKey("OrderId")] public Order Order { get; set; } = null!;

    public int ProductId { get; set; }
    [ForeignKey("ProductId")] public Product Product { get; set; } = null!;

    public int Price { get; set; }

    public int Quantity { get; set; }

    public string ColorName { get; set; } = null!;

    public string HexCode { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}