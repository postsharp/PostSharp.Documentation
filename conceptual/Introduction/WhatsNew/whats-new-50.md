---
uid: whats-new-50
title: "What's New in PostSharp 5.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 5.0

As a new major version, PostSharp 5.0 was the opportunity to introduce a few breaking changes. First, we dropped support for Microsoft's failed platforms Windows Phone and WinRT in favor of .NET Core, .NET Standard and Visual Studio 2017. Then, we completely revisited PostSharp Logging. Additionally, we added a caching aspect and three MVVM aspects.

> [!IMPORTANT]
> As a major version, PostSharp 5.0 contains some breaking changes. See <xref:breaking-changes-50> for details. 


## Logging: complete revamping

That's a complete rewrite! The new PostSharp Logging is fully customizable and faster than ever. See <xref:logging> for details. 


## Caching: a brand new feature

We've added a brand new ready-made caching framework, which includes not only a caching aspect but also a cache invalidation aspect. PostSharp Caching 5.0 comes with support for MemoryCache and Redis. See <xref:caching> for details. 


## Filled gaps in support for async methods

We've put a lot of efforts to put async methods on par with normal methods in PostSharp:

* The <xref:PostSharp.Aspects.MethodInterceptionAspect> now has an <xref:PostSharp.Aspects.MethodInterceptionAspect.OnInvokeAsync(PostSharp.Aspects.MethodInterceptionArgs)> advice, which you can implement to intercept async methods. 

* You can now set the return value and change the <xref:PostSharp.Aspects.MethodExecutionArgs.ReturnValue> and the <xref:PostSharp.Aspects.MethodExecutionArgs.FlowBehavior> of an async method in an <xref:PostSharp.Aspects.OnMethodBoundaryAspect> aspect. 

* The <xref:PostSharp.Aspects.MethodInterceptionAspect> and <xref:PostSharp.Aspects.OnMethodBoundaryAspect> aspects now advise async methods and iterators semantically by default (i.e. the state machine itself is advised), while previous versions advised the kick-off methods by default. The new property <xref:PostSharp.Aspects.MethodInterceptionAspect.SemanticallyAdvisedMethodKinds> controls whether the advice is applied semantically or not. The <xref:PostSharp.Aspects.MethodInterceptionAspect.UnsupportedTargetAction> property determines what should be done when semantic advising of the current method is not supported. 


## XAML: Command, Dependency Property and Attached Property

If you're writing XAML applications, you probably wrote a lot of boilerplate code for commands and dependency properties. We've created new aspects to automate that. See <xref:xaml> for details. 


## Code Contracts: support for out parameters and return values

It is now possible to add code contracts to return values and `out` or `ref` parameters. The values are validated when the method succeeds. 


## Architecture Framework: new constraints

PostSharp 5.0 adds three constraints:

* The <xref:PostSharp.Constraints.NamingConventionAttribute> constraint is a simple way to force derived class to respect some naming convention. 

* The <xref:PostSharp.Constraints.ParameterValueConstraint> abstract constraint lets you validate, at build-time, the value passed to method parameters. 

* The <xref:PostSharp.Constraints.ReferenceConstraint> abstract constraint allows you to validate which code uses (i.e. references) your type or method. 


## Support for Visual Studio 2017

PostSharp 5.0 supports Visual Studio 2017 including the new project format (package references) and the new features of C# 7.0.


## Support for .NET Core and .NET Standard

PostSharp 5.0 supports .NET Core 1.0 and .NET Standard 1.3 in Visual Studio 2017 and .NET Core CLI. See <xref:requirements> for details. 


## Support for NuGet 3

PostSharp 5.0 supports NuGet 3 and it no longer needs *install.ps1* to introduce itself into the build chain. 


## End of support for Windows Phone, WinRT and Silverlight

Let's face it, these platforms were a failure. We no longer want to pay a price for that and we're dropping support in PostSharp 5.0.


## Suspended support for Xamarin

Usage data show that only a few customers are using PostSharp with Xamarin, so we demoted the priority of this platform. It is not supported in PostSharp 5.0. Affected customers should contact our support team. We're considering to support Xamarin through .NET Standard if there is significant demand for it.

