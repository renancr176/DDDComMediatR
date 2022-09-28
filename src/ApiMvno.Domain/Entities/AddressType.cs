using ApiMvno.Domain.Core.DomainObjects;
using ApiMvno.Domain.Enums;

namespace ApiMvno.Domain.Entities;

public class AddressType : EntityIntId
{
    public AddressTypeEnum Type { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; } = true;

    #region Relationships

    public virtual ICollection<Address> Addresses { get; set; }

    #endregion
}