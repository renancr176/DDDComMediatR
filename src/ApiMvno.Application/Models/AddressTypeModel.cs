using ApiMvno.Domain.Enums;

namespace ApiMvno.Application.Models;

public class AddressTypeModel : EntityIntIdModel
{
    public AddressTypeEnum Type { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; } = true;
}