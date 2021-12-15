using System;
using System.Runtime.Caching;

namespace WebCacheSample
{
    public class ExpensiveDataProvider
    {
        private const string Key = "expensive_key";

        public DateTime GetDate()
        {
            var factory = new Lazy<DateTime>(() => CalculateDate());
            var date = (Lazy<DateTime>) MemoryCache.Default.AddOrGetExisting(Key, factory, new CacheItemPolicy()
            {
                SlidingExpiration = TimeSpan.FromDays(7)
            });
            return (date ?? factory).Value;
        }

        public DateTime CalculateDate() => DateTime.Now;
    }
}