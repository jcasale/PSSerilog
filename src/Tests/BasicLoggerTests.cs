namespace Tests;

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

[TestClass]
public class BasicLoggerTests
{
    [TestMethod]
    public void BasicLogger_ShouldWork()
    {
        var debugFilePath = Path.GetTempFileName();
        var logFilePath = Path.GetTempFileName();
        try
        {
            BasicLogger_ShouldWork(debugFilePath, logFilePath);
        }
        finally
        {
            File.Delete(debugFilePath);
            File.Delete(logFilePath);
        }
    }

    private static void BasicLogger_ShouldWork(string debugFilePath, string logFilePath)
    {
        if (string.IsNullOrWhiteSpace(debugFilePath))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(debugFilePath));
        }

        if (string.IsNullOrWhiteSpace(logFilePath))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(logFilePath));
        }

        var currentDirectory = Directory.GetCurrentDirectory();
        var modulePath = "./PSSerilog.psd1";
        var message = Guid.NewGuid().ToString();

        var script = ((FormattableString)$"""
            Set-StrictMode -Version Latest

            Import-Module '{modulePath}' -ErrorAction Stop

            $debugFile = [IO.File]::CreateText('{debugFilePath}')
            [Serilog.Debugging.SelfLog]::Enable([IO.TextWriter]::Synchronized($debugFile))

            $logger = New-SerilogBasicLogger -Path '{logFilePath}' -Name MyLogger

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

        var debugFileContent = File.ReadAllLines(debugFilePath);
        foreach (var line in debugFileContent)
        {
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Debug File: {0}", line));
        }

        var logFileContent = File.ReadAllLines(logFilePath);
        foreach (var line in logFileContent)
        {
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Log File: {0}", line));
        }

        foreach (var line in errorData)
        {
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Stdout: {0}", line));
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
}