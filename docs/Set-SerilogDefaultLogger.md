---
external help file: PSSerilog.dll-Help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# Set-SerilogDefaultLogger

## SYNOPSIS

Sets the default logger to the specified logger instance.

## DESCRIPTION

The Set-SerilogDefaultLogger cmdlet sets the default logger to the specified logger instance.

The default logger can be accessed by using the Get-SerilogDefaultLogger cmdlet.

## EXAMPLES

### ----------- Example 1: Set the default logger to the specified instance -----------

```powershell
PS> Set-SerilogDefaultLogger -Logger $logger
```

### ----------- Example 2: Set the default logger to the specified instance and terminate if the default was already set -----------

```powershell
PS> $logger = New-SerilogBasicLogger `
    -Name $name `
    -Path $path `
    -ErrorAction Stop |
  Set-SerilogDefaultLogger -ErrorAction Stop
```

### ----------- Example 3: Set the default logger to the specified instance and continue if the default was already set -----------

```powershell
PS> $logger = New-SerilogBasicLogger `
    -Name $name `
    -Path $path `
    -ErrorAction Stop |
  Set-SerilogDefaultLogger -ErrorAction SilentlyContinue
```