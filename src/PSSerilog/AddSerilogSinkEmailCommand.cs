namespace PSSerilog;

using System;
using System.Management.Automation;
using System.Net;
using System.Net.Security;

using MailKit.Security;

using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.Email;

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
        HelpMessage = "The maximum delay between event batches. The default is 2 seconds.")]
    public TimeSpan? BufferingTimeLimit { get; set; }

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
            From = From,
            To = [.. To],
            Host = MailServer,
            Credentials = Credentials,
            IsBodyHtml = IsBodyHtml,
            ServerCertificateValidationCallback = ServerCertificateValidationCallback
        };

        if (Port is not null)
        {
            options.Port = Port.Value;
        }

        if (!string.IsNullOrWhiteSpace(Subject))
        {
            options.Subject = new MessageTemplateTextFormatter(Subject, FormatProvider);
        }

        if (!string.IsNullOrWhiteSpace(Body))
        {
            options.Body = new MessageTemplateTextFormatter(Body, FormatProvider);
        }

        if (ConnectionSecurity is not null)
        {
            options.ConnectionSecurity = ConnectionSecurity.Value;
        }

        var batchingOptions = new BatchingOptions();

        if (EagerlyEmitFirstEvent is not null)
        {
            batchingOptions.EagerlyEmitFirstEvent = EagerlyEmitFirstEvent.Value;
        }

        if (BatchSizeLimit is not null)
        {
            batchingOptions.BatchSizeLimit = BatchSizeLimit.Value;
        }

        if (BufferingTimeLimit is not null)
        {
            batchingOptions.BufferingTimeLimit = BufferingTimeLimit.Value;
        }

        if (QueueLimit is not null)
        {
            batchingOptions.QueueLimit = QueueLimit.Value;
        }

        Configuration.WriteTo.Email(
            options,
            batchingOptions,
            RestrictedToMinimumLevel,
            LevelSwitch);

        WriteObject(Configuration);
    }
}