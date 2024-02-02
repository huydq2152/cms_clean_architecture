using Contracts.Dtos;

namespace CleanArchitecture.Application.Dtos.Posts;

public class PostCategoryDto : FullAuditedEntityBaseDto<int>
{
    public string Code { get; set; }
    public string Name { get; set; }
}