namespace Tests;

using System;
using System.Collections;
using System.Globalization;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

using PSSerilog;

[TestClass]
public class ConsoleSinkTests
{
    [TestMethod]
    public void Logger_WithConsoleSink_ShouldWork()
    {
        const string key = "MyValue";
        const int value = 42;
        const string message = "Hello world!";
        var properties = new Hashtable { { key, value } };
        var outputTemplate = ((FormattableString)$$"""[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level}] [{{{key}}}] {Message:l}{NewLine}{Exception}""").ToString(CultureInfo.InvariantCulture);
        var expected = ((FormattableString)$" [Information] [{value}] {message}").ToString(CultureInfo.InvariantCulture);

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
            .AddParameter(nameof(NewSerilogLoggerConfigurationCommand.MinimumLevel), "Verbose")
            .AddParameter(nameof(NewSerilogLoggerConfigurationCommand.Properties), properties)
            .AddCommand("Add-SerilogSinkConsole")
            .AddParameter(nameof(AddSerilogSinkConsoleCommand.OutputTemplate), outputTemplate)
            .AddCommand("New-SerilogLogger")
            .AddCommand("Set-Variable")
            .AddParameter("Name", "logger");

        powerShell
            .AddStatement()
            .AddScript("$writer = [IO.StringWriter]::new()")
            .AddScript("[Console]::SetOut($writer)");

        powerShell
            .AddStatement()
            .AddScript(((FormattableString)$"$logger.Information('{message}')").ToString(CultureInfo.InvariantCulture));

        powerShell
            .AddStatement()
            .AddScript("$writer.ToString()");

        powerShell
            .AddStatement()
            .AddScript("$logger.Dispose()");

        var results = powerShell.Invoke<string>();

        Assert.IsFalse(powerShell.HadErrors);
        Assert.ContainsSingle(results);
        Assert.EndsWith(expected, results.Single().TrimEnd(), StringComparison.Ordinal);
    }
}