namespace PSSerilog;

using System;
using System.Collections;
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

    [Parameter(
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables the enrichment of log events with properties.")]
    public Hashtable Properties { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        var configuration = new LoggerConfiguration();

        if (LogContext.IsPresent)
        {
            configuration.Enrich.FromLogContext();
        }

        if (MachineName.IsPresent)
        {
            configuration.Enrich.WithMachineName();
        }

        if (EnvironmentUserName.IsPresent)
        {
            configuration.Enrich.WithEnvironmentUserName();
        }

        if (ProcessId.IsPresent)
        {
            configuration.Enrich.WithProcessId();
        }

        if (ThreadId.IsPresent)
        {
            configuration.Enrich.WithThreadId();
        }

        if (Properties is not null)
        {
            foreach (DictionaryEntry entry in Properties)
            {
                configuration.Enrich.WithProperty(entry.Key.ToString(), entry.Value);
            }
        }

        if (MinimumLevel is not null)
        {
            switch (MinimumLevel.Value)
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

        WriteObject(configuration);
    }
}