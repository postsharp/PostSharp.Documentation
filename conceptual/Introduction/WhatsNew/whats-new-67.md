---
uid: whats-new-67
title: "What's New in PostSharp 6.7"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 6.7

> [!NOTE]
> PostSharp 6.7 removes support for .NET Framework 4.0 and Visual Studio 2015, and has several other minor breaking changes. See <xref:breaking-changes-67> for details. 


## Support for Xamarin and Blazor

We have revamped our test suite and we can now confidently say that we are supporting Xamarin and Blazor (both client- and server-side) as run-time platforms, but still only through .NET Standard.

That means that you can now add supported PostSharp packages to your .NET Standard libraries and then reference these libraries in your Xamarin or Blazor application project. Adding PostSharp directly to a Xamarin or Blazor application project is not supported. For details, see <xref:xamarin> and <xref:blazor>. 


## End of Support for .NET Framework 4.0 and Visual Studio 2015

We're sorry-not-sorry to say goodbye to .NET Framework 4.0, released in March 2010 and supported by Microsoft until January 2016. The oldest supported version of .NET Framework becomes 4.5.0.

Our telemetry shows that a couple of percent of users are still targeting .NET Framework 4.0. However, we have taken this decision to allow ourselves to move forward with our build and test pipeline, since recent frameworks no longer support old platforms.

Consistently with our [support policies](https://www.postsharp.net/support/policies), we are also retiring support for Visual Studio 2015. Microsoft has ceased to provide mainstream support for Visual Studio 2015 in October 2020. 

For users who cannot update their development environment, we recommend to stay with PostSharp 6.5, our latest Long-Term Support version.


## Logging: better integration with existing logging frameworks

We're introducing two features to make it easier to use PostSharp Logging with existing logging frameworks:

* PostSharp Logging can now collect log records coming from hand-written logging code and redirect them into its own pipeline. For details, see <xref:log-collecting>. 

* PostSharp Logging can now write its output to several target frameworks instead of just one. For details, see <xref:log-multiplexer>. 


## Better support for async iterators

The <xref:PostSharp.Aspects.OnMethodBoundaryAspect> now supports async iterators for <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)>, <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnSuccess(PostSharp.Aspects.MethodExecutionArgs)>, <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnException(PostSharp.Aspects.MethodExecutionArgs)> and <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnExit(PostSharp.Aspects.MethodExecutionArgs)> advices. 


## Telemetry: reporting of performance issues

We are now detecting when PostSharp Tools for Visual Studio blocks the UI thread and, with your permission, we will report these situations to our online telemetry service. We have also improved the user experience for exception reports.


## No registration needed

We are no longer asking your email address to start with the trial or with PostSharp Community.

