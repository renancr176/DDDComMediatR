using ApiMvno.Domain.Core.DomainObjects;
using ApiMvno.Domain.Enums;

namespace ApiMvno.Domain.Entities
{
    public class LineType : Entity
    {
        public LineTypeEnum Type { get; set; }
        public string Name { get; set; }
        public int NumasyId { get; set; }
        public bool Active { get; set; } = true;

        #region Relationships

        public virtual ICollection<CompanyLineType> CompanyLineTypes { get; set; }

        #endregion
    }
}
