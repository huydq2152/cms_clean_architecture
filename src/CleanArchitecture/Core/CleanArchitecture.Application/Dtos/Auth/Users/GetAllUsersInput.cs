using Contracts.Common.Models.Paging;

namespace CleanArchitecture.Application.Dtos.Auth.Users;

public class GetAllUsersInput: PagingRequestParameters
{
    public string Keyword { get; set; }
}