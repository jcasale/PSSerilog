namespace PSSerilog;

using System.Globalization;
using System.Management.Automation;

using Serilog;

[Cmdlet(VerbsCommon.New, "SerilogBasicLogger")]
[OutputType(typeof(ILogger))]
public class NewSerilogBasicLoggerCommand : PSCmdlet
{
    [Parameter(
        Position = 0,
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The path to the log file.")]
    [ValidateNotNullOrEmpty]
    public string Path { get; set; }

    [Parameter(
        Position = 1,
        Mandatory = false,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The message template describing the format used to write to the sink.")]
    [ValidateNotNullOrEmpty]
    public string OutputTemplate { get; set; } = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{SourceContext}] [{Level}] {Message:l}{NewLine}{Exception}";

    protected override void ProcessRecord()
    {
        var configuration = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: OutputTemplate, formatProvider: CultureInfo.InvariantCulture)
            .WriteTo.File(Path, outputTemplate: OutputTemplate, formatProvider: CultureInfo.InvariantCulture);

        var logger = configuration.CreateLogger();

        WriteObject(logger);
    }
}