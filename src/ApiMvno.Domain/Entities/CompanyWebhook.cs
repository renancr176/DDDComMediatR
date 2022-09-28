using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class CompanyWebhook : Entity
    {
        public Guid CompanyId { get; set; }
        public Guid EventTypeId { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        

        #region Relationships

        public virtual EventType EventType { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Webhook> Webhooks { get; set; }

        #endregion
    }
}
