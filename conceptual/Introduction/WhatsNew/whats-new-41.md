---
uid: whats-new-41
title: "What's New in PostSharp 4.1"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 4.1

The focus of PostSharp 4.1 was to broaden the set of supported platforms for both PostSharp, with the addition of Xamarin and Visual Studio 2015, and improvements in the support of Windows Phone and Windows Store.


## Support for Xamarin

Xamarin has become an inseparable part of the .NET ecosystem and was the number-one feature request of the PostSharp community. PostSharp 4.1 makes it possible to build applications for iOS and Android using Xamarin.

Note that Xamarin applications must be built using Visual Studio. Xamarin Studio is not supported.


## Threading Pattern Library: support for Windows Phone and Windows Store

Threading Pattern Library newly supports Windows Phone, Windows Store and Xamarin. This allows you to create thread-safe applications for both Windows and Windows Phone (both Silverlight and WinRT) in the same way as for desktop applications.


## Support for Visual Studio 2015

PostSharp Tools for Visual Studio have been almost completely rewritten to take advantage of the new compiler family "Roslyn" at the heart of Visual Studio 2015. New features include integration with the light bulb (instead of the smart tag), live code diagnostics and a few refactorings.


## PostSharp Assistant

PostSharp Assistant guides you when you are implementing various patterns from Pattern Libraries so that you don't miss any detail. For instance, it would point at relevant documentation articles or at pieces of code that need to be fixed.

PostSharp Assistant is supported in Visual Studio 2015.

