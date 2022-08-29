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
        if (Log.Logger != Serilog.Core.Logger.None)
        {
            this.ThrowTerminatingError(new ErrorRecord(
                new InvalidOperationException("The default logger is already set."),
                "DefaultLoggerAlreadySet",
                ErrorCategory.InvalidOperation,
                null));

            return;
        }

        Log.Logger = this.Logger;

        this.WriteObject(this.Logger);
    }
}