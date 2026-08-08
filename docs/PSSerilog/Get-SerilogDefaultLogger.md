---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 08-08-2026
PlatyPS schema version: 2024-05-01
title: Get-SerilogDefaultLogger
---

# Get-SerilogDefaultLogger

## SYNOPSIS

Gets the currently configured logger.

## SYNTAX

### __AllParameterSets

```
Get-SerilogDefaultLogger [-ExcludeSilentLogger] [<CommonParameters>]
```

## ALIASES

None.

## DESCRIPTION

The `Get-SerilogDefaultLogger` cmdlet gets the currently configured logger from the Serilog static logger.

## EXAMPLES

### Example 1: Get the currently configured logger

```powershell
PS> $logger = Get-SerilogDefaultLogger
```

### Example 2: Get the currently configured logger and throw if the static logger has not been set

```powershell
PS> $logger = Get-SerilogDefaultLogger -ExcludeSilentLogger
```

## PARAMETERS

### -ExcludeSilentLogger

Indicates that this cmdlet throws a terminating error if the static logger has not been overridden from the default "SilentLogger" instance.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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

### System.Management.Automation.SwitchParameter

You can pipe a value indicating whether to throw a terminating error when the static logger remains at its default instance.

## OUTPUTS

### Serilog.ILogger

Returns the currently configured default logger.

## NOTES

None.

## RELATED LINKS

None.
