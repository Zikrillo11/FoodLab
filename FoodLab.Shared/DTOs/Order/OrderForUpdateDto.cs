namespace FoodLab.Shared.DTOs.Order;

public class OrderForUpdateDto
{
    public long Id { get; set; }
    public string Status { get; set; } = null!;
}
