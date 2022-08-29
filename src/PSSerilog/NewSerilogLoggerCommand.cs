namespace PSSerilog;

using System;
using System.Management.Automation;
using Serilog;
using Serilog.Core;

[Cmdlet(VerbsCommon.New, "SerilogLogger", DefaultParameterSetName = ParameterAttribute.AllParameterSets)]
[OutputType(typeof(ILogger))]
public class NewSerilogLoggerCommand : PSCmdlet
{
    [Parameter(
        Position = 0,
        Mandatory = true,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The logging configuration to create the logger from.")]
    [ValidateNotNull]
    public LoggerConfiguration Configuration { get; set; }

    [Parameter(
        Position = 1,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(Name),
        HelpMessage = "The source context of the logger.")]
    [ValidateNotNullOrEmpty]
    public string Name { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        var logger = this.ParameterSetName switch
        {
            ParameterAttribute.AllParameterSets => this.Configuration.CreateLogger(),
            nameof(this.Name) => this.Configuration.CreateLogger().ForContext(Constants.SourceContextPropertyName, this.Name),
            _ => throw new InvalidOperationException($"Unknown parameter set name: \"{this.ParameterSetName}\".")
        };

        this.WriteObject(logger);
    }
}