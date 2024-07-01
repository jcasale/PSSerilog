Set-StrictMode -Version Latest

Import-Module .\PSSerilog.psd1

$template = '[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level}] [{MyValue}] {Message:l}{NewLine}{Exception}'

$configuration = New-SerilogLoggerConfiguration -MinimumLevel Verbose -Properties @{MyValue=42} |
  Add-SerilogSinkConsole -OutputTemplate $template

$logger = New-SerilogLogger -Configuration $configuration

$logger.Information('Message 1')
