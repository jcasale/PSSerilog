namespace PSSerilog;

using System.Management.Automation;

using Serilog.Core;
using Serilog.Events;

[Cmdlet(VerbsCommon.New, "SerilogLoggingLevelSwitch")]
[OutputType(typeof(LoggingLevelSwitch))]
public class NewSerilogLoggingLevelSwitchCommand : PSCmdlet
{
    [Parameter(
        Position = 0,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The initial level to which the switch is set.")]
    public LogEventLevel? MinimumLevel { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        var levelSwitch = MinimumLevel is null
            ? new LoggingLevelSwitch()
            : new LoggingLevelSwitch(MinimumLevel.Value);

        WriteObject(levelSwitch);
    }
}