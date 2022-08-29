---
external help file: PSSerilog.dll-Help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# New-SerilogGlobalContext

## SYNOPSIS

Creates a new global logging context.

## DESCRIPTION

The New-SerilogGlobalContext cmdlet creates a new global logging context and returns the context so that it can be disposed when no longer needed.

## EXAMPLES

### ----------- Example 1: Create a global logging context for a property -----------

```powershell
PS> $context = New-SerilogGlobalContext -Name OrderId -Value 42
```