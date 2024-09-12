using Contracts.Common.Models.Paging;

namespace CleanArchitecture.Application.Dtos.Auth.Roles;

public class GetAllRolesInput: PagingRequestParameters
{
    public string Keyword { get; set; }
}