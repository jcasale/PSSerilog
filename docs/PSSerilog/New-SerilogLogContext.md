---
document type: cmdlet
external help file: PSSerilog.dll-Help.xml
HelpUri: ''
Locale: en-US
Module Name: PSSerilog
ms.date: 08-08-2026
PlatyPS schema version: 2024-05-01
title: New-SerilogLogContext
---

# New-SerilogLogContext

## SYNOPSIS

Creates a new logging context.

## SYNTAX

### __AllParameterSets

```
New-SerilogLogContext [-Name] <string> [-Value] <Object> [-DestructureObjects] [<CommonParameters>]
```

## ALIASES

None.

## DESCRIPTION

The `New-SerilogLogContext` cmdlet creates a new logging context and returns the context so that
the caller can dispose it when no longer needed.

## EXAMPLES

### Example 1: Create a logging context for the EventId property and dispose it when no longer needed

```powershell
PS> $context = New-SerilogLogContext -Name EventId -Value 42
PS> try { $logger.Information('Hello World!') } finally { $context.Dispose() }
```

## PARAMETERS

### -DestructureObjects

Convert a non-primitive, non-array type to a structure.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: 2
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Name

The name of the property.

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

### -Value

The value of the property.

```yaml
Type: System.Object
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: 1
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

### System.Management.Automation.SwitchParameter

You can pipe a value indicating whether to convert a non-primitive, non-array value to a structure.

### System.String

You can pipe the name of the property.

### System.Object

You can pipe the value of the property.

## OUTPUTS

### System.IDisposable

Returns a handle that removes the property from the log context when disposed.

## NOTES

Call `Dispose` on the returned `IDisposable` to remove the property from the log context. Use a
`try`/`finally` block to ensure the context is always cleaned up.

## RELATED LINKS

None.
