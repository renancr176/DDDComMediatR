using ApiMvno.Domain.Enums;

namespace ApiMvno.Application.Models;

public class PhoneTypeModel : EntityIntIdModel
{
    public PhoneTypeEnum Type { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
}