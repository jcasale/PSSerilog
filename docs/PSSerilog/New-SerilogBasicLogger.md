---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 09-17-2026
PlatyPS schema version: 2024-05-01
title: New-SerilogBasicLogger
---

# New-SerilogBasicLogger

## SYNOPSIS

Creates a basic logger with a common configuration.

## SYNTAX

### __AllParameterSets

```
New-SerilogBasicLogger [-Path] <string> [[-SourceContext] <string>] [<CommonParameters>]
```

## ALIASES

None.

## DESCRIPTION

The `New-SerilogBasicLogger` cmdlet creates a basic logger with a common configuration.

This cmdlet performs the following functions:

* Creates a new Serilog configuration with a console and file sink.
* Creates a new Serilog logger.
* Returns the logger instance.

## EXAMPLES

### Example 1: Create a basic logger

```powershell
$logger = New-SerilogBasicLogger -Path x:/path/logs/my-logger.log
```

This command creates a new root logger that writes log entries to the console and
the specified path.

### Example 2: Create a basic logger with a source context

```powershell
$logger = New-SerilogBasicLogger `
    -Path x:/path/logs/my-logger.log `
    -SourceContext MyLogger
```

This command creates a new root logger named with the MyLogger source
context that writes log entries to the console and the specified path.

## PARAMETERS

### -Path

The path to the log file.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SourceContext

The source context of the logger.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: 1
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

You can pipe the path to the log file.

### System.String

You can pipe the source context of the logger.

## OUTPUTS

### Serilog.ILogger

Returns the configured logger.

## NOTES

None.

## RELATED LINKS

None.
