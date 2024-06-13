namespace Tests;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using MimeKit;

using PSSerilog;

using SmtpServer;
using SmtpServer.Authentication;
using SmtpServer.Storage;

using Xunit;

public class IntegrationTests
{
    [Fact]
    public void LoggerWithConsoleSinkShouldWork()
    {
        const string message = "Hello world!";
        const string outputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level}] {Message:l}{NewLine}{Exception}";
        var result = ((FormattableString)$" [Information] {message}\r\n").ToString(CultureInfo.InvariantCulture);

        var initialSessionState = InitialSessionState.CreateDefault();

        var entry1 = new SessionStateCmdletEntry("New-SerilogLoggerConfiguration", typeof(NewSerilogLoggerConfigurationCommand), null);
        initialSessionState.Commands.Add(entry1);

        var entry2 = new SessionStateCmdletEntry("New-SerilogLogger", typeof(NewSerilogLoggerCommand), null);
        initialSessionState.Commands.Add(entry2);

        var entry3 = new SessionStateCmdletEntry("Add-SerilogSinkConsole", typeof(AddSerilogSinkConsoleCommand), null);
        initialSessionState.Commands.Add(entry3);

        using var runSpace = RunspaceFactory.CreateRunspace(initialSessionState);
        using var powerShell = PowerShell.Create();

        runSpace.Open();
        powerShell.Runspace = runSpace;

        powerShell
            .AddCommand("Set-StrictMode")
            .AddParameter("Version", "Latest");

        powerShell
            .AddStatement()
            .AddCommand("New-SerilogLoggerConfiguration")
            .AddParameter(nameof(NewSerilogLoggerConfigurationCommand.MinimumLevel), Serilog.Events.LogEventLevel.Verbose)
            .AddCommand("Add-SerilogSinkConsole")
            .AddParameter(nameof(AddSerilogSinkFileCommand.OutputTemplate), outputTemplate)
            .AddCommand("New-SerilogLogger")
            .AddCommand("Set-Variable")
            .AddParameter("Name", "logger");

        powerShell
            .AddStatement()
            .AddScript(
                ((FormattableString)$$"""
                $originalOut = [Console]::Out
                $writer = [IO.StringWriter]::new()
                try
                {
                 [Console]::SetOut($writer)
                 $logger.Information('{{message}}')
                }
                finally
                {
                 [Console]::SetOut($originalOut)
                }

                $writer.ToString()
                """).ToString(CultureInfo.InvariantCulture));

        powerShell
            .AddStatement()
            .AddScript("$logger.Dispose()");

        var results = powerShell.Invoke<string>();

        Assert.False(powerShell.HadErrors);
        Assert.Single(results);
        Assert.EndsWith(result, results[0], StringComparison.Ordinal);
    }

    [Fact]
    public async Task LoggerWithEmailSinkShouldWork()
    {
        const string sender = "sender@domain.com";
        const string recipient = "recipient@domain.com";
        const string server = "localhost";
        const int port = 25000;
        const string message = "Hello world!";
        var result = string.Format(CultureInfo.InvariantCulture, " [Information] {0}", message);

        using var cancellationTokenSource = new CancellationTokenSource();

        Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

        var messages = new List<MimeMessage>();
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IUserAuthenticator>(new TestUserAuthenticator())
            .AddSingleton<IMailboxFilter>(new TestMailboxFilter())
            .AddSingleton<IMessageStore>(new TestMessageStore(messages))
            .BuildServiceProvider();

        var options = new SmtpServerOptionsBuilder()
            .ServerName(server)
            .Endpoint(builder => builder.Endpoint(new IPEndPoint(IPAddress.Loopback, port)))
            .Build();

        var smtpServer = new SmtpServer(options, serviceProvider);
        smtpServer.SessionFaulted += (_, args) =>
        {
            Debug.WriteLine(args.Exception);
            cancellationTokenSource.Cancel();
        };

        var smtpServerTask = smtpServer.StartAsync(cancellationTokenSource.Token);

        var initialSessionState = InitialSessionState.CreateDefault();

        var entry1 = new SessionStateCmdletEntry("New-SerilogLoggerConfiguration", typeof(NewSerilogLoggerConfigurationCommand), null);
        initialSessionState.Commands.Add(entry1);

        var entry2 = new SessionStateCmdletEntry("New-SerilogLogger", typeof(NewSerilogLoggerCommand), null);
        initialSessionState.Commands.Add(entry2);

        var entry3 = new SessionStateCmdletEntry("Add-SerilogSinkEmail", typeof(AddSerilogSinkEmailCommand), null);
        initialSessionState.Commands.Add(entry3);

        using var runSpace = RunspaceFactory.CreateRunspace(initialSessionState);
        using var powerShell = PowerShell.Create();

        runSpace.Open();
        powerShell.Runspace = runSpace;

        powerShell
            .AddCommand("Set-StrictMode")
            .AddParameter("Version", "Latest");

        powerShell
            .AddStatement()
            .AddCommand("New-SerilogLoggerConfiguration")
            .AddParameter(nameof(NewSerilogLoggerConfigurationCommand.MinimumLevel), Serilog.Events.LogEventLevel.Verbose)
            .AddCommand("Add-SerilogSinkEmail")
            .AddParameter(nameof(AddSerilogSinkEmailCommand.From), sender)
            .AddParameter(nameof(AddSerilogSinkEmailCommand.To), recipient)
            .AddParameter(nameof(AddSerilogSinkEmailCommand.MailServer), server)
            .AddParameter(nameof(AddSerilogSinkEmailCommand.Port), port)
            .AddCommand("New-SerilogLogger")
            .AddCommand("Set-Variable")
            .AddParameter("Name", "logger");

        powerShell
            .AddStatement()
            .AddScript(((FormattableString)$"$logger.Information('{message}')").ToString(CultureInfo.InvariantCulture));

        powerShell
            .AddStatement()
            .AddScript("$logger.Dispose()");

        powerShell.Invoke();

        Assert.False(powerShell.HadErrors);

        var stopWatch = Stopwatch.StartNew();
        while (stopWatch.ElapsedMilliseconds < 5000 && messages.Count == 0)
        {
            await Task.Delay(100, cancellationTokenSource.Token);
            Debug.WriteLine($"Messages Count: {messages.Count}");
        }

        stopWatch.Stop();

        Assert.Single(messages);
        Assert.EndsWith(result, messages[0].TextBody, StringComparison.Ordinal);

        smtpServer.Shutdown();
        await smtpServerTask;
    }

    [Fact]
    public void LoggerWithFileSinkShouldWork()
    {
        const string key = "MyValue";
        const int value = 42;
        const string message = "Hello world!";
        var outputTemplate = string.Format(
            CultureInfo.InvariantCulture,
            "[{{Timestamp:yyyy-MM-dd HH:mm:ss.fff}}] [{{Level}}] [{{{0}}}] {{Message:l}}{{NewLine}}{{Exception}}",
            key);
        var result = string.Format(
            CultureInfo.InvariantCulture,
            " [Information] [{0}] {1}",
            value,
            message);

        var initialSessionState = InitialSessionState.CreateDefault();

        var entry1 = new SessionStateCmdletEntry("New-SerilogLoggerConfiguration", typeof(NewSerilogLoggerConfigurationCommand), null);
        initialSessionState.Commands.Add(entry1);

        var entry2 = new SessionStateCmdletEntry("New-SerilogLogger", typeof(NewSerilogLoggerCommand), null);
        initialSessionState.Commands.Add(entry2);

        var entry3 = new SessionStateCmdletEntry("Add-SerilogSinkFile", typeof(AddSerilogSinkFileCommand), null);
        initialSessionState.Commands.Add(entry3);

        var entry4 = new SessionStateCmdletEntry("New-SerilogGlobalContext", typeof(NewSerilogGlobalContextCommand), null);
        initialSessionState.Commands.Add(entry4);

        using var runSpace = RunspaceFactory.CreateRunspace(initialSessionState);
        using var powerShell = PowerShell.Create();

        runSpace.Open();
        powerShell.Runspace = runSpace;

        var path = Path.GetTempFileName();
        string[] results;
        try
        {
            powerShell
                .AddCommand("Set-StrictMode")
                .AddParameter("Version", "Latest");

            powerShell
                .AddStatement()
                .AddCommand("New-SerilogLoggerConfiguration")
                .AddParameter(nameof(NewSerilogLoggerConfigurationCommand.MinimumLevel), Serilog.Events.LogEventLevel.Verbose)
                .AddParameter(nameof(NewSerilogLoggerConfigurationCommand.GlobalContext))
                .AddCommand("Add-SerilogSinkFile")
                .AddParameter(nameof(AddSerilogSinkFileCommand.Path), path)
                .AddParameter(nameof(AddSerilogSinkFileCommand.OutputTemplate), outputTemplate)
                .AddCommand("New-SerilogLogger")
                .AddCommand("Set-Variable")
                .AddParameter("Name", "logger");

            powerShell
                .AddStatement()
                .AddCommand("New-SerilogGlobalContext")
                .AddParameter(nameof(NewSerilogGlobalContextCommand.Name), key)
                .AddParameter(nameof(NewSerilogGlobalContextCommand.Value), value)
                .AddCommand("Set-Variable")
                .AddParameter("Name", "context");

            powerShell
                .AddStatement()
                .AddScript(((FormattableString)$"$logger.Information('{message}')").ToString(CultureInfo.InvariantCulture));

            powerShell
                .AddStatement()
                .AddScript("$context.Dispose()");

            powerShell
                .AddStatement()
                .AddScript("$logger.Dispose()");

            powerShell.Invoke();

            results = File.ReadAllLines(path);
        }
        finally
        {
            File.Delete(path);
        }

        Assert.False(powerShell.HadErrors);
        Assert.Single(results);
        Assert.EndsWith(result, results[0], StringComparison.Ordinal);
    }
}