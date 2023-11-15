---
uid: caching-getting-started
title: "Caching Method Return Values"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Caching Method Return Values

Suppose you have a time-consuming method that always returns the same return value when called with the same arguments. You decided to cache it. This topic describes how to proceed.


## Caching the return value of a method


### To make a return value of a method being cached:

1. Add a reference to the [PostSharp.Patterns.Caching](https://www.nuget.org/packages/PostSharp.Patterns.Caching/) package. 


2. Add the <xref:PostSharp.Patterns.Caching.CacheAttribute> custom attribute on the method which should be cached. Such method is called the *cached method*. 


3. Now you need to specify which caching framework or caching server is to be used by the <xref:PostSharp.Patterns.Caching.CacheAttribute> aspect. We call this the *caching backend*. You must specify the caching backend by setting the <xref:PostSharp.Patterns.Caching.CachingServices.DefaultBackend> property. See <xref:caching-backends> for a list of caching backends. 

    > [!IMPORTANT]
    > The caching backend has to be set before any cached method is called for the first time.


4. Unless all method parameters are intrinsic types such as `int` or `string`, you need to ensure that the parameter types generate a meaningful cache key. See <xref:caching-keys> for details. 



### Example

In this example, the `GetNumber` method return value is cached. 

```csharp
using System;
using System.Threading;
using PostSharp.Patterns.Caching;
using PostSharp.Patterns.Caching.Backends;

namespace PostSharp.Samples.Caching.MethodResults
{
    class Program
    {
        static void Main( string[] args )
        {
            CachingServices.DefaultBackend = new MemoryCachingBackend();

            Console.WriteLine( "Retrieving value of 1 for the 1st time should hit the database." );
            Console.WriteLine( "Retrieved: " + GetNumber( 1 ) );

            Console.WriteLine( "Retrieving value of 1 for the 2nd time should NOT hit the database." );
            Console.WriteLine( "Retrieved: " + GetNumber( 1 ) );

            Console.WriteLine( "Retrieving value of 2 for the 1st time should hit the database." );
            Console.WriteLine( "Retrieved: " + GetNumber( 2 ) );

            Console.WriteLine( "Retrieving value of 2 for the 2nd time should NOT hit the database." );
            Console.WriteLine( "Retrieved: " + GetNumber( 2 ) );
        }

        [Cache]
        static int GetNumber( int id )
        {
            Console.WriteLine( $">> Retrieving {id} from the database..." );
            Thread.Sleep( 1000 );
            return id;
        }
    }
}
```

The output of this sample is:

```
Retrieving value of 1 for the 1st time should hit the database.
>> Retrieving 1 from the database...
Retrieved: 1
Retrieving value of 1 for the 2nd time should NOT hit the database.
Retrieved: 1
Retrieving value of 2 for the 1st time should hit the database.
>> Retrieving 2 from the database...
Retrieved: 2
Retrieving value of 2 for the 2nd time should NOT hit the database.
Retrieved: 2
```


## Configuring the cache behavior

The following elements of the <xref:PostSharp.Patterns.Caching.CacheAttribute> aspect behavior can be configured: 

* expiration (absolute and sliding),

* priority,

* auto-reload, and

* enabled/disabled.


## Configuring caching with custom attributes

You can configure the <xref:PostSharp.Patterns.Caching.CacheAttribute> aspect by setting the properties of the <xref:PostSharp.Patterns.Caching.CacheAttribute> custom attribute. The inconvenience of this approach is that you have to repeat the configuration for each cached method. To configure several methods in a single line of code, you can add the <xref:PostSharp.Patterns.Caching.CacheConfigurationAttribute> custom attribute to the declaring type, a parent of the declaring type, or the declaring assembly. 

When PostSharp processes the <xref:PostSharp.Patterns.Caching.CacheAttribute> aspect on a given method, it looks for configuration in the following order. 

* the <xref:PostSharp.Patterns.Caching.CacheAttribute> itself, 

* a <xref:PostSharp.Patterns.Caching.CacheConfigurationAttribute> attribute on the declaring class of the cached method, 

* a <xref:PostSharp.Patterns.Caching.CacheConfigurationAttribute> attribute any parent class of the cached method (starting from the declaring class to the parent class), 

* a <xref:PostSharp.Patterns.Caching.CacheConfigurationAttribute> added to the assembly declaring the cached method. Note that all <xref:PostSharp.Patterns.Caching.CacheConfigurationAttribute> assembly-level attributes defined in a different assembly than the current one are ignored. 

* the caching profile (see below).

Configuration is defined on a per-property basis. For each property of the cache aspect, the value set by the highest item in the preceding list wins.


### Example

In the following account, the absolute expiration of cache items is set to 60 minutes for methods of the `AccountServices` class, but to 20 minutes for the `GetAccountsOfCustomer` method. 

```csharp
using System;
using System.Collections.Generic;
using PostSharp.Patterns.Caching;

namespace PostSharp.Samples.Caching
{
    [CacheConfiguration( AbsoluteExpiration = 60 )]
    class AccountServices
    {
        [Cache]
        public static Account GetAccount(int id)
        {
           // Detailed skipped.
        }

        [Cache( AbsoluteExpiration = 20 )]
        public static IEnumerable<Account> GetAccountsOfCustomer(int customerId)
        {
            // Detailed skipped.
        }

        public static void UpdateAccount(Account account)
        {
            // Detailed skipped.
        }
    }
}
```


## Configuring caching with caching profiles

Using custom attributes to configure caching has two major inconveniences: it is hard to share caching configuration between several classes that don't derive from the same parent, and the configuration cannot be modified at run time. To work around these limitations, you can use caching profiles.

Caching profiles are useful in the following scenarios:

* to centralize the configuration of several cached methods (which may belong to different type hierarchies) into a single location;

* to modify the configuration of cached methods (such as expiration settings) at run-time; and

* to completely disable or re-enable caching at run-time.

Caching profiles are represented by the <xref:PostSharp.Patterns.Caching.CachingProfile> class. They are exposed on the <xref:PostSharp.Patterns.Caching.CachingServices.Profiles> property, which is a collection indexed by the profile name. The default caching profile is accessible via the <xref:PostSharp.Patterns.Caching.CachingServices.CachingProfileRegistry.Default> property. 

> [!NOTE]
> Configuration specified thanks to the <xref:PostSharp.Patterns.Caching.CacheAttribute> aspect and the <xref:PostSharp.Patterns.Caching.CacheConfigurationAttribute> custom attribute have priority over the configuration of the <xref:PostSharp.Patterns.Caching.CachingProfile>. 


### To use a caching profile:

1. Set the <xref:PostSharp.Patterns.Caching.CacheAttribute.ProfileName> property of the <xref:PostSharp.Patterns.Caching.CacheAttribute> aspect or the <xref:PostSharp.Patterns.Caching.CacheConfigurationAttribute> custom attribute. 


2. Configure the <xref:PostSharp.Patterns.Caching.CachingProfile> object exposed on the <xref:PostSharp.Patterns.Caching.CachingServices.Profiles> collection. 



### Example

The following code snippet sets the profile name to `Account` for all methods of the `AccountServices` class. 

```csharp
using System;
using System.Collections.Generic;
using System.Threading;
using PostSharp.Patterns.Caching;

namespace PostSharp.Samples.Caching
{
    [CacheConfiguration( ProfileName = "Account" )]
    class AccountServices
    {
        [Cache]
        public static Account GetAccount(int id)
        {
            // Details skipped.
        }

        [Cache]
        public static IEnumerable<Account> GetAccountsOfCustomer(int customerId)
        {
            // Details skipped.
        }

        public static void UpdateAccount(Account account)
        {
            // Details skipped.
        }
    }
}
```

The following code snippet sets the absolute expiration to 60 seconds for all methods using the `Account` profile. 

```csharp
CachingServices.Profiles["Account"].AbsoluteExpiration = TimeSpan.FromSeconds(60);
```


## Disabling caching at run time

You can disable caching at run time by setting the <xref:PostSharp.Patterns.Caching.CachingProfile.IsEnabled> property of a caching profile to `false`, for instance: 

```csharp
CachingServices.Profiles.Default.IsEnabled = false;
             CachingServices.Profiles["Account"].IsEnabled = false;
```

