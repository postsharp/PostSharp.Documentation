// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Amazon;
using BuildPostSharpDocumentation;
using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using PostSharp.Engineering.BuildTools.AWS.S3.Publishers;
using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Utilities;
using Spectre.Console.Cli;
using System.IO;
using System.Diagnostics;
using PostSharp.Engineering.BuildTools.Build.Publishers;
using PostSharp.Engineering.BuildTools.Dependencies.Definitions;
using PostSharp.Engineering.BuildTools.Search;

const string docPackageFileName = "PostSharp.Doc.zip";

var product = new Product( PostSharpDependencies.PostSharpDocumentation )
{
    Solutions = new Solution[]
    {
        new DotNetSolution( Path.Combine( "code", "PostSharp.Documentation.Prerequisites.sln" ) )
        {
            CanFormatCode = true
        },
        new DocFxSolution( "docfx.json" ),
    },
    PublicArtifacts = Pattern.Create(
        docPackageFileName
    ),
    Dependencies = new[] { DevelopmentDependencies.PostSharpEngineering },
    AdditionalDirectoriesToClean = new[] { "obj", "docfx\\_site" },

    // Disable automatic build triggers.
    Configurations = Product.DefaultConfigurations
        .WithValue( BuildConfiguration.Debug, c => c with { BuildTriggers = default } )
        .WithValue( BuildConfiguration.Public,
            c => c with
            {
                PublicPublishers = new Publisher[]
                {
                    new MergePublisher(),
                    new DocumentationPublisher( new S3PublisherConfiguration[]
                    {
                        new(docPackageFileName, RegionEndpoint.EUWest1, "doc.postsharp.net",
                            docPackageFileName),
                    } )
                }
            } ),
    Extensions = new ProductExtension[]
    {
        new UpdateSearchProductExtension<UpdatePostSharpDocumentationCommand>(
            "https://0fpg9nu41dat6boep.a1.typesense.net",
            "postsharpdoc",
            "https://doc-production.postsharp.net/sitemap.xml",
            true )
    }
};

product.PrepareCompleted += OnPrepareCompleted;


var commandApp = new CommandApp();

commandApp.AddProductCommands( product );

return commandApp.Run( args );


static void OnPrepareCompleted( PrepareCompletedEventArgs args )
{
    var nuget = Path.Combine( Path.GetDirectoryName( Process.GetCurrentProcess().MainModule!.FileName )!, "nuget.exe " );

    if ( !ToolInvocationHelper.InvokeTool( args.Context.Console, nuget,
        "restore \"docfx\\packages.config\" -OutputDirectory \"docfx\\packages\"", args.Context.RepoDirectory ) )
    {
        args.IsFailed = true;
    }
}