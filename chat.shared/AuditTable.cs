namespace chat.shared;

public abstract class AuditTable : BaseModel
{
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}