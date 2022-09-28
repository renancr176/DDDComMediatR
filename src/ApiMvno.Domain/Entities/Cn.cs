using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class Cn : Entity
    {
        public int Code { get; set; }
        public string Uf { get; set; }
        public bool Active { get; set; } = true;

        #region Relationships

        public virtual ICollection<CompanyCn> CompanyCns { get; set; }

        #endregion
    }
}
