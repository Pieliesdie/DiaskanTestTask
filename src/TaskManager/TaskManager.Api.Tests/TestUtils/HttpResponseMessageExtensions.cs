using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TaskManager.Api.Tests.TestUtils;

public static class HttpResponseMessageExtensions
{
    public static async Task<T> Read<T>(this Task<HttpResponseMessage> task)
    {
        var resp = await task;
        await resp.EnsureSuccessful();
        return await resp.Content.ReadFromJsonAsync<T>(TestConstants.JsonOptions)
            ?? throw new InvalidOperationException("Десериализовалось в null");
    }

    public static async Task<T?> ReadNullable<T>(this Task<HttpResponseMessage> task)
    {
        var resp = await task;
        await resp.EnsureSuccessful();
        return await resp.Content.ReadFromJsonAsync<T>(TestConstants.JsonOptions);
    }

    public static async Task EnsureSuccessful(this HttpResponseMessage resp)
    {
        if (!resp.IsSuccessStatusCode)
        {
            var content = await resp.Content.ReadAsStringAsync();
            throw new Exception($"Получен ответ {(int)resp.StatusCode}: {content}");
        }
    }

    public static async Task<HttpResponseMessage> EnsureSuccessful(this Task<HttpResponseMessage> task)
    {
        var resp = await task;
        await resp.EnsureSuccessful();
        return resp;
    }
}