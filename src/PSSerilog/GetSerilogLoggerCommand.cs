namespace PSSerilog;

using System.Management.Automation;
using Serilog;

[Cmdlet(VerbsCommon.Get, "SerilogLogger")]
[OutputType(typeof(ILogger))]
public class GetSerilogLoggerCommand : SerilogLoggerBase
{
    /// <inheritdoc />
    protected override void ProcessRecord() => this.WriteObject(this.GetLoggers(), true);
}