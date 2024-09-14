namespace CleanArchitecture.Application.Dtos.Reports.PostReporting;

public class PostReportingInput
{
    public string Filter { get; set; }
        
    public DateTime StartDate { get; set; }
        
    public DateTime EndDate { get; set; }
    
    public int? PostCategoryId { get; set; }
}