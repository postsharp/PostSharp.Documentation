---
uid: requirements-20240
title: "PostSharp 2024.0: Requirements and Compatibility"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 2024.0 supports C# 12.0 or earlier and VB 16.9 or earlier. It requires Microsoft Visual Studio 2019 or 2022, .NET Framework 4.7.2 or later, and is compatible with various operating systems and .NET SDK versions. It also supports Xamarin and Blazor via .NET Standard."
---
# PostSharp 2023.0: Requirements and Compatibility

You can use PostSharp to build applications that target a wide range of target devices. This article lists the requirements for development, build and end-user devices.

> [!IMPORTANT]
> Please read our [Supported Platforms Policies](https://www.postsharp.net/support/policies#platforms) on our web site as it contains important explanations, restrictions and disclaimers regarding this article. 


## Supported programming languages

This version of PostSharp supports the following languages:

* C# 12.0 or earlier,

* VB 16.9 or earlier.

PostSharp only supports the compilers that ship with the supported versions of Visual Studio, .NET Core SDK, or .NET SDK. Other compiler versions, especially pre-Roslyn ones, are no longer supported.

You may use PostSharp with an unsupported language version at your own risks by setting the `PostSharpSkipLanguageVersionValidation` MSBuild property to `True`. There are two risks in doing that: inconsistent or erroneous behavior of the current version of PostSharp, and breaking changes in the future version of PostSharp that will support this language version. 


## Requirements on development workstations

This section lists the supported platforms, and most importantly platform versions, on which PostSharp is intended to be used while developing software.

The following software components need to be installed before PostSharp can be used:

* Any of the following versions of Microsoft Visual Studio:
    * Visual Studio 2019 (16.11).
    * Visual Studio 2022 (17.4 and later).

    The debugging experience may be inconsistent with other IDEs than Visual Studio or when PostSharp Tools for Visual Studio are not installed.

* .NET Framework 4.7.2 or later.

* Any of the following operating systems:
    * Windows 10, Windows 11 X64: any version in mainstream Microsoft support, except LTSB and S editions.
    * Experimentally Windows 11 ARM64: any version in mainstream Microsoft support, except LTSB and S editions.

* Optionally, one of the following versions of .NET SDK:
    * .NET SDK 6.0 (build 6.0.100 or later).
    * .NET SDK 7.0 (build 7.0.100 or later).
    * .NET SDK 8.0 (build 8.0.100 or later).

## Requirements on build servers

* Any of the following operating systems currently in mainstream support by respective vendors:
    * Windows Server 2019
    * Windows Server 2022
    * Windows 10 x64
    * Windows 11 x64 or ARM64
    * Ubuntu 18.04 LTS x64, 
    * Ubuntu 20.04 LTS x64,
    * Ubuntu 22.04 LTS x64 or ARM64,
    * macOS x64,
    * macOS ARM64,
    * Alpine 3.x x64.

At least one of the following:
* .NET Framework 4.7.2 and later + Build Tools for Visual Studio 2019 or 2022 or Visual Studio 2022 or 2019.
* .NET SDK 6.0 (build 6.0.100 or later).
* .NET SDK 7.0 (build 7.0.100 or later).
* .NET SDK 8.0 (build 8.0.100 or later).

## Requirements on end-user devices

The following table displays the versions of the target frameworks that are supported by the current release of PostSharp and its components.

| Package | .NET Framework | .NET Core & .NET | .NET Standard*** | Xamarin*** | Blazor*** |
|---------|----------------|------------------|------------------|------------|-----------|
| *PostSharp* | 3.5 SP1, 4.5 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.6 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.7, 4.8 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">**</superscript>  | 6.0, 7.0, 8.0 | 1.3, 1.4, 1.5, 1.6, 2.0, 2.1 | Supported | Supported |
| *PostSharp.Patterns.Common*<br>*PostSharp.Patterns.Aggregation*<br>*PostSharp.Patterns.Model* | 4.5 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.6 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.7, 4.8 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">**</superscript>  | 6.0, 7.0, 8.0 | 1.3, 1.4, 1.5, 1.6, 2.0, 2.1 | Supported | Supported |
| *PostSharp.Patterns.Diagnostics* <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">%</superscript> | 4.5 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.6 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.7, 4.8 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">**</superscript>  | 6.0, 7.0, 8.0 | 1.3, 1.4, 1.5, 1.6, 2.0, 2.1 | Supported | Supported |
| *PostSharp.Patterns.Threading* | 4.5 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.6 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.7, 4.8 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">**</superscript>  | 6.0, 7.0, 8.0 | 1.3, 1.4, 1.5, 1.6, 2.0, 2.1 | Supported | - |
| *PostSharp.Patterns.Xaml* | 4.5 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.6 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.7, 4.8 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">**</superscript>  | 6.0-windows, 7.0-windows, 8.0-windows | - | - | - |
| *PostSharp.Patterns.Caching* <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">%%</superscript> | 4.6.1 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.7, 4.8 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">**</superscript>  | 6.0, 7.0, 8.0 | 2.0, 2.1 | Supported | Supported |

> [!NOTE]
> <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript> .NET Framework versions 4.5, 4.5.1, 4.5.2, 4.6, 4.6.1 are no longer supported by Microsoft. Although we still provide libraries targeting them, we no longer run our tests on these specific versions of the .NET Framework.

> [!NOTE]
> <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">**</superscript> .NET Framework ARM64 is experimentally supported for PostSharp Pattern Libraries. We recommend using PostSharp Pattern Libraries on ARM64 Windows with emulated .NET Framework x64.

> [!NOTE]
> <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">***</superscript> PostSharp does not implicitly support all platforms that support .NET Standard. Only platforms mentioned in this table are supported.

> [!NOTE]
> <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">%</superscript> Each PostSharp.Patterns.Diagnostics logging backend has different set of supported target frameworks. Please refer to individual packages on [nuget.org](http://www.nuget.org).

> [!NOTE]
> <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">%%</superscript> Each PostSharp.Patterns.Caching caching backend has different set of supported target frameworks. Please refer to individual packages on [nuget.org](http://www.nuget.org).


## Compatibility with Xamarin

Xamarin is supported as a runtime platform only via .NET Standard. You can add supported PostSharp packages to your .NET Standard libraries and then reference these libraries in your Xamarin application project. Adding PostSharp directly to a Xamarin application project is not supported.

Xamarin applications use a linker that is executed during build to discard the unused code and reduce the size of the application. To prevent the linker from removing the code required by PostSharp you need to add a custom linker configuration to your project. See <xref:xamarin> for more details. 


## Compatibility with Blazor

Blazor is supported as a runtime platform only via .NET Standard. You can add supported PostSharp packages to your .NET Standard libraries and then reference these libraries in your Blazor application project. Adding PostSharp directly to a Blazor application project is not supported.

Blazor applications use a linker that is executed during build to discard the unused code and reduce the size of the application. To prevent the linker from removing the code required by PostSharp you need to add a custom linker configuration to your project. See <xref:blazor> for more details. 

To debug Blazor applications, `PostSharpDebuggerExtensionsMode` MSBuild property should be set to `Disabled` for all referenced projects using PostSharp, otherwise the debugger may fail to load debugging symbols properly. See <xref:blazor> for more details. 


## Compatibility with ASP.NET 1.0 - 4.8

There are two ways to develop web applications using Microsoft .NET:

* **ASP.NET Application projects ** are very similar to other projects; they need to be built before they can be executed. Since they are built using MSBuild, you can use PostSharp as with any other kind of project. 

* **ASP.NET Site projects ** are very specific: there is no MSBuild project file (a site is actually a directory), and these projects must not be built. ASP.NET Site projects are not supported. 


## Compatibility with Microsoft Code Analysis

By default, PostSharp reconfigures the build process so that Code Analysis is executed on the assemblies as they were *before* being enhanced by PostSharp. If you are using Code Analysis as an integrated part of Visual, no change of configuration is required. 

You can request the Code Analysis to execute on the output of PostSharp by setting the `ExecuteCodeAnalysisOnPostSharpOutput` MSBuild property to `True`. For more information, see <xref:configuration-msbuild>. 


## Compatibility with Microsoft Code Contracts

PostSharp configures the build process so that Microsoft Code Contracts is executed before PostSharp. Additionally, Microsoft Code Contracts' static analyzer will be executed synchronously (instead of asynchronously without PostSharp), which will significantly impact the build performance.


## Compatibility with Obfuscators

PostSharp generates assemblies that are theoretically compatible with all obfuscators.

> [!NOTE]
> PostSharp Logging is not designed to work with obfuscated assemblies.

> [!CAUTION]
> PostSharp emits constructs that are not emitted by Microsoft compilers (for instance `methodof`). These unusual constructs may reveal bugs in third-party tools, because they are generally tested against the output of Microsoft compilers. 


## Compatibility with ARM platforms

**Developing** on ARM platforms (e.g. Mac M1, Raspberry Pi) is not supported. 

**Targeting** ARM platforms is not supported. Target Any-CPU instead. 


## Known Incompatibilities

PostSharp is not compatible with the following products or features:

| Product or Feature | Reason | Workaround |
|--------------------|--------|------------|
| Visual Studio 2017 | No longer under Microsoft mainstream support | Use PostSharp 6.10. |
| Visual Studio 2015 | No longer under Microsoft mainstream support | Use PostSharp 6.5. |
| Visual Studio 2013 | No longer under Microsoft mainstream support | Use PostSharp 6.0. |
| Visual Studio 2012 | No longer under Microsoft mainstream support | Use PostSharp 5.0. |
| Visual Studio 2010 | No longer under Microsoft mainstream support | Use PostSharp 3.1. |
| ILMerge | Bug in ILMerge | Use another merging product (such as ILPack, SmartAssembly). |
| Edit-and-Continue | Not Supported | Rebuild the project after edits |
| Silverlight 3 or earlier | No longer under Microsoft mainstream support | Use PostSharp 2.1. |
| Silverlight 4 | No longer under Microsoft mainstream support | Use PostSharp 3.1. |
| Silverlight 5 | Low customer demand. | Use PostSharp 4.3. |
| .NET Compact Framework | No support for PCL | Use PostSharp 2.1. |
| .NET Framework 4.0 | No longer under Microsoft mainstream support | Target .NET Framework 4.5 or use PostSharp 6.5. |
| .NET Framework 4.5, 4.5.1, 4.5.2, 4.6, 4.6.1 | No longer under Microsoft mainstream support | Target .NET Framework 4.6.2 or use PostSharp 6.10. |
| .NET Framework 2.0 | No longer under Microsoft mainstream support | Target .NET Framework 3.5 or use PostSharp 3.1. |
| .NET Core SDK 1.0, 1.1, 2.2, 3.0, 3.1 | No longer supported by Microsoft (end of life) | Use .NET 8.0 SDK (LTS). |
| .NET SDK 5.0 (any version) | No longer supported by Microsoft (end of life) | Use .NET 8.0 SDK (LTS). |
| Windows Phone 7 | No longer under Microsoft mainstream support | Use PostSharp 3.1 |
| Windows Phone 8, WinRT | Low customer demand. | Use PostSharp 4.3 |
| Visual Studio Express | Microsoft's licensing policy | Use Visual Studio Community Edition |
| ASP.NET Web Sites | Not built using MSBuild | Convert the ASP.NET Web Site to an ASP.NET Web Application. |
| Universal Windows Platform (UWP) | Not supported (low customer demand) | Contact PostSharp support team. |
| Mono, Unity3D | Unsupported | None. |


