namespace PSSerilog;

using System;
using System.Management.Automation;

using Serilog;
using Serilog.Core;

[Cmdlet(VerbsCommon.New, "SerilogLogger")]
[OutputType(typeof(ILogger))]
public class NewSerilogLoggerCommand : PSCmdlet
{
    [Parameter(
        Position = 0,
        Mandatory = true,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(Configuration),
        HelpMessage = "The logging configuration to create the logger from.")]
    [ValidateNotNull]
    public LoggerConfiguration Configuration { get; set; }

    [Parameter(
        Position = 0,
        Mandatory = true,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(SourceContext),
        HelpMessage = "The logger to create the new logger from that will be enriched with a source context.")]
    [ValidateNotNull]
    public ILogger Logger { get; set; }

    [Parameter(
        Position = 1,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(SourceContext),
        HelpMessage = "The source context of the logger.")]
    [ValidateNotNullOrEmpty]
    public string SourceContext { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        var logger = ParameterSetName switch
        {
            nameof(Configuration) => Configuration.CreateLogger(),
            nameof(SourceContext) => Logger.ForContext(Constants.SourceContextPropertyName, SourceContext),
            _ => throw new InvalidOperationException($"Unknown parameter set name: \"{ParameterSetName}\".")
        };

        WriteObject(logger);
    }
}