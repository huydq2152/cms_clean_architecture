namespace Contracts.Domains.Interfaces;

public interface IDeletionAudited
{
    int? DeleterUserId { get; set; }
    DateTime? DeletionTime { get; set; }
    bool IsDeleted { get; set; }
}