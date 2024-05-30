using Library.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class VerificationCodeCacheService : IVerificationCodeCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(10);
        public VerificationCodeCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public int? GetVerificationCode(string userId)
        {
           
            return _memoryCache.Get<int?>(GetCacheKey(userId));

        }

        public void SetVerificationCode(string userId, int verificationCode)
        {
           _memoryCache.Set(GetCacheKey(userId), verificationCode, _cacheExpiration);
        }

        private string GetCacheKey(string userId)
        {
            return $"VerificationCode_{userId}";
        }
    }
}
