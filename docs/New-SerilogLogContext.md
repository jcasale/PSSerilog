---
external help file: PSSerilog.dll-Help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# New-SerilogLogContext

## SYNOPSIS

Creates a new logging context.

## DESCRIPTION

The New-SerilogLogContext cmdlet creates a new logging context and returns the context so that it can be disposed when no longer needed.

## EXAMPLES

### ----------- Example 1: Create a logging context for the EventId property and dispose it when no longer needed -----------

```powershell
PS> $context = New-SerilogLogContext -Name EventId -Value 42
PS> try { $logger.Information('Hello World!') } finally { $context.Dispose() }
```