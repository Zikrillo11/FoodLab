namespace FoodLab.Shared.DTOs.Order;

public class OrderForResultDto
{
    public long Id { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
}
