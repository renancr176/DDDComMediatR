using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities;

public class Company : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }

    #region Relationships

    public virtual ICollection<CompanyAddress> CompanyAddresses { get; set; }
    public virtual ICollection<CompanyPhone> CompanyPhones { get; set; }

    #endregion

    public Company()
    {
    }

    public Company(string name, string email, string document, ICollection<CompanyAddress> companyAddresses, ICollection<CompanyPhone> companyPhones)
    {
        Name = name;
        Email = email;
        Document = document;
        CompanyAddresses = companyAddresses;
        CompanyPhones = companyPhones;
    }
}