namespace PSSerilog;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using Serilog;

public abstract class SerilogLoggerBase : PSCmdlet
{
    private static readonly object sync = new();

    protected static readonly string SessionStateName = "PSSerilogSessionState";
    protected static readonly string SessionStateKey = "Loggers";

    protected void AddLogger(ILogger logger)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        var action = () =>
        {
            var loggers = this.GetLoggerState();
            loggers.Add(logger);
        };

        SafeInvoke(action);
    }

    protected List<ILogger> GetLoggers()
    {
        List<ILogger> loggers = new();

        SafeInvoke(() => loggers.AddRange(this.GetLoggerState()));

        return loggers;
    }

    protected void RemoveLoggers() => SafeInvoke(
        () =>
        {
            var loggers = this.GetLoggerState();
            while (loggers.Count > 0)
            {
                (loggers[0] as IDisposable)?.Dispose();

                loggers.RemoveAt(0);
            }
        });

    private List<ILogger> GetLoggerState()
    {
        if (this.SessionState.PSVariable.GetValue(SessionStateName, null) is not Hashtable state)
        {
            state = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            this.SessionState.PSVariable.Set(SessionStateName, state);
        }

        List<ILogger> loggers;

        if (state.ContainsKey(SessionStateKey))
        {
            loggers = (List<ILogger>)state[SessionStateKey];
        }
        else
        {
            loggers = new List<ILogger>();
            state[SessionStateKey] = loggers;
        }

        return loggers;
    }

    private static void SafeInvoke(Action action)
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        if (!Monitor.TryEnter(sync, 5000))
        {
            throw new InvalidOperationException("Timeout waiting for lock on session state.");
        }

        try
        {
            action();
        }
        finally
        {
            Monitor.Exit(sync);
        }
    }
}