namespace CleanArchitecture.Application.Dtos.Auth.Roles
{
    public class PermissionDto
    {
        public int RoleId { get; set; }
        public IList<RoleClaimsDto> RoleClaims { get; set; }
    }
}
