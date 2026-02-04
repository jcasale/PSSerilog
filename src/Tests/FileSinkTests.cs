namespace Tests;

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

using PSSerilog;

[TestClass]
public class FileSinkTests
{
    [TestMethod]
    public void Logger_WithFileSink_ShouldWork()
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
                .AddParameter(nameof(NewSerilogLoggerConfigurationCommand.MinimumLevel), "Verbose")
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

        Assert.IsFalse(powerShell.HadErrors);
        Assert.ContainsSingle(results);
        Assert.EndsWith(expected, results.Single(), StringComparison.Ordinal);
    }
}