namespace PSSerilog;

using System.Management.Automation;
using Serilog;
using Serilog.Core;

[Cmdlet(VerbsCommon.Add, "SerilogLogger")]
[OutputType(typeof(ILogger))]
public class AddSerilogLoggerCommand : SerilogLoggerBase
{
    [Parameter(
        Position = 0,
        Mandatory = true,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The logger to add to session state and begin tracking.")]
    [ValidateNotNull]
    public Logger Logger { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        this.AddLogger(this.Logger);

        this.WriteObject(this.Logger);
    }
}