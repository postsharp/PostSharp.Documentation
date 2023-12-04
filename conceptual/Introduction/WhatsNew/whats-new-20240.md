---
uid: whats-new-20240
title: "What's New in PostSharp 2024.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 2024.0

PostSharp 2024.0 brings a major platform update, including support for new build and target platforms, updated dependencies of many packages.

> [!NOTE]
> Note that this release contains multiple breaking changes. See <xref:breaking-changes-20240> for details. 

## PostSharp Core

* PostSharp now supports .NET 8.0 SDK and C# 12.

* When building .NET Framework projects, PostSharp executes as x64 server process by default. When running as x86 process, it will run by default as a standalone process. This helps avoiding memory issues in x86 server compilations.

* PostSharp internal build-time dependencies were upgraded. This included upgrading PostSharp .NET compiler to target .NET 6.0. Minimum supported version of SDK is now .NET SDK 6.0.

* PostSharp newly supports ARM64 on Windows ARM64, Linux ARM64 and MacOS on Apple Silicon when building .NET, .NET Core and .NET Standard projects under .NET 8.0 SDK. On Windows ARM64 it is possible to target all supported platforms - x86, x64, ARM64 by cross-targeting using different versions of the .NET SDK (exactly the same version of the SDK is required to be installed).

* PostSharp now also supports ARM64 when building .NET Framework projects on Windows ARM64 under .NET Framework 4.8.1. On Windows ARM64, PostSharp will run as ARM64 server process by default when targeting AnyCPU. On other platforms, .NET Framework 4.7.2 remains the minimum required version.

* Usage of `BinaryFormatter` during aspect serialization, used when aspects are marked with `[Serializable]` is deprecated due to security concerns. See more about the security vulnerability [here](https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide). Usage of `[PSerializable]` is advised. An MSBuild property `PostSharpBinaryFormatterAllowed` has been added to allow usage of the old method in legacy application, but it is a discouraged and unsupported use case.

## PostSharp Pattern Libraries

* Dependencies were upgraded to versions without known security vulnerabilities. This affected most pattern libraries without breaking changes, but some pattern libraries dropped support for older target frameworks, notable `PostSharp.Patterns.Caching`, `PostSharp.Patterns.Caching.Azure`, `PostSharp.Patterns.Diagnostics.Microsoft`, `PostSharp.Patterns.Diagnostics.ApplicationInsights`.

* PostSharp.Patterns.Caching now uses JsonCachingSerializer by default instead of BinarySerializer, which is now deprecated. This serializer internally used BinaryFormatter, which is considered dangerous. Again, see more about the security vulnerability [here](https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide).

* PostSharp.Patterns.Caching.Azure now uses the latest Azure Service Bus API - Azure.ServiceBus NuGet package - through the new invalidator class `AzureCacheInvalidator3` that is available on all supported target frameworks. Previous implementations are now deprecated - `AzureCacheInvalidator`, `AzureCacheInvalidator2`.

* PostSharp.Patterns.Threading.DeadlockDetectionPolicy is now marked as obsolete. The aspect was implemented before async methods were added to C# and it's usefulness for development of modern application is very low. Since async
methods are now an ubiquitous part of .NET libraries, it's almost impossible to detect deadlocks through build-time code instrumentation of your code as there will almost always be an async method between synchronization actions. Please refer to [Microsoft guide for debugging deadlocks](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/debug-deadlock).