$path = Join-Path $PSScriptRoot 'Serilog.dll'
$serilog = [Reflection.Assembly]::LoadFrom($path)
$handler = [ResolveEventHandler]{
    param($s, $a)

    if ($a.Name.StartsWith('Serilog,'))
    {
        return $serilog
    }

    foreach($assembly in [AppDomain]::CurrentDomain.GetAssemblies())
    {
        if ($assembly.FullName -eq $a.Name)
        {
            return $assembly
        }
    }

    return $null
}

[AppDomain]::CurrentDomain.add_AssemblyResolve($handler)