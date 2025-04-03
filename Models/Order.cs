using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MvcApi.Models.Enums;

namespace MvcApi.Models;

public class Order
{
    [Key] public int Id { get; set; }
    
    public int TotalPrice { get; set; }
    
    public string DeliveryAddress { get; set; } = null!;

    public OrderPaymentMethod PaymentMethod { get; set; } = OrderPaymentMethod.Online;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    
    public Guid CustomerId { get; set; }
    [ForeignKey("CustomerId")] public User Customer { get; set; } = null!;
    
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}