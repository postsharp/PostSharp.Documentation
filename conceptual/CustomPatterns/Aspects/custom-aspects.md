---
uid: custom-aspects
title: "Developing Custom Aspects"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides a guide on how to develop custom aspects in PostSharp, including creation, initialization, configuration, validation, serialization, and handling dependencies."
---
# Developing Custom Aspects

This chapter describes how to build your own aspect. It includes the following topics:

| Section | Description |
|---------|-------------|
| <xref:simple-aspects> | This topic describes how to create aspects that contain a single transformation (named *simple aspects*). It describes all kinds of simple aspects.  |
| <xref:aspect-lifetime> | This topic explains the lifetime of aspects, which are instantiated at build time, serialized, then deserialized at run time and executed. |
| <xref:aspect-initialization> | This topic discusses different techniques to initialize aspects. |
| <xref:aspect-configuration> | This topic describes the options of aspect configuration. |
| <xref:aspect-validation> | This topic shows how to validate that an aspect has been applied to a valid target declaration. |
| <xref:complex-aspects> | This topic describes how to create aspects that are composed of several primitive transformations, using advices and pointcuts. |
| <xref:aspect-dependencies> | This topic explains how to express aspect dependencies to prevent issues that would otherwise happen if several aspects are added to the same declaration. |
| <xref:aspect-serialization> | This topic explains aspect serialization and how to customize it. |
| <xref:customize-vs-appearance> | This topic shows how aspect can influence how they appear in Visual Studio tooltips and code saving metrics. |
| <xref:consuming-dependencies> | This topic describes several strategies to consume services from aspects. |

