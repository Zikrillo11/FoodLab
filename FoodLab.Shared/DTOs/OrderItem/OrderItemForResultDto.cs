namespace FoodLab.Shared.DTOs.OrderItem;

public class OrderItemForResultDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; } 
    public decimal Price { get; set; }
    public long Quantity { get; set; }
    public decimal Total => Price * Quantity;
}
