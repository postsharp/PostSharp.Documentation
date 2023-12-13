---
uid: configuration-serialization
title: "Including CLR Objects in Configuration"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Including CLR Objects in Configuration

PostSharp includes a basic facility to describe CLR objects using XML. This facility is used to implement the [Specific rules for the Multicast section](xref:configuration-schema#multicast) and [loggingprofiles](xref:logging-customizing#editing-a-build-time-configuration) sections of the configuration file, and can be used to define custom sections. 

The facility is consciously limited in features. It was only designed to provide the same features as custom attributes in programming languages.


## Basic rules

The basic rules apply to XML serialized objects:

* The local name of the XML element must exactly match the type name of the CLR type. An exception to this rule is that the `Attribute` prefix can be omitted. 

* The XML namespace of the element must be in the form `clr-namespace:namespace;assembly:assembly` where *namespace* is the namespace of the CLR type and *assembly* is the name of the assembly declaring the type. 

* The type must have a public parameterless constructor.

* Names of XML attributes must exactly match the name of a public field or property of the CLR type.


## Formatting of attributes

Values of XML attributes, mapping to CLR fields and properties, must be formatted according to the rule relevant for each type:

| Type | Formatting |
|------|------------|
| Intrinsics | An intrinsic is any of the following types: `bool`, `char`, `sbyte`, `byte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `float`, `double` or `string`. Conversion is done using the <xref:System.Convert> class.  |
| Arrays | A semicolon-separated list of elements. |
| Enumerations | A list of enumeration member names separated by characters `|` or `+`. Names in the list are combined using the + operator.  |
| Types | An assembly-qualified type name. |
| Object | Fields and properties of type <xref:System.Object> are not supported.  |


## Specific rules for the Multicast section

The following additional rules apply to the <xref:configuration-schema> section of the configuration file: 

* The class must derive from <xref:PostSharp.Extensibility.MulticastAttribute>. 

* The <xref:PostSharp.Extensibility.MulticastAttribute.AttributePriority> property may not be defined. This attribute is added automatically according to the order of the XML element in the section. 

