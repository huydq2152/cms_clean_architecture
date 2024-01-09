namespace CleanArchitectureDemo.Domain.Common.Interfaces;

public interface IDeletionAudited
{
    int? DeleterUserId { get; set; }
    DateTime? DeletionTime { get; set; }
    bool IsDeleted { get; set; }
}