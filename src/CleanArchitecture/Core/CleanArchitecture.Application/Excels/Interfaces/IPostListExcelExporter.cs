using CleanArchitecture.Application.Excels.Exporting.Dtos;
using Contracts.Common.Models.Files;

namespace CleanArchitecture.Application.Excels.Interfaces
{
    public interface IPostListExcelExporter
    {
        FileDto ExportToFile(List<ExportPostDto> posts);
    }
}