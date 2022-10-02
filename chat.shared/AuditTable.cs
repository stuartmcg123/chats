namespace chat.shared;

public abstract class AuditTable : BaseModel
{
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}