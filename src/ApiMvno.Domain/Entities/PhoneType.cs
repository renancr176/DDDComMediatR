
using ApiMvno.Domain.Core.DomainObjects;
using ApiMvno.Domain.Enums;

namespace ApiMvno.Domain.Entities
{
    public class PhoneType : EntityIntId
    {
        public PhoneTypeEnum Type { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; } = true;

        #region Relationships

        public virtual ICollection<Phone> Phones{ get; set; }

        #endregion

        public PhoneType()
        {
        }

        public PhoneType(PhoneTypeEnum type, string name, bool active)
        {
            Type = type;
            Name = name;
            Active = active;
        }
    }
}
