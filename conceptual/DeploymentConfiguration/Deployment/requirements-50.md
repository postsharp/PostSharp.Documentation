---
uid: requirements-50
title: "PostSharp 5.0: Requirements and Compatibility"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document details the requirements and compatibility for using PostSharp 5.0, including software components that need to be installed, requirements for end-user devices, and known incompatibilities and issues."
---
# PostSharp 5.0: Requirements and Compatibility

You can use PostSharp to build applications that target a wide range of target devices. This article lists the requirements for development, build and end-user devices.


## Requirements on development workstations and build servers

The following software components need to be installed before PostSharp can be used:

* Any of the following versions of Microsoft Visual Studio:
* Visual Studio 2012 Update 5

* Visual Studio 2013 Update 5

* Visual Studio 2015 Update 3

* Visual Studio 2017 Update 2

    Note that Visual Studio Express is not supported.
    The debugging experience may be inconsistent with other IDEs than Visual Studio or when PostSharp Tools for Visual Studio are not installed.

* .NET Framework 4.6 or 4.7.

* Windows 7 SP1, Windows 8.1, Windows 10, Windows Server 2008 SP2, Windows Server 2008 R2 SP1, Windows Server 2012, Windows Server 2012 R2, Windows Server 2016

* NuGet Package Manager 2.8 or later.

Please also see our [support policies](https://www.postsharp.net/support/policies). 

> [!NOTE]
> The latest version of NuGet Package Manager will be installed automatically by PostSharp if NuGet 2.8 is not already installed. This operation requires administrative privileges.

> [!CAUTION]
> NuGet Package Manager needs to be configured manually in order to match the requirements of some corporate environments, especially in situations with a large number of Visual Studio solutions. Please contact our technical support if this is a concern for your team.


## Requirements on end-user devices

The following table displays the versions of the target frameworks that are supported by the current release of PostSharp and its components.

| Package | .NET Framework | .NET Core | .NET Standard |
|---------|----------------|-----------|---------------|
| *PostSharp* | 3.5 SP1, 4.0, 4.5, 4.6, 4.7 | 1.1 | 1.3 |
| *PostSharp.Patterns.Common*<br>*PostSharp.Patterns.Aggregation*<br>*PostSharp.Patterns.Threading*<br>*PostSharp.Patterns.Model*<br>*PostSharp.Patterns.Diagnostics* | 4.0, 4.5, 4.6, 4.7 | 1.1 | 1.3 |
| *PostSharp.Patterns.Xaml* | 4.0, 4.5, 4.6, 4.7 | - | - |
| *PostSharp.Patterns.Caching* | 4.6, 4.7 | - | - |

> [!NOTE]
> PostSharp does not implicitly support all platforms that support .NET Standard. Only platforms mentioned in this table are supported.

> [!NOTE]
> .NET Standard 2.0 and .NET Core 2.0 are supported in PostSharp 5.1.


## Compatibility with ASP.NET

There are two ways to develop web applications using Microsoft .NET:

* **ASP.NET Application projects ** are very similar to other projects; they need to be built before they can be executed. Since they are built using MSBuild, you can use PostSharp as with any other kind of project. 

* **ASP.NET Site projects ** are very specific: there is no MSBuild project file (a site is actually a directory), and these projects must not be built. ASP.NET Site projects are not supported. 


## Compatibility with Microsoft Code Analysis

By default, PostSharp reconfigures the build process so that Code Analysis is executed on the assemblies as they were *before* being enhanced by PostSharp. If you are using Code Analysis as an integrated part of Visual, no change of configuration is required. 

You request the Code Analysis to execute on the output of PostSharp by setting the `ExecuteCodeAnalysisOnPostSharpOutput` MSBuild property to `True`. For more information, see <xref:configuration-msbuild>. 


## Compatibility with Microsoft Code Contracts

PostSharp configures the build process so that Microsoft Code Contracts is executed before PostSharp. Additionally, Microsoft Code Contracts' static analyzer will be executed synchronously (instead of asynchronously without PostSharp), which will significantly impact the build performance.


## Compatibility with Obfuscators

PostSharp generates assemblies that are theoretically compatible with all obfuscators.

> [!NOTE]
> PostSharp Diagnostics is not designed to work with obfuscated assemblies.

> [!CAUTION]
> PostSharp constructs that are not emitted by Microsoft compilers (for instance `methodof`). These unusual constructs may reveal bugs in third-party tools, because they are generally tested against the output of Microsoft compilers. 


## Known Incompatibilities

PostSharp is not compatible with the following products or features:

| Product or Feature | Reason | Workaround |
|--------------------|--------|------------|
| Visual Studio 2010 | Not Supported | Use PostSharp 3.1. |
| ILMerge | Bug in ILMerge | Use another merging product (such as ILPack, SmartAssembly). |
| Edit-and-Continue | Not Supported | Rebuild the project after edits |
| Silverlight 3 or earlier | No longer under Microsoft mainstream support | Use PostSharp 2.1. |
| Silverlight 4 | No longer under Microsoft mainstream support | Use PostSharp 3.1. |
| Silverlight 5 | Low customer demand. | Use PostSharp 4.3. |
| .NET Compact Framework | No support for PCL | Use PostSharp 2.1. |
| .NET Framework 2.0 | No longer under Microsoft mainstream support | Target .NET Framework 3.5 or use PostSharp 3.1. |
| Windows Phone 7 | No longer under Microsoft mainstream support | Use PostSharp 3.1 |
| Windows Phone 8, WinRT | Low customer demand. | Use PostSharp 4.3 |
| Visual Studio Express | Microsoft's licensing policy | Use Visual Studio Community Edition |
| ASP.NET Web Sites | Not built using MSBuild | Convert the ASP.NET Web Site to an ASP.NET Web Application. |
| Universal Windows Platform (UWP) | Not supported (low customer demand) | Contact PostSharp support team. |
| Xamarin | Support suspended (deprioritized because of low customer demand) | Use PostSharp 4.3. Contact PostSharp support team to discuss prioritization. |
| Mono, Unity3D | Unsupported | None. |
| .NET Standard 2.0 | Not supported. | Use PostSharp 5.1. |


## Known Issues

Known issues are documented in the release notes of each product build.


