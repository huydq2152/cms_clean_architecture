using CleanArchitecture.Application.Dtos.Reports.PostReporting;

namespace CleanArchitecture.Application.Interfaces.Services.Reports;

public interface IPostReportingService
{
    Task<List<PostReportingOutput>> PostReport(PostReportingInput input);
}