---
uid: whats-new-15
title: "What's New in PostSharp 1.5"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 1.5 introduces aspect inheritance, the ability to read assemblies without loading them in the CLR, lazy loading of assemblies, build-time performance improvements, support for Mono, Silverlight 2.0, Compact Framework 2.0, and a pluggable aspect serializer."
---
# What's New in PostSharp 1.5

PostSharp 1.5 was published 3 years after the start of the project, and was the first release to be really production-ready.


## Aspect inheritance

It is now possible to put an aspect on an interface and have it implicitly applied to all classes implementing that interface. The same works with classes, virtual or interface methods, and parameters of virtual or interface methods. Read more...


## Reading assemblies without loading them in the CLR

In version 1.0, PostSharp required assemblies to be loaded in the CLR (i.e. in the application domain) to be able to read them. This limitation belongs to the past. When PostSharp processes a Silverlight or a Compact Framework assembly, it is never loaded by the CLR.


## Lazy loading of assemblies

When PostSharp has to load a dependency assembly, it now reads only the metadata objects it really needs, resulting in a huge performance improvement and much lower memory consumption.


## Build-time performance improvement

The code has been carefully profiled and optimized for maximal performance.


## Support for Mono

PostSharp is now truly cross-platform. Binaries compiled on the Microsoft platform can be executed under Novell Mono. Both Windows and Linux are tested and supported. A NAnt task makes it easier to use PostSharp in these environments.


## Support for Silverlight 2.0 and the Compact Framework 2.0

You can add aspects to your projects targeting Silverlight 2.0 or the Compact Framework 2.0.


## Pluggable aspect serializer & partial trust

Previously, all aspects were serializers using the standard .NET binary formatter. It is now possible to choose another serializer or implement your own, and enhance assemblies that be executed with partial trust.


