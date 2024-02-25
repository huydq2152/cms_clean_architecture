using Contracts.Dtos;

namespace CleanArchitecture.Application.Dtos.Auth
{
    public class CreateOrUpdateRoleDto: FullAuditedEntityBaseDto<int?>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
