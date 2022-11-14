namespace PSSerilog;

using System.Management.Automation;
using Serilog;

[Cmdlet(VerbsCommon.Close, "SerilogDefaultLogger")]
public class CloseSerilogDefaultLoggerCommand : PSCmdlet
{
    /// <inheritdoc />
    protected override void ProcessRecord() => Log.CloseAndFlush();
}