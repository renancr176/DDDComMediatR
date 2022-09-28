using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class CompanyNetworkProfile : Entity
    {
        public Guid CompanyId { get; set; }
        public string PartnerCode { get; set; }
        public string Apn { get; set; }
        public int DownloadSpeed { get; set; }
        public int UpdloadSpeed { get; set; }
        

        #region Relationships

        public virtual Company Company { get; set; }

        #endregion
    }
}
