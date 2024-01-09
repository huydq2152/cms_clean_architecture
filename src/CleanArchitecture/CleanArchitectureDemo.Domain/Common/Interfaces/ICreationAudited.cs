namespace CleanArchitectureDemo.Domain.Common.Interfaces;

public interface ICreationAudited
{
    int? CreatorUserId { get; set; }
    DateTime CreationTime { get; set; }
}