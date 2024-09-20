namespace Infrastructure.Excels.Abstracts.Importing;

public class ImportFromExcelJobInput
{
    public Guid BinaryObjectId { get; set; }

    public int UserId { get; set; }
}