Set-StrictMode -Version Latest

Import-Module .\PSSerilog.psd1

$expressionTemplate = "[{@t:yyyy-MM-dd HH:mm:ss.fff}] [{@l}] {@m:l}{#if Rest() <> {}}`n`t{Rest()}{#end}`n{@x}"