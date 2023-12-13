---
uid: module-initializer
title: "Executing Code Just After the Assembly is Loaded"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document explains how to use the PostSharp.Aspects.ModuleInitializerAttribute attribute to implement module initializers in C# similar to Visual Basic."
---
# Executing Code Just After the Assembly is Loaded

Visual Basic has a concept of module. The module is a special class that gets initialized immediately when the assembly is loaded. This feature is implemented by the CLR, but is not exposed to the C# language. The <xref:PostSharp.Aspects.ModuleInitializerAttribute> attribute allows you to have module initializers in C#. 


### To add a module initializer to your project:

1. Create a public or internal method that has no parameter and no return value. The type declaring the method cannot have generic parameters.


2. Add the <xref:PostSharp.Aspects.ModuleInitializerAttribute> attribute to this method. 


You can add several module initializers a project. Module initializers will be executed in the order you specified in the attribute constructor.

## See Also

**Reference**

<xref:PostSharp.Aspects.ModuleInitializerAttribute>
<br>
