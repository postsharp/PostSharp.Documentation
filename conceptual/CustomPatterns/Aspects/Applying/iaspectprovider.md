---
uid: iaspectprovider
title: "Adding Aspects Programmatically using IAspectProvider"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides a guide on how to add aspects programmatically using the IAspectProvider interface in PostSharp, including creating aspect graphs and initializing aspects at runtime."
---
# Adding Aspects Programmatically using IAspectProvider

You may have situations where you are looking to implement an aspect as part of a larger pattern. Perhaps you want to add an aspect, implement an interface and dynamically inject some logic into the target code. In those situations, you will want to apply an aspect to the target code and have that aspect then add other aspects to other elements of code.

The theoretical concept can cause some mental gymnastics, so let's take a look at the implementation.

1. Create an aspect that implements that <xref:PostSharp.Aspects.IAspectProvider> interface. 

    ```csharp
    public class ProviderAspect : IAspectProvider 
     { 
        public IEnumerable<AspectInstance> ProvideAspects(object targetElement) 
        { 
            throw new System.NotImplementedException(); 
        } 
     }
    ```


2. Cast the target object parameter to the type that will be targeted by this aspect: `Assembly`, `Type`, `MethodInfo`, `ConstructorInfo` or `LocationInfo`. 

    ```csharp
    public IEnumerable<AspectInstance> ProvideAspects(object targetElement) 
     { 
        Type type = (Type) targetElement; 
     
        throw new NotImplementedException(); 
     }
    ```


3. The <xref:PostSharp.Aspects.IAspectProvider.ProvideAspects(System.Object)> method returns an <xref:PostSharp.Aspects.AspectInstance> of the aspect type you want, for every target element of code. 

    ```csharp
    public IEnumerable<AspectInstance> ProvideAspects(object targetElement) 
     { 
         Type type = (Type)targetElement; 
     
         return type.GetMethods().Select( 
            m => new AspectInstance(m, new LoggingAspect() ) ); 
     
     }
    ```


This aspect will now add aspects dynamically at compile time. Use of the <xref:PostSharp.Aspects.IAspectProvider> interface and technique is usually reserved for situations where you are trying to implement a larger design pattern. For example, it would be used when implementing an aspect that created the <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> pattern across a large number of locations in your codebase. It is overkill for many of the situations that you will encounter. Use it only for complicated pattern implementation aspects that you will create. 

> [!NOTE]
> To read more about <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute>, see <xref:inotifypropertychanged-customization>. 

> [!NOTE]
> PostSharp does not automatically initialize the aspects provided by <xref:PostSharp.Aspects.IAspectProvider>, even if the method `CompileTimeInitialize` is defined. Any initialization, if necessary, should be done in the `ProvideAspects` method or in the constructor of provided aspects. 
However, these aspects are initialized at run time just like normal aspects using the `RunTimeInitialize` method. 


## Creating Graphs of Aspects

It is common that aspects provided by <xref:PostSharp.Aspects.IAspectProvider> (children aspects) form a complex object graph. For instance, children aspects may contain a reference to the parent aspect. 

An interesting feature of PostSharp is that object graphs instantiated at compile-time are serialized, and can be used at run-time. In other words, if you store a reference to another aspect in a child aspect, you will be able to use this reference at run time.

## C# 14 Extension Block Members

C# 14 introduced extension members. Both <xref:PostSharp.Aspects.IAspectProvider> and <xref:PostSharp.Aspects.Advices.IAdviceProvider> can be used on extension member implementation methods (members of the class containing extension blocks).

Targeting extension block metadata types (nested types emitted by the C# compiler) and all their members will result in an error `LA0167`. You can use <xref:PostSharp.Reflection.ReflectionHelper.IsExtensionBlockMetadata> to identify extension block metadata members if filtering is needed.

## See Also

**Reference**

<xref:PostSharp.Aspects.IAspectProvider>
<br><xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute>
<br><xref:PostSharp.Aspects.IAspectProvider.ProvideAspects(System.Object)>
<br><xref:PostSharp.Aspects.AspectInstance>
<br><br>**Other Resources**

<xref:inotifypropertychanged>
<br><xref:inotifypropertychanged-customization>
<br>[C# 14 Extension Members - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/extension)
<br>[Exploring extension members - .NET Blog](https://devblogs.microsoft.com/dotnet/csharp-exploring-extension-members/)

<br>
