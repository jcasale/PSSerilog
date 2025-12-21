namespace Tests;

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

using PSSerilog;

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
    public void LoggerWithFileSinkShouldWork()
    {
        const string key = "MyValue";
        const int value = 42;
        const string message = "Hello world!";
        var properties = new Hashtable { { key, value } };
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
                .AddParameter(nameof(NewSerilogLoggerConfigurationCommand.Properties), properties)
                .AddCommand("Add-SerilogSinkFile")
                .AddParameter(nameof(AddSerilogSinkFileCommand.Path), path)
                .AddParameter(nameof(AddSerilogSinkFileCommand.OutputTemplate), outputTemplate)
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