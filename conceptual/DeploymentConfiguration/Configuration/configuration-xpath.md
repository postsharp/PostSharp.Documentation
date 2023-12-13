---
uid: configuration-xpath
title: "Using Expressions in Configuration Files"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document explains how to use expressions in PostSharp configuration files, which work similarly to XSLT and are interpreted as XPath expressions. It covers accessing properties, using functions and operators, and mixing expressions with literal strings."
---
# Using Expressions in Configuration Files

Many attributes of the configuration schema accept expressions, which are dynamically evaluated. Expressions in the PostSharp configuration system work similarly as in XSLT. Substrings enclosed by curled brackets, for instance `{$property}`, are interpreted as XPath expressions. 

For instance, the following code contains two XPath expressions:

```xml
<Project xmlns="http://schemas.postsharp.org/1.0/configuration">
  <Property Name="LoggingEnabled" Value="{has-plugin('PostSharp.Patterns.Diagnostics')}" Deferred="true" />
  <Multicast>
    <When Condition="{$LoggingEnabled}">
      <d:Log  />
    </When>
  </Multicast>
</Project>
```

Please check the [MSDN documentation](http://msdn.microsoft.com/en-us/library/ms256138(v=vs.110).aspx) for general information about XPath. 

> [!NOTE]
> In the context of PostSharp configuration files, XPath expressions cannot refer to XML elements or attributes, but only to variables, functions, operators and constants.


## Accessing properties

PostSharp properties are mapped to XPath variables.

For instance, the expression `{$LoggingEnabled}` evaluates o the value of the *LoggingEnabled* property. 


## Using operators and functions

You can use any XPath function and operators.

Additionally to [standard XPath 1.0 functions](http://msdn.microsoft.com/en-us/library/ms256138(v=vs.110).aspx), PostSharp defines the following functions: 

| Function | Description |
|----------|-------------|
| `has-plugin(name)` | Evaluates to `true` if the given plug-in can be found in the project, otherwise `false`. The argument *name* is the filename without extension of the plug-in assembly without the *.Weaver.dll* suffix. It is typically identical to the package name. For example, for the PostSharp logging weaver, it would be *PostSharp.Patterns.Diagnostics*. <br>A plug-in can be found in the project if it's in the `PostSharpSearchPath` property or if it's imported in a NuGet package.  |
| `environment(variable)` | Returns the value of an environment variable. |


## Mixing expressions and literal strings

An attribute value can contain both text and expressions. This is illustrated in the following example:

```xml
<Project xmlns="http://schemas.postsharp.org/1.0/configuration">
          <Property Name="A" Value="A" />            <!-- Evaluates to A -->
          <Property Name="B" Value="B;{$A}" />       <!-- Evaluates to B;A -->
          <Property Name="C" Value="C;{$B};{$A}" />  <!-- Evaluates to C;B;A;A -->
        </Project>
```


