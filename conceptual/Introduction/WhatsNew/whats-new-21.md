---
uid: whats-new-21
title: "What's New in PostSharp 2.1"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 2.1 introduces several improvements, including build-time performance, compatibility with obfuscators and Code Contracts, support for NuGet and Silverlight 5.0, and a new license server."
---
# What's New in PostSharp 2.1

The objective of release 2.1 was to fix a number of 'gray points' of the version 2.0, which added friction to the adoption path of PostSharp, or even prevented people from using the product.


## Build-time performance improvement

We traded our old text-based compilation engine to a brand new binary writer.


## Support for NuGet and improved no-setup experience

PostSharp 2.1 can be installed directly from [NuGet](http://www.nuget.org/List/Packages/PostSharp). Local installation is no longer a requirement to use the Visual Studio Extension. However, because the setup program creates ngenned images, it still provides the faster experience. 


## Compatibility with obfuscators

PostSharp can now be used jointly, and without limitation of features, with some obfuscators.


## Extended reflection API

The class <xref:PostSharp.Reflection.ReflectionSearch> allows you to programmatically navigate the structure of an assembly: find custom attributes of a given type, find children of a given type, find members of a given type, find methods referring a given type or members, or find members accessed from a given method. 


## Architectural validation

Architecture Validation allows you annotate your code with constraints, which define the conditions in which your API is allowed to be used. Constraints are verified at build time and their violation generates a build warning and an error. See <xref:constraints> for details. 


## Compatibility with Code Contracts

PostSharp 2.1 can be used jointly with Microsoft Code Contracts. Aspects and contracts can be applied to the same method.


## Support for Silverlight 5.0

Silverlight 5.0 is added to the list of supported platforms.


## License server

The license server helps customer manage and deploy license keys. The license server is a simple ASP.NET application that can be deployed easily on any Windows machine. Its use is optional.


