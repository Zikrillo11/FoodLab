namespace FoodLab.Shared.DTOs.Product;

public class ProductForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } 
    public decimal Price { get; set; }
    public string CategoryName { get; set; }
}
