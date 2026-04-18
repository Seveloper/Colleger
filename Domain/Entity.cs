namespace Domain;

public abstract class Entity
{
    public int Id { get; set; }
    public Status Status { get; set; } = Status.Active;

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
