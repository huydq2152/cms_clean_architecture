using CleanArchitecture.Infrastructure.MasstransisRabbitMQ.Consumers.Abstractions.Messages;
using Contracts.MasstransisRabbitMQ.IntegrationEvents;
using MediatR;

namespace CleanArchitecture.Infrastructure.MasstransisRabbitMQ.MessageBus.Consumers.Events;

public class SendEmailWhenReceivedSmsEventConsumer: Consumer<DomainEvent.EmailNotificationEvent>
{
    public SendEmailWhenReceivedSmsEventConsumer(ISender sender) : base(sender)
    {
        
    }
}