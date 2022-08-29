namespace PSSerilog;

using System.Management.Automation;
using Serilog;

[Cmdlet(VerbsLifecycle.Stop, "SerilogLogging")]
public class StopSerilogLoggingCommand : SerilogLoggerBase
{
    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        this.RemoveLoggers();

        Log.CloseAndFlush();
    }
}