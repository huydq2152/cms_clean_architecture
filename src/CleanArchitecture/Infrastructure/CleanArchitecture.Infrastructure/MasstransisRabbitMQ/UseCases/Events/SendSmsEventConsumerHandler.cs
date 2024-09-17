using Contracts.MasstransisRabbitMQ.IntegrationEvents;
using MediatR;

namespace CleanArchitecture.Infrastructure.MasstransisRabbitMQ.UseCases.Events;

public class SendSmsEventConsumerHandler: IRequestHandler<DomainEvent.SmsNotificationEvent>
{
    public async Task Handle(DomainEvent.SmsNotificationEvent request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}