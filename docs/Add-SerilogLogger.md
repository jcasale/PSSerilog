---
external help file: PSSerilog.dll-Help.xml
Module Name: PSSerilog
online version:
schema: 2.0.0
---

# Add-SerilogLogger

## SYNOPSIS

Adds a logger to session state so that it can be disposed.

## DESCRIPTION

The Add-SerilogLogger cmdlet adds a logger to session state so that it can be disposed with a call to Stop-Serilog.

## EXAMPLES

### ----------- Example 1: Add a logger to session state -----------

```powershell
PS> Add-SerilogLogger -Logger $logger
```