using CleanArchitecture.Infrastructure.MasstransisRabbitMQ.Consumers.Abstractions.Messages;
using Contracts.MasstransisRabbitMQ.IntegrationEvents;
using MediatR;

namespace CleanArchitecture.Infrastructure.MasstransisRabbitMQ.MessageBus.Consumers.Events;

public class SendSmsWhenReceivedSmsEventConsumer: Consumer<DomainEvent.SmsNotificationEvent>
{
    public SendSmsWhenReceivedSmsEventConsumer(ISender sender) : base(sender)
    {
        
    }
}