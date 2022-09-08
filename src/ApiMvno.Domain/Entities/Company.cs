using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities;

public class Company : Entity
{
    public string Name { get; set; }

    public Company(string name)
    {
        Name = name;
    }
}