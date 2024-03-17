namespace PSSerilog;

using System;
using System.Management.Automation;

using Serilog;

[Cmdlet(VerbsCommon.Set, "SerilogDefaultLogger")]
[OutputType(typeof(ILogger))]
public class SetSerilogDefaultLoggerCommand : PSCmdlet
{
    [Parameter(
        Position = 0,
        Mandatory = true,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The logger to set as the default.")]
    [ValidateNotNull]
    public ILogger Logger { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        if (Log.Logger == Serilog.Core.Logger.None)
        {
            Log.Logger = this.Logger;
        }
        else
        {
            this.WriteError(new ErrorRecord(
                new InvalidOperationException("The default logger is already set."),
                "DefaultLoggerAlreadySet",
                ErrorCategory.InvalidOperation,
                null));
        }

        this.WriteObject(this.Logger);
    }
}