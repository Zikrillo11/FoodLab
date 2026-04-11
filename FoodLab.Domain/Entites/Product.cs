using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLab.Domain.Entites;

[Table("Product")]
public class Product : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }

    [Column("Description")]
    public string Description { get; set; }

    [Column("Price")]
    public decimal Price { get; set; }

    [Column("ImageUrl")]
    public string ImageUrl { get; set; }

    [Column("IsAvailable")]
    public bool IsAvailable { get; set; }

    [Column("CategoryId")]
    public long CategoryId { get; set; }

    [Column("Category")]
    public Category Category { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
