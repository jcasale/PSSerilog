---
external help file: PSSerilog.dll-Help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# Add-SerilogSinkEmail

## SYNOPSIS

Adds an email sink to the specified logger configuration.

## DESCRIPTION

The Add-SerilogSinkEmail cmdlet adds an email sink to the specified logger configuration.

## EXAMPLES

### ----------- Example 1: Add an email sink to the specified logger configuration -----------

```powershell
PS> Add-SerilogSinkEmail `
  -Configuration $configuration `
  -From sender@domain.com `
  -To recipient@domain.com `
  -MailServer mail.domain.com
```