---
external help file: PSSerilog.dll-help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# New-SerilogBasicLogger

## SYNOPSIS

Creates a basic logger with a common configuration.

## DESCRIPTION

The New-SerilogBasicLogger cmdlet creates a basic logger with a common configuration.

This cmdlet performs the following functions:

  * Creates a new Serilog configuration with a console and file sink.
  * Creates a new Serilog logger
  * Assigns the new logger to the static Serilog default logger.
  * Begins tracking the logger in session state for disposal.
  * Returns the logger instance.

## EXAMPLES

### ----------- Example 1: Create a basic logger -----------

```powershell
$logger = New-SerilogBasicLogger -Name MyLogger -Path x:/path/logs/my-logger.log
```

This command creates a new logger with the MyLogger source context and writes log entries to the console and indicated path.