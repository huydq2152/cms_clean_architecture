using Contracts.MasstransitRabbitMQ.Abstractions.Messages;

namespace Contracts.MasstransitRabbitMQ.IntegrationEvents;

public class DomainEvent
{
    public record EmailNotificationEvent : INotificationEvent
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int TransactionId { get; set; }
    }
    
    public record SmsNotificationEvent : INotificationEvent
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int TransactionId { get; set; }
    }
}