using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class Phone : Entity
    {
        public long PhoneTypeId { get; set; }
        public long Number { get; set; }

        #region Relationships

        public virtual PhoneType PhoneType { get; set; }
        public virtual ICollection<CompanyPhone> CompanyPhones { get; set; }

        #endregion

        public Phone()
        {
        }

        public Phone(long phoneTypeId, long number)
        {
            PhoneTypeId = phoneTypeId;
            Number = number;
        }

        public Phone(Guid id, long phoneTypeId, long number)
            : this (phoneTypeId, number)
        {
            Id = id != Guid.Empty ? id : Guid.NewGuid();
        }
    }
}
