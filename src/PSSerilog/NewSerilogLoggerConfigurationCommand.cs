namespace PSSerilog;

using System;
using System.Management.Automation;

using Serilog;
using Serilog.Events;

[Cmdlet(VerbsCommon.New, "SerilogLoggerConfiguration")]
[OutputType(typeof(LoggerConfiguration))]
public class NewSerilogLoggerConfigurationCommand : PSCmdlet
{
    [Parameter(
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Configures the minimum level at which events will be passed to sinks (default Information level).")]
    [ValidateNotNull]
    public LogEventLevel? MinimumLevel { get; set; }

    [Parameter(
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables the enrichment of log events with properties from log context.")]
    public SwitchParameter LogContext { get; set; }

    [Parameter(
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables the enrichment of log events with properties from global context.")]
    public SwitchParameter GlobalContext { get; set; }

    [Parameter(
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables the enrichment of log events with the machine name.")]
    public SwitchParameter MachineName { get; set; }

    [Parameter(
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables the enrichment of log events with the user name.")]
    public SwitchParameter EnvironmentUserName { get; set; }

    [Parameter(
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables the enrichment of log events with the process id.")]
    public SwitchParameter ProcessId { get; set; }

    [Parameter(
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables the enrichment of log events with the thread id.")]
    public SwitchParameter ThreadId { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        var configuration = new LoggerConfiguration();

        if (this.LogContext.IsPresent)
        {
            configuration.Enrich.FromLogContext();
        }

        if (this.GlobalContext.IsPresent)
        {
            configuration.Enrich.FromGlobalLogContext();
        }

        if (this.MachineName.IsPresent)
        {
            configuration.Enrich.WithMachineName();
        }

        if (this.EnvironmentUserName.IsPresent)
        {
            configuration.Enrich.WithEnvironmentUserName();
        }

        if (this.ProcessId.IsPresent)
        {
            configuration.Enrich.WithProcessId();
        }

        if (this.ThreadId.IsPresent)
        {
            configuration.Enrich.WithThreadId();
        }

        if (this.MinimumLevel is not null)
        {
            switch (this.MinimumLevel.Value)
            {
                case LogEventLevel.Verbose:

                    configuration.MinimumLevel.Verbose();

                    break;

                case LogEventLevel.Debug:

                    configuration.MinimumLevel.Debug();

                    break;

                case LogEventLevel.Information:

                    configuration.MinimumLevel.Information();

                    break;

                case LogEventLevel.Warning:

                    configuration.MinimumLevel.Warning();

                    break;

                case LogEventLevel.Error:

                    configuration.MinimumLevel.Error();

                    break;

                case LogEventLevel.Fatal:

                    configuration.MinimumLevel.Fatal();

                    break;

                default:

                    throw new InvalidOperationException();
            }
        }

        this.WriteObject(configuration);
    }
}