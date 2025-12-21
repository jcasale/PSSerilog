namespace PSSerilog;

using System;
using System.Management.Automation;
using System.Text;

using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.File;

[Cmdlet(VerbsCommon.Add, "SerilogSinkFile", DefaultParameterSetName = nameof(OutputTemplate))]
[OutputType(typeof(LoggerConfiguration))]
public class AddSerilogSinkFileCommand : PSCmdlet
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
    public string OutputTemplate { get; set; } = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

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
        HelpMessage = "The path to the file.")]
    [ValidateNotNullOrEmpty]
    public string Path { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The minimum level at which events will be passed to sinks. Ignored when level switch is specified.")]
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Verbose;

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The approximate maximum size, in bytes, to which a log file will be allowed to grow. For unrestricted growth, pass null. The default is 1 GB.")]
    public long? FileSizeLimitBytes { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The switch allowing the pass-through minimum level to be changed at runtime.")]
    [ValidateNotNull]
    public LoggingLevelSwitch LevelSwitch { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables buffered output flushing.")]
    public SwitchParameter Buffered { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables sharing of the output file.")]
    public SwitchParameter Shared { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The flushing interval.")]
    public TimeSpan? FlushToDiskInterval { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The rolling interval. Defaults to infinite.")]
    public RollingInterval RollingInterval { get; set; } = RollingInterval.Infinite;

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables rolling on file size limit.")]
    public SwitchParameter RollOnFileSizeLimit { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The maximum number of log files that will be retained, including the current log file. For unlimited retention, pass null. The default is 31.")]
    public int? RetainedFileCountLimit { get; set; } = 31;

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The character encoding used to write the text file. The default is UTF-8 without BOM.")]
    public Encoding Encoding { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The character encoding used to write the text file. The default is UTF-8 without BOM.")]
    public FileLifecycleHooks Hooks { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The maximum time after the end of an interval that a rolling log file will be retained. Must be greater than or equal to 0.")]
    public TimeSpan? RetainedFileTimeLimit { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        switch (ParameterSetName)
        {
            case nameof(OutputTemplate):

                Configuration.WriteTo.File(
                    Path,
                    MinimumLevel,
                    OutputTemplate,
                    FormatProvider,
                    FileSizeLimitBytes,
                    LevelSwitch,
                    Buffered,
                    Shared,
                    FlushToDiskInterval,
                    RollingInterval,
                    RollOnFileSizeLimit,
                    RetainedFileCountLimit,
                    Encoding,
                    Hooks,
                    RetainedFileTimeLimit);

                break;

            case nameof(Formatter):

                Configuration.WriteTo.File(
                    Formatter,
                    Path,
                    MinimumLevel,
                    FileSizeLimitBytes,
                    LevelSwitch,
                    Buffered,
                    Shared,
                    FlushToDiskInterval,
                    RollingInterval,
                    RollOnFileSizeLimit,
                    RetainedFileCountLimit,
                    Encoding,
                    Hooks,
                    RetainedFileTimeLimit);

                break;

            default:

                throw new InvalidOperationException($"Unknown parameter set name: \"{ParameterSetName}\".");
        }

        WriteObject(Configuration);
    }
}