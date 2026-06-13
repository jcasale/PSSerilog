---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 05-18-2026
PlatyPS schema version: 2024-05-01
title: New-SerilogLogger
---

# New-SerilogLogger

## SYNOPSIS

Creates a new logger.

## SYNTAX

### __AllParameterSets (Default)

```
New-SerilogLogger [-Configuration] <LoggerConfiguration> [<CommonParameters>]
```

### Name

```
New-SerilogLogger [-Configuration] <LoggerConfiguration> [[-Name] <string>] [<CommonParameters>]
```

## ALIASES

None.

## DESCRIPTION

The `New-SerilogLogger` cmdlet creates a new logger with an optional source context.

## EXAMPLES

### Example 1: Create a new logger

```powershell
PS> New-SerilogLogger -Configuration $configuration
```

### Example 2: Create a new logger with a source context

```powershell
PS> New-SerilogLogger -Configuration $configuration -Name MyLogger
```

## PARAMETERS

### -Configuration

The logging configuration to create the logger from.

```yaml
Type: Serilog.LoggerConfiguration
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Name

The source context of the logger.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: Name
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

### Serilog.LoggerConfiguration

You can pipe the logging configuration to create the logger from.

### System.String

You can pipe the source context of the logger.

## OUTPUTS

### Serilog.ILogger

Returns the configured logger.

## NOTES

## RELATED LINKS
