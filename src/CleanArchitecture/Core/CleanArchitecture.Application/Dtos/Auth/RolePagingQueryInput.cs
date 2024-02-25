using Shared.SeedWork;

namespace CleanArchitecture.Application.Dtos.Auth;

public class RolePagingQueryInput: PagingRequestParameters
{
    public string Keyword { get; set; }
}