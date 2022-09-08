using ApiMvno.Domain.Core.Messages;
using ApiMvno.Domain.Entities;

namespace ApiMvno.Application.Events;

public class CompanyCreatedEvent : Event
{
    public Company Company { get; set; }

    public CompanyCreatedEvent(Company company)
    {
        Company = company;
    }
}