using ApiMvno.Domain.Core.DomainObjects;
using ApiMvno.Domain.Core.Enums;

namespace ApiMvno.Domain.Entities
{
    public class EventType : Entity
    {
        public EventTypeEnum Type { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; } = true;

        #region Relationships

        public virtual ICollection<CompanyWebhook> CompanyWebhooks { get; set; }

        #endregion
    }
}
