---
external help file: PSSerilog.dll-Help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# Get-SerilogDefaultLogger

## SYNOPSIS

Gets the currently configured logger.

## DESCRIPTION

The Get-SerilogDefaultLogger cmdlet gets the currently configured logger from the Serilog static logger.

## EXAMPLES

### ----------- Example 1: Get the currently configured logger -----------

```powershell
PS> $logger = Get-SerilogDefaultLogger
```

### ----------- Example 2: Get the currently configured logger, throwing an error if the static logger has not been set -----------

```powershell
PS> $logger = Get-SerilogDefaultLogger -ExcludeSilentLogger
```