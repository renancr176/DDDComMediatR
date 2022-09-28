using ApiMvno.Application.Commands.AddressCommands;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CompanyCommands
{
    public class UpdateCompanyCommand : Command<CompanyModel>
    {

        public UpdateCompanyCommand(Guid id,
                                    string email,
                                    bool national,
                                    bool active,
                                    string tradeName,
                                    string smallName,
                                    string mvnoCode,
                                    string dealerCode,
                                    string logoUrl,
                                    int cntPersonGroupId,
                                    string salesforceQueueName,
                                    string salesforceCode,
                                    string jscCode,
                                    string cntCode,
                                    UpdateAddressCommand addresses)
        {
            Id = id;
            Email = email;
            National = national;
            Active = active;
            TradeName = tradeName;
            SmallName = smallName;
            MvnoCode = mvnoCode;
            DealerCode = dealerCode;
            LogoUrl = logoUrl;
            CntPersonGroupId = cntPersonGroupId;
            SalesforceQueueName = salesforceQueueName;
            SalesforceCode = salesforceCode;
            JscCode = jscCode;
            CntCode = cntCode;
            Addresses = addresses;
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public bool National { get; private set; }
        public bool Active { get; private set; }
        public string TradeName { get; private set; }
        public string SmallName { get; private set; }
        public string MvnoCode { get; private set; }
        public string DealerCode { get; private set; }
        public string LogoUrl { get; private set; }
        public int CntPersonGroupId { get; private set; }
        public string SalesforceQueueName { get; private set; }
        public string SalesforceCode { get; private set; }
        public string JscCode { get; private set; }
        public string CntCode { get; private set; }
        public UpdateAddressCommand Addresses { get; set; }

    }
}
