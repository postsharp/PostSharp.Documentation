---
uid: aspect-configuration
title: "Aspect Configuration"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document explains how to configure aspects in PostSharp, either declaratively using custom attributes, or imperatively by overriding configuration methods. It also discusses the benefits and limitations of each method. "
---
# Aspect Configuration

Configuration settings of aspects determine how they should be processed by their weaver. Configuration settings are always evaluated at build time. Most aspects have one or many of them. For instance, the aspect type <xref:PostSharp.Aspects.OnExceptionAspect> has a configuration setting determining the type of exceptions handled with this aspect. 

There are two ways to configure an aspect: declarative and imperative.


## Declarative Configuration

You can configure an aspect declaratively by applying the appropriate custom attribute on the aspect class. Aspect configuration attributes are in the namespace <xref:PostSharp.Aspects.Configuration>. Every aspect type has its corresponding type of configuration attribute. The name of the custom attribute starts with the name of the aspect and has the suffix `ConfigurationAttribute`. For instance, the configuration attribute of the aspect class <xref:PostSharp.Aspects.OnExceptionAspect> is <xref:PostSharp.Aspects.Configuration.OnExceptionAspectConfigurationAttribute>. 

Declarative configuration has always precedence over imperative configuration: if some property of the configuration custom attribute is set on the aspect class, or on any parent, the corresponding imperative semantic will not be evaluated.

Once a configuration property has been set in a parent class, it cannot be overwritten in a child class.

Note that these restrictions are enforced at the level of properties. If a property of a configuration custom attribute is not set in a parent class, it can still be overwritten in a child class or by an imperative semantic.


## Imperative Configuration

A second way to configure an aspect class is to override its configuration methods or set its configuration property.

> [!NOTE]
> Imperative configuration is only available when you target the full .NET Framework. It is not available for Silverlight or the Compact Framework.


### Benefits of Imperative Configuration

The advantage of imperative configuration is that it can be arbitrarily complex (since the code of the configuration method is executed inside the weaver). Specifically, it allows the configuration to be dependent on how the aspect is actually used, for instance the configuration can depend on the value of a property of the aspect custom attribute.


### Implementation Note

Under the hood, aspects implement the method <xref:PostSharp.Aspects.IAspectBuildSemantics.GetAspectConfiguration(System.Object)>. This method should return a configuration object, derived from the class <xref:PostSharp.Aspects.Configuration.AspectConfiguration>. Every aspect class has its own aspect configuration class. For instance, the configuration attribute of the aspect class <xref:PostSharp.Aspects.OnExceptionAspect> is <xref:PostSharp.Aspects.Configuration.OnExceptionAspectConfiguration>. The aspect type <xref:PostSharp.Aspects.OnExceptionAspect> implements <xref:PostSharp.Aspects.IAspectBuildSemantics.GetAspectConfiguration(System.Object)> by creating an instance of <xref:PostSharp.Aspects.Configuration.OnExceptionAspectConfiguration>, then it invokes the method <xref:PostSharp.Aspects.OnExceptionAspect.GetExceptionType(System.Reflection.MethodBase)> and copies the return value of this method to the property <xref:PostSharp.Aspects.Configuration.OnExceptionAspectConfiguration.ExceptionType>. Therefore, there are two ways to configure an aspect: either by overriding configuration methods and setting configuration properties (these methods and properties are provided by the framework for convenience only), or by implementing the method <xref:PostSharp.Aspects.IAspectBuildSemantics.GetAspectConfiguration(System.Object)>. If your aspect does not derive from the aspect class <xref:PostSharp.Aspects.OnExceptionAspect>, but directly implements the aspect interface <xref:PostSharp.Aspects.IOnExceptionAspect>, you can use only the later method. 


