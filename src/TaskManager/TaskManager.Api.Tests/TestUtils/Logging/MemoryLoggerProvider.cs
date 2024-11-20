using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace TaskManager.Api.Tests.TestUtils.Logging;

public class MemoryLoggerProvider : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, MemoryLogger> loggers = new ();

    public readonly ConcurrentQueue<LogEvent> Logs = new ();

    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, t => new MemoryLogger(this, t));
    }

    public void Dispose() { }

    public void ClearAll()
    {
        Logs.Clear();
        foreach (var logger in loggers.Values)
        {
            logger.Logs.Clear();
        }
    }

    public void Dump(Action<string> write)
    {
        foreach (var log in Logs)
        {
            write(log.ToString());
        }
    }

    public void DumpAndClear(Action<string> writeCallback)
    {
        Dump(writeCallback);
        ClearAll();
    }

    public string GetAllAsString()
    {
        return string.Join("\n", Logs.Select(e => e.ToString()));
    }
}