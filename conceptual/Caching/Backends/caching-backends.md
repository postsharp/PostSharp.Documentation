---
uid: caching-backends
title: "Caching Back-Ends"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Caching Back-Ends

The <xref:PostSharp.Patterns.Caching.CacheAttribute> can be used with different caching frameworks or caching servers. This concept is called a *caching backend*. Caching backends are represented by the <xref:PostSharp.Patterns.Caching.Implementation.CachingBackend> abstract class. You can use an existing implementation or implement your own caching backend. 


## In this chapter

| Section | Description |
|---------|-------------|
| <xref:caching-memory> | This article shows how to store cached values using the <xref:System.Runtime.Caching.MemoryCache> class or the `IMemoryCache` abstraction.  |
| <xref:caching-redis> | This article shows how to store cached values using Redis and using a combination of Redis and <xref:System.Runtime.Caching.MemoryCache> class.  |

## See Also

**Reference**

<xref:PostSharp.Patterns.Caching.Backends.MemoryCachingBackend>
<br><xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend>
<br>