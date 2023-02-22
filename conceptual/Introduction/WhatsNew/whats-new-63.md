---
uid: whats-new-63
title: "What's New in PostSharp 6.3"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 6.3

Note that we've updated our platform requirements. See <xref:breaking-changes-63> for details. 


## Support for building on Linux and MacOS

It is now possible to build .NET Core projects on Linux and MacOS. All distributions officially supported by .NET Core have been tested.


## PostSharp Tools for Visual Studio improvements

* **Performance improvements**. We now use all asynchronous APIs of Visual Studio SDK, and we've rewritten some old WinForms controls into WPF. 

* **Support for shared and multi-target projects**. When editing a source file shared by several projects or targets, and if different aspects were used (e.g. using conditional compilation), the code adornments and tooltips now accurately reflect the aspects applied for the current project and/or target. The Aspect Explorer has been updated as well. Previously, PostSharp Tools for Visual Studio did not differentiate assemblies of the same name. 

* **Support for per-monitor awareness of DPI**. 

* **Redesign of the Code Action manager** dialog, which is now available as a page under the Options dialog box. 


## Support for Deterministic Build

PostSharp should now respect the [-deterministic](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/deterministic-compiler-option) compiler option. When this option is enabled, subsequent builds with the exact same input will result in the exact same output. 


## Free ordering of OnMethodBoundary aspects without OnYield/OnResume advices on async methods

It is now possible to freely order an <xref:PostSharp.Aspects.OnMethodBoundaryAspect> before or after a <xref:PostSharp.Aspects.MethodInterceptionAspect>, but only if the <xref:PostSharp.Aspects.OnMethodBoundaryAspect> does not implement the <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnYield(PostSharp.Aspects.MethodExecutionArgs)> or <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnResume(PostSharp.Aspects.MethodExecutionArgs)> advices. 

Previously, on async or iterator methods, aspects of type <xref:PostSharp.Aspects.OnMethodBoundaryAspect> had to be ordered after any <xref:PostSharp.Aspects.MethodInterceptionAspect>. 

Note that ordering is still limited for iterator methods.


## Contracts: ability to customize the type of thrown exceptions

In previous versions, it was possible to customize the exception messages, but not the exception types itself. We now use a factory pattern to instantiate exceptions, so you can now completely customize the exceptions thrown by standard contracts. See <xref:contract-custom-exceptions> for details. 

