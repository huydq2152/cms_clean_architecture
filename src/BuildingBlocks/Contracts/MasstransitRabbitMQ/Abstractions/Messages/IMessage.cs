using MassTransit;
using MediatR;

namespace Contracts.MasstransitRabbitMQ.Abstractions.Messages;

[ExcludeFromTopology]
public interface IMessage: IRequest
{
    public int Id { get; set; }
    public DateTime CreationTime { get; set; }
}