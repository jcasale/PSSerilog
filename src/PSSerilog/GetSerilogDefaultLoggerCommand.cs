namespace PSSerilog;

using System.Management.Automation;

using Serilog;

[Cmdlet(VerbsCommon.Get, "SerilogDefaultLogger")]
[OutputType(typeof(ILogger))]
public class GetSerilogDefaultLoggerCommand : PSCmdlet
{
    /// <inheritdoc />
    protected override void ProcessRecord() => WriteObject(Log.Logger);
}