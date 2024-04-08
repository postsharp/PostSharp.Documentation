---
uid: programmatic-tooltip
title: "Pushing Information to Visual Studio Tools for Metalama and PostSharp Programmatically"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document discusses how to use the IWeavingSymbolsService in Visual Studio Tools for Metalama and PostSharp to push information from your aspect at build time."
---
# Pushing Information to Visual Studio Tools for Metalama and PostSharp Programmatically

The <xref:PostSharp.Extensibility.IWeavingSymbolsService> service allows you to push information from your aspect, at build time, to Visual Studio Tools for Metalama and PostSharp. 

This service can be used in the following scenarios:

* Adding a text to CodeLens and Intellisense tooltip of a declaration.

* Adding code saving information.

* Adding an annotation that means that Visual Studio Tools for Metalama and PostSharp should consider that a declaration has been decorated with a custom attribute. This annotation is then taken into account by the analytic engine that powers the real-time quick actions and diagnostics of Visual Studio Tools for Metalama and PostSharp. For instance, the <xref:PostSharp.Patterns.Model.TypeAdapters.FieldRule> facility uses this feature. 

To get an instance of this service, use the <xref:PostSharp.Extensibility.IProject.GetService``1(System.Boolean)> method from `PostSharpEnvironment.CurrentProject.GetService`. 


