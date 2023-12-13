---
uid: caching-redis
title: "Using Redis Cache"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides instructions on how to use Redis Cache with PostSharp, including server configuration, setting up PostSharp for Redis caching, adding local in-memory cache, and using dependencies with the Redis caching backend."
---
# Using Redis Cache

[Redis](https://redis.io/) is a popular choice for distributed, in-memory caching. 

Our implementation uses [StackExchange.Redis library](https://stackexchange.github.io/StackExchange.Redis/) internally and is compatible with on-premises instances of Redis Cache as well as with the [Azure Redis Cache](https://azure.microsoft.com/en-us/services/cache/) cloud service. 


## Configuring the Redis server


### To prepare your Redis server for use with PostSharp caching:

1. Set up the eviction policy to `volatile-lru` or `volatile-random`. See [https://redis.io/topics/lru-cache#eviction-policies](https://redis.io/topics/lru-cache#eviction-policies) for details. 

    > [!CAUTION]
    > Other eviction policies than `volatile-lru` or `volatile-random` are not supported. 


2. Set up the key-space notification to include the `AKE` events. See [https://redis.io/topics/notifications#configuration](https://redis.io/topics/notifications#configuration) for details. 



## Configuring the caching backend in PostSharp


### To set up PostSharp to use Redis for caching:

1. Add a reference to the [PostSharp.Patterns.Caching.Redis](https://www.nuget.org/packages/PostSharp.Patterns.Caching.Redis/) package. 


2. Create an instance of [StackExchange.Redis.ConnectionMultiplexer](https://stackexchange.github.io/StackExchange.Redis/Configuration) . 


3. Create an instance of the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend> class using the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend.Create(StackExchange.Redis.IConnectionMultiplexer,PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration)> factory method and assign the instance to <xref:PostSharp.Patterns.Caching.CachingServices.DefaultBackend>. 


> [!IMPORTANT]
> The caching backend has to be set before any cached method is called for the first time.


### Example

```csharp
string connectionConfiguration = "localhost";
ConnectionMultiplexer connection = ConnectionMultiplexer.Connect( connectionConfiguration );
RedisCachingBackendConfiguration redisCachingConfiguration = new RedisCachingBackendConfiguration();
CachingServices.DefaultBackend = RedisCachingBackend.Create( connection, redisCachingConfiguration );
```


## Adding a local in-memory cache to a remote Redis server

For higher performance, you can add an additional, in-process layer of caching between your application and the remote Redis server. To enable the local cache, set the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.IsLocallyCached> property to `true`. 

The benefit of using local caching is to decrease latency between the application and the Redis server, and to decrease CPU load due to the deserialization of objects. The inconvenience is that there is that distributed local caches are synchronized asynchronously, therefore different application instances may see different values of cache items during a few milliseconds. However, the application instance initiating the change will have a consistent view of the cache.


## Using dependencies with the Redis caching backend

Support for dependencies is disabled by default with the Redis caching backend because it has an important performance and deployment impact. From a performance point of view, the cache dependencies need to be stored in Redis (therefore consuming memory) and handled in a transactional way (therefore consuming processing power). As for deployment, the problem is that the cache GC process, which cleans up dependencies when cache items are expired from the cache, needs to run continuously, even when the application is not running.

If you choose to enable dependencies with Redis, you need to make sure that there is at least one instance of the cache GC process is running. It is legal to have several instances of this process running, but since all instances will compete to process the same messages, it is better to ensure that only a small number of instances (ideally one) is running.


### To use dependencies with the Redis caching backend:

1. Make sure that at least one instance of the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> class is alive at any moment (whenever the application is running or not). If several instances of your application use the same Redis server, a single instance of the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> class is required. You may package the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> into a separate application of cloud service. 


2. In case of an outage of the service running the GC process, execute the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector.PerformFullCollectionAsync(PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend,System.Threading.CancellationToken)> method. 


3. Set the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.SupportsDependencies> property to `true`. 





