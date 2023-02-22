---
uid: configuration
title: "Configuration"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Configuration

For most use cases, PostSharp does not require any custom configuration. PostSharp gets its default configuration from three sources:

* **MSBuild integration**. PostSharp gets most of its configuration settings directly from the parent MSBuild project. 

* **NuGet integration**. Some PostSharp plug-ins delivered as NuGet packages may modify PostSharp configuration files during installation. 

* **PostSharp Tools**. When adding aspects and policies from Visual Studio, PostSharp may automatically modify some configuration files. 

Even if most configuration settings are correct by default, you may want to understand the configuration system to troubleshoot configuration and installation issues, or simply to implement more advanced configuration scenarios.

PostSharp can be configured using the Visual Studio user interface, by editing MSBuild project files, or by editing PostSharp configuration files.

