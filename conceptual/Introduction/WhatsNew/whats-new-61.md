---
uid: whats-new-61
title: "What's New in PostSharp 6.1"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 6.1

In PostSharp 6.1, we primarily focused on two directions: the logging API so that we can cope with distributed systems, and the debugging experience. Additionally, we fixed gapped in .NET Standard and C# 7.3 support.

> [!IMPORTANT]
> Despite being a minor version, PostSharp 6.1 contains some low-impact breaking changes in the logging API. The chances that you would be negatively affected are minor. See <xref:breaking-changes-61> for details. 


## Logging: major improvements in the front-end API

We've worked hard to improve the scenario of logging high-load distributed systems (such microservices) into a structured database (such as Elastic Search), and enable statistical processing of the log records.

To achieve this goal, we had to bring several improvements to PostSharp Logging:

* **Semantic-First Logging**, suitable for statistical processing of messages and machine learning. See [semantic](xref:log-custom-messages#writing-semantic-messages-for-easy-statistical-processing) for details.

* **Custom Properties** on custom messages and activities, including cross-process properties (aka baggage). See <xref:log-properties> for details. 

* **Hierarchical and Cross-Process Activity ID** that can be easily filtered and sorted on to get a logical view of a request in a distributed system. See <xref:distributed-logging> for details. 

* **Sampled Logging**: Instead of hoarding gigabytes of useless logs, you can now enable logging for selected requests only. See <xref:log-enabling> for details. 

* **Execution time measurement for custom activities**. See [execution-time](xref:log-custom-activities#measuring-the-execution-time-of-activities) for details. 

* **Serilog backend improvements**: You now have more control over generated Serilog properties. See <xref:PostSharp.Patterns.Diagnostics.Backends.Serilog.SerilogLoggingBackendOptions.IncludedSpecialProperties> and <xref:PostSharp.Patterns.Diagnostics.Backends.Serilog.SerilogLoggingBackendOptions.LogEventEnricher> properties for details. 

* **Application Insights backend improvements**: the backend now supports custom properties. See <xref:PostSharp.Patterns.Diagnostics.Backends.ApplicationInsights.ApplicationInsightsLoggingBackendOptions> class for details. 

Additionally, PostSharp 6.1 brings the following improvements to logging:

* We are now using the type name instead of the role name by default to read the logging level from the backend.

* Simplified API to set up verbosity. See <xref:PostSharp.Patterns.Diagnostics.LoggingBackend.DefaultVerbosity> for details. 

* Exception logging is now again implemented in a catch block instead of an exception filter block.

* Exception formatting can now be customized by setting the <xref:PostSharp.Patterns.Diagnostics.Backends.TextLoggingBackendOptions.ExceptionFormatter> property. 


## Visual Studio debugger: better support for async methods

We are releasing a complete refactoring of our add-in to the Visual Studio debugger. It solves a dozen of issues and finally fixes the debugging behavior of intercepted async methods.


## Support for C# 7.3

PostSharp was affected by the following features of C# 7.3:

* `in` parameters, 

* `unmanaged`, <xref:System.Enum> and <xref:System.Delegate> constraints. 


## Fixing gaps in platform support

* PostSharp.Patterns.Caching now supports .NET Standard 2.0 and .NET Framework 4.5.

* PostSharp.Patterns.Diagnostics.Backends.CommonLogging now supports .NET Standard 2.0.


## Miscelanneous

* Resolved ambiguous method matching in <xref:PostSharp.Aspects.Advices.LocationValidationAdvice> by adding the <xref:PostSharp.Aspects.Advices.LocationValidationAdvice.Priority> property. 

