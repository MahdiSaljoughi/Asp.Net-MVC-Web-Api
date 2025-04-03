using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApi.Models;

public class ProductImage
{
    [Key] public int Id { get; set; }

    public int ProductId { get; set; }
    [ForeignKey("ProductId")] public Product Product { get; set; } = null!;

    [Required] public string Url { get; set; }

    [Required] public string ImageAlt { get; set; }
}