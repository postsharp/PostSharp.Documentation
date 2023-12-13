---
uid: breaking-changes-67
title: "Breaking Changes in PostSharp 6.7"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Breaking Changes in PostSharp 6.7

PostSharp 6.7 contains the following breaking changes:


## Changes causing build errors

* Previously, a `[Command]` property where an `Execute` method could not be found did nothing. Now, this will emit an error. 

* Attempting to add a `[Cache]` aspect to a method with `in` or `ref` parameters will now emit an error instead of ignoring these parameters. 


## Changes causing a silent change in behavior

* Logging: indentation of async methods in logging is no longer based on the execution context but rather on the thread context. This improves performance.

* Logging: the <xref:PostSharp.Patterns.Diagnostics.Contexts.LoggingContext.ParentContext> property now always refers to the thread context. 

* Threading: read-only fields and getter-only properties in classes with a threading model are now protected by the threading model. They were previously ignored. This change would typically not affect your release builds since threading models are only enforced in debug builds by default.


## Deprecated platforms and features

* The PostSharp Serilog backend now requires Serilog 2.3.0 at least, and .NET Framework 4.5.0 at least.

* .NET Framework 4.0 is no longer supported. The oldest supported version of .NET Framework becomes 4.5.0.

* Visual Studio 2015 is no longer supported. Visual Studio 2017 Update 1 (15.9) is now the oldest supported version.

If you cannot update your development environments or your projects, we suggest that you use PostSharp 6.5 LTS.

