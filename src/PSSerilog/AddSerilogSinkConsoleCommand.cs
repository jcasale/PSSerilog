namespace PSSerilog;

using System;
using System.Management.Automation;

using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog.Templates;

[Cmdlet(VerbsCommon.Add, "SerilogSinkConsole", DefaultParameterSetName = nameof(OutputTemplate))]
[OutputType(typeof(LoggerConfiguration))]
public class AddSerilogSinkConsoleCommand : PSCmdlet
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
    public string OutputTemplate { get; set; } = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(OutputTemplate),
        HelpMessage = "The culture-specific formatting information.")]
    [ValidateNotNull]
    public IFormatProvider FormatProvider { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(OutputTemplate),
        HelpMessage = "The theme to apply to the styled output.")]
    [ValidateNotNull]
    public ConsoleTheme Theme { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(OutputTemplate),
        HelpMessage = "Applies the selected or default theme even when output redirection is detected.")]
    public SwitchParameter ApplyThemeToRedirectedOutput { get; set; }

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
        HelpMessage = "The expression template describing the format used to write to the sink.")]
    [ValidateNotNullOrEmpty]
    public string ExpressionTemplate { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The minimum level at which events will be passed to sinks. Ignored when level switch is specified.")]
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Verbose;

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The switch allowing the pass-through minimum level to be changed at runtime.")]
    [ValidateNotNull]
    public LoggingLevelSwitch LevelSwitch { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The level at which events will be written to standard error.")]
    public LogEventLevel? StandardErrorFromLevel { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        switch (ParameterSetName)
        {
            case nameof(OutputTemplate):

                Configuration.WriteTo.Console(
                    MinimumLevel,
                    OutputTemplate,
                    FormatProvider,
                    LevelSwitch,
                    StandardErrorFromLevel,
                    Theme,
                    ApplyThemeToRedirectedOutput);

                break;

            case nameof(Formatter):

                Configuration.WriteTo.Console(
                    Formatter,
                    MinimumLevel,
                    LevelSwitch,
                    StandardErrorFromLevel);

                break;

            case nameof(ExpressionTemplate):

                Configuration.WriteTo.Console(
                    new ExpressionTemplate(ExpressionTemplate),
                    MinimumLevel,
                    LevelSwitch,
                    StandardErrorFromLevel);

                break;

            default:

                throw new InvalidOperationException($"Unknown parameter set name: \"{ParameterSetName}\".");
        }

        WriteObject(Configuration);
    }
}