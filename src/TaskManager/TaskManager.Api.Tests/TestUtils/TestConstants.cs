using System.Text.Json;

namespace TaskManager.Api.Tests.TestUtils;

public static class TestConstants
{
    public static readonly JsonSerializerOptions JsonOptions;

    static TestConstants()
    {
        JsonOptions = new ()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        Program.ConfigureJson(JsonOptions);
    }
}