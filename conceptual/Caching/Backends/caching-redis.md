---
uid: caching-redis
title: "Using Redis Cache"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides instructions on how to use Redis Cache with PostSharp, including server configuration, setting up PostSharp for Redis caching, adding local in-memory cache, and using dependencies with the Redis caching backend."
---
# Using Redis Cache

[Redis](https://redis.io/) is a popular choice for distributed, in-memory caching.

Our implementation uses [StackExchange.Redis library](https://stackexchange.github.io/StackExchange.Redis/) internally. It is compatible with on-premises Redis Cache instances as well as with the [Azure Redis Cache](https://azure.microsoft.com/en-us/services/cache/) cloud service. We tested our adapter with single-node, master/replica, and sharded topologies.

## Configuring the Redis server

### To prepare your Redis server for use with PostSharp caching:

1. Set up the eviction policy to `volatile-lru` or `volatile-random`. See https://redis.io/topics/lru-cache#eviction-policies for details.

    > [!CAUTION]
    > Other eviction policies than `volatile-lru` or `volatile-random` are not supported.

    ```
    maxmemory-policy volatile-lru
    ```

2. If you intend to enable events or use dependencies, configure keyspace notifications to include the `Exe` events. See https://redis.io/topics/notifications#configuration for details.

    ```
    notify-keyspace-events "Exe"
    ```

## Configuring the caching backend in PostSharp

### To set up PostSharp to use Redis for caching:

1. Add a reference to the [PostSharp.Patterns.Caching.Redis](https://www.nuget.org/packages/PostSharp.Patterns.Caching.Redis/) package.

2. Create an instance of [StackExchange.Redis.ConnectionMultiplexer](https://stackexchange.github.io/StackExchange.Redis/Configuration).

3. Create an instance of the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend> class using the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend.Create*> factory method and assign the instance to <xref:PostSharp.Patterns.Caching.CachingServices.DefaultBackend>, passing a <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration> object.

> [!IMPORTANT]
> The caching backend has to be set before any cached method is called for the first time.

### Example

```csharp
string connectionConfiguration = "localhost";
ConnectionMultiplexer connection = ConnectionMultiplexer.Connect( connectionConfiguration );
RedisCachingBackendConfiguration redisCachingConfiguration = new RedisCachingBackendConfiguration() { Prefix = "MyApp-1.0.1" };
CachingServices.DefaultBackend = RedisCachingBackend.Create( connection, redisCachingConfiguration );
```

### Important configuration settings

- The <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.Prefix> property should be set to a value that uniquely identifies the version of the data model. This will allow you to run several versions of your application side by side with the same database.
- The <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.DefaultExpiration> property is the expiration for items that do not have an explicit expiration and are not marked unremovable. It defaults to 30 days.
- The <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.Database> property allows you to select the logical database. It defaults to 0. If you run several applications on the same Redis node or cluster, it’s recommended to place each application in a separate database. This also allows you to clear the cache easily with the `FLUSHDB` Redis command.

## Adding a local in-memory cache to a remote Redis server

For higher performance, you can add an additional, in-process layer of caching between your application and the remote Redis server. To enable the local cache, set the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.IsLocallyCached> property to `true`.

The benefit of using local caching is to reduce latency between the application and the Redis server and to decrease CPU load due to object deserialization.

This feature relies on Redis' Pub/Sub subsystem to propagate cache invalidation events. This synchronization is asynchronous. Therefore, different nodes may have different views of the cache — even if you are using a single Redis node. Since Redis' master/replica synchronization is also asynchronous, having an in-memory caching layer does not fundamentally differ from a master/replica setup, although delays will be higher.

The following code enables the in-memory cache:

```csharp
RedisCachingBackendConfiguration redisCachingConfiguration = new RedisCachingBackendConfiguration() { IsLocallyCached = true, SupportsEvents = true };
CachingServices.DefaultBackend = RedisCachingBackend.Create( connection, redisCachingConfiguration );
```

> [!IMPORTANT]
> If one node enables the local cache, all nodes must enable events.

In-memory cache must be enabled globally for the whole back-end. It is not possible to enable it at the level of individual classes or methods.

## Using dependencies with the Redis caching backend

Support for dependencies is disabled by default with the Redis caching backend because it has an important performance and deployment impact. From a performance perspective, the dependency graphs need to be stored in Redis. As for deployment, the cache GC process, which cleans up dependencies when cache items expire, needs to run continuously, even when the application is not running.

To use dependencies with the Redis caching backend:

1. Make sure that at least one instance of the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> class is alive at any moment (whether the application is running or not). If several instances of your application use the same Redis server, a single instance of the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> class is required. You may package the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> into a separate application or cloud service.

    This component performs two tasks:

    - It subscribes to `__keyevent@0__:expired` and `__keyevent@0__:evicted` notifications and cleans up the dependency graphs in real time. This process generally works well, but it is temporarily disabled when the system is overloaded.
    - It periodically scans the entire database and removes any unreachable data (garbage).

2. Set the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.SupportsDependencies?text=RedisCachingBackendConfiguration.SupportsDependencies> property to `true`.

If you choose to enable dependencies with Redis, you need to ensure that at least one instance of the cache GC process is running. It is acceptable to have several instances running, but since all instances will compete to process the same messages, it’s better to have only a small number of instances (ideally one) running.

The dependencies feature of <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend> is designed so that the cache is always consistent from the client’s point of view. Read operations are fast and transactionless. Write operations are optimized for performance and read consistency, but may leave garbage behind in case of a race condition.

Garbage may therefore be due to three factors:
- The <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> component was not running when a cache eviction or expiration happened.
- The <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> component was temporarily disabled because of high system load.
- There was a race condition in setting a cache value (the version that loses becomes garbage).

## Support for Redis clusters

 <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend> has been tested in single-node, master/replica, and sharded cluster deployments.

## Resilience

 As with any part of a distributed system, <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend> is a complex component that must be tuned and monitored with care.

### Enabling logging

<xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend> uses PostSharp Logging. While tuning performance, we recommend monitoring warnings.

The following code shows how to enable logging for the caching component and redirect logging output to the console. See <xref:backends> to plug caching into your logging framework.

```csharp
ConsoleLoggingBackend loggingBackend = new();
LoggingServices.DefaultBackend = loggingBackend;
LoggingServices.DefaultBackend.DefaultVerbosity.SetMinimalLevel( LogLevel.Warning, LoggingRoles.Caching );
```

> [!IMPORTANT]
> If one node enables the local cache, all nodes must enable events.

In-memory cache must be enabled globally for the whole back-end. It is not possible to enable it at the level of individual classes or methods.

### Exception handling

Failures are highly likely if your system is overloaded. Here is how they are handled:

1. Foreground operations fail fast. In case of a cache exception, we skip the cache. Additionally, when a writing operation fails, we schedule a background recovery task to invalidate the cache item. (based on the principle that the worst outcome is an inconsistent cache)
2. Background operations are retried according to a retry policy (see <xref:PostSharp.Patterns.Caching.Resilience.IRetryPolicy>) that can be configured through <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.TransactionRetryPolicy> and <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.BackgroundRecoveryRetryPolicy> properties. The default retry policy implements an exponential backoff delay strategy.

The exception handling policy described above is abstracted by the <xref:PostSharp.Patterns.Caching.Resilience.IExceptionHandlingPolicy> interface and configured by the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.ExceptionHandlingPolicy> property.

### Performance tuning

With any level of load, it is recommended to enable cache key compression by assigning the <xref:PostSharp.Patterns.Caching.CachingServices.DefaultKeyBuilder?CachingServices.DefaultKeyBuilder> property to a <xref:PostSharp.Patterns.Caching.Implementation.CacheKeyBuilder> instance with a non-default <xref:PostSharp.Patterns.Caching.Implementation.CacheKeyHashingAlgorithm>.

If you expect high load, it is recommended to tune the following <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration> properties:

- <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.BackgroundTasksMaxConcurrency> is critical to the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> component. It should be high enough to utilize the maximum network resources of your deployment, but small enough not to allow for a large operation backlog to form.
- <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.BackgroundTasksOverloadedThreshold> is used only by the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector>. It will cause the component to stop processing eviction and expiration notifications in case the operation backlog is too large. In this case, GC would rely on periodic database scanning.

The following <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollectorOptions> properties must also be properly tuned:

- The <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollectorOptions.CacheCleanupDelay> property is the delay between the initialization of the component and the first cleanup, then between two subsequent cache cleanups, and defaults to 4 hours. Cleaning up the database too frequently is useless performance overhead, but doing it too late degrades performance even more. If the database contains too much garbage, Redis will start evicting _useful_ data, affecting your application performance. However it will never evict garbage. That's why you should increase the cache cleanup frequency if you see high memory usage or high levels of evictions.
- The <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollectorOptions.CacheCleanupOptions.CacheCleanupOptions> property affects the cleanup process. It's important to keep the cleanup slow enough to avoid impacting your application's performance, but fast enough to finish before Redis runs out of memory. The <xref:PostSharp.Patterns.Caching.Implementation.CacheCleanupOptions.WaitDelay> is an artificial delay between processing each cache key, defaulting to 100 ms.

Note that you can run a manual cleanup by calling the <xref:PostSharp.Patterns.Caching.Implementation.CachingBackend.CleanUpAsync*?CachingBackend.CleanUpAsync> method. Do not run this method with the default options on a production database; these options are optimized for the cleanup operation's performance and may overload your server.

### Monitoring

The following metrics are relevant to assess the health of your caching set up:

- Number of cache evictions per second. A high number might mean either insufficient caching memory, or ineffective caching strategy - caching things that are not worth it (too many cache misses)
- Number of cache expirations per second. A high number might mean too small expiration delays
- Number of warnings per minute in the caching component. A high number means that your system is overloaded.

If you want to gather statistics about cache hits and misses, you can do so by implementing a <xref:PostSharp.Patterns.Caching.Implementation.CachingBackendEnhancer> that overrides the <xref:PostSharp.Patterns.Caching.Implementation.CachingBackend.GetItemCore*> and <xref:PostSharp.Patterns.Caching.Implementation.CachingBackend.GetItemAsyncCore*> methods (a `null` return value means a cache miss).

## Data schema

When dependencies are enabled, <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend> relies on these keys:

1. Item version: `<prefix>:<schema-version>:{<item-key>}:ver` — unique string identifier.
2. Item value: `<prefix>:<schema-version>:{<item-key>}:val:<item-version>` — a list with values: `[<item-value>, <item-sliding-expiration>, <tag0>, <tag1> ... <tagn>]`.
3. Backward dependencies: `<prefix>:<schema-version>:{<dependency-key>}:bdep` — hash set of `<item-version>:<item-key>`.
4. Forward dependencies: `<prefix>:<schema-version>:{<item-key>}:fdep:<item-version>` — list of `<dependency-key>`.

When dependencies are disabled, only the item value record is used.

In this description, elements between angle brackets are placeholders and mean the following:

- `<item-key>` is the cache key (as generated by <xref:PostSharp.Patterns.Caching.Implementation.CacheKeyBuilder>), where `{`, `}` and `:` have been escaped.
- `<item-value>` is the serialized cache value.
- `<prefix>` is the value of the <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.Prefix> property.
- `<schema-version>` is the version of the data schema internal to <xref:PostSharp.Patterns.Caching.Backends.Redis.RedisCachingBackend>.
- `<item-version>` is a randomly generated item version.
- `<dependency-key>` is either a dependency key or a cache item key, when the cache item is itself a dependency (recursive dependencies), where `{`, `}` and `:` have been escaped.

## Clearing the cache

To remove all cache keys, you can:

* Use the `FLUSHDB` Redis command to delete all keys in the selected database, even those that don’t match the prefix.
* Use the `SCAN <prefix>:*` command to identify all keys, then use `DEL` for each key.
* Use the <xref:PostSharp.Patterns.Caching.Implementation.CachingBackend.ClearAsync*> method, which does a `SCAN <prefix>:<schema-version>:*` command, then `UNLINK` for each key.
