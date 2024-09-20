using Contracts.MasstransitRabbitMQ.IntegrationEvents;
using Infrastructure.MasstransitRabbitMQ.Consumers.Abstractions.Messages;
using MediatR;

namespace CleanArchitecture.Infrastructure.MasstransitRabbitMQ.MessageBus.Consumers.Events;

public class SendEmailWhenReceivedSmsEventConsumer: Consumer<DomainEvent.EmailNotificationEvent>
{
    public SendEmailWhenReceivedSmsEventConsumer(ISender sender) : base(sender)
    {
        
    }
}