---
uid: whats-new-42
title: "What's New in PostSharp 4.2"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 4.2 introduces improvements to Aggregatable Pattern, NotifyPropertyChanged, Threading Models, and Aspect Framework. It also adds support for ASP.NET v5, Visual Basic, and IncrediBuild, along with code saving metrics."
---
# What's New in PostSharp 4.2

With PostSharp 4.2, we had two major objectives. The first was to dogfood our Threading Models into PostSharp Tools for Visual Studio, which pushed us to add many improvements both to the threading aspects and to the underlying aspect framework. The second focus was to expose code saving metrics, so you know how many lines of code you likely saved thanks to PostSharp. Additionally, we kept up with Microsoft and implemented support for the Elvis operator in NotifyPropertyChanged, and added experimental support for ASP.NET v5.


## Improvements to Aggregatable Pattern

We added the following improvements to the Aggregatable pattern and to other patterns that depend on it:

* New advisable class <xref:PostSharp.Patterns.Collections.AdvisableHashSet`1> in replacement of <xref:System.Collections.Generic.HashSet`1>. 

* New methods to the <xref:PostSharp.Patterns.Collections.AdvisableCollection`1> class: <xref:PostSharp.Patterns.Collections.AdvisableCollection`1.AddRange(System.Collections.Generic.IEnumerable{`0})>, <xref:PostSharp.Patterns.Collections.AdvisableCollection`1.InsertRange(System.Int32,System.Collections.Generic.IEnumerable{`0})>, <xref:PostSharp.Patterns.Collections.AdvisableCollection`1.RemoveRange(System.Int32,System.Int32)>. 

* Support for immutable collections like <xref:System.Collections.Immutable.ImmutableArray> or <xref:System.Collections.Immutable.ImmutableDictionary>. 

* Support for type adapters to allow third-party classes (at least read-only ones) to work with the Aggregatable pattern. See <xref:PostSharp.Patterns.Model.TypeAdapters.TypeAdapter> for details. 

* Ability to programmatically and automatically mark a field as child or reference without having to use a custom attribute in source code. See <xref:PostSharp.Patterns.Model.TypeAdapters.FieldRule> for details. 

* Performance improvement: memory no longer needs to be allocated at run time after objects are constructed, resulting in lower load on garbage collection.

* New extensions methods for advisable collections. See <xref:PostSharp.Patterns.Collections.Extensions>. 


## Improvements to NotifyPropertyChanged

Improvements include to <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> include: 

* Support for the Elvis operator (`?.`) and properties of local variables. 

* Notification of the `PropertyChanging` event. See <xref:inotifypropertychanging> for details. 

* Better error messages.

* Inclusion of property dependencies in Visual Studio tooltips.

* Performance improvement: memory no longer needs to be allocated at run time after objects are constructed, resulting in lower load on garbage collection.


## Improvements to Threading Models

Improvements to threading models include:

* Performance improvement: memory no longer needs to be allocated at run time after objects are constructed, resulting in lower load on garbage collection.

* Several performance improvements regarding the instantiation of large object graphs. See <xref:threading-performance> for details. 

* New <xref:PostSharp.Patterns.Threading.ThreadSafetyPolicy> attribute to emit warning when a class is not assigned to any threading model or when a static field is not of a thread-safe type. See <xref:thread-safety-policy> for details. 

* Better support for async methods: for lock-based models, locks are being awaited asynchronously instead of synchronously. These new high-performance advices are not yet fully implemented and tested for general use, therefore they are unsupported (except when they are used with our ready-made patterns) and undocumented.

* Complete dogfooding in our PostSharp Tools for Visual Studio, resulting in dozen of bug fixes and usability improvements.

* In the actor model, methods with non-void return type are now allowed and their execution will be done in the actor context, but the calling thread will wait synchronously for the execution to complete. Void but non-async methods must now be annotated with [Dispatched] (and until the next major version) to specify if execution must be synchronous or asynchronous.


## Improvements to the Aspect Framework

Improvements to the aspect framework include:

* New advice <xref:PostSharp.Aspects.Advices.OnAspectsInitializedAdvice> invoked after all aspects on the current objects have been initialized. 

* Ability to customize the description of aspects and advices in Visual Studio tooltips. See <xref:customizing-aspect-description> for details. 

* In the <xref:PostSharp.Aspects.OnMethodBoundaryAspect> and related aspects, ability to yield (await) a state machine upon on entry and resume. 

> [!CAUTION]
> To support performance improvements in ready-made patterns, we included a new family of advices that accept context on the stack instead of the heap.


## Improved Support for Visual Basic

Visual Basic is now supported and tested at the same level as C# both in PostSharp Compiler and PostSharp Tools for Visual Studio.


## Code Saving Metrics

PostSharp will now estimate the number of handwritten lines of code and the number of lines of code that you likely saved using PostSharp. For details, see <xref:estimating-code-saving>. 


## Module Initializers

You can now define methods that get executed immediately after the assembly is loaded, before any other code is executed. See <xref:module-initializer> and <xref:PostSharp.Aspects.ModuleInitializerAttribute> for details. 


## Support for IncrediBuild (Experimental)

If you use IncrediBuild, PostSharp can now be executed on a remote computer. Please contact PostSharp support for details.


## Support for ASP.NET v5 (Experimental)

You can now use PostSharp in ASP.NET v5 code. The support is currently limited to the .NET Framework (CoreCLR is not supported). Support is not built-in in the normal PostSharp distribution. You need to download the *PostSharp.Dnx* project from GitHub. Please see [https://github.com/postsharp/PostSharp.Dnx](https://github.com/postsharp/PostSharp.Dnx) for details. 


