// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.
using HtmlAgilityPack;
using PostSharp.Engineering.BuildTools.Search.Crawlers;
using System;
using System.Linq;

namespace BuildPostSharpDocumentation;

internal class PostSharpDocCrawler : DocFxDocumentParser
{
    protected override BreadcrumbInfo GetBreadcrumbData( string[] breadcrumbLinks )
    {
        var relevantBreadCrumbTitles = breadcrumbLinks
            .Skip( 4 )
            .ToArray(); 
        
        var breadcrumb = string.Join(
            " > ",
            relevantBreadCrumbTitles );

        var isDefaultKind = breadcrumbLinks.Length < 5;

        var category = breadcrumbLinks.Length < 5
            ? null
            : NormalizeCategoryName( breadcrumbLinks.Skip( 4 ).First() );

        var isApiReference = category?.Equals( "api reference", StringComparison.OrdinalIgnoreCase ) ?? false;

        var kind = isDefaultKind
            ? "General Information"
            : isApiReference
                ? "API Documentation"
                : "Conceptual Documentation";

        int kindRank;
        
        if ( isDefaultKind )
        {
            kindRank = (int) PostSharpDocFxRank.Common;
        }
        else if ( isApiReference )
        {
            kindRank = (int) PostSharpDocFxRank.Api;
        }
        else
        {
            kindRank = (int) PostSharpDocFxRank.Conceptual;
        }

        return new(
            breadcrumb,
            [kind],
            kindRank,
            category == null ? [] : [category],
            relevantBreadCrumbTitles.Length,
            false,
            isApiReference );
    }
}
