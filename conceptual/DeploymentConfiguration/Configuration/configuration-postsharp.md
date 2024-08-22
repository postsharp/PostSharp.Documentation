---
uid: configuration-postsharp
title: "Well-Known PostSharp Properties"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides a list of well-known PostSharp properties that can be set from the MSBuild project, their corresponding MSBuild property names, and descriptions. It also mentions the option of defining custom PostSharp properties."
---
# Well-Known PostSharp Properties

The following table lists the PostSharp properties that may be set from the MSBuild project. The second column specifies the name of the MSBuild property that influences the value of the PostSharp property, if any.

| Property Name | MSBuild Property Name | Description |
|---------------|-----------------------|-------------|
| `Configuration` | `Configuration` | Build configuration (typically `Debug` or `Release`).  |
| `Platform` | `Platform` | Target processor architecture (typically `AnyCPU`, `x86` or `x64`).  |
| `MSBuildProjectFullPath` | `MSBuildProjectFullPath` | Full path of the C# or VB project being built. |
| `IgnoredAssemblies` |  | Comma-separated list of assembly short names (without extension) that should be ignored by the dependency scanning algorithm. Add an assembly to this list if it is obfuscated, or contains native code, and causes PostSharp to fail. |
| `ReferenceDirectory` | `MSBuildProjectDirectory` | Directory with respect to which relative paths are resolved. |
| `SearchPath` | `PostSharpSearchPath` | Comma-separated list of directories containing reference assemblies and plug-ins. |
| `TargetFrameworkIdentifier` | `TargetFrameworkIdentifier` | Identifier of the target framework of the current project (i.e. the framework on which the application will run). For instance `.NETFramework` or `Silverlight`.  |
| `TargetFrameworkVersion` | `TargetFrameworkVersion` | Version of the target framework of the current project (i.e. the framework on which the application will run). For instance `v4.0`.  |
| `TargetFrameworkProfile` | `TargetFrameworkProfile` | Profile of the target framework of the current project (i.e. the framework on which the application will run). For instance `WindowsPhone`.  |
| `BenchmarkOutputFile` | `PostSharpBenchmarkOutputFile` | When this property is set, PostSharp will append build-time profiling information to a file whose path is set in this property. If the file already exists, PostSharp will append new data to the existing file. PostSharp will lock the file to make sure the option can be used in parallel builds. If the path is a relative path, it will be resolved relatively to the project directory. |

Other properties are recognized but are of little interest for end-users. For a complete list of properties, see *PostSharp.targets*. 


## Using custom properties

By defining your own PostSharp properties, you can pass information from the build environment to aspects, or to any code running in PostSharp. Custom PostSharp properties behave exactly as other PostSharp properties, so they can be defined and read using the same procedures.


