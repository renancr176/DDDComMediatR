using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities;

public class Company : Entity
{
    public Guid? MainCompanyId { get; set; }
    public string Email { get; set; }
    public bool National { get; set; }
    public string Document { get; set; }
    public string Name { get; set; }
    public string TradeName { get; set; }
    public bool ValidateDocument { get; set; } = false;
    public bool Active { get; set; } = true;
    public string? SmallName { get; set; }
    public string? MvnoCode { get; set; }
    public string? DealerCode { get; set; }
    public string? LogoUrl { get; set; }
    public string? SalesforceQueueName { get; set; }
    public string? SalesforceCode { get; set; }
    public string? JscCode { get; set; }
    public int? CntPersonGroupId { get; set; }
    public string? CntCode { get; set; }

    #region Relationships

    public virtual ICollection<CompanyAddress> CompanyAddress { get; set; }
    public virtual ICollection<CompanyContact> CompanyContacts{ get; set; }
    public virtual ICollection<CompanyPhone> CompanyPhones { get; set; }
    public virtual ICollection<CompanyLineType> CompanyLineTypes { get; set; }
    public virtual ICollection<CompanyCn> CompanyCns { get; set; }
    public virtual ICollection<CompanySimCardReplacementReason> CompanySimCardReplacementReasons { get; set; }
    public virtual ICollection<CompanyWebhook> CompanyWebhooks { get; set; }
    public virtual ICollection<CompanyNotification> CompanyNotifications { get; set; }
    public virtual ICollection<CompanyNetworkProfile> CompanyNetworkProfiles { get; set; }
    public virtual ICollection<CompanyDueDay> CompanyDueDays { get; set; }
    public virtual ICollection<CompanyAccountType> CompanyAccountTypes { get; set; }
    public virtual ICollection<CompanyLineCancelationReason> CompanyLineCancelationReasons { get; set; }

    #endregion
}