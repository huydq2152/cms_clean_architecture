namespace Infrastructure.Cache.Storage
{
    public interface ITempFileCacheManager
    {
        Task SetFile(string token, byte[] content);

        Task<byte[]> GetFile(string token);

        Task SetFile(string token, TempFileInfo info);
        
        Task<TempFileInfo> GetFileInfo(string token);
    }
}