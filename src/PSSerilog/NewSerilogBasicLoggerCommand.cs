namespace PSSerilog;

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
    public string Name { get; set; }

    protected override void ProcessRecord()
    {
        var template = string.IsNullOrWhiteSpace(this.Name)
            ? "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level}] {Message:l}{NewLine}{Exception}"
            : "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{SourceContext}] [{Level}] {Message:l}{NewLine}{Exception}";

        var configuration = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: template)
            .WriteTo.File(this.Path, outputTemplate: template);

        var logger = string.IsNullOrWhiteSpace(this.Name)
            ? configuration.CreateLogger()
            : configuration.CreateLogger().ForContext(Constants.SourceContextPropertyName, this.Name);

        this.WriteObject(logger);
    }
}