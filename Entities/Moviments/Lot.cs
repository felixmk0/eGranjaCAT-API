using nastrafarmapi.Entities;

public class Lot
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool Active { get; set; } = true;

    public string CreatedBy { get; set; } = null!;
    public User CreatedByUser { get; set; } = null!;

    public int FarmId { get; set; }
    public Farm Farm { get; set; } = null!; 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
