using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities;

public class UserCompany : Entity
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? CreateByUserId { get; set; }
    public Guid? DeletedByUserId { get; set; }

    #region Relationships

    public virtual User User { get; set; }
    public virtual User CreateByUser { get; set; }
    public virtual User DeletedByUser { get; set; }

    #endregion

    public UserCompany(Guid userId, Guid companyId)
    {
        UserId = userId;
        CompanyId = companyId;
    }
}