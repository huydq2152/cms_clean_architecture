using Infrastructure.Cache.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Cache.Storage
{
    public class TempFileCacheManager : ITempFileCacheManager
    {
        private readonly IDistributedCache _cache;

        public TempFileCacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetFile(string token, byte[] content)
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            
            await _cache.SetAsync(token, content, cacheEntryOptions); 
        }

        public async Task<byte[]> GetFile(string token)
        {
            return await _cache.GetAsync(token);
        }

        public async Task SetFile(string token, TempFileInfo info)
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            await _cache.SetAsync(token, info, cacheEntryOptions);
        }
        
        public async Task<TempFileInfo> GetFileInfo(string token)
        {
            if (_cache.TryGetValue(token, out TempFileInfo? fileInfo))
            {
                return fileInfo;
            }

            return null; 
        }
    }
}