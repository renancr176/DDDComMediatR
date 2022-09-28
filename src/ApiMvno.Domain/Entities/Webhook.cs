using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class Webhook : Entity
    {
        public Guid CompanyWebhookId { get; set; }
        public int Retries { get; set; }
        public DateTime SendDate { get; set; }
        public bool SuccessResponse { get; set; }
        public string JsonData { get; set; }
        public string ResponseJson { get; set; }
        

        #region Relationships

        public virtual CompanyWebhook CompanyWebhook { get; set; }

        #endregion
    }
}
