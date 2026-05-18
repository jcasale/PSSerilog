---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 05-18-2026
PlatyPS schema version: 2024-05-01
title: New-SerilogLoggingLevelSwitch
---

# New-SerilogLoggingLevelSwitch

## SYNOPSIS

Creates a new logging level switch.

## SYNTAX

### __AllParameterSets

```
New-SerilogLoggingLevelSwitch [[-MinimumLevel] <LogEventLevel>] [<CommonParameters>]
```

## ALIASES

None.

## DESCRIPTION

The `New-SerilogLoggingLevelSwitch` cmdlet creates a new logging level switch that lets you alter
the minimum level at runtime.

## EXAMPLES

### Example 1: Create a new logging level switch with an initial level of Verbose

```powershell
PS> $loggingLevelSwitch = New-SerilogLoggingLevelSwitch -MinimumLevel Verbose
```

## PARAMETERS

### -MinimumLevel

The initial level to which the switch is set.

```yaml
Type: System.Nullable`1[Serilog.Events.LogEventLevel]
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
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

### Serilog.Events.LogEventLevel

The initial minimum level for the switch.

## OUTPUTS

### Serilog.Core.LoggingLevelSwitch

The logging level switch instance.

## NOTES

## RELATED LINKS
