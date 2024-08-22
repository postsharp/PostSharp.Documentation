using Spectre.Console.Cli;

namespace PostSharp.Documentation.Docfx.Cli;

public class DocfxSettings : CommandSettings
{
    [CommandArgument( 0, "<config-path>" )]
    public string ConfigurationPath { get; set; } = null!;
}