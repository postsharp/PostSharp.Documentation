---
uid: messages
title: "Working with Errors, Warnings, and Messages"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document discusses how PostSharp, a compiler, can emit messages, warnings, and errors, and how custom code can use this feature. It also mentions an experimental feature in PostSharp 2.1."
---
# Working with Errors, Warnings, and Messages

As any compiler, PostSharp can emit messages, warnings, and errors, commonly referred to as *message*. Custom code running at build time (typically the implementation of `CompileTimeValidate` or of a custom constraint) can use PostSharp messaging facility to emit their own messages. 

In this section:

* <xref:ignoring-warnings>
* <xref:emitting-errors>. 

> [!TIP]
> PostSharp 2.1 contains an experimental feature that adds file and line information to errors and warnings. The feature requires Visual Studio. In must be enabled manually in the **PostSharp** tab of Visual Studio options. 


