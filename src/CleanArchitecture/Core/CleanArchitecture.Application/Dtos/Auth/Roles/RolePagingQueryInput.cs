using Shared.SeedWork;
using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Auth.Roles;

public class RolePagingQueryInput: PagingRequestParameters
{
    public string Filter { get; set; }
}