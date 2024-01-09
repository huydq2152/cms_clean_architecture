namespace CleanArchitectureDemo.Domain.Common.Interfaces;

public interface IModificationAudited
{
    int? LastModifiedUserId { get; set; }
    DateTime? LastModificationTime { get; set; }
}