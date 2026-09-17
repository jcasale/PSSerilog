namespace PSSerilog;

using System.Globalization;
using System.Management.Automation;

using Serilog;
using Serilog.Core;

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
        HelpMessage = "The source context of the logger.")]
    [ValidateNotNullOrEmpty]
    public string SourceContext { get; set; }

    protected override void ProcessRecord()
    {
        var hasSourceContext = MyInvocation.BoundParameters.ContainsKey(nameof(SourceContext));

        var template = hasSourceContext
            ? "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{SourceContext}] [{Level}] {Message:l}{NewLine}{Exception}"
            : "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level}] {Message:l}{NewLine}{Exception}";

        var configuration = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: template, formatProvider: CultureInfo.InvariantCulture)
            .WriteTo.File(Path, outputTemplate: template, formatProvider: CultureInfo.InvariantCulture);

        if (hasSourceContext)
        {
            configuration.Enrich.WithProperty(Constants.SourceContextPropertyName, SourceContext);
        }

        var logger = configuration.CreateLogger();

        WriteObject(logger);
    }
}