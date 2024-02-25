using Shared.SeedWork;

namespace CleanArchitecture.Application.Dtos.Auth.Roles;

public class RolePagingQueryInput: PagingRequestParameters
{
    public string Keyword { get; set; }
}