using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class Phone : Entity
    {
        public long PhoneTypeId { get; set; }
        public int Number { get; set; }
        public string Teste { get; set; }

        #region Relationships

        public virtual PhoneType PhoneType { get; set; }
        public virtual ICollection<CompanyContactPhone> CompanyContactPhones { get; set; }
        public virtual ICollection<CompanyPhone> CompanyPhones { get; set; }

        #endregion
    }
}
