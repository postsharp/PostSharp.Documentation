---
uid: cache-locking
title: "Preventing Concurrent Execution of Cached Methods"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp offers a lock manager feature to prevent concurrent execution of resource-heavy methods. It provides two lock managers: NullLockManager and LocalLockManager. Users can configure lock timeouts and implement a distributed lock manager using third-party implementations."
---
# Preventing Concurrent Execution of Cached Methods

When the evaluation of a method consumes significant resources or time, you may want to prevent a situation where several threads, processes or machines are evaluating the same method with the same parameters at the same time. You can achieve it by instructing PostSharp to use a lock manager. PostSharp implements two lock managers: the default <xref:PostSharp.Patterns.Caching.Locking.NullLockManager>, and <xref:PostSharp.Patterns.Caching.Locking.LocalLockManager>. 


## Preventing concurrent execution in the current process

By default, the caching aspect allows concurrent execution of the same method with the same arguments.

The <xref:PostSharp.Patterns.Caching.Locking.LocalLockManager> class implements that is able to prevent execution of methods running in the current process (or <xref:System.AppDomain>, to be exact). 

To configure the lock manager, you have to set the <xref:PostSharp.Patterns.Caching.CachingProfile.LockManager> property. Each caching profile must be set up separately. 

The following code shows how to configure locking for two profiles:

```csharp
CachingServices.Profiles.Default.LockManager = new LocalLockManager();          
CachingServices.Profiles["MyProfile"].LockManager = new LocalLockManager();
```

> [!NOTE]
> Each instance of the <xref:PostSharp.Patterns.Caching.Locking.LocalLockManager> class maintains its own set of locks. However, whether several profiles use the same or a different instance of the <xref:PostSharp.Patterns.Caching.Locking.LocalLockManager> does not matter because each method is associated with one and only one profile. 


## Handling lock timeouts

By default (unless you use the default <xref:PostSharp.Patterns.Caching.Locking.NullLockManager>), the caching aspect will wait for a lock during an infinite amount of time. Suppose that the thread that evaluates the method gets stuck (e.g. it is involved in a deadlock). Because of the locking mechanism, all threads evaluating the same method will also get stuck. To avoid this situation, you can choose to implement a timeout behavior. 

Two properties influence the timeout behavior:

| Property | Description |
|----------------------------------------------|------------------------------------------------|
| <xref:PostSharp.Patterns.Caching.CachingProfile.AcquireLockTimeout> | Maximum time that the caching aspect will wait for the lock manager to acquire a lock. To specify an infinite waiting time, set this property to `TimeSpan.FromMilliseconds( -1 )`. The default behavior is to wait infinitely.  |
| <xref:PostSharp.Patterns.Caching.CachingProfile.AcquireLockTimeoutStrategy> | Implements the logic executed when the caching aspect could not acquire a lock because of a timeout. The default behavior is to throw a <xref:System.TimeoutException>. You can implement your own strategy by implementing the <xref:PostSharp.Patterns.Caching.Locking.IAcquireLockTimeoutStrategy> interface.  |

> [!NOTE]
> This section only covers the time it takes to acquire a lock. It does not cover the execution time of the method that has already acquired the lock.

The following code shows how to set a 10-second timeout and ignore any timeout situation.

```csharp
CachingServices.Profiles.Default.AcquireLockTimeout = TimeSpan.FromSeconds(10);
          CachingServices.Profiles.Default.AcquireLockTimeoutStrategy = new IgnoreLockStrategy();
```

Here is the code of the `IgnoreLockStrategy` class: 

```csharp
public class IgnoreLockStrategy : IAcquireLockTimeoutStrategy
    {
        public void OnTimeout( string key )
        {
           // The cacheable method will be evaluated regardless of our unability to acquire a lock,
           // unless we throw an exception here.
        }
    }
```


## Implementing a distributed lock manager

Implementing a distributed locking algorithm is a highly complex task and we at PostSharp decided not to get involved in this business (just as we do not provide the implementation of a cache itself). However, PostSharp gives you the ability to use any third-party implementation.

To create make your lock manager work with the caching aspect, you should implement the <xref:PostSharp.Patterns.Caching.Locking.ILockManager> and <xref:PostSharp.Patterns.Caching.Locking.ILockHandle> interfaces. 


