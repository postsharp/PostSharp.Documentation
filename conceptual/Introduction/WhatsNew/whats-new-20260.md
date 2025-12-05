---
uid: whats-new-20260
title: "What's New in PostSharp 2026.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 2026.0 adds support for .NET 10.0 and C# 14, removes support for older .NET Framework versions (3.5-4.5.1), and unifies Pattern Libraries to target .NET Framework 4.7.1."
---
# What's New in PostSharp 2026.0

PostSharp 2026.0 introduces support for .NET 10.0 and C# 14.0.

> [!NOTE]
> Please note that this release includes several breaking changes. Refer to <xref:breaking-changes-20260> for more detailed information.

## PostSharp Compiler

* PostSharp now supports the .NET 10.0 SDK and C# 14.

* Based on usage data, we've removed PostSharp.Redist support for .NET Framework 3.5-4.5.1, .NET Standard 1.3. This means it is no longer possible to use PostSharp with these target frameworks. The package instead targets .NET Framework 4.5.2 and .NET Standard 2.0.

* We've removed direct support for .NET 5.0 and .NET 7.0 target frameworks. You can still use PostSharp with these target frameworks, but the support is provided through .NET Standard 2.0 and .NET 6.0 library respectively.

* In both above cases, if your projects cannot be compatible with supported target frameworks, please continue using PostSharp 2024.0 LTS, which will be supported for at least a year after 2026.0 is made LTS. Qualifying Enterprise users can request longer support duration than this, which is handled on case-by-case basis.

* In C# 14, extension blocks are implemented by the compiler as static methods and a set of special metadata types that are intended for C# compiler to match extension methods and properties with the receiver type. Since both the implementation methods and the metadata types may not be expected by existing aspects, we've decided to introduce support into the multicast engine. For more information refer to <xref:extension-blocks-multicast>.

* <xref:PostSharp.Aspects.IAspectProvider> and <xref:PostSharp.Aspects.Advices.IAdviceProvider> will now report an when targeting an extension member metadata declaration. For more information see <xref:iaspectprovider#extension-block-members>.

## PostSharp Pattern Libraries

* Dependencies of pattern libraries were upgraded to versions without known security vulnerabilities.

* All Pattern libraries now support .NET 10.0.

* Based on usage data, we've unified support for .NET Framework on version 4.7.1 for all Pattern Libraries and removed libraries targeting all previous versions of .NET Framework. Additionally, we've removed support for .NET Standard 1.3. 

* We've removed direct support for .NET 5.0 and .NET 7.0 target frameworks. You can still use Pattern Libraries with these target frameworks, but support will be provided through .NET Standard 2.0 and .NET 6.0 assemblies respectively.

* As mentioned before, if this affects you, we recommend staying on PostSharp 2024.0 LTS before you are able to upgrade to supported target frameworks.

* Deadlock detection support and APIs were removed from our packages. This component was deprecated in 2024.0 LTS and was not maintained since.

* Undo-redo patterns and `[Recordable]` aspect are now considered obsolete and will not be maintained. The APIs will be removed in a future version.
