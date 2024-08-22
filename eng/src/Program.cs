// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Amazon;
using BuildPostSharpDocumentation;
using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using PostSharp.Engineering.BuildTools.S3.Publishers;
using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Utilities;
using Spectre.Console.Cli;
using System.IO;
using System.Diagnostics;
using PostSharp.Engineering.BuildTools.Build.Publishers;
using PostSharp.Engineering.BuildTools.Dependencies.Definitions;
using PostSharp.Engineering.BuildTools.Dependencies.Model;
using PostSharp.Engineering.BuildTools.Search;
using PostSharpDocumentationDependencies = PostSharp.Engineering.BuildTools.Dependencies.Definitions.PostSharpDependencies;
using PostSharpDependencies = PostSharp.Engineering.BuildTools.Dependencies.Definitions.PostSharpDependencies.V2024_1;

const string docPackageFileName = "PostSharp.Doc.zip";

var product = new Product( PostSharpDocumentationDependencies.PostSharpDocumentation )
{
    Solutions =
    [
        new DotNetSolution( Path.Combine( "code", "PostSharp.Documentation.Prerequisites.sln" ) )
        {
            CanFormatCode = true
        },
        new DocFxMetadataSolution( "docfx.json" ),
        new DocFxBuildSolution( "docfx.json", docPackageFileName )
    ],
    PublicArtifacts = Pattern.Create(
        docPackageFileName
    ),
    ParametrizedDependencies =
    [
        DevelopmentDependencies.PostSharpEngineering.ToDependency(),
        PostSharpDependencies.PostSharp.ToDependency(
            new ConfigurationSpecific<BuildConfiguration>(
                BuildConfiguration.Release, BuildConfiguration.Release, BuildConfiguration.Release
            ) )
    ],
    AdditionalDirectoriesToClean = [Path.Combine( "artifacts", "api" ), Path.Combine( "artifacts", "site" )],

    // Disable automatic build triggers.
    Configurations = Product.DefaultConfigurations
        .WithValue( BuildConfiguration.Debug, c => c with { BuildTriggers = default } )
        .WithValue( BuildConfiguration.Public,
            c => c with
            {
                PublicPublishers =
                [
                    new MergePublisher(),
                    new DocumentationPublisher( new S3PublisherConfiguration[]
                    {
                        new(docPackageFileName, RegionEndpoint.EUWest1, "doc.postsharp.net",
                            docPackageFileName),
                    }, "https://postsharp-helpbrowser.azurewebsites.net/" )
                ]
            } ),
    Extensions =
    [
        // Run `b generate-scripts` after changing these parameters.
        new UpdateSearchProductExtension<UpdatePostSharpDocumentationCommand>(
            "https://0fpg9nu41dat6boep.a1.typesense.net",
            "postsharpdoc",
            "https://doc-production.postsharp.net/il/sitemap.xml",
            true )
    ]
};

var commandApp = new CommandApp();

commandApp.AddProductCommands( product );

return commandApp.Run( args );