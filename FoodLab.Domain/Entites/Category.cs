using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLab.Domain.Entites;

[Table("Category")]
public class Category : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }

    [Column("Icon")]
    public string Icon { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
