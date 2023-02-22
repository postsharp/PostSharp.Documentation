---
uid: multicast
title: "Adding Aspects Declaratively Using Attributes"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Adding Aspects Declaratively Using Attributes

In .NET, you normally need to write one line of code for any application of a target attribute. If a custom attribute applies to all types in a namespace, you have to manually add the custom attribute to every single type.

By contrast, multicast custom attributes allow you to apply a custom attribute on multiple declarations from a single line of code by using wildcards or regular expressions, or by filtering on some attributes. It makes it easy to apply an aspect to, say, all public static methods of a namespace, with a single line of code.

Multicast attributes can be inherited: you can put an attribute on an interface and ask it to apply to all classes implementing this interface. Attribute inheritance also works for classes, virtual or interface methods, and parameters of virtual or interface methods.

Custom attributes supporting multicasting need to be derived from <xref:PostSharp.Extensibility.MulticastAttribute>. All PostSharp aspects and constraints are derived from this class. 

> [!NOTE]
> Multicasting of custom attribute is a feature of PostSharp. If you do not transform your assembly using PostSharp, multicast attributes will behave as plain old custom attributes.

> [!NOTE]
> This documentation often refers to this as "quoteInline" multicasting and inheritance. This is not totally accurate. Although this feature has been developed to support aspects, you can use it for your own custom attributes, even if they are not aspects. To use multicasting and inheritance for custom attributes that are not aspects, simply derive the attribute class from <xref:PostSharp.Extensibility.MulticastAttribute> instead of <xref:System.Attribute>. 

Attribute multicasting supports the following scenarios:

* <xref:apply-to-single-declaration>
* <xref:attribute-multicasting>
* <xref:aspect-inheritance>
* <xref:multicast-override>
* <xref:multicast-reflection>
For a conceptual overview of this feature, see:

* <xref:multicast-conceptual>
* <xref:multicast-inheritance>
## See Also

**Reference**

<xref:PostSharp.Extensibility.MulticastAttribute>
<br><xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute>
<br><xref:PostSharp.Aspects.IAspectProvider>
<br>**Other Resources**

<xref:multicast-override>
<br><xref:multicast-reflection>
<br>