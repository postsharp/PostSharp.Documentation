---
uid: requirements-69
title: "PostSharp 6.9: Requirements and Compatibility"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 6.9 supports C# 9.0 and VB 16.0, and requires Microsoft Visual Studio 2017 Update 1 or 2019, .NET Framework 4.7.2, and .NET Core SDK 2.1 or later. It's compatible with Xamarin and Blazor via .NET Standard, but not with ARM platforms or certain older versions of Visual Studio."
---
# PostSharp 6.9: Requirements and Compatibility

You can use PostSharp to build applications that target a wide range of target devices. This article lists the requirements for development, build and end-user devices.

> [!IMPORTANT]
> Please read our [Supported Platforms Policies](https://www.postsharp.net/support/policies#platforms) on our web site as it contains important explanations, restrictions and disclaimers regarding this article. 


## Supported programming languages

This version of PostSharp supports the following languages:

* C# 9.0 or earlier,

* VB 16.0 or earlier.

PostSharp only supports the compilers that ship with the supported versions of Visual Studio, .NET Core SDK, or .NET SDK. Other compiler versions, especially pre-Roslyn ones, are no longer supported.

You may use PostSharp with an unsupported language version at your own risks by setting the `PostSharpSkipLanguageVersionValidation` MSBuild property to `True`. There are two risks in doing that: inconsistent or erroneous behavior of the current version of PostSharp, and breaking changes in the future version of PostSharp that will support this language version. 


## Requirements on development workstations and build servers

This section lists the supported platforms, and most importantly platform versions, on which PostSharp is intended to run.

The following software components need to be installed before PostSharp can be used:

* Any of the following versions of Microsoft Visual Studio:
* Visual Studio 2017 Update 1 (15.9).

* Visual Studio 2019.

    The debugging experience may be inconsistent with other IDEs than Visual Studio or when PostSharp Tools for Visual Studio are not installed.

* .NET Framework 4.7.2 or later.

* Any of the following operating systems:
* Windows 10: any version in mainstream Microsoft support, except LTSB and S editions.

* On build agents only: Windows Server 2012, Windows Server 2012 R2, Windows Server 2016, Ubuntu 16.04, Ubuntu 18.04, Alpine 3.10, macOS 10.14.


* Optionally, one of the following versions of .NET Core SDK:
* .NET Core SDK 2.1 (build 2.1.500 or later).

* .NET Core SDK 3.1 (build 3.1.100 or later).

* .NET SDK 5.0 (build 5.0.100 or later).



## Requirements on end-user devices

The following table displays the versions of the target frameworks that are supported by the current release of PostSharp and its components.

| Package | .NET Framework | .NET Core & .NET | .NET Standard* | Xamarin* | Blazor* |
|---------|----------------|------------------|----------------|----------|---------|
| *PostSharp* | 3.5 SP1, 4.5 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.6, 4.7, 4.8  | 2.1, 3.1, 5.0 | 1.3, 1.4, 1.5, 1.6, 2.0, 2.1 | Supported | Supported |
| *PostSharp.Patterns.Common*<br>*PostSharp.Patterns.Aggregation*<br>*PostSharp.Patterns.Model* | 4.5 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.6, 4.7, 4.8  | 2.1, 3.1, 5.0 | 1.3, 1.4, 1.5, 1.6, 2.0, 2.1 | Supported | Supported |
| *PostSharp.Patterns.Diagnostics* | 4.5 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.6, 4.7, 4.8  | 2.1, 3.1, 5.0 | 1.3, 1.4, 1.5, 1.6, 2.0, 2.1 | Supported | Supported |
| *PostSharp.Patterns.Threading* | 4.5 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.6, 4.7, 4.8  | 2.1, 3.1, 5.0 | 1.3, 1.4, 1.5, 1.6, 2.0, 2.1 | Supported | - |
| *PostSharp.Patterns.Xaml* | 4.5 <superscript xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5">*</superscript>, 4.6, 4.7, 4.8  | 3.1, 5.0-windows | - | - | - |
| *PostSharp.Patterns.Caching* | 4.6, 4.7, 4.8 | 2.1, 3.1, 5.0 | 2.0, 2.1 | Supported | Supported |

> [!NOTE]
> .NET Framework 4.5 is no longer supported by Microsoft. Although we still provide libraries targeting them, we no longer run our tests on these specific versions of the .NET Framework.

> [!NOTE]
> PostSharp does not implicitly support all platforms that support .NET Standard. Only platforms mentioned in this table are supported.


## Compatibility with Xamarin

Xamarin is supported as a runtime platform only via .NET Standard. You can add supported PostSharp packages to your .NET Standard libraries and then reference these libraries in your Xamarin application project. Adding PostSharp directly to a Xamarin application project is not supported.

Xamarin applications use a linker that is executed during build to discard the unused code and reduce the size of the application. To prevent the linker from removing the code required by PostSharp you need to add a custom linker configuration to your project. See <xref:xamarin> for more details. 


## Compatibility with Blazor

Blazor is supported as a runtime platform only via .NET Standard. You can add supported PostSharp packages to your .NET Standard libraries and then reference these libraries in your Blazor application project. Adding PostSharp directly to a Blazor application project is not supported.

Blazor applications use a linker that is executed during build to discard the unused code and reduce the size of the application. To prevent the linker from removing the code required by PostSharp you need to add a custom linker configuration to your project. See <xref:blazor-linker> for more details. 

To debug Blazor applications, `PostSharpDebuggerExtensionsMode` MSBuild property should be set to `Disabled` for all referenced projects using PostSharp, otherwise the debugger may fail to load debugging symbols properly. See <xref:blazor-debugging> for more details. 


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

Development on ARM platforms (e.g. Mac M1, Raspberry Pi) is not supported.

Targeting ARM platforms is not supported. Target Any-CPU instead.


## Known Incompatibilities

PostSharp is not compatible with the following products or features:

| Product or Feature | Reason | Workaround |
|--------------------|--------|------------|
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
| .NET Framework 2.0 | No longer under Microsoft mainstream support | Target .NET Framework 3.5 or use PostSharp 3.1. |
| .NET Core SDK 1.0, 1.1, 2.2, 3.0 (any version) | No longer supported by Microsoft (end of life) | Use .NET Core 3.1 (LTS). |
| Windows Phone 7 | No longer under Microsoft mainstream support | Use PostSharp 3.1 |
| Windows Phone 8, WinRT | Low customer demand. | Use PostSharp 4.3 |
| Visual Studio Express | Microsoft's licensing policy | Use Visual Studio Community Edition |
| ASP.NET Web Sites | Not built using MSBuild | Convert the ASP.NET Web Site to an ASP.NET Web Application. |
| Universal Windows Platform (UWP) | Not supported (low customer demand) | Contact PostSharp support team. |
| Mono, Unity3D | Unsupported | None. |


