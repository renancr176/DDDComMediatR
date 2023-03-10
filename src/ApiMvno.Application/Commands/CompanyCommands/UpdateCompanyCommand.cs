using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CompanyCommands
{
    public class UpdateCompanyCommand : Command<CompanyModel?>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public UpdateCompanyCommand()
        {
        }

        public UpdateCompanyCommand(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
