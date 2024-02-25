using Shared.SeedWork;

namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class UserPagingQueryInput: PagingRequestParameters
{
    public string Keyword { get; set; }
}