using Contracts.MasstransitRabbitMQ.IntegrationEvents;
using Infrastructure.MasstransitRabbitMQ.Consumers.Abstractions.Messages;
using MediatR;

namespace CleanArchitecture.Infrastructure.MasstransitRabbitMQ.MessageBus.Consumers.Events;

public class SendSmsWhenReceivedSmsEventConsumer: Consumer<DomainEvent.SmsNotificationEvent>
{
    public SendSmsWhenReceivedSmsEventConsumer(ISender sender) : base(sender)
    {
        
    }
}