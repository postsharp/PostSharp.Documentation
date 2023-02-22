---
uid: whats-new-20
title: "What's New in PostSharp 2.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 2.0

PostSharp 1.0 and 1.5 made aspect-oriented programming (AOP) popular in the .NET community. PostSharp 2.0 makes it mainstream by enhancing convenience (Visual Studio Extension), reliability (dependency enforcement), run-time performance (optimizer), and features (composite aspects, property- and event-level aspects).


## Visual Studio Extension

As developers start being comfortable with PostSharp and add more and more aspects to their code, two questions become manifest: How can I know to which elements of code my aspect has been applied? How can I know which aspects have been applied to the element of code I am looking at? Answering these two questions is precisely what the PostSharp Extension for Visual Studio 2008 and 2010 has been designed for. It provides two new features to the IDE: an Aspect Browser tool window and new adornments of enhanced elements of code with clickable tooltip.


## Composite aspects (advices and pointcuts)

Part of the success of PostSharp 1.5 was due to its ability to introduce aspects without appealing to barbaric terms such as advices and pointcuts. So why introduce them now? Because they make it easier to develop complex aspects. Thanks to advices and pointcuts, you can implement complex patterns such as observability awareness (INotifyPropertyChanged) with just a few lines of code. And just with PostSharp 1.5, you can still write your own aspects without knowing about advices and pointcuts.


## Adaptive code generation

PostSharp 2.0 generates much smarter, faster, and smaller code than before. Let's face it: PostSharp 1.5 was quite dumb. It generated a lot of instructions that your aspects did not even need. PostSharp 2.0 analyzes your aspect to see which features are actually being used at run time, and generates only instructions that support these features. Result: you could probably not write much faster code by hand.


## Interception aspect for fields and properties

PostSharp 2.0 comes with a new kind of aspect that handles fields and properties: <xref:PostSharp.Aspects.LocationInterceptionAspect> (in replacement of `OnFieldAccessAspect`). The aspect is much more usable than its predecessor; for instance, it is possible to call the field or property getter from the setter. 


## Interception aspect for events

The new aspect kind <xref:PostSharp.Aspects.EventInterceptionAspect> allows an aspect to intercept all event semantics: add, remove, and fire. 


## Aspect dependencies

By enforcing aspect dependency rules, PostSharp ensures that aspects behave in a predictable and robust way, even when multiple aspects are applied to the same element of code. This feature is important for large and complex projects, where aspects may be written by different teams, or provided by numerous third-party vendors who don't know about each other.


## Instance-scoped aspects

In PostSharp 1.5, all aspects had static scope, i.e. there was a single instance of the aspect for every element of code to which they applied. It is now possible to define aspects that have instance lifetime. For instance, if the aspect is applied to an instance field, a new instance of the aspect will be created for every instance of the type declaring the field. This is named an instance-scoped aspect.


## Support for new platforms

* Microsoft .NET Framework 4.0

* Microsoft Silverlight 3.0

* Microsoft Silverlight 4.0

* Microsoft Windows Phone 7 (Applications and Games)

* Microsoft .NET Compact Framework 3.5

* Novell Mono 2.6


## Build performance improvements

Just starting the CLR and loading system assemblies takes considerable time, too much for an application (such as PostSharp) that is typically started very frequently and whose running time is just a couple of seconds. To cope with this issue, PostSharp now preferably runs as a background application

