---
uid: backends
title: "Connecting to Source and Target Logging Frameworks"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Connecting to Source and Target Logging Frameworks

This chapter explains how to configure PostSharp Logging to specifically work with different logging frameworks.

PostSharp sends log events, both automatic and manual, to a *logging backend* which is responsible for outputting them on screen, into a file, database or elsewhere. PostSharp contains logging backends for many different logging frameworks and you can also create your own backend. 

These articles explain how to set up PostSharp with various logging frameworks:

* <xref:logging-console>
* <xref:logging-tracesource>
* <xref:logging-trace>
* <xref:nlog>
* <xref:log4net>
* <xref:serilog>
* <xref:etw>
* <xref:common-logging>
* <xref:application-insights>
* <xref:logging-aspnetcore>
In addition, the following scenarios are supported:

| Section | Description |
|---------|-------------|
| <xref:custom-logging-backend> | This article shows how to write PostSharp Logging output to any logging framework by building your own adapter, named *backend* in PostSharp jargon.  |
| <xref:log-multiplexer> | This article explains how to write PostSharp Logging output to several logging frameworks, each possibly with different verbosity. |
| <xref:audit> | This article shows how to automatically add audit records to your application. |
PostSharp Logging can also collect events from many logging frameworks. This is called *collecting logs*. For details, see <xref:log-collecting>. 

## See Also

**Other Resources**

<xref:add-logging>
<br>