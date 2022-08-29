---
external help file: PSSerilog.dll-Help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# New-SerilogLoggingLevelSwitch

## SYNOPSIS

Creates a new logging level switch.

## DESCRIPTION

The New-SerilogLoggingLevelSwitch cmdlet creates a new logging level switch that can be used to alter the minimum level at runtime.

## EXAMPLES

### ----------- Example 1: Create a new logging level switch with an initial level of verbose -----------

```powershell
PS> $loggingLevelSwitch = New-SerilogLoggingLevelSwitch -MinimumLevel Verbose
```