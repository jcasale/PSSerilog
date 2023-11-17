Set-StrictMode -Version Latest

Import-Module .\PSSerilog.psd1

$template = '[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level}] [{MyValue}] {Message:l}{NewLine}{Exception}'

$configuration = New-SerilogLoggerConfiguration -MinimumLevel Verbose -GlobalContext |
  Add-SerilogSinkConsole -OutputTemplate $template

$logger = New-SerilogLogger -Configuration $configuration

$context = New-SerilogGlobalContext -Name MyValue -Value 42
$logger.Information('Message 1')
$context.Dispose()

write-host ""

$logger.Information('Message 2')