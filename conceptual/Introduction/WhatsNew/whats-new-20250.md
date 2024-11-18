---
uid: whats-new-20250
title: "What's New in PostSharp 2025.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 2025.0 adds support for .NET 9.0, Windows long paths and improves support for Windows PDB."
---
# What's New in PostSharp 2025.0

PostSharp 2025.0 introduces support for .NET 9.0 along with some platform updates for Windows users.

> [!NOTE]
> Please note that this release includes several breaking changes. Refer to <xref:breaking-changes-20250> for more detailed information.

## PostSharp Compiler

* PostSharp now supports the .NET 9.0 SDK and C# 13.

* PostSharp's internal build-time dependencies were upgraded. This included upgrading PostSharp .NET compiler to target .NET 8.0. Minimum supported version of SDK is now .NET SDK 8.0.

* PostSharp now supports Windows Long paths if enabled in Windows. This removes some previous limitations when searching for assemblies and for aspect code. For more information, please [read Microsoft Documentation](https://learn.microsoft.com/en-us/windows/win32/fileio/maximum-file-path-limitation).

* PostSharp's support of Windows PDB was revamped, and now uses Microsoft libraries for both reading and writing PDBs. This allowed for supporting newer WindowsPDB features like embedded source and source-link, along with many other minor improvements.

## PostSharp Pattern Libraries

* Dependencies of pattern libraries were upgraded to versions without known security vulnerabilities.