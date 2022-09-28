using ApiMvno.Application.Commands.AddressCommands;
using ApiMvno.Application.Commands.PhoneCommands;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class CreateCompanyCommand : Command<CompanyModel>
{
    
    public Guid Id { get; set; }
    public Guid MainCompany { get; set; }
    public string Email { get; set; }
    public bool National { get; set; }
    public string Document { get; set; }
    public bool Active { get; set; }
    public string Name { get; set; }
    public string TradeName { get; set; }
    public bool ValidateDocument { get; set; }
    public string SmallName { get; set; }
    public string MvnoCode { get; set; }
    public string DealerCode { get; set; }
    public string LogoUrl { get; set; }
    public int CntPersonGroupId { get; set; }
    public string SalesforceQueueName { get; set; }
    public string SalesforceCode { get; set; }
    public string JscCode { get; set; }
    public string CntCode { get; set; }
    public IEnumerable<CreateAddressCommand> Addresses { get; set; }
    public IEnumerable<CreateCompanyContactCommand> CompanyContacts { get; set; }
    public IEnumerable<CreatePhoneCommand> Phones { get; set; }
}