using Shared.SeedWork;
using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class UserPagingQueryInput: PagingRequestParameters
{
    public string Keyword { get; set; }
}