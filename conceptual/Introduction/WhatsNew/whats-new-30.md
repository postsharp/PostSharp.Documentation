---
uid: whats-new-30
title: "What's New in PostSharp 3.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 3.0

The focus in PostSharp 3.0 was to deliver more value to customers with less initial learning. Instead of having to learn the product before being able to build aspects, customers can now choose from a set of ready-made implementations of some of the most popular design pattern, and apply them to their application from the Visual Studio code editor, using smart tags and wizards. We also improved support for Windows Phone, Silverlight, Windows Store and Portable Class Library.


## Model Pattern Library

The <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> aspect is a ready-made implementation of the NotifyPropertyChanged design pattern. The <xref:PostSharp.Patterns.Contracts> namespace provides code contracts that can validate, at run time, the value of a parameter, a property, or a field. 


## Diagnostics Pattern Library

The <xref:PostSharp.Patterns.Diagnostics.LogAttribute> and <xref:PostSharp.Patterns.Diagnostics.LogExceptionAttribute> aspects provide a ready-made and high-performance implementation of a tracing aspect. They are compatible with the most popular logging framework, including log4net, NLog, and Enterprise Library. 


## Threading Pattern Library

PostSharp Threading Pattern Library invites you to raise the level of abstraction in which multithreading is being addressed. It provides three threading models: actors (<xref:PostSharp.Patterns.Threading.ActorAttribute>), reader-writer synchronized (<xref:PostSharp.Patterns.Threading.ReaderWriterSynchronizedAttribute>) and thread unsafe (<xref:PostSharp.Patterns.Threading.ThreadUnsafeAttribute>). Additionally, <xref:PostSharp.Patterns.Threading.BackgroundAttribute> and <xref:PostSharp.Patterns.Threading.DispatchedAttribute> allow you to easily dispatch a thread back and forth between a background and the UI thread. 


## Smart tags and wizards in Visual Studio

Smart tags allow for better discoverability of ready-made aspects and pattern implementations. When the aspect requires configuration, a wizard user interface collects the parameters and then generates the proper code.


## Better platform support through Portable Class Libraries

Windows Phone, Windows Store and Silverlight are now first-class citizens. All features that are available for the .NET Framework now also work with these platforms. All platforms are supported transparently through the portable class library. To provide this feature, we had to develop the <xref:PostSharp.Serialization.PortableFormatter>, a portable serializer similar in function to the `BinaryFormatter`. All you have to do is to replace `[Serializable]` with `[PSerializable]`. 


## Unified deployment through NuGet and Visual Studio Gallery

Installation of PostSharp is now unified and built on top of Visual Studio Gallery and NuGet Package Manager.


## Transparency to obfuscators

PostSharp no longer requires specific support from obfuscators, as it no longer uses strings to refer to metadata declarations.


## Deprecation of old platforms

Support for Silverlight 3, .NET Compact Framework, and Mono has been deprecated.

