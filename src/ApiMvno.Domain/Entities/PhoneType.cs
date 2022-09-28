
using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class PhoneType : EntityIntId
    {
        public string Name { get; set; }
        public bool Active { get; set; } = true;

        #region Relationships

        public virtual ICollection<Phone> Phones{ get; set; }

        #endregion
    }
}
