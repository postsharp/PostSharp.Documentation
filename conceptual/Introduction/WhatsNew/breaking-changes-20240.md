---
uid: breaking-changes-20240
title: "Breaking Changes in PostSharp 2024.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Breaking Changes in PostSharp 2024.0

PostSharp 2024.0 contains larger number of breaking changes mainly related to platform upgrades and dependency security.

## PostSharp
* .NET 6.0 SDK and later is required to build .NET Standard, .NET Core and .NET projects. Older SDKs are not supported anymore.
* When building .NET Framework projects, x64 version of PostSharp Compiler will be used by default. 
  Projects that reference x86-specific libraries from projects targeting AnyCPU (default setting) should explicitly set `PostSharpTargetProcessor` property to `x86`.
* When building .NET Framework projects with x86 version of PostSharp Compiler, pipe-server is not used by default anymore. This may lead to longer build times.
  If usage of pipe-server is necessary, `PostSharpUsePipeServer` MSBuild property needs to be set to `true`.
* PostSharp aspects are now strongly encouraged to use `[PSerializable]` instead of `[Serializable]` for aspects serialization. 
* Projects that still need to use `[Serializable]` for aspects should explicitly set `PostSharpBinaryFormatterAllowed` MSBuild property to `True`.
* PostSharp Compiler internal dependencies were upgraded, which will require downloading of NuGet packages during the first build. This may require manual action for users with 

## PostSharp.Patterns.Common
* The dependency on `System.Text.RegularExpressions` has been updated from 4.3.0 to 4.3.1.

## PostSharp.Patterns.Caching
* The default `BinarySerializer` was changed to `JsonCachingSerializer`. In persisted backends this mean that values are not compatible across with older versions of the library and versions should not be mixed.
* `BinarySerializer` is now considered obsolete (produces a warning).
* The package has a new dependency on the `System.Text.Json, 6.0.0` package.
* Support for targeting .NET Core 3.0 and earlier has been dropped.
* Support for targeting .NET Framework 4.6.0 and earlier has been dropped.

## PostSharp.Patterns.Caching.Azure
* Replaces `AzureCacheInvalidator` and `AzureCacheInvalidator2` with <xref:PostSharp.Patterns.Caching.Azure.AzureServiceBusCacheInvalidator>, which is based on the latest Azure API.
* The package has a new dependency on `Azure.Messaging.ServiceBus` 7.16.1 instead of `WindowsAzure.ServiceBus` and `Microsoft.Azure.ServiceBus`.
* The package has a new dependency on `Azure.Identity` 1.10.4.
* Support for targeting .NET Framework 4.7.0 and earlier has been dropped.
* Support for targeting .NET Core 3.0 and earlier has been dropped.

## PostSharp.Patterns.Diagnostics.AspNetCore
* The dependency on `Microsoft.AspNetCore.Mvc.Core` has been updated from 2.0.0 to 2.1.0.

## PostSharp.Patterns.Diagnostics.HttpClient
* The dependency on `System.Net.Http` has been updated from 4.0.0 to 4.3.4.

## PostSharp.Patterns.Diagnostics.ApplicationInsights
* The dependency on `Microsoft.ApplicationInsights` has been updated from 2.3.0 to 2.21.0.
* Support for targeting .NET Standard 1.6 and earlier has been dropped.
* Support for targeting .NET Framework 4.5.1 and earlier has been dropped.

## PostSharp.Patterns.Diagnostics.Log4Net
* The dependency on `log4net` has been updated from 2.0.0 or 2.0.6 to 2.0.12 depending on the target framework.

## PostSharp.Patterns.Diagnostics.Microsoft
* Support for targeting .NET Standard 1.6 and earlier has been dropped.
* Support for targeting .NET Framework 4.7.0 and earlier has been dropped.

## PostSharp.Patterns.Diagnostics.Serilog
* The dependency on `Serilog` has been updated for .NET Framework from 1.5.9 to 2.3.0 and is now unified for all target frameworks.

## PostSharp.Patterns.Threading
* <xref:PostSharp.Patterns.Threading.DeadlockDetectionPolicy> is now considered obsolete and will not be maintained.