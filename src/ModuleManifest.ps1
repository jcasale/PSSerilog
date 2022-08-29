[CmdletBinding()]
param
(
    [Parameter(Position=0)]
    [string]
    $Path = (Join-Path $PSScriptRoot 'PSSerilog.psd1'),
    [Parameter(Position=1)]
    [Version]
    $Version
)

Set-StrictMode -Version Latest

if (Test-Path $Path)
{
    Remove-Item $Path -Force
}

if ($null -eq $Version)
{
    try
    {
        $result = & git.exe describe --tags --abbrev=0
    }
    catch
    {
        throw
    }

    if ($LASTEXITCODE -ne 0)
    {
        throw
    }

    $softwareVersion = $result.Split('-')[0].TrimStart('v')

    Write-Verbose ('Using git version {0}.' -f $softwareVersion)
}
else
{
    $softwareVersion = $Version.ToString()

    Write-Verbose ('Using specified version {0}.' -f $softwareVersion)
}

$content = @'
@{{
  RootModule = 'PSSerilog.dll'
  ModuleVersion = '{0}'
  GUID = '407659af-362f-47f3-946b-fc0cf70a94ce'
  Author = 'Joseph L. Casale'
  CompanyName = 'Joseph L. Casale'
  Copyright = '(c) Joseph L. Casale. All rights reserved.'
  Description = 'A PowerShell module for logging based on the Serilog library.'
  RequiredAssemblies = @('Serilog.dll')
  NestedModules = @()
  FunctionsToExport = @()
  CmdletsToExport = @(
    'Add-SerilogLogger',
    'Add-SerilogSinkConsole',
    'Add-SerilogSinkEmail',
    'Add-SerilogSinkEventLog',
    'Add-SerilogSinkFile',
    'Get-SerilogDefaultLogger',
    'Get-SerilogLogger',
    'New-SerilogBasicLogger',
    'New-SerilogGlobalContext',
    'New-SerilogLogContext',
    'New-SerilogLogger',
    'New-SerilogLoggerConfiguration',
    'New-SerilogLoggingLevelSwitch',
    'Set-SerilogDefaultLogger',
    'Stop-SerilogLogging'
  )
  VariablesToExport = @()
  AliasesToExport = @()
  PrivateData = @{{ PSData = @{{}} }}
}}
'@ -f $softwareVersion

Set-Content -Value $content -Path $Path