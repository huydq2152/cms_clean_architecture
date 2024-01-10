namespace Contracts.Domains.Interfaces;

public interface ICreationAudited
{
    int? CreatorUserId { get; set; }
    DateTime CreationTime { get; set; }
}