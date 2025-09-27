namespace PSSerilog;

using System;
using System.Management.Automation;

using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.EventLog;
using Serilog.Templates;

[Cmdlet(VerbsCommon.Add, "SerilogSinkEventLog", DefaultParameterSetName = nameof(OutputTemplate))]
[OutputType(typeof(LoggerConfiguration))]
public class AddSerilogSinkEventLogCommand : PSCmdlet
{
    [Parameter(
        Position = 0,
        Mandatory = true,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The logging configuration to add the sink to.")]
    [ValidateNotNull]
    public LoggerConfiguration Configuration { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(OutputTemplate),
        HelpMessage = "The message template describing the format used to write to the sink.")]
    [ValidateNotNullOrEmpty]
    public string OutputTemplate { get; set; } = "{Message}{NewLine}{Exception}";

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(OutputTemplate),
        HelpMessage = "The culture-specific formatting information.")]
    [ValidateNotNull]
    public IFormatProvider FormatProvider { get; set; }

    [Parameter(
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(Formatter),
        HelpMessage = "The formatter to convert the log events into text for the file.")]
    [ValidateNotNull]
    public ITextFormatter Formatter { get; set; }

    [Parameter(
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(ExpressionTemplate),
        HelpMessage = "The message expression template describing the format used to write to the sink.")]
    [ValidateNotNullOrEmpty]
    public string ExpressionTemplate { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The source name by which the application is registered on the local computer.")]
    [ValidateNotNullOrEmpty]
    public string Source { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log.")]
    [ValidateNotNullOrEmpty]
    public string LogName { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The name of the machine hosting the event log written to. Defaults to the local machine.")]
    [ValidateNotNullOrEmpty]
    public string MachineName { get; set; } = ".";

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables the creation of the event source.")]
    public SwitchParameter ManageEventSource { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The minimum level at which events will be passed to sinks. Ignored when level switch is specified.")]
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Verbose;

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Supplies event ids for emitted log events. Defaults to a custom provider that expects an 'EventId' context property.")]
    public IEventIdProvider EventIdProvider { get; set; } = new EventIdProvider();

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        switch (ParameterSetName)
        {
            case nameof(OutputTemplate):

                Configuration.WriteTo.EventLog(
                    Source,
                    LogName,
                    MachineName,
                    ManageEventSource,
                    OutputTemplate,
                    FormatProvider,
                    MinimumLevel,
                    EventIdProvider);

                break;

            case nameof(Formatter):

                Configuration.WriteTo.EventLog(
                    Formatter,
                    Source,
                    LogName,
                    MachineName,
                    ManageEventSource,
                    MinimumLevel,
                    EventIdProvider);

                break;

            case nameof(ExpressionTemplate):

                Configuration.WriteTo.EventLog(
                    new ExpressionTemplate(ExpressionTemplate),
                    Source,
                    LogName,
                    MachineName,
                    ManageEventSource,
                    MinimumLevel,
                    EventIdProvider);

                break;

            default:

                throw new InvalidOperationException($"Unknown parameter set name: \"{ParameterSetName}\".");
        }

        WriteObject(Configuration);
    }
}