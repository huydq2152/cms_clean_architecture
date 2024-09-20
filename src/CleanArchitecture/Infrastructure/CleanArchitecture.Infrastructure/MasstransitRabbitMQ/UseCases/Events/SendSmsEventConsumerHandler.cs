using Contracts.MasstransitRabbitMQ.IntegrationEvents;
using MediatR;

namespace CleanArchitecture.Infrastructure.MasstransitRabbitMQ.UseCases.Events;

public class SendSmsEventConsumerHandler: IRequestHandler<DomainEvent.SmsNotificationEvent>
{
    public async Task Handle(DomainEvent.SmsNotificationEvent request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}