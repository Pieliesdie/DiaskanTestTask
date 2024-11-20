using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace TaskManager.Api.Tests.TestUtils.Logging;

public class MemoryLogger<T>(MemoryLoggerProvider provider) : MemoryLogger(provider, typeof(T).FullName), ILogger<T>
{
    public MemoryLogger() : this(new MemoryLoggerProvider()) { }
}

public class MemoryLogger(MemoryLoggerProvider provider, string? categoryName) : ILogger
{
    public readonly ConcurrentQueue<LogEvent> Logs = new ();

    public MemoryLogger() : this(new MemoryLoggerProvider(), null) { }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var logEvent = new LogEvent<TState>
        {
            Logger = categoryName,
            DateTime = DateTime.UtcNow,
            EventId = eventId,
            Exception = exception,
            LogLevel = logLevel,
            Message = formatter(state, exception),
            State = state
        };
        Logs.Enqueue(logEvent);
        provider.Logs.Enqueue(logEvent);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= LogLevel.Information;
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return MemoryLoggerScope.Instance;
    }
}

public sealed class MemoryLoggerScope : IDisposable
{
    public static readonly MemoryLoggerScope Instance = new ();

    public void Dispose() { }
}