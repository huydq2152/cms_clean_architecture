using Contracts.Dtos.Interface;

namespace Contracts.Dtos;

public class FullAuditedEntityBaseDto<T> : EntityBaseDto<T>, IFullAuditedEntityBaseDto
{
    public int? CreatorUserId { get; set; }
    public DateTime CreationTime { get; set; }
    public int? LastModifiedUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public int? DeleterUserId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }
}