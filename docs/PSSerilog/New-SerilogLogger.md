---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 08-08-2026
PlatyPS schema version: 2024-05-01
title: New-SerilogLogger
---

# New-SerilogLogger

## SYNOPSIS

Creates a new logger.

## SYNTAX

### Configuration

```
New-SerilogLogger [-Configuration] <LoggerConfiguration> [<CommonParameters>]
```

### SourceContext

```
New-SerilogLogger [-Logger] <ILogger> [[-SourceContext] <string>] [<CommonParameters>]
```

## ALIASES

None.


## DESCRIPTION

The `New-SerilogLogger` cmdlet creates a new logger.
You can create a new root logger using an existing configuration.
You can also create a new named logger using an existing logger and a source context.

## EXAMPLES

### Example 1: Create a new logger

```powershell
PS> $logger = New-SerilogLogger -Configuration $configuration
```

### Example 2: Create a new logger with a source context

```powershell
PS> $log = New-SerilogLogger -Logger $logger -SourceContext MyLogger
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
- Name: Configuration
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Logger

The logger to create the new logger from that will be enriched with a source context.

```yaml
Type: Serilog.ILogger
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: SourceContext
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
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
- Name: SourceContext
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

You can pipe the logging configuration used to create the logger.

### Serilog.ILogger

You can pipe the root logger used to create the named logger.

### System.String

You can pipe the source context of the logger.

## OUTPUTS

### Serilog.ILogger

Returns the configured logger.

## NOTES

None.

## RELATED LINKS

None.
