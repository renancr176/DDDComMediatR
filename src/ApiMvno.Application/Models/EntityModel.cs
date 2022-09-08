namespace ApiMvno.Application.Models;

public abstract class EntityModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}