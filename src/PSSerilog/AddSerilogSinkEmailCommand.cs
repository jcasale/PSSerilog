namespace PSSerilog;

using System;
using System.Management.Automation;
using System.Net;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.Email;
using Serilog.Templates;

[Cmdlet(VerbsCommon.Add, "SerilogSinkEmail", DefaultParameterSetName = nameof(OutputTemplate))]
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
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = nameof(OutputTemplate),
        HelpMessage = "The message template describing the format used to write to the sink.")]
    [ValidateNotNullOrEmpty]
    public string OutputTemplate { get; set; } = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";

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
        ParameterSetName = nameof(ExpressionTemplate),
        HelpMessage = "The expression template describing the format used to write to the sink.")]
    [ValidateNotNullOrEmpty]
    public string ExpressionTemplate { get; set; }

    [Parameter(
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The email address emails will be sent from.")]
    public string FromEmail { get; set; }

    [Parameter(
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The email address(es) emails will be sent to.")]
    public string[] ToEmail { get; set; }

    [Parameter(
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The SMTP email server to use.")]
    public string MailServer { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The port used for the connection. Default is 25.")]
    public int Port { get; set; } = 25;

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables the use of SSL in the SMTP client.")]
    public SwitchParameter EnableSsl { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Enables HTML in the body of the email.")]
    public SwitchParameter IsBodyHtml { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The credentials used for authentication.")]
    public NetworkCredential NetworkCredential { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The maximum number of events to post in a single batch.")]
    public int BatchPostingLimit { get; set; } = 100;

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The time to wait between checking for event batches.")]
    public TimeSpan? Period { get; set; }

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The subject, can be a plain string or a template such as {Timestamp} [{Level}] occurred.")]
    public string MailSubject { get; set; } = "Log Email";

    [Parameter(
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The minimum level at which events will be passed to sinks. Ignored when level switch is specified.")]
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Verbose;

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        var emailConnectionInfo = new EmailConnectionInfo
        {
            NetworkCredentials = this.NetworkCredential,
            Port = this.Port,
            FromEmail = this.FromEmail,
            ToEmail = string.Join(";", this.ToEmail),
            EmailSubject = this.MailSubject,
            EnableSsl = this.EnableSsl,
            MailServer = this.MailServer,
            IsBodyHtml = this.IsBodyHtml
        };

        switch (this.ParameterSetName)
        {
            case nameof(this.OutputTemplate):

                this.Configuration.WriteTo.Email(
                    emailConnectionInfo,
                    this.OutputTemplate,
                    this.MinimumLevel,
                    this.BatchPostingLimit,
                    this.Period,
                    this.FormatProvider,
                    this.MailSubject);

                break;

            case nameof(this.Formatter):

                this.Configuration.WriteTo.Email(
                    emailConnectionInfo,
                    this.Formatter,
                    this.MinimumLevel,
                    this.BatchPostingLimit,
                    this.Period,
                    this.MailSubject);

                break;

            case nameof(this.ExpressionTemplate):

                this.Configuration.WriteTo.Email(
                    emailConnectionInfo,
                    new ExpressionTemplate(this.ExpressionTemplate),
                    this.MinimumLevel,
                    this.BatchPostingLimit,
                    this.Period,
                    this.MailSubject);

                break;

            default:

                throw new InvalidOperationException($"Unknown parameter set name: \"{this.ParameterSetName}\".");
        }

        this.WriteObject(this.Configuration);
    }
}