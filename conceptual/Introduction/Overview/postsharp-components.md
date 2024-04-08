---
uid: postsharp-components
title: "PostSharp Components"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp offers components for Visual Studio, available as NuGet packages or a zip archive for teams not using NuGet. It includes build-time and run-time packages for various functions and aspects."
---
# PostSharp Components


## Visual Studio Tools for Metalama and PostSharp

This is the user interface of PostSharp. It extends the Visual Studio editor and provides a new menu, option pages, toolbox windows, diagnostics, code actions, and debugging enhancements.

For details regarding the installation of this component, see <xref:installation> and <xref:installation-silent>. 


## NuGet packages

All build-time and run-time artifacts are released as NuGet packages. Build-time packages are required to build your projects, but only the content of run-time packages is required to execute your applications.

If you build NuGet packages that use PostSharp but does not define custom aspects, your package should only reference the relevant PostSharp run-time packages, not the build-time ones.

> [!NOTE]
> The PostSharp License Agreement refers to run-time packages as *redistributables*. The license agreement allows for royalty-free redistribution of run-time packages, but stricter conditions apply to the redistribution of build-time packages. 

The following table lists all PostSharp packages:

| Run-time package | Build-time package | Description |
|------------------------------------------------------|--------------------------------------------------------|-------------------------------------------------|
| *PostSharp.Redist* | *PostSharp* | PostSharp Framework. The build-time package includes the PostSharp compiler. |
| *PostSharp.Patterns.Common.Redist* | *PostSharp.Patterns.Common* | Common logic shared between pattern libraries. Code contracts. |
| *PostSharp.Patterns.Aggregation.Redist* | *PostSharp.Patterns.Aggregation* | Aggretable and Disposable aspects. |
| *PostSharp.Patterns.Model.Redist* | *PostSharp.Patterns.Model* | NotifyPropertyChanged aspect and Undo/Redo. |
| *PostSharp.Patterns.XAML.Redist* | *PostSharp.Patterns.XAML* | Command, Dependency Property and Attached Property aspects. WPF controls for undo/redo. |
| *PostSharp.Patterns.Threading.Redist* | *PostSharp.Patterns.Threading* | Threading models, thread dispatching aspects, deadlock detection. |
| *PostSharp.Patterns.Caching.Redist* | *PostSharp.Patterns.Caching* | Caching aspect. |
| *PostSharp.Patterns.Caching.Redis* | N/A | Redis connector for *PostSharp.Patterns.Caching*.  |
| *PostSharp.Patterns.Caching.Azure* | N/A | Azure connector for *PostSharp.Patterns.Caching*.  |
| *PostSharp.Patterns.Diagnostics.Redist* | *PostSharp.Patterns.Diagnostics* | Logging aspect. |
| *PostSharp.Patterns.Diagnostics.ApplicationInsights* | N/A | Application Insights connector for *PostSharp.Patterns.Diagnostics*.  |
| *PostSharp.Patterns.Diagnostics.CommonLogging* | N/A | Common.Logging connector for *PostSharp.Patterns.Diagnostics*.  |
| *PostSharp.Patterns.Diagnostics.Log4Net* | N/A | Log4Net connector for *PostSharp.Patterns.Diagnostics*.  |
| *PostSharp.Patterns.Diagnostics.Microsoft* | N/A | Microsoft.Extensions.Logging connector for *PostSharp.Patterns.Diagnostics*.  |
| *PostSharp.Patterns.Diagnostics.NLog* | N/A | NLog connector for *PostSharp.Patterns.Diagnostics*.  |
| *PostSharp.Patterns.Diagnostics.Serilog* | N/A | Serilog connector for *PostSharp.Patterns.Diagnostics*.  |
| *PostSharp.Patterns.Diagnostics.Tracing* | N/A | System.Diagnostics connector for *PostSharp.Patterns.Diagnostics*.  |
| *PostSharp.Patterns.Diagnostics.Configuration* | N/A | Configuration of verbosity from a remote or local XML file. |
| *PostSharp.Patterns.Diagnostics.DiagnosticSource* | N/A | Collects events from <xref:System.Diagnostics.DiagnosticSource>.  |
| *PostSharp.Patterns.Diagnostics.HttpClient* | N/A | Collects events from <xref:System.Net.Http.HttpClient>.  |
| *PostSharp.Patterns.Diagnostics.AspNetCore* | N/A | Collects events from ASP.NET Core and ASP.NET 5. |

## See Also

**Other Resources**

<xref:deployment-end-user>
<br>
