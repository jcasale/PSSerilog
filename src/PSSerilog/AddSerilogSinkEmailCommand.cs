namespace PSSerilog;

using System;
using System.Management.Automation;
using System.Net;
using System.Net.Security;
using MailKit.Security;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.Email;
using Serilog.Sinks.PeriodicBatching;

[Cmdlet(VerbsCommon.Add, "SerilogSinkEmail")]
[OutputType(typeof(LoggerConfiguration))]
public class AddSerilogSinkEmailCommand : PSCmdlet
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
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The email address emails will be sent from.")]
    [ValidateNotNullOrEmpty]
    public string From { get; set; }

    [Parameter(
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The email address(es) emails will be sent to.")]
    [ValidateNotNullOrEmpty]
    public string[] To { get; set; }

    [Parameter(
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The SMTP email server to use.")]
    [ValidateNotNullOrEmpty]
    public string MailServer { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The port used for the connection. The default is 25.")]
    public int? Port { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The network credentials to use to authenticate with the mail server.")]
    public ICredentialsByHost Credentials { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "A message template describing the email subject. The default is \"Log Messages\".")]
    public string Subject { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "A message template describing the format of the email body. The default is \"{Timestamp} [{Level}] {Message}{NewLine}{Exception}\"..")]
    public string Body { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Sets whether the body contents of the email is HTML. The default is false.")]
    public bool IsBodyHtml { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The security applied to the SMTP connection. The default is Auto.")]
    public SecureSocketOptions? ConnectionSecurity { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Provides a method that validates server certificates.")]
    public RemoteCertificateValidationCallback ServerCertificateValidationCallback { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The culture-specific formatting information.")]
    public IFormatProvider FormatProvider { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The minimum level at which events will be passed to sinks. Ignored when level switch is specified.")]
    public LogEventLevel RestrictedToMinimumLevel { get; set; } = LogEventLevel.Verbose;

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The switch allowing the pass-through minimum level to be changed at runtime.")]
    public LoggingLevelSwitch LevelSwitch { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Eagerly emit a batch containing the first received event, regardless of the target batch size or batching time. This helps with perceived liveness when running/debugging applications interactively. The default is true.")]
    public bool? EagerlyEmitFirstEvent { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The maximum number of events to include in a single batch. The default is 1000.")]
    public int? BatchSizeLimit { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The time to wait between checking for event batches. The default is 2 seconds.")]
    public TimeSpan? Period { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Maximum number of events to hold in the sink's internal queue, or null for an unbounded queue. The default is 100000.")]
    public int? QueueLimit { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        var options = new EmailSinkOptions
        {
            From = this.From,
            To = [..this.To],
            Host = this.MailServer,
            Credentials = this.Credentials,
            IsBodyHtml = this.IsBodyHtml,
            ServerCertificateValidationCallback = this.ServerCertificateValidationCallback
        };

        if (this.Port is not null)
        {
            options.Port = this.Port.Value;
        }

        if (!string.IsNullOrWhiteSpace(this.Subject))
        {
            options.Subject = new MessageTemplateTextFormatter(this.Subject, this.FormatProvider);
        }

        if (!string.IsNullOrWhiteSpace(this.Body))
        {
            options.Body = new MessageTemplateTextFormatter(this.Body, this.FormatProvider);
        }

        if (this.ConnectionSecurity is not null)
        {
            options.ConnectionSecurity = this.ConnectionSecurity.Value;
        }

        var batchingOptions = new PeriodicBatchingSinkOptions();

        if (this.EagerlyEmitFirstEvent is not null)
        {
            batchingOptions.EagerlyEmitFirstEvent = this.EagerlyEmitFirstEvent.Value;
        }

        if (this.BatchSizeLimit is not null)
        {
            batchingOptions.BatchSizeLimit = this.BatchSizeLimit.Value;
        }

        if (this.Period is not null)
        {
            batchingOptions.Period = this.Period.Value;
        }

        if (this.QueueLimit is not null)
        {
            batchingOptions.QueueLimit = this.QueueLimit.Value;
        }

        this.Configuration.WriteTo.Email(
            options,
            batchingOptions,
            this.RestrictedToMinimumLevel,
            this.LevelSwitch);

        this.WriteObject(this.Configuration);
    }
}