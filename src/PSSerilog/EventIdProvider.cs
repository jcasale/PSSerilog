namespace PSSerilog;

using System;
using System.Globalization;

using Serilog.Events;
using Serilog.Sinks.EventLog;

/// <summary>
/// Extracts the event id from the log properties.
/// </summary>
public class EventIdProvider : IEventIdProvider
{
    /// <inheritdoc/>
    public ushort ComputeEventId(LogEvent logEvent)
    {
        if (logEvent is null)
        {
            throw new ArgumentNullException(nameof(logEvent));
        }

        if (!logEvent.Properties.TryGetValue("EventId", out var property))
        {
            return 0;
        }

        if (property is not ScalarValue scalar)
        {
            return 0;
        }

        if (short.TryParse(scalar.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
        {
            return (ushort)result;
        }

        return 0;
    }
}