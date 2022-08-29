---
external help file: PSSerilog.dll-Help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# New-SerilogLogger

## SYNOPSIS

Creates a new logger.

## DESCRIPTION

The New-SerilogLogger cmdlet creates a new logger with an optional source context.

## EXAMPLES

### ----------- Example 1: Creates a new logger -----------

```powershell
PS> New-SerilogLogger -Configuration $configuration
```

### ----------- Example 2: Creates a new logger with a source context -----------

```powershell
PS> New-SerilogLogger -Configuration $configuration -Name MyLogger
```