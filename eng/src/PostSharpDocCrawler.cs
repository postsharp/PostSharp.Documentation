// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using HtmlAgilityPack;
using PostSharp.Engineering.BuildTools.Search.Crawlers;
using System;
using System.Linq;

namespace BuildPostSharpDocumentation;

public class PostSharpDocCrawler : DocFxCrawler
{
    protected override BreadcrumbInfo GetBreadcrumbData( HtmlNode[] breadcrumbLinks )
    {
        var relevantBreadCrumbTitles = breadcrumbLinks
            .Skip( 4 )
            .Select( n => n.GetText() )
            .ToArray(); 
        
        var breadcrumb = string.Join(
            " > ",
            relevantBreadCrumbTitles );

        var isDefaultKind = breadcrumbLinks.Length < 5;

        var category = breadcrumbLinks.Length < 5
            ? null
            : NormalizeCategoryName( breadcrumbLinks.Skip( 4 ).First().GetText() );

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
            new[] { kind },
            kindRank,
            category == null ? Array.Empty<string>() : new[] { category },
            relevantBreadCrumbTitles.Length,
            false,
            isApiReference );
    }
}