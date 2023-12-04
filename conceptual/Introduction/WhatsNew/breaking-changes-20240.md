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
* PostSharp aspects are now required to use `[PSerializable]` for aspects serialization. 
  Projects that require use of binary serialization or need to continue using `[Serializable]` for aspects, should explicitly set `PostSharpBinaryFormatterAllowed` MSBuild property to `True`.
* PostSharp dependency upgrade will lead to package download. When used in environments with restricted access to `nuget.org`, this may require a manual download and deployment of these packages.

## PostSharp.Patterns.Common
* Requires System.Text.RegularExpressions 4.3.1 (was 4.3.0).

## PostSharp.Patterns.Caching
* The default `BinarySerializer` was changed to `JsonCachingSerializer`. In persisted backends this mean that values are not compatible across with older versions of the library and versions should not be mixed.
* `BinarySerializer` is now considered obsolete (produces a warning).
* Supports targeting only .NET Framework 4.6.1 and later and .NET Standard 2.0 and later, .NET Core 3.1 and .NET 5.0 and later.
* Drops targeting support for .NET Core 2.0 to 3.0.
* Drops targeting support for .NET Framework 4.5.0 to 4.6.0.
* Requires System.Text.Json 6.0.0 (new dependency).

## PostSharp.Patterns.Caching.Azure
* Replaces `AzureCacheInvalidator` and `AzureCacheInvalidator2` with `AzureCacheInvalidator3`, which is based on the latest Azure API.
* Supports targeting only .NET Framework 4.7.1 and later and .NET Standard 2.0 and later, .NET Core 3.1 and .NET 5.0 and later.
* Drops targeting support for .NET Core 2.0 to 3.0.
* Drops targeting support for .NET Framework 4.5.0 to 4.7.0.
* Requires Azure.Messaging.ServiceBus, 7.16.1. Depending on target framework this was WindowsAzure.ServiceBus 4.1.2 (.NET Framework) or Microsoft.Azure.ServiceBus 5.2.0 (.NET/.NET Core/.NET Standard). 
* Requires Azure.Identity, 1.10.4.

## PostSharp.Patterns.Diagnostics.AspNetCore
* Requires `Microsoft.AspNetCore.Mvc.Core` 2.1.0 (was 2.0.0).

## PostSharp.Patterns.Diagnostics.HttpClient
* Requires `System.Net.Http` 4.3.4 (was 4.0.0) on .NET Framework.

## PostSharp.Patterns.Diagnostics.ApplicationInsights
* Supports only .NET Framework 4.5.2 and later and .NET Standard 2.0 compatible frameworks.
* Drops support for .NET Standard 1.3 to 1.6.
* Drops support for .NET Framework 4.5.0 and 4.5.1.
* Requires `Microsoft.ApplicationInsights` 2.21.0 (was 2.3.0).

## PostSharp.Patterns.Diagnostics.Log4Net
* Requires `log4net` 2.0.12 (was 2.0.0 or 2.0.6 depending on the target framework).

## PostSharp.Patterns.Diagnostics.Microsoft
* Supports only .NET Standard 2.0 compatible frameworks.
* Drops support for .NET Standard 1.3 to 1.6.
* Drops support for .NET Framework 4.6.0 to 4.7.0.
* Requires `Microsoft.Extensions.Logging` 2.1.0 on all target frameworks.

## PostSharp.Patterns.Diagnostics.Serilog
* Requires `Serilog` 2.3.0 on all target frameworks.

## PostSharp.Patterns.Threading
* `DeadlockDetectionPolicy` is now considered obsolete and will not be maintained.