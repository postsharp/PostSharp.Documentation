---
uid: whats-new-20240
title: "What's New in PostSharp 2024.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 2023.0 introduces support for .NET 8.0 and C# 12, including ARM64 build platforms and experimental support for ARM64 .NET Framework and Visual Studio."
---
# What's New in PostSharp 2024.0

PostSharp 2024.0  introduces a significant platform update, incorporating support for new build and target platforms along with updated dependencies for numerous packages.

> [!NOTE]
> Please note that this release includes multiple breaking changes. Refer to <xref:breaking-changes-20240> for more detailed information.

## PostSharp Compiler

* PostSharp now supports the .NET 8.0 SDK and C# 12.

* For .NET Framework projects, PostSharp defaults to executing as an x64 server process. When running as an x86 process, it defaults to operating as a standalone process, which helps prevent memory issues in x86 server compilations.

* PostSharp's internal build-time dependencies were upgraded. This included upgrading PostSharp .NET compiler to target .NET 6.0. Minimum supported version of SDK is now .NET SDK 6.0.

* PostSharp now offers support for ARM64 on Windows ARM64, Linux ARM64 and MacOS on Apple Silicon when building .NET, .NET Core and .NET Standard projects under the .NET 8.0 SDK. On Windows ARM64, it is possible to target all supported target platforms - x86, x64, ARM64 - by cross-targeting with different versions of the .NET SDK. It is crucial to install the exact same version of the .NET SDK for all required platforms.

* PostSharp now also supports ARM64 when building .NET Framework projects on Windows ARM64 under .NET Framework 4.8.1. On Windows ARM64, PostSharp defaults to running as an ARM64 server process by default when targeting AnyCPU. On other build platforms, the minimum required version remains .NET Framework 4.7.2.

* The usage of `BinaryFormatter` during aspect serialization - employed when aspects are marked with `[Serializable]` - is deprecated due to security concerns. More information about the security vulnerability can be found [here](https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide). It is recommended to use [PSerializable] instead. An MSBuild property, `PostSharpBinaryFormatterAllowed`, has been has been introduced to permit the usage of the old method in legacy applications, but this is a discouraged and unsupported practice.

## PostSharp Pattern Libraries

* Dependencies of pattern libraries were upgraded to versions without known security vulnerabilities. This affected most pattern libraries without causing breaking changes, but some pattern libraries dropped support for older target frameworks, notably `PostSharp.Patterns.Caching`, `PostSharp.Patterns.Caching.Azure`, `PostSharp.Patterns.Diagnostics.Microsoft`, `PostSharp.Patterns.Diagnostics.ApplicationInsights`.

* In `PostSharp.Patterns.Caching`, the default serializer is now `JsonCachingSerializer`, replacing the deprecated `BinarySerializer`. The legacy use of `BinarySerializer` internally relied on `BinaryFormatter`, considered unsafe due to security vulnerabilities. Further details on this security concern can be found [here](https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide).

* `PostSharp.Patterns.Caching.Azure` now uses the `Azure.ServiceBus` package through the new invalidator class <xref:PostSharp.Patterns.Caching.Backends.Azure.AzureServiceBusCacheInvalidator> for all supported target frameworks (.NET Framework 4.7.1 and later, .NET Core 3.1 and later). Previous implementations, `AzureCacheInvalidator` and `AzureCacheInvalidator2`, are deprecated.  

* <xref:PostSharp.Patterns.Threading.DeadlockDetectionPolicy> is now marked as obsolete. The aspect was designed before async methods were added to the C# language and its utility for development for modern application development is minimal. Due to the ubiquity of async methods in .NET libraries, detecting deadlocks through build-time code instrumentation is nearly impossible. Typically, a non-instrumented async method exists between synchronization actions forming a deadlock. For guidance on debugging deadlocks, please consult the [Microsoft guide for debugging deadlocks](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/debug-deadlock).

## PostSharp Tools for Visual Studio

* PostSharp Tools for Visual Studio can be installed on Visual Studio 2022 ARM64, but this is currently supported only experimentally.