using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLab.Domain.Entites;

[Table("Order")]
public class Order : BaseEntity
{
    [Column("CustomerName")]
    public string CustomerName { get; set; }

    [Column("PhoneNumber")]
    public string PhoneNumber { get; set; } 

    [Column("Address")]
    public string Address { get; set; }

    [Column("TotalPrice")]
    public decimal TotalPrice { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
