using MediatR;

namespace ApiMvno.Application.Events;

public class CompanyCreatedEventHandler : INotificationHandler<CompanyCreatedEvent>
{
    public async Task Handle(CompanyCreatedEvent notification, CancellationToken cancellationToken)
    {
        //Cadastra no rabbit 

        //notification.Company;
    }
}