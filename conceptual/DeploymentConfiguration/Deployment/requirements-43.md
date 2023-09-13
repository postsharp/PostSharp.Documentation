---
uid: requirements-43
title: "PostSharp 4.3: Requirements and Compatibility"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# PostSharp 4.3: Requirements and Compatibility

You can use PostSharp to build applications that target a wide range of target devices. This article lists the requirements on development, build and end-user devices.


## Requirements on development workstations and build servers

The following software components need to be installed before PostSharp can be used:

* Microsoft Visual Studio 2012 or 2013, 2015 except Express editions, but including Community Edition (Visual Studio is not required on build servers).

* .NET Framework 4.5.

* Windows Vista SP2, Windows 7 SP1, Windows 8, Windows 8.1, Windows Server 2003 SP2, Windows Server 2003 R2 SP2, Windows Server 2008 SP2, Windows Server 2008 R2 SP1, Windows Server 2012, Windows Server 2012 R2.

* NuGet Package Manager 2.2 or later.

> [!NOTE]
> The latest version of NuGet Package Manager will be installed automatically by PostSharp if NuGet 2.2 is not already installed. This operation requires administrative privileges.

> [!CAUTION]
> NuGet Package Manager needs to be configured manually in order to match the requirements of some corporate environments, especially in situations with a large number of Visual Studio solutions. Please contact our technical support if this is a concern for your team.


## Requirements on end-user devices

The following table displays the versions of the target frameworks that are supported by the current release of PostSharp and its components.

| PostSharp Component | .NET Framework | Silverlight | Windows Phone (Silverlight) | Windows Phone (WinRT) | Windows (WinRT) | Xamarin |
|---------------------|----------------|-------------|-----------------------------|-----------------------|-----------------|---------|
| Aspect Framework | 3.5 SP1, 4.0, 4.5, 4.6 | 4, 5 | 7, 8 | 8.1 | 8, 8.1 | 3.8 |
| Architecture Framework | 3.5 SP1, 4.0, 4.5, 4.6 | 4, 5 | 7, 8 | 8.1 | 8, 8.1 | 3.8 |
| Diagnostics Pattern Library | 4.0, 4.5, 4.6 | - | - | - | - | - |
| Model Pattern Library | 4.0, 4.5, 4.6 | - | 8 | 8.1 | 8, 8.1 | 3.8 |
| Threading Pattern Library | 4.0, 4.5, 4.6 | - | 8 | 8.1 | 8, 8.1 | 3.8 |
| Threading Pattern Library - Deadlock Detection | 4.0, 4.5, 4.6 | - | - | - | - | - |

> [!NOTE]
> PostSharp supports *Portable Class Library* projects that target frameworks shown in the table. 


## Compatibility with ASP.NET

There are two ways to develop web applications using Microsoft .NET:

* **ASP.NET Application projects ** are very similar to other projects; they need to be built before they can be executed. Since they are built using MSBuild, you can use PostSharp as with any other kind of project. 

* **ASP.NET Site projects ** are very specific: there is no MSBuild project file (a site is actually a directory), and these projects must not be built. ASP.NET Site projects are not supported. 

Additionally, ASP.NET "vNext", which has a different project system than MSBuild, is not supported.


## Compatibility with Microsoft Code Analysis

By default, PostSharp reconfigures the build process so that Code Analysis is executed on the assemblies as they were *before* being enhanced by PostSharp. If you are using Code Analysis as an integrated part of Visual, no change of configuration is required. 

You request the Code Analysis to execute on the output of PostSharp by setting the `ExecuteCodeAnalysisOnPostSharpOutput` MSBuild property to `True`. For more information, see <xref:configuration-msbuild>. 


## Compatibility with Microsoft Code Contracts

PostSharp configures the build process so that Microsoft Code Contracts is executed before PostSharp. Additionally, Microsoft Code Contracts' static analyzer will be executed synchronously (instead of asynchronously without PostSharp), which will significantly impact the build performance.


## Compatibility with Obfuscators

Starting from version 3, PostSharp generates assemblies that are theoretically compatible with all obfuscators.

> [!CAUTION]
> PostSharp 3 generates constructs that are not emitted by Microsoft compilers (for instance `methodof`). These unusual constructs may reveal bugs in third-party tools, because they are generally tested against the output of Microsoft compilers. 


## Known Incompatibilities

PostSharp is not compatible with the following products or features:

| Product or Feature | Reason | Workaround |
|--------------------|--------|------------|
| Visual Studio 2010 | Not Supported | Use PostSharp 3.1. |
| ILMerge | Bug in ILMerge | Use another product (such as SmartAssembly). |
| Edit-and-Continue | Not Supported | Rebuild the project after edits |
| Silverlight 3 or earlier | No longer under Microsoft mainstream support | Use PostSharp 2.1 or Silverlight 5 |
| Silverlight 4 | No longer under Microsoft mainstream support | Use PostSharp 3.1 or Silverlight 5 |
| .NET Compact Framework | No support for PCL | Use PostSharp 2.1 or Windows Phone 8 |
| .NET Framework 2.0 | No longer under Microsoft mainstream support | Target .NET Framework 3.5 or use PostSharp 3.1 |
| Windows Phone 7 | No longer under Microsoft mainstream support | Target Windows Phone 8 or use PostSharp 3.1 |
| Mono | Not Supported | Compile on Windows using MSBuild |
| Visual Studio Express | Microsoft's licensing policy | Use Visual Studio Community Edition |
| Delayed strong-name signing on cloud build servers | No way to unregister verification of strong names | Use normal (non-delayed) strong-name signing or use build servers where you have administrative access. |
| ASP.NET Web Sites | Not built using MSBuild | Convert the ASP.NET Web Site to an ASP.NET Web Application. |

