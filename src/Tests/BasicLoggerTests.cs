namespace Tests;

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

[TestClass]
public sealed class BasicLoggerTests : IDisposable
{
    private readonly string _debugFilePath = Path.GetTempFileName();
    private readonly string _logFilePath = Path.GetTempFileName();

    public TestContext TestContext { get; set; }

    [TestMethod]
    public void BasicLogger_ShouldWork()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var modulePath = "./PSSerilog.psd1";
        var message = Guid.NewGuid().ToString();

        var script = ((FormattableString)$"""
            Set-StrictMode -Version Latest

            Import-Module '{modulePath}' -ErrorAction Stop

            $debugFile = [IO.File]::CreateText('{_debugFilePath}')
            [Serilog.Debugging.SelfLog]::Enable([IO.TextWriter]::Synchronized($debugFile))

            $logger = New-SerilogBasicLogger -Path '{_logFilePath}'

            $logger.Information('{message}')

            $logger.Dispose()
            """).ToString(CultureInfo.InvariantCulture);

        var scriptBytes = Encoding.Unicode.GetBytes(script);
        var encodedCommand = Convert.ToBase64String(scriptBytes);
        var arguments = string.Format(
            CultureInfo.InvariantCulture,
            "-NoLogo -NoProfile -NonInteractive -ExecutionPolicy Bypass -EncodedCommand {0}",
            encodedCommand);

        var errorData = new ConcurrentQueue<string>();
        var outputData = new ConcurrentQueue<string>();
        using var process = new Process();

        process.StartInfo.FileName = "powershell.exe";
        process.StartInfo.Arguments = arguments;
        process.StartInfo.WorkingDirectory = currentDirectory;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.ErrorDataReceived += OnErrorDataReceived;
        process.OutputDataReceived += OnOutputDataReceived;

        process.Start();

        process.BeginErrorReadLine();
        process.BeginOutputReadLine();

        process.WaitForExit();

        var debugFileContent = File.ReadAllLines(_debugFilePath);
        foreach (var line in debugFileContent)
        {
            TestContext.WriteLine(string.Format(CultureInfo.InvariantCulture, "Debug File: {0}", line));
        }

        var logFileContent = File.ReadAllLines(_logFilePath);
        foreach (var line in logFileContent)
        {
            TestContext.WriteLine(string.Format(CultureInfo.InvariantCulture, "Log File: {0}", line));
        }

        foreach (var line in errorData)
        {
            TestContext.WriteLine(string.Format(CultureInfo.InvariantCulture, "Stderr: {0}", line));
        }

        Assert.AreEqual(0, process.ExitCode);

        Assert.IsEmpty(debugFileContent);

        Assert.ContainsSingle(logFileContent);
        Assert.EndsWith(message, logFileContent.Single(), StringComparison.Ordinal);

        Assert.IsEmpty(errorData);

        Assert.ContainsSingle(outputData);
        Assert.EndsWith(message, outputData.Single(), StringComparison.Ordinal);

        return;

        void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data))
            {
                return;
            }

            errorData.Enqueue(e.Data);
        }

        void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data))
            {
                return;
            }

            outputData.Enqueue(e.Data);
        }
    }

    public void Dispose()
    {
        File.Delete(_debugFilePath);
        File.Delete(_logFilePath);
    }
}