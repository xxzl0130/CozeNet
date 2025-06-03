using CozeNet.Core.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CozeNet.AspNetCore;

public interface IAuthStore
{
    /// <summary>
    /// 获取coze 访问令牌
    /// </summary>
    /// <param name="durationSecond">有效期</param>
    /// <returns></returns>
    Task<string> GetAccessTokenAsync(int? durationSecond = null);
}

public class AuthStore(IAuthorization authorization, IDistributedCache cache) : IAuthStore
{
    private const string CacheKey = "coze_accessToken";
    private static readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
    public async Task<string> GetAccessTokenAsync(int? durationSecond = null)
    {
        durationSecond ??= 36400; //默认36400s 即一天
        var token = await cache.GetStringAsync(CacheKey);
        if (!string.IsNullOrEmpty(token)) return token;
        await _semaphoreSlim.WaitAsync();//使用通道为1的信号量阻塞，保证只有线程进行token刷新
        try
        {
            // 双重检查，避免锁内重复刷新
            token = await cache.GetStringAsync(CacheKey);
            if (!string.IsNullOrEmpty(token)) return token;
            // token过期，重新获取
            token = await authorization.GetAccessTokenAsync(durationSecond.Value);
            await cache.SetStringAsync(CacheKey, token, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Math.Max(durationSecond.Value - 10, 30)), //过期稍微提前一点
            });
            return token;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to refresh access token.", ex);
        }
        finally
        {
            _semaphoreSlim.Release();
        }

    }
}