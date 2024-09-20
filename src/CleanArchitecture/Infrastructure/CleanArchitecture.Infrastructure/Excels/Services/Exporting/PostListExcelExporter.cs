using CleanArchitecture.Application.Excels.Exporting.Dtos;
using CleanArchitecture.Application.Excels.Interfaces;
using CleanArchitecture.Infrastructure.Cache.Storage;
using CleanArchitecture.Infrastructure.Excels.Abstracts.Exporting;
using Contracts.Common.Models.Files;

namespace CleanArchitecture.Infrastructure.Excels.Services.Exporting
{
    public class PostListExcelExporter : MiniExcelExcelExporterBase, IPostListExcelExporter
    {
        public PostListExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ExportPostDto> posts)
        {
            var items = new List<Dictionary<string, object>>();
            foreach (var post in posts)
            {
                items.Add(new Dictionary<string, object>()
                {
                    { "Code", post.Code },
                    { "Name", post.Name },
                    { "IsActive", post.IsActive }
                });
            }
        
            return CreateExcelPackage("posts.xlsx", items);
        }
    }
}