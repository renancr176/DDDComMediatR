using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class CompanyNotification : Entity
    {
        public Guid CompanyId { get; set; }
        public string Email { get; set; }

        #region Relationships

        public virtual Company Company { get; set; }

        #endregion
    }
}
