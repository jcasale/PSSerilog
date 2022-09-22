# PowerShell Serilog Logging Module

The `PSSerilog` module provides logging based on the Serilog library.

## Installation

The module is distributed as a Windows Installer package (the PowerShell Gallery is not suitable for some enterprises).

Run the installer manually or in unattended mode:

```bat
msiexec.exe /i ps-serilog.msi /qn
```

The default installation path is:

```bat
%ProgramFiles%\WindowsPowerShell\Modules\PSSerilog
```

## Documentation

Use `Get-Command` and `Get-Help` to enumerate the cmdlets with this module and obtain their documentation:

```powershell
Get-Command -Module PSSerilog
Get-Help New-SerilogLoggerConfiguration -Full
```

## Examples

- Create a basic logger using a sane pattern:

    ```powershell
    try
    {
        $name = [IO.Path]::GetFileNameWithoutExtension($MyInvocation.MyCommand.Name)
        $path = [IO.Path]::ChangeExtension($MyInvocation.MyCommand.Path, '.log')
        $logger = New-SerilogBasicLogger -Name $name -Path $path -ErrorAction Stop
    }
    catch
    {
        throw
    }

    function main
    {
        [CmdletBinding()]
        param()

        $logger.Information('Executing script...')

        # Your code follows.
    }

    try
    {
        main -ErrorAction Stop
    }
    catch
    {
        $logger.Fatal($_.Exception, 'Execution failed.')

        throw
    }
    finally
    {
        Stop-SerilogLogging
    }
    ```

- Create a basic logger and apply it to the Serilog default static logger:

    ```powershell
    $name = [IO.Path]::GetFileNameWithoutExtension($MyInvocation.MyCommand.Name)
    $path = [IO.Path]::ChangeExtension($MyInvocation.MyCommand.Path, '.log')
    $logger = New-SerilogBasicLogger -Name $name -Path $path -ErrorAction Stop |
        Set-SerilogDefaultLogger -ErrorAction Stop
    ```

    > **Warning**
    > Don't call `Set-SerilogDefaultLogger` more than once without shutting down logging by calling `Stop-SerilogLogging`.