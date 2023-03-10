namespace ApiMvno.Infra.CrossCutting.Portability.Models;

public abstract class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public abstract class BaseModelIntId : BaseModel
{
    public new long Id { get; set; }
}
