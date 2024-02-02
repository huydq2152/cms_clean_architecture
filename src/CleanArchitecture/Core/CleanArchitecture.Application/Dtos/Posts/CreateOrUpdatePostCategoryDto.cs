using Contracts.Dtos;

namespace CleanArchitecture.Application.Dtos.Posts;

public class CreateOrUpdatePostCategoryDto : FullAuditedEntityBaseDto<int?>
{
    public string Code { get; set; }
    public string Name { get; set; }
}