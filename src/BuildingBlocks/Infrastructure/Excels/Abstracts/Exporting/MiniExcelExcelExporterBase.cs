using Contracts.Common.Models.Files;
using Infrastructure.Cache.Storage;
using MiniExcelLibs;
using TestMiniExcel.Common.Constants;

namespace Infrastructure.Excels.Abstracts.Exporting;

public abstract class MiniExcelExcelExporterBase 
{
    private readonly ITempFileCacheManager _tempFileCacheManager;
        
    protected MiniExcelExcelExporterBase(ITempFileCacheManager tempFileCacheManager)
    {
        _tempFileCacheManager = tempFileCacheManager;
    }

    protected FileDto CreateExcelPackage(string fileName, List<Dictionary<string, object>> items)
    {
        var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            
        Save(items, file);

        return file;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="file"></param>
    protected virtual void Save(List<Dictionary<string, object>> items, FileDto file)
    {
        using var stream = new MemoryStream();
        stream.SaveAs(items);
        _tempFileCacheManager.SetFile(file.FileToken, stream.ToArray());
    }
}