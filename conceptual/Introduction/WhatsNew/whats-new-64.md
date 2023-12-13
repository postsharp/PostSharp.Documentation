---
uid: whats-new-64
title: "What's New in PostSharp 6.4"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 6.4 introduces full support for .NET Core 3.0, .NET Standard 2.1, and C# 8.0. It also fixes field and property initializer interception, allows free ordering of OnMethodBoundary aspects, and can export build-time profiling information to a CSV file."
---
# What's New in PostSharp 6.4

Note there is a breaking change in <xref:PostSharp.Aspects.LocationInterceptionAspect>. See <xref:breaking-changes-64> for details. 


## Support for .NET Core 3.0 and .NET Standard 2.1

PostSharp now fully supports .NET Core 3.0 and .NET Standard 2.1. We've also updated our package *PostSharp.Patterns.Xaml* to make sure it works with WPF on .NET Core 3.0. 


## Support for C# 8.0

Several new features of C# 8.0 affected PostSharp: default interface methods, nullable reference types, async streams, and read-only struct members.

We have tested and fixed PostSharp for all these features.

Note that async streams are not yet idiomatically supported in PostSharp Aspect Framework, i.e. PostSharp will not be able to apply semantic advising to methods returning an async stream (see <xref:semantic-advising> for details). PostSharp will treat them as plain methods returning an `object`. The caching aspect also does not support methods returning an async stream. 


## Support for field/property initializers

In previous versions of PostSharp, a <xref:PostSharp.Aspects.LocationInterceptionAspect> could not properly intercept field and property initializers. That is, you could not react to the situation where the field or property was assigned on the same line as the declaration. Initializers were simply ignored. 

This has been fixed in PostSharp 6.4, and this is a breaking change.

Initialization of *static* fields and properties is now intercepted by the <xref:PostSharp.Aspects.LocationInterceptionAspect.OnSetValue(PostSharp.Aspects.LocationInterceptionArgs)> advice just as any other assignment. 

However, initialization of *instance* fields cannot be intercepted because <xref:PostSharp.Aspects.LocationInterceptionAspect> expects the current object to be already initialized (and the `this` reference to be usable), which is not the case until the base constructor has been called. Therefore, we defined a new advice method <xref:PostSharp.Aspects.LocationInterceptionAspect.OnInstanceLocationInitialized(PostSharp.Aspects.LocationInitializationArgs)> that is being called as soon as the base constructor has completed. 


## Free ordering of OnMethodBoundary aspects on iterators with semantic advising

It is now possible to freely order an <xref:PostSharp.Aspects.OnMethodBoundaryAspect> before or after a <xref:PostSharp.Aspects.MethodInterceptionAspect> on iterator methods, even with semantic advising enabled. 

Previously, on iterator methods, aspects of type <xref:PostSharp.Aspects.OnMethodBoundaryAspect> had to be ordered after any <xref:PostSharp.Aspects.MethodInterceptionAspect>. 

For details, see <xref:semantic-advising> 


## Export of build-time profiling information to a CSV file

This option is meant to help us diagnose performance issues with PostSharp when there are several projects in the solution. It is now possible to export build-time profiling information to a CSV file. This performance file can be shared by several projects, and then analyzed by a tool like Pivot Table in Excel. For details, see the `BenchmarkOutputFile` property in <xref:configuration-postsharp>. 

Note that this feature is meant to be used by our support team only. It currently does not include any information that could be useful to users.


