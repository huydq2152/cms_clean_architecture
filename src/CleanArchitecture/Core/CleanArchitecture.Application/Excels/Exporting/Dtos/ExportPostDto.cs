using Euroland.FlipIT.SData.API.Dto;
using MiniExcelLibs.Attributes;

namespace CleanArchitecture.Application.Excels.Exporting.Dtos;

public class ExportPostDto: IDtoObject
{
    [ExcelColumn(Name = "Code", Index = 0, Width = 40)]
    public string Code { get; set; }
    
    [ExcelColumn(Name = "Name", Index = 1, Width = 100)]
    public string Name { get; set; }
    
    [ExcelColumn(Ignore = true)]
    public bool IsActive { get; set; }
}