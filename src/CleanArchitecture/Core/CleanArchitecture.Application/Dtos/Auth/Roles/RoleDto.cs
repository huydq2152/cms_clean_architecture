using Contracts.Dtos;

namespace CleanArchitecture.Application.Dtos.Auth.Roles
{
    public class RoleDto: FullAuditedEntityBaseDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
