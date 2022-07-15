using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace EventManagement.Api.Extensions;

internal static class DistributedCacheExtensions
{
    public static async Task SetRecordAsync<T>(
        this IDistributedCache cache,
        Guid recordId,
        T data,
        TimeSpan? absoluteExpireTime = null
        )
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60)
        };

        var jsonData = JsonSerializer.Serialize(data);

        await cache.SetStringAsync(recordId.ToString(), jsonData, options);
    }

    public static async Task<T?> GetRecordAsync<T>(
        this IDistributedCache cache,
        Guid recordId
        )
    {
        var jsonData = await cache.GetStringAsync(recordId.ToString());

        if (jsonData is null) return default;

        var dataResponse = JsonSerializer.Deserialize<T>(jsonData);

        return dataResponse;
    } 
}