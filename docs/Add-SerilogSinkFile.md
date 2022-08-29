---
external help file: PSSerilog.dll-Help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# Add-SerilogSinkFile

## SYNOPSIS

Adds a file sink to the specified logger configuration.

## DESCRIPTION

The Add-SerilogSinkFile cmdlet adds a file sink to the specified logger configuration.

## EXAMPLES

### ----------- Example 1: Add a file sink to the specified logger configuration -----------

```powershell
PS> Add-SerilogSinkFile -Configuration $configuration -Path $logFilePath
```