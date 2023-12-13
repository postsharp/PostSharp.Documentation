---
uid: whats-new-60
title: "What's New in PostSharp 6.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 6.0 now supports .NET Core 2.0-2.1 and .NET Standard 2.0, runs natively in .NET Core, and supports C# 7.2. It has also improved the robustness of the logging component and is now GDPR compliant. "
---
# What's New in PostSharp 6.0

The primary focus of PostSharp 6.0 was to execute the PostSharp compiler itself on .NET Core when a .NET Core project is being built. This required the biggest refactoring of our compiler's internals since PostSharp 2.0, which broke backward-compatibility of build-time components, and warranted a major version change.

Additionally, PostSharp 6.0 brings support for C# 7.2 and improves the robustness of the logging component.

> [!IMPORTANT]
> As a major version, PostSharp 6.0 contains some breaking changes. See <xref:breaking-changes-60> for details. 


## Support for .NET Core 2.0-2.1 and .NET Standard 2.0

.NET Core 2.0-2.1 and .NET Standard 2.0 are now completely supported. PostSharp 5.0 used to have partial and buggy support for these frameworks, and significant work was required to get it right (see below).


## PostSharp compiler runs natively in .NET Core

Up to version 5.0, the PostSharp compiler would always run the .NET Framework, even if it was building a project targeting .NET Core. That caused some serious challenges and problems because PostSharp is executing user code at build time, therefore it executed in .NET Framework code written for .NET Core.

Starting from PostSharp 6.0, PostSharp will run under .NET Core if you're building a project for .NET Core. Most of the PostSharp build-time code has been ported to .NET Standard 2.0.

This was a major refactoring and we've repaid a great deal of our technical debt. Support for new releases of .NET Core should become much quicker. This evolution also opens the path to supporting PostSharp on Linux, but there is still work to reach this objective.


## Support for Portable PDB

Portable PDB (both standalone and embedded) are now fully supported.


## Support for C# 7.2

PostSharp 6.0 properly handles and understands `in` parameters. It also understands `ref struct`, but prevents them from being used as parameters in methods that are enhanced by an aspect (except logging). 


## Logging: robustness to logging faults

Errors in the logging component or in your logging code will no longer cause your application to crash. For details, see <xref:logging-exception-handling>. 


## Logging: adapters for log4net and NLog now support .NET Standard

*PostSharp.Patterns.Diagnostics.Log4Net* and *PostSharp.Patterns.Diagnostics.NLog* now support .NET Standard. 


## Logging: no need to initialize before the first logged method is hit

It is now possible to change the logging back-ends dynamically, even after a log record has been emitted. As a result, it is no longer necessary to avoid any logging until the logging service was initialized.


## Caching: preventing concurrent execution (locking)

You can now configure a lock manager to prevent concurrent execution of the same method with the same arguments on different threads, processes, or machines. See <xref:cache-locking> for details. 


## Tools for Visual Studio: support for the new CPS-based project systems.

The new project systems, which work with simpler project files and include built-in support for NuGet package references, are now properly supported.


## GDPR Compliance

PostSharp is now compliant with GDPR. A few changes were needed, including requiring explicit consent before performing license audit (instead of just informing) or sending the newsletter (instead of soft opt in), and making all data transfers secure.


## End of support for Visual Studio 2012

PostSharp 6.0 no longer includes support for Visual Studio 2012, as it is no longer in Microsoft mainstream support.


## End of support for .NET Core 1.1 as a build platform

You can still build .NET Core 1.1 and .NET Standard 1.* projects, but using the .NET Core 2.0 SDK.


## Removal of the UI to use PostSharp without NuGet

If is still possible to use PostSharp without NuGet, but we removed the UI from PostSharp Tools for Visual Studio.


