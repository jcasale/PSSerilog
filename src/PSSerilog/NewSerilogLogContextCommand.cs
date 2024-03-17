namespace PSSerilog;

using System;
using System.Management.Automation;

using Serilog.Context;

[Cmdlet(VerbsCommon.New, "SerilogLogContext")]
[OutputType(typeof(IDisposable))]
public class NewSerilogLogContextCommand : PSCmdlet
{
    [Parameter(
        Position = 0,
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The name of the property.")]
    [ValidateNotNullOrEmpty]
    public string Name { get; set; }

    [Parameter(
        Position = 1,
        Mandatory = true,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The value of the property.")]
    [ValidateNotNull]
    public object Value { get; set; }

    [Parameter(
        Position = 2,
        ValueFromPipeline = false,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "Convert a non-primitive, non-array type to a structure.")]
    public SwitchParameter DestructureObjects { get; set; }

    /// <inheritdoc />
    protected override void ProcessRecord()
    {
        var context = LogContext.PushProperty(this.Name, this.Value, this.DestructureObjects);

        this.WriteObject(context);
    }
}