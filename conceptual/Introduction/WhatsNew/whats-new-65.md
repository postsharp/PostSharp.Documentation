---
uid: whats-new-65
title: "What's New in PostSharp 6.5"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 6.5

Most of our efforts with PostSharp 6.5 went into improving the build-time and design-time experience of PostSharp. We also now proudly and officially support Docker after have successfully tested our product with all Docker images of .NET Core provided by Microsoft.

See also <xref:breaking-changes-65>. 


## Performance enhancements

* The **startup time of PostSharp on .NET Core** has decreased from 1,100 ms to 400 ms, a 2.5 times improvement. Building a lot of small projects should now be significantly faster. To achieve this improvement, we generate ReadyToRun images of PostSharp on the fly on each build machine. This feature can be disabled by setting the `PostSharpReadyToRunDisabled` MSBuild property to `True`. 
    Therefore we are glad to announce that PostSharp performance on .NET Core is now on a par with the one on .NET Framework!

* **Emitting errors and warnings** is now significantly faster: we removed an overhead of approximately another 400 ms on the first message and further improved the time to emit subsequent messages. The expensive part of emitting a message was to determine in which file and on which line the message should be anchored, and this is what we worked on. 
    Previously, on the first message, PostSharp loaded Roslyn to parse the source file and tried to locate the line and column of the offending code element. Loading Roslyn was a significant one-time overhead unless native images were present, and even then the cost of parsing was linear to the number of offending files -- which could grow high if there was a lot of warnings. Now, we are using a Roslyn analyzer to export the location of all code elements (you will find a new *pspdb* file in your output directory). This analyzer resides in the Roslyn process itself and uses the already-parsed Roslyn code model, therefore it is very fast. This approach also allows us to find the source code of types with no method body at all, such as interfaces or enums, which previously we were not able to do. The new strategy causes a performance loss when there is no warning at all, but usage data shows that only a minority of projects would be negatively affected. 
    The analyzer can be disabled by setting the `PostSharpRoslynAnalyzerDisabled` property to `True`, but in this case errors and warnings will not be resolved to a source code location. 

* **PostSharp Tools for Visual Studio** is now even smoother since we continued to apply the async pattern everywhere. We also optimized memory usage so you should get a better experience with large solutions. 




## Installer improvements

The installer now lets you:

* choose which instances of Visual Studio PostSharp should be installed into,

* kill blocking processes, and

* easily see the installation log in case of failure of *VsixInstaller.exe*. 




## Docker support

We've tested PostSharp on Docker thoroughly and fixed a few issues with thin Windows images.


## Platform updates

* We added support for the modern `Microsoft.Extensions.Caching.Memory.IMemoryCache` interface of .NET Core and `Microsoft.Azure.ServiceBus`, the new API for Azure Service Bus. 

* Visual Studio 2017 RTM (15.0) is no longer supported. It is replaced by Visual Studio 2017 Update 1 (15.9, LTS).

