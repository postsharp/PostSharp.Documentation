---
uid: testing-aspects
title: "Testing and Debugging"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document discusses testing and debugging techniques for aspects in PostSharp, which differ from normal class libraries due to their unique characteristics."
---
# Testing and Debugging

Aspects should be tested as any piece of code. However, testing techniques for aspects differ from testing techniques for normal class libraries because of a number of reasons:

* Aspects instantiation is not user-controlled.

* Aspects partially execute at build time.

* Aspects can emit build errors. Logic that emits build errors should be tested too.

These characteristics are no obstacle to proper testing of aspects.

This chapter contains the following sections:

* <xref:simple-tests> explains how to test the behavior of an aspect. 

* <xref:testing-application> shows how to test that an aspect has been applied to the expected set of code artifacts. 

* <xref:consuming-dependencies> describes several ways for aspects to consume dependencies from dependency-injection containers and service locators. 

* <xref:debugging-runtime> explains how to debug run-time logic. 

* <xref:debugging-buildtime> explains how to debug build-time logic. 


