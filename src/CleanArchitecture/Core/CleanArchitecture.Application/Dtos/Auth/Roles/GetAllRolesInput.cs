using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Auth.Roles;

public class GetAllRolesInput: PagingRequestParameters
{
    public string Filter { get; set; }
}