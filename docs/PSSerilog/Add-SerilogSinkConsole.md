---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 08-08-2026
PlatyPS schema version: 2024-05-01
title: Add-SerilogSinkConsole
---

# Add-SerilogSinkConsole

## SYNOPSIS

Adds a console sink to the specified logger configuration.

## SYNTAX

### OutputTemplate (Default)

```
Add-SerilogSinkConsole [-Configuration] <LoggerConfiguration> [-OutputTemplate <string>]
 [-FormatProvider <IFormatProvider>] [-Theme <ConsoleTheme>] [-ApplyThemeToRedirectedOutput]
 [-MinimumLevel <LogEventLevel>] [-LevelSwitch <LoggingLevelSwitch>]
 [-StandardErrorFromLevel <LogEventLevel>] [<CommonParameters>]
```

### Formatter

```
Add-SerilogSinkConsole [-Configuration] <LoggerConfiguration> -Formatter <ITextFormatter>
 [-MinimumLevel <LogEventLevel>] [-LevelSwitch <LoggingLevelSwitch>]
 [-StandardErrorFromLevel <LogEventLevel>] [<CommonParameters>]
```

## ALIASES

None.

## DESCRIPTION

The `Add-SerilogSinkConsole` cmdlet adds a console sink to the specified logger configuration.

## EXAMPLES

### Example 1: Add a console sink to a logger configuration

```powershell
PS> Add-SerilogSinkConsole -Configuration $configuration
```

### Example 2: Add a console sink using a custom formatter

```powershell
PS> Add-SerilogSinkConsole -Configuration $configuration -Formatter $formatter
```

## PARAMETERS

### -ApplyThemeToRedirectedOutput

Applies the selected or default theme even when output redirection is detected.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: OutputTemplate
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Configuration

The logging configuration to add the sink to.

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

### -FormatProvider

The culture-specific formatting information.

```yaml
Type: System.IFormatProvider
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: OutputTemplate
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Formatter

The formatter to convert the log events into text for the file.

```yaml
Type: Serilog.Formatting.ITextFormatter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: Formatter
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LevelSwitch

The switch allowing the pass-through minimum level to be changed at runtime.

```yaml
Type: Serilog.Core.LoggingLevelSwitch
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

### -MinimumLevel

The minimum level at which events will be passed to sinks. Ignored when level switch is specified.

```yaml
Type: Serilog.Events.LogEventLevel
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

### -OutputTemplate

The message template describing the format used to write to the sink.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: OutputTemplate
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -StandardErrorFromLevel

The level at which events will be written to standard error.

```yaml
Type: System.Nullable`1[Serilog.Events.LogEventLevel]
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

### -Theme

The theme to apply to the styled output.

```yaml
Type: Serilog.Sinks.SystemConsole.Themes.ConsoleTheme
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: OutputTemplate
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

You can pipe a value indicating whether to apply the selected or default theme even when output redirection is detected.

### Serilog.LoggerConfiguration

You can pipe the logging configuration to add the sink to.

### System.IFormatProvider

You can pipe culture-specific formatting information.

### Serilog.Formatting.ITextFormatter

You can pipe the formatter used to convert log events into text for the sink.

### Serilog.Core.LoggingLevelSwitch

You can pipe the switch that allows the pass-through minimum level to be changed at runtime.

### Serilog.Events.LogEventLevel

You can pipe the minimum level at which events are passed to sinks.

### System.String

You can pipe the message template describing the format used to write to the sink.

### System.Nullable`1[Serilog.Events.LogEventLevel]

You can pipe the level at which events are written to standard error.

### Serilog.Sinks.SystemConsole.Themes.ConsoleTheme

You can pipe the theme to apply to styled output.

## OUTPUTS

### Serilog.LoggerConfiguration

Returns the logging configuration after adding the console sink.

## NOTES

None.

## RELATED LINKS

None.
