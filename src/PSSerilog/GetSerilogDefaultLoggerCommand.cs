namespace PSSerilog;

using System;
using System.Management.Automation;

using Serilog;
using Serilog.Core;

[Cmdlet(VerbsCommon.Get, "SerilogDefaultLogger")]
[OutputType(typeof(ILogger))]
public class GetSerilogDefaultLoggerCommand : PSCmdlet
{
    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Indicates that this cmdlet throws a non-terminating error if the static logger has not been overridden from the default \"SilentLogger\" instance.")]
    public SwitchParameter ExcludeSilentLogger { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        if (ExcludeSilentLogger.IsPresent && Log.Logger == Logger.None)
        {
            WriteError(new(new InvalidOperationException("The default logger has not been set."), "DefaultLoggerNotSet", ErrorCategory.InvalidOperation, null));

            return;
        }

        WriteObject(Log.Logger);
    }
}