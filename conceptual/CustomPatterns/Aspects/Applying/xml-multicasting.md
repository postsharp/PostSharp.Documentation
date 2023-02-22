---
uid: xml-multicasting
title: "Adding Aspects Using XML"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Adding Aspects Using XML

PostSharp not only allows aspects to be applied in code, but also through XML. This is accomplished by adding them to your project's *postsharp.config* file. See <xref:configuration-system> for information on these files. 

Adding aspects through XML gives the advantage of applying aspects without modifying the source code, which could be an advantage in some legacy projects. It allows you to apply aspects to several or all projects in your solution without manually including a source file in each project.


## Specifying an attribute in XML

```csharp
namespace MyCustomAttributes
{
    // We set up multicast inheritance so  the aspect is automatically added to children types.
    [MulticastAttributeUsage( MulticastTargets.Class, Inheritance = MulticastInheritance.Strict )]
    [PSerializable]
    public sealed class AutoDataContractAttribute : TypeLevelAspect, IAspectProvider
    {
        // Details skipped.
    }
}
```

Normally `AutoDataContractAttribute` would be applied to `Customer` in code as follows: 

```csharp
namespace MyNamespace
{
    [AutoDataContractAttribute]
    class Customer    
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
```

Using XML instead, we can remove the custom attribute from source code and instead specify a `Multicast` element in the PostSharp project file, which is named *postsharp.config* and placed in the same directory as the project file: 

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.postsharp.org/1.0/configuration">
    <Multicast xmlns:my="clr-namespace:MyCustomAttributes;assembly:MyAssembly">
        <my:AutoDataContractAttribute  AttributeTargetTypes=" MyNamespace.Customer" />
    </Multicast>    
</Project>
```

In this snippet, the `xmlns:my` attribute associates a prefix to an XML namespace, which must be mapped to the .NET namespace and assembly where custom attributes classes are defined: 

```xml
<Multicast xmlns:my="clr-namespace:MyCustomAttributes;assembly:MyAssembly">
```

The next line then specifies the custom attribute to apply and the target attributes to apply the custom attributes to:

```xml
<my:AutoDataContractAttribute  AttributeTargetTypes="MyNamespace.Customer" />
```

The XML element name must be the name of a class inside the .NET namespace and assembly as defined by the XML namespace. Attributes of this XML element map to public properties or fields of this class.

Note that any property inherited from <xref:PostSharp.Extensibility.MulticastAttribute> can be used here in order to apply the aspect to several classes at a time. See the section <xref:attribute-multicasting> for details about these properties. 

## See Also

**Other Resources**

<xref:attribute-multicasting>
<br><xref:configuration-system>
<br>