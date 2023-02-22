---
uid: threading-library
title: "Multithreading"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Multithreading

The Threading Pattern Library helps building multithreaded applications with fewer lines of code and fewer defects. The library implements locking models (thread unsafe, reader-writer synchronized, actor), thread synchronization aspects (to background thread, to UI thread), and a deadlock detection facility.

It provides the following features:

* **Threading Models.** A threading model is a design pattern that gives guarantees that your code executes safely on a multithreaded computer. Three models are implemented: Thread Exclusive (thread unsafe), Reader-Writer Synchronized, and Actor. See <xref:threading-models> for details. 

* **Thread Dispatching.** Custom attributes <xref:PostSharp.Patterns.Threading.DispatchedAttribute> and <xref:PostSharp.Patterns.Threading.BackgroundAttribute> cause the execution of a method to be dispatched to the UI thread or to a background thread, respectively. For details, read <xref:ui-dispatching> and <xref:background-dispatching>. 

* **Deadlock Detection.** Detects deadlocks at run time and throws an exception instead of allowing your application to freeze. For details, see <xref:deadlock-detection>. 

