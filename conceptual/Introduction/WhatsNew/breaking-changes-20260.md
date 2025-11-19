---
uid: breaking-changes-20260
title: "Breaking Changes in PostSharp 2026.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 2026.0 introduces major breaking changes including discontinued support for older than .NET Framework 4.7.1, .NET Standard 1.3, and end-of-life .NET versions."
---
# Breaking Changes in PostSharp 2026.0

PostSharp 2026.0 contains major breaking changes related to platform discontinuation.

## PostSharp.Redist
* Assemblies targeting .NET Framework 3.5, .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued. 
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.5.2.
* Projects targeting .NET Framework 3.5, 4.0, 4.5, 4.5.1, .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8.0 or later should be used in the final application instead.

## PostSharp.Patterns.Common.Redist
* Assemblies targeting .NET Framework 4.5, 4.6, .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.7 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7, .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Aggregation.Redist
* Assemblies targeting .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7, .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8.0 or later should be used in the final application instead.

## PostSharp.Patterns.Caching
* Assemblies targeting .NET Framework 4.6.1, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.7 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.6.1 to 4.7 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Caching.Azure
* Assemblies targeting .NET 5.0, and .NET 7.0 were discontinued.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Caching.IMemoryCache
* Assemblies targeting .NET 5.0, and .NET 7.0 were discontinued.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Caching.Redis
* Assemblies targeting .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.7 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.7 is unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.Redist
* Assemblies targeting .NET Framework 4.5 and 4.6, .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.7 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7 and .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.ApplicationInsights
* Assemblies targeting .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5.2 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5.2 to 4.7 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.AspNetCore
* Assemblies targeting .NET 5.0, and .NET 7.0 were discontinued.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.AspNetFramework
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7 are unsupported.

## PostSharp.Patterns.Diagnostics.CommonLogging
* Assemblies targeting .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7 and .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.Configuration
* Assembly targeting .NET Framework 4.5 was discontinued.
* Assembly targeting .NET Framework 4.6.1 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.DiagnosticSource
* Assemblies targeting .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.HttpClient
* Assemblies targeting .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.Log4Net
* Assemblies targeting .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7 and .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.Microsoft
* Assemblies targeting .NET 5.0, and .NET 7.0 were discontinued.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.NLog
* Assemblies targeting .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7 and .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.Serilog
* Assemblies targeting .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7 and .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Diagnostics.Tracing
* Assemblies targeting .NET Framework 4.5, .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.6 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7 and .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8 or later should be used in the final application instead.

## PostSharp.Patterns.Model.Redist
* Assemblies targeting .NET Framework 4.5, .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7, .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8.0 or later should be used for the executed application instead.

## PostSharp.Patterns.Threading.Redist
* Assemblies targeting .NET Framework 4.5, .NET Standard 1.3, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7, .NET Standard 1.3 to 1.6 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8.0 or later should be used for the executed application instead.

## PostSharp.Patterns.Xaml
* Assemblies targeting .NET Core 3.0, .NET 5.0, and .NET 7.0 were discontinued.
* Assembly targeting .NET Framework 4.5 was retargeted to .NET Framework 4.7.1.
* Projects targeting .NET Framework 4.5 to 4.7, .NET Core 3.0 to 3.1 are unsupported.
* Projects targeting .NET 5.0 to 7.0 are unsupported at runtime. .NET 8.0 or later should be used for the executed application instead.

