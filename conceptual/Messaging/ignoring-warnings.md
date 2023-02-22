---
uid: ignoring-warnings
title: "Ignoring and Escalating Warnings"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Ignoring and Escalating Warnings

As with conventional compilers, warnings emitted by PostSharp, as well as those emitted by custom code running at build time in PostSharp, can be ignored (in that case they will not be displayed) or escalated into errors.

Warnings can be ignored either globally, using a project-wide setting, or locally for a given element of code. Warnings can be escalated only globally.


## Ignoring or escalating warnings globally

There are several ways to ignore or escalate a warning for a complete project:

* In Visual Studio, in the **PostSharp** tab of the project properties dialog. See <xref:configuration> for details. 

* By defining the `PostSharpDisabledMessages` or `PostSharpEscalatedMessages` MSBuild properties. See <xref:configuration> and <xref:configuration-msbuild> for details. 

> [!NOTE]
> The value `*` can be used to escalate all warnings into errors. 


## Ignoring warnings locally

Most warnings are related to a specific element of code. To disable a specific warning for a specific element of code, add the <xref:PostSharp.Extensibility.SuppressWarningAttribute> custom attribute to that element of code, or to any enclosing element of code (for instance, adding the attribute to a type will make it effective for all members of this type). 

You can create your own custom attribute derived from <xref:PostSharp.Extensibility.SuppressWarningAttribute> and make it conditional to a compilation symbol by using the <xref:System.Diagnostics.ConditionalAttribute> custom attribute. 

