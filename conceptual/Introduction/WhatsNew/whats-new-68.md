---
uid: whats-new-68
title: "What's New in PostSharp 6.8"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 6.8


## Support for .NET 5 and C# 9

We've completed the work on .NET 5 that we've already started in PostSharp 6.7. Additionally, we have tested PostSharp with C# 9 and performed a few corrections to support the new features like function pointers.


## Logging

PostSharp 6.8 includes several improvements in logging:

* Configuration of verbosity using a configuration file, which you can also store in a cloud storage service like Google Drive, and update without redeploying or restarting your app. For details, see <xref:log-enabling>. 

* Per-transaction (or per-request) configuration of verbosity: you can for instance log only requests coming from a specific IP, or a random 10% of all requests, of one request per minute for the `/invoices` API. This is also described in <xref:log-enabling>. To see how to use this facility with custom transactions, see <xref:custom-logging-transactions>. 

* Automatic instrumentation of ASP.NET Core and HttpClient, including support for per-request logging and distributed logging.

* The mechanism to pass properties to custom activities or messages has been refactor. It is now both faster and more usable than before in most use cases. For details, see <xref:log-properties>. 

* We have removed the back-ends for Enterprise Library and Gibraltar Loupe, and made the audit component obsolete.


## Usage measurement for Per-Usage

It is now possible to know exactly how many lines of code you would be consuming with a Per-Usage Subscription even if you don't have one yet. For details, see <xref:per-repo-licenses>. 

