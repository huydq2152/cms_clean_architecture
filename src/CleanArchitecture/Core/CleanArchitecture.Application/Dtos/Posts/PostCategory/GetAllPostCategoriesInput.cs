﻿using Shared.SeedWork.Paging;

namespace CleanArchitecture.Application.Dtos.Posts.PostCategory;

public class GetAllPostCategoriesInput: PagingRequestParameters
{
    public string Keyword { get; set; }
    public int? ParentId { get; set; }
}