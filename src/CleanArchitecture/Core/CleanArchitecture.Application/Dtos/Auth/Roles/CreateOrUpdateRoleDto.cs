using Contracts.Dtos;

namespace CleanArchitecture.Application.Dtos.Auth.Roles
{
    public class CreateOrUpdateRoleDto: EntityBaseDto<int?>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
