using Contracts.MasstransitRabbitMQ.Abstractions.Messages;
using MassTransit;
using MediatR;

namespace Infrastructure.MasstransitRabbitMQ.Consumers.Abstractions.Messages;

public abstract class Consumer<TMessage>: IConsumer<TMessage> where TMessage: class, INotificationEvent
{
    private readonly ISender _sender;

    protected Consumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<TMessage> context)
    {
        await _sender.Send(context.Message);
    }
}