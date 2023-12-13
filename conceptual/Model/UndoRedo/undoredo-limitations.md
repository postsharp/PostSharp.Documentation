---
uid: undoredo-limitations
title: ""
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document discusses the limitations of the PostSharp product, including its intrinsic single-threaded nature and lack of support for async methods in recordable classes."
---

## Essentially Single-Threaded

But the <xref:PostSharp.Patterns.Recording.Recorder> class is intrinsically single-threaded. You can use recordable objects in a multithreaded context, but you should make sure that objects that share the same recorder are not accessed concurrently from several threads. Note that this is a limitation of the undo/redo concept, not a limitation of our implementation. 


## No Support for Async Methods

Async methods are not supported as units of undoable operations. In short, it means that you cannot have async methods in recordable classes unless you make the method non-recorded using the <xref:PostSharp.Patterns.Recording.RecordingScopeAttribute> custom attribute. 


