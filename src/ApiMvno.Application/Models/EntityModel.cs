namespace ApiMvno.Application.Models;

public abstract class EntityModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public abstract class EntityIntIdModel : EntityModel
{
    public new long Id { get; set; }
}