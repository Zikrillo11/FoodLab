using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLab.Domain.Entites;

[Table("OrderItem")]
public class OrderItem : BaseEntity
{
    [Column("ProductId")]
    public long ProductId { get; set; }

    [Column("Product")]
    public Product Product { get; set; }

    [Column("Quantity")]
    public long Quantity { get; set; }

    [Column("Price")]
    public decimal Price { get; set; }
}
