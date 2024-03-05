using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class GetAllUsersInput: PagingRequestParameters
{
    public string Keyword { get; set; }
}