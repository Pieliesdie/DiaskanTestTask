using System;
using Microsoft.Extensions.Logging;

namespace TaskManager.Api.Tests.TestUtils.Logging;

public class LogEvent
{
    public required string? Logger { get; init; }
    public required DateTime DateTime { get; init; }
    public required LogLevel LogLevel { get; init; }
    public required EventId EventId { get; init; }
    public required string? Message { get; init; }
    public required Exception? Exception { get; init; }

    public override string ToString()
    {
        return Exception != null
            ? $"{DateTime:HH:mm:ss.fff} | {LogLevel} | {Logger} | {Message} | {Exception}"
            : $"{DateTime:HH:mm:ss.fff} | {LogLevel} | {Logger} | {Message}";
    }
}

public class LogEvent<TState> : LogEvent
{
    public required TState? State { get; init; }
}