namespace CleanArchitecture.Application.Dtos.Auth
{
    public class PermissionDto
    {
        public int RoleId { get; set; }
        public IList<RoleClaimsDto> RoleClaims { get; set; }
    }
}
