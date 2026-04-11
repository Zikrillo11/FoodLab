namespace FoodLab.Shared.DTOs.Base;

public class BaseDto
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public long CreatedBy { get; set; }
    public long UpdatedBy { get; set; }
}
