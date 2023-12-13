---
uid: programmatic-tooltip
title: "Pushing Information to PostSharp Tools Programmatically"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document discusses how to use the IWeavingSymbolsService in PostSharp Tools for Visual Studio to push information from your aspect at build time."
---
# Pushing Information to PostSharp Tools Programmatically

The <xref:PostSharp.Extensibility.IWeavingSymbolsService> service allows you to push information from your aspect, at build time, to PostSharp Tools for Visual Studio. 

This service can be used in the following scenarios:

* Adding some text to the Intellisense tooltip of a declaration.

* Adding some code saving information.

* Add some annotation that means that PostSharp Tools should consider that a declaration has been decorated with a custom attribute. This annotation is then taken into account by the analytic engine that powers the real-time quick actions and diagnostics of PostSharp Tools. For instance, the <xref:PostSharp.Patterns.Model.TypeAdapters.FieldRule> facility uses this feature. 

To get an instance of this service, use the <xref:PostSharp.Extensibility.IProject.GetService``1(System.Boolean)> method from `PostSharpEnvironment.CurrentProject.GetService`. 


