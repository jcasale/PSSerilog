---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 05-18-2026
PlatyPS schema version: 2024-05-01
title: New-SerilogBasicLogger
---

# New-SerilogBasicLogger

## SYNOPSIS

Creates a basic logger with a common configuration.

## SYNTAX

### __AllParameterSets

```
New-SerilogBasicLogger [-Path] <string> [[-Name] <string>] [<CommonParameters>]
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
$logger = New-SerilogBasicLogger -Name MyLogger -Path x:/path/logs/my-logger.log
```

This command creates a new logger with the MyLogger source context and writes log entries to
the console and the indicated path.

## PARAMETERS

### -Name

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

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

The path to the log file, or the source context name of the logger.

## OUTPUTS

### Serilog.ILogger

The configured logger instance.

## NOTES

## RELATED LINKS
