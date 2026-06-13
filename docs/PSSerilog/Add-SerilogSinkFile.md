---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 05-18-2026
PlatyPS schema version: 2024-05-01
title: Add-SerilogSinkFile
---

# Add-SerilogSinkFile

## SYNOPSIS

Adds a file sink to the specified logger configuration.

## SYNTAX

### OutputTemplate (Default)

```
Add-SerilogSinkFile [-Configuration] <LoggerConfiguration> -Path <string> [-OutputTemplate <string>]
 [-FormatProvider <IFormatProvider>] [-MinimumLevel <LogEventLevel>] [-FileSizeLimitBytes <long>]
 [-LevelSwitch <LoggingLevelSwitch>] [-Buffered] [-Shared] [-FlushToDiskInterval <timespan>]
 [-RollingInterval <RollingInterval>] [-RollOnFileSizeLimit] [-RetainedFileCountLimit <int>]
 [-Encoding <Encoding>] [-Hooks <FileLifecycleHooks>] [-RetainedFileTimeLimit <timespan>]
 [<CommonParameters>]
```

### Formatter

```
Add-SerilogSinkFile [-Configuration] <LoggerConfiguration> -Formatter <ITextFormatter>
 -Path <string> [-MinimumLevel <LogEventLevel>] [-FileSizeLimitBytes <long>]
 [-LevelSwitch <LoggingLevelSwitch>] [-Buffered] [-Shared] [-FlushToDiskInterval <timespan>]
 [-RollingInterval <RollingInterval>] [-RollOnFileSizeLimit] [-RetainedFileCountLimit <int>]
 [-Encoding <Encoding>] [-Hooks <FileLifecycleHooks>] [-RetainedFileTimeLimit <timespan>]
 [<CommonParameters>]
```

## ALIASES

None.

## DESCRIPTION

The `Add-SerilogSinkFile` cmdlet adds a file sink to the specified logger configuration.

## EXAMPLES

### Example 1: Add a file sink to a logger configuration

```powershell
PS> Add-SerilogSinkFile -Configuration $configuration -Path $logFilePath
```

### Example 2: Add a rolling daily file sink with a 7-file retention limit

```powershell
PS> Add-SerilogSinkFile -Configuration $configuration -Path $logFilePath -RollingInterval Day -RetainedFileCountLimit 7
```

## PARAMETERS

### -Buffered

Enables buffered output flushing.

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

### -Encoding

The character encoding used to write the text file. The default is UTF-8 without BOM.

```yaml
Type: System.Text.Encoding
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

### -FileSizeLimitBytes

The approximate maximum size, in bytes, to which a log file will be allowed to grow. For unrestricted growth, pass null. The default is 1 GB.

```yaml
Type: System.Nullable`1[System.Int64]
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

### -FlushToDiskInterval

The flushing interval.

```yaml
Type: System.Nullable`1[System.TimeSpan]
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

### -Hooks

The character encoding used to write the text file. The default is UTF-8 without BOM.

```yaml
Type: Serilog.Sinks.File.FileLifecycleHooks
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

### -Path

The path to the file.

```yaml
Type: System.String
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RetainedFileCountLimit

The maximum number of log files that will be retained, including the current log file. For unlimited retention, pass null. The default is 31.

```yaml
Type: System.Nullable`1[System.Int32]
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

### -RetainedFileTimeLimit

The maximum time after the end of an interval that a rolling log file will be retained. Must be greater than or equal to 0.

```yaml
Type: System.Nullable`1[System.TimeSpan]
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

### -RollingInterval

The rolling interval. Defaults to infinite.

```yaml
Type: Serilog.RollingInterval
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

### -RollOnFileSizeLimit

Enables rolling on file size limit.

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

### -Shared

Enables sharing of the output file.

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

You can pipe a value indicating whether buffered output flushing is enabled.

### Serilog.LoggerConfiguration

You can pipe the logging configuration to add the sink to.

### System.Text.Encoding

You can pipe the character encoding used to write the text file.

### System.Nullable`1[System.Int64]

You can pipe the approximate maximum size, in bytes, to which a log file can grow.

### System.Nullable`1[System.TimeSpan]

You can pipe the flushing interval.

### System.IFormatProvider

You can pipe culture-specific formatting information.

### Serilog.Formatting.ITextFormatter

You can pipe the formatter used to convert log events into text for the file.

### Serilog.Sinks.File.FileLifecycleHooks

You can pipe hooks called during log file lifecycle events.

### Serilog.Core.LoggingLevelSwitch

You can pipe the switch that allows the pass-through minimum level to be changed at runtime.

### Serilog.Events.LogEventLevel

You can pipe the minimum level at which events are passed to sinks.

### System.String

You can pipe the message template describing the format used to write to the sink.

### System.String

You can pipe the path to the file.

### System.Nullable`1[System.Int32]

You can pipe the maximum number of log files to retain.

### System.Nullable`1[System.TimeSpan]

You can pipe the maximum time after the end of an interval that a rolling log file is retained.

### Serilog.RollingInterval

You can pipe the rolling interval.

### System.Management.Automation.SwitchParameter

You can pipe a value indicating whether rolling on file size limit is enabled.

### System.Management.Automation.SwitchParameter

You can pipe a value indicating whether sharing of the output file is enabled.

## OUTPUTS

### Serilog.LoggerConfiguration

Returns the logging configuration after adding the file sink.

## NOTES

## RELATED LINKS
