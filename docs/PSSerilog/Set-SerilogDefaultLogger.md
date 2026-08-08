---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 08-08-2026
PlatyPS schema version: 2024-05-01
title: Set-SerilogDefaultLogger
---

# Set-SerilogDefaultLogger

## SYNOPSIS

Sets the default logger to the specified logger instance.

## SYNTAX

### __AllParameterSets

```
Set-SerilogDefaultLogger [-Logger] <ILogger> [<CommonParameters>]
```

## ALIASES

None.

## DESCRIPTION

The `Set-SerilogDefaultLogger` cmdlet sets the default logger to the specified logger instance.

Access the default logger by using the `Get-SerilogDefaultLogger` cmdlet.

## EXAMPLES

### Example 1: Set the default logger to the specified instance

```powershell
PS> Set-SerilogDefaultLogger -Logger $logger
```

### Example 2: Set the default logger and stop if one was already set

```powershell
$logger = New-SerilogBasicLogger `
    -Path $path `
    -ErrorAction Stop |
  Set-SerilogDefaultLogger -ErrorAction Stop
```

This `Set-SerilogDefaultLogger` cmdlet returns the same logger passed in, regardless of
whether the cmdlet succeeds.

### Example 3: Set the default logger and silently continue if one was already set

```powershell
$logger = New-SerilogBasicLogger `
    -Path $path `
    -ErrorAction Stop |
  Set-SerilogDefaultLogger -ErrorAction SilentlyContinue
```

This `Set-SerilogDefaultLogger` cmdlet returns the same logger passed in, regardless of
whether the cmdlet succeeds.

## PARAMETERS

### -Logger

The logger to set as the default.

```yaml
Type: Serilog.ILogger
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

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Serilog.ILogger

You can pipe the logger to set as the default.

## OUTPUTS

### Serilog.ILogger

Returns the logger set as default.

## NOTES

Use `-ErrorAction Stop` to terminate if a default logger is already set, or
`-ErrorAction SilentlyContinue` to ignore the condition.

## RELATED LINKS

None.
