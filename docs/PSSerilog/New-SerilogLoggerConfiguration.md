---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 05-18-2026
PlatyPS schema version: 2024-05-01
title: New-SerilogLoggerConfiguration
---

# New-SerilogLoggerConfiguration

## SYNOPSIS

Creates a new logging configuration.

## SYNTAX

### __AllParameterSets

```
New-SerilogLoggerConfiguration [-MinimumLevel <LogEventLevel>] [-LogContext] [-MachineName]
 [-EnvironmentUserName] [-ProcessId] [-ThreadId] [-Properties <hashtable>] [<CommonParameters>]
```

## ALIASES

None.

## DESCRIPTION

The `New-SerilogLoggerConfiguration` cmdlet creates a new logging configuration.

## EXAMPLES

### Example 1: Create a new logging configuration

```powershell
PS> $configuration = New-SerilogLoggerConfiguration
```

### Example 2: Create a configuration enriched with machine name and log context at the Debug level

```powershell
PS> $configuration = New-SerilogLoggerConfiguration -MinimumLevel Debug -MachineName -LogContext
```

## PARAMETERS

### -EnvironmentUserName

Enables the enrichment of log events with the user name.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LogContext

Enables the enrichment of log events with properties from log context.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MachineName

Enables the enrichment of log events with the machine name.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MinimumLevel

Configures the minimum level at which events will be passed to sinks (default Information level).

```yaml
Type: System.Nullable`1[Serilog.Events.LogEventLevel]
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ProcessId

Enables the enrichment of log events with the process id.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Properties

Enables the enrichment of log events with properties.

```yaml
Type: System.Collections.Hashtable
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ThreadId

Enables the enrichment of log events with the thread id.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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

### System.Management.Automation.SwitchParameter

You can pipe a value indicating whether to enrich log events with the user name.

### System.Management.Automation.SwitchParameter

You can pipe a value indicating whether to enrich log events with properties from log context.

### System.Management.Automation.SwitchParameter

You can pipe a value indicating whether to enrich log events with the machine name.

### System.Nullable`1[Serilog.Events.LogEventLevel]

You can pipe the minimum level at which events are passed to sinks.

### System.Management.Automation.SwitchParameter

You can pipe a value indicating whether to enrich log events with the process id.

### System.Collections.Hashtable

You can pipe properties to enrich log events with.

### System.Management.Automation.SwitchParameter

You can pipe a value indicating whether to enrich log events with the thread id.

## OUTPUTS

### Serilog.LoggerConfiguration

Returns a logging configuration for further configuration or logger creation.

## NOTES

## RELATED LINKS
