---
uid: assembly-binding-resolution
title: "Resolution of Assembly Binding Redirections"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp follows assembly binding redirections at compile time and stores the assembly binding redirection in a file named PostSharpHost.config. Users can override the default assembly redirections if required."
---
# Resolution of Assembly Binding Redirections

PostSharp executes your project assemblies at compile time. This is why PostSharp must follow assembly binding redirections at compile time. Although the default assembly redirection mechanism works fine in most cases, there may be situations where you will need to override it.

> [!NOTE]
> To learn why PostSharp executes your project assemblies at compile time, see <xref:aspect-lifetime>. 


## Default assembly binding redirections

In order to follow the same assembly binding redirections as you application does at run time, PostSharp analyzes your projects and configuration files (typically *app.config* or *web.config*) and generates assembly binding redirection configuration. PostSharp stores the assembly binding redirection in a file named *PostSharpHost.config* and stored in the *obj* folder. You can review the *PostSharpHost.config* file to get an idea of what configuration PostSharp uses to resolve assemblies. For an empty ASP.NET MVC 5 application the *PostSharpHost.config* may look like this 

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="Microsoft.Owin.Security" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="0.0.0.0-3.0.1.0" newVersion="3.0.1.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="Microsoft.Owin.Security.OAuth" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="0.0.0.0-3.0.1.0" newVersion="3.0.1.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="Microsoft.Owin.Security.Cookies" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="0.0.0.0-3.0.1.0" newVersion="3.0.1.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="Microsoft.Owin" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="0.0.0.0-3.0.1.0" newVersion="3.0.1.0" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>
```

> [!NOTE]
> We didn't reinvent the wheel. Under the hood, PostSharp relies on the [GenerateBindingRedirects](https://msdn.microsoft.com/en-us/library/microsoft.build.tasks.generatebindingredirects.aspx) MSBuild task. 


## Overriding default assembly redirections

In case the default mechanism does not work, you can disable it by setting the MSBuild property `PostSharpDisableDefaultBindingRedirects` to `True` in your project file: 

```xml
<PropertyGroup>
  <PostSharpDisableDefaultBindingRedirects>True</PostSharpDisableDefaultBindingRedirects>
</PropertyGroup>
```

With this configuration PostSharp doesn’t analyze assembly binding redirections.

> [!WARNING]
> Do not set `PostSharpDisableDefaultBindingRedirects` to `True` unless you really have to. It may produce difficult to predict results. 

> [!NOTE]
> The default algorithm is always disabled for Windows Phone Silverlight projects because it does not work.

If you disable default binding redirections, you may want to specify a file with your own assembly binding redirection configuration

```xml
<PropertyGroup>
  <PostSharpDisableDefaultBindingRedirects>True</PostSharpDisableDefaultBindingRedirects>
  <PostSharpHostConfigurationFile>web.config</PostSharpHostConfigurationFile>
</PropertyGroup>
```

With this configuration PostSharp uses explicit assembly binding redirection configuration from the `web.config` file. 


