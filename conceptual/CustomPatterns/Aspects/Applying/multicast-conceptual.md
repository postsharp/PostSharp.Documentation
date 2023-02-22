---
uid: multicast-conceptual
title: "Understanding Attribute Multicasting"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Understanding Attribute Multicasting


## Overview of the Multicasting Algorithm

Every multicast attribute class must be assigned a set of legitimate targets using the <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute> custom attribute, which is the equivalent and complement of <xref:System.AttributeUsageAttribute> for multicast attributes. Multicast attributes can be applied to types, methods, fields, properties, events, and parameters. For instance, a caching aspect targets methods. A field validation aspect targets fields. 

When a field-level multicast attribute is applied to a type, the attribute is implicitly applied to all fields of that type. When it is applied to an assembly, it is implicitly applied to all fields of that assembly.

The general rule is: when a multicast attribute is applied to a container, it is implicitly (and recursively) applied to all elements of that container.

The next table illustrates how this rule translates for different kinds of targets.

| Directly applied to | Implicitly applied to |
|---------------------|-----------------------|
| Assembly or Module | Types, methods, fields, properties, parameters, and events contained in this assembly or module. |
| Type | Methods, fields, properties, parameters, and events contained in this type. |
| Property or Event | Accessors of this property or event. |
| Method | This method and the parameters of this method. |
| Field | This field. |
| Parameter | This parameter. |


## Filtering Target Elements of Code

Note that the default behavior is maximalist: we apply the attribute to *all* contained elements. However, PostSharp provides a way to restrict the set of elements to which the attribute is multicast: filtering. 

Both the attribute developer and the user of the aspect can specify filters.


### Developer-Specified Filtering

Just like normal custom attributes should be decorated with the `[AttributeUsage]` custom attribute, multicast custom attributes must be decorated by the `[MulticastAttributeUsage]` attribute (see <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute>). It specifies which are the valid targets of the multicast attributes. 

For instance, the following piece of code specifies that the attribute `GuiThreadAttribute` can be applied to instance methods. Aspect users experience a build-time error when trying to use this aspect on a constructor or static method. 

```csharp
[MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
              [AttributeUsage(AttributeTargets.Assembly|AttributeTargets.Class|AttributeTargets.Method, AllowMultiple = true)]
              [PSerializable]
              public class GuiThreadAttribute : MethodInterceptionAspect
              {
              // Details skipped.
              }
```

Note the presence of the <xref:System.AttributeUsageAttribute> attribute in the sample above. It tells the C# or VB compiler that the attribute can be directly applied to assemblies, classes, constructors, or methods. But this aspect will never be eventually applied to an assembly or a class. Indeed, the <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute> attribute specifies that the sole valid targets are methods. Furthermore, the `TargetMemberAttributes` property establishes a filter that includes only instance methods. 

Therefore, if the aspect is applied to a type containing an abstract method, the aspect will not be multicast to this method, nor to its constructors.

> [!TIP]
> In addition to multicast filtering, consider using programmatic validation of aspect usage. Any custom attribute can implement <xref:PostSharp.Extensibility.IValidableAnnotation> to implement build-time validation of targets. Aspects that derive from <xref:PostSharp.Aspects.Aspect> already implement these interfaces: your aspect can override the method <xref:PostSharp.Aspects.Aspect.CompileTimeValidate(System.Object)>. 

> [!TIP]
> As an aspect developer, you should enforce as many restrictions as necessary to ensure that your aspect is only used in the way you intended, and raise errors in other cases. Using an aspect in an unexpected way may result in runtime errors that are difficult to debug.


### User-Specified Filtering

The attribute user can specify multicasting filters using specific properties of the <xref:PostSharp.Extensibility.MulticastAttribute> class. To make it clear that these properties only impact the multicasting process, they have the prefix `Attribute`. Only the attribute user can set the values of these properties. 

As an aspect developer, do not set the values of these properties in the aspect construtor. Instead, use multicast filtering or programmatic validation of aspect usage described in the section [Developer-Specified Filtering](#developer-specified-filtering). 

As an aspect user, it is important to understand that you can only apply aspects to elements of code that have been allowed by the developer of the aspect.

For instance, the following element of code adds a tracing aspect to all public methods of a namespace:

```csharp
[assembly: Trace( AttributeTargetTypes="AdventureWorks.BusinessLayer.*", AttributeTargetMemberAttributes = MulticastAttributes.Public )]
```


## Filtering Properties

The following table lists the filters available to users and developers of aspects:

| T:PostSharp.Extensibility.MulticastAttribute Property (for ;aspect users) | T:PostSharp.Extensibility.MulticastAttributeUsageAttribute Property (for aspect developers) | Description |
|--------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------|-------------|
| <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetElements> | <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.ValidOn> | Restricts the kinds of targets (assemblies, classes, value types, delegates, interfaces, properties, events, properties, methods, constructors, parameters) to which the attribute can be indirectly applied. |
| <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetAssemblies> |  | Wildcard expression or regular expression specifying to which assemblies the attribute is multicast. |
|  | <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.AllowExternalAssemblies> | Determines whether the aspect can be applied to elements defined in a different assembly than the current one. |
| <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypes> |  | Wildcard expression or regular expression filtering by name the type to which the attribute is applied, or the declaring type of the member to which the attribute is applied. |
| <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypeAttributes> | <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.TargetTypeAttributes> | Restricts the visibility of the type to which the aspect is applied, or of the type declaring the member to which the aspect is applied. |
| <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetMembers> |  | Wildcard expression or regular expression filtering by name the member to which the attribute is applied. |
| <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetMemberAttributes> | <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.TargetMemberAttributes> | Restricts the attributes (visibility, virtuality, abstraction, literality, ...) of the member to which the aspect is applied. |
| <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetParameters> |  | Wildcard expression or regular expression specifying to which parameter the attribute is multicast. |
| <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetParameterAttributes> | <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.TargetParameterAttributes> | Restricts the attributes (in/out/ref) of the parameter to which the aspect is applied. |
| <xref:PostSharp.Extensibility.MulticastAttribute.AttributeInheritance> | <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.Inheritance> | Specifies whether the aspect is propagated along the lines of inheritance of the target interface, class, method, or parameter (see <xref:multicast-inheritance>).  |

> [!CAUTION]
> Whenever possible, do not rely on naming conventions to apply aspects (properties <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypes>, <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetMembers> and <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetParameters>). This may work perfectly today, and break tomorrow if someone renames an element of code without being aware of the aspect. 


## Overriding Filtering Attributes

Suppose we have two classes `A` and `B`, `B` being derived from `A`. Both `A` and `B` can be decorated with <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute>. However, since `B` is derived from `A`, filters on `B` cannot be more permissive than filters on `A`. 

In other words, the <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute> custom attribute is inherited. It can be overwritten in derived classes, but derived class cannot *enlarge* the set of possible targets. They can only *restrict* it. 

Similarly (and hopefully predictably), the aspect user is subject to the same rule: they can restrict the set of possible targets supported by the aspect, but cannot enlarge it.

