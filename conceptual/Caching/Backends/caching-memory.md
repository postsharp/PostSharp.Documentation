---
uid: caching-memory
title: "Using In-Memory Cache"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document explains how to use in-memory caching in PostSharp. It provides instructions for .NET Framework and .NET Core applications, using two different back-end classes."
---
# Using In-Memory Cache

For local, in-memory caching, PostSharp offers two different back-end classes:

* <xref:PostSharp.Patterns.Caching.Backends.MemoryCachingBackend> relies on <xref:System.Runtime.Caching.MemoryCache> and can only be used with projects targeting the .NET Framework v4.5 and later. 

* <xref:PostSharp.Patterns.Caching.Backends.MemoryCacheBackend> relies on `Microsoft.Extensions.Caching.Memory.IMemoryCache` and requires .NET Standard 2.0, therefore it can be used in .NET Core applications too. 


## In-memory caching for .NET Framework

To use the <xref:System.Runtime.Caching.MemoryCache> class to store cached values in memory, assign an instance of a <xref:PostSharp.Patterns.Caching.Backends.MemoryCachingBackend> class to <xref:PostSharp.Patterns.Caching.CachingServices.DefaultBackend> property. 

```csharp
CachingServices.DefaultBackend = new MemoryCachingBackend();
```

By default, the <xref:System.Runtime.Caching.MemoryCache.Default> instance is used. To use other instance of the <xref:System.Runtime.Caching.MemoryCache> than the default one, an instance of the <xref:System.Runtime.Caching.MemoryCache> class can be passed to the constructor of the <xref:PostSharp.Patterns.Caching.Backends.MemoryCachingBackend> class. 

```csharp
MemoryCache cache = new MemoryCache( "myCache" );
CachingServices.DefaultBackend = new MemoryCachingBackend( cache );
```

See [MSDN](https://msdn.microsoft.com/en-us/library/system.runtime.caching.memorycache(v=vs.110).aspx) for details on the <xref:System.Runtime.Caching.MemoryCache> class. 


## In-memory caching for .NET Standard and .NET Core

To use an instance implementing the `Microsoft.Extensions.Caching.Memory.IMemoryCache` interface to store cached values in memory: 

1. Add a reference to the [PostSharp.Patterns.Caching.IMemoryCache](https://www.nuget.org/packages/PostSharp.Patterns.Caching.IMemoryCache/) package. 


2. Assign an instance of a <xref:PostSharp.Patterns.Caching.Backends.MemoryCacheBackend> class to the <xref:PostSharp.Patterns.Caching.CachingServices.DefaultBackend> property. Pass the `IMemoryCache` to the constructor. 

    ```csharp
    IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
    CachingServices.DefaultBackend = new MemoryCacheBackend(cache);
    ```


See [Cache in-memory in ASP.NET Core on MSDN](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-3.1) for more information on the use of IMemoryCache. 


