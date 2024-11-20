using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TaskManager.Api.Tests.TestUtils.Logging;

public static class MemoryLoggingBuilderExtensions
{
    public static IServiceCollection AddMemoryLogging(this IServiceCollection lb, MemoryLoggerProvider provider)
    {
        return lb
               .RemoveByServiceType<ILoggerProvider>()
               .RemoveByServiceType<ILoggerFactory>()
               .RemoveByServiceType<ILogger>()
               .RemoveByServiceType(typeof(ILogger<>))
               .RemoveByServiceType<LoggerFactory>()
               .RemoveByServiceType(typeof(Logger<>))
               .AddLogging(l => l.AddProvider(provider)
                                 .SetMinimumLevel(LogLevel.Information));
    }
}