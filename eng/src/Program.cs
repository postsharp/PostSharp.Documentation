// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Amazon;
using BuildPostSharpDocumentation;
using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using System.IO;
using PostSharp.Engineering.BuildTools.Build.Publishers;
using PostSharp.Engineering.BuildTools.Dependencies.Definitions;
using PostSharp.Engineering.BuildTools.Dependencies.Model;
using PostSharp.Engineering.BuildTools.Search;
using PostSharp.Engineering.DocFx;
using PostSharpDocumentationDependencies = PostSharp.Engineering.BuildTools.Dependencies.Definitions.PostSharpDependencies;
using PostSharpDependencies = PostSharp.Engineering.BuildTools.Dependencies.Definitions.PostSharpDependencies.V2025_1_GitHub;

const string docPackageFileName = "PostSharp.Doc.zip";

var product = new Product( PostSharpDocumentationDependencies.PostSharpDocumentation )
{
    Solutions =
    [
        new DotNetSolution( Path.Combine( "code", "PostSharp.Documentation.Prerequisites.sln" ) )
        {
            CanFormatCode = true
        },
        new DocFxApiSolution( "docfx.json" ),
        new DocFxSiteSolution( "docfx.json", docPackageFileName )
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
                    new DocumentationPublisher( [
                        new(docPackageFileName, RegionEndpoint.EUWest1, "doc.postsharp.net",
                            docPackageFileName)
                    ], "https://postsharp-helpbrowser.azurewebsites.net/" )
                ]
            } ),
    Extensions =
    [
        
        // Run `b generate-scripts` after changing these parameters.
        new UpdateSearchProductExtension(
            "https://typesense.postsharp.net",
            "postsharpdoc",
            "https://doc-production.postsharp.net/sitemap.xml",
            () => new PostSharpDocCrawler(),
            ["PostSharp"])
        
    ]
};

var app = new EngineeringApp( product );
app.AddDocFxCommands( );
return app.Run( args );