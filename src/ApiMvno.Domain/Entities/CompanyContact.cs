using ApiMvno.Domain. Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class CompanyContact : Entity
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public bool NotifyChanges { get; set; }
        

        #region Relationships

        public virtual Company Company { get; set; }

        public virtual ICollection<CompanyContactPhone> CompanyContactPhones{ get; set; }

        #endregion
    }
}
