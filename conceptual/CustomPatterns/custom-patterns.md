---
uid: custom-patterns
title: ""
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
Ready-made patterns implementation are only the tip of the iceberg. There are a lot more design patterns that are not available off-the-shelf in PostSharp. There are even more patterns that are not general enough to be recognized as design patterns because they are specific to your application, However, even custom patterns deserve automation for exactly the same reason as standard patterns. 

There are two ways you can automate custom patterns:

* Automate their implementation using one or more aspects.

* Automate the validation of their handwritten implementation to make code reviews lighter.

This is what this part is about. It is composed of the following chapters:

| Chapter | Description |
|---------|-------------|
| <xref:custom-aspects> | This chapter describes how to build custom aspects with PostSharp to automate the implementation of patterns. |
| <xref:testing-aspects> | This topic discusses how to test aspects, how to work with dependency injection and inversion-of-control containers, and how to attach the debugger to PostSharp at build time. |
| <xref:constraints> | This chapter explains how to automate the validation of your code against pattern implementation guidelines and architecture rules. |
