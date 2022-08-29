---
external help file: PSSerilog.dll-Help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# Add-SerilogSinkEventLog

## SYNOPSIS

Adds a Windows Event Log sink to the specified logger configuration.

## DESCRIPTION

The Add-SerilogSinkEventLog cmdlet adds a Windows Event Log sink to the specified logger configuration.

A custom event id provider that expects an 'EventId' context property is used by default.

## EXAMPLES

### ----------- Example 1: Add a Windows Event Log sink to the specified logger configuration -----------

```powershell
PS> Add-SerilogSinkEventLog -Configuration $configuration
```