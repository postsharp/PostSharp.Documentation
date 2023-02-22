---
uid: aspect-serialization
title: "Understanding Aspect Serialization"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Understanding Aspect Serialization

As explained in section <xref:aspect-lifetime>, aspects are first instantiated at build time by the weaver, are then initialized by the `CompileTimeInitialize` method, and serialized and stored in the assembly as a managed resource. Aspects are then deserialized at run time, before being executed. 

Because of the aspect life cycle, aspect classes must be made serializable as described in this section.


## Default serialization strategy

Typically, aspects can be made serializable by adding a custom attribute to the class, which causes all fields of the class to be serialized. Fields that do not need to be serialized must be annotated with an opt-out custom attribute. PostSharp chooses the serialization strategy according to these custom attributes. The serialization strategy is implemented in classes derived from the abstract <xref:PostSharp.Aspects.Serialization.AspectSerializer> class. The default serialization strategy is implemented in the <xref:PostSharp.Aspects.Serialization.PortableAspectSerializer> class, that is backed by <xref:PostSharp.Serialization.PortableFormatter>. 

This is how you can apply default serialization strategy to your aspect:

* To make the class serializable, annotate the class with the [<xref:PostSharp.Serialization.PSerializableAttribute>] custom attribute. 

* To exclude the field from the serialization, annotate the field with the [<xref:PostSharp.Serialization.PNonSerializedAttribute>] custom attribute. 


## Fallback serialization strategy

In some cases, the default serialization strategy implemented by the <xref:PostSharp.Aspects.Serialization.PortableAspectSerializer> class may not be appropriate for your aspects. For example, the data structures used in your classes may not be supported by the <xref:PostSharp.Serialization.PortableFormatter> implementation or you may need your code to be backward compatible with PostSharp 4.2 and earlier. In versions 4.2 and earlier the default serialization strategy was implemented in the <xref:PostSharp.Aspects.Serialization.BinaryAspectSerializer> class, that was backed by <xref:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter>. You can still use <xref:PostSharp.Aspects.Serialization.BinaryAspectSerializer> as a fallback serialization strategy in PostSharp 4.3 and later. 

To apply fallback serialization strategy to your aspects, use [<xref:System.SerializableAttribute>] custom attribute instead of [<xref:PostSharp.Serialization.PSerializableAttribute>], and use [<xref:System.NonSerializedAttribute>] custom attribute instead of [<xref:PostSharp.Serialization.PNonSerializedAttribute>]. 

> [!NOTE]
> The <xref:PostSharp.Aspects.Serialization.BinaryAspectSerializer> class is supported only in projects that target the .NET Framework with full trust. 

> [!SECURITY NOTE]
> <xref:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter> is considered dangerous and therefore [<xref:System.SerializableAttribute>] should not be used for aspect serialization. See section <xref:binary-formatter-security> for details. 


## Aspects without serialization

In some situations, serializing and deserializing the aspect may be a suboptimal solution. In case aspect field values are a pure function of constructor arguments and properties, it may be more efficient to emit code that instantiates these aspects at run time instead of serializing-deserializing them. This is the case, typically, if the aspect does not implement the `CompileTimeInitialize` method. 

In this situation, it is better to use a different serializer: <xref:PostSharp.Aspects.Serialization.MsilAspectSerializer>. 

> [!NOTE]
> <xref:PostSharp.Aspects.Serialization.MsilAspectSerializer> is actually **not** a serializer. When you use this implementation instead of a real serializer, the aspect is **not** serialized, but the weaver generates MSIL instructions to build the aspect instance at run time, by calling the aspect class constructor and by setting its fields and properties. 

You can specify which serializer should be used for a specific aspect class by setting the property <xref:PostSharp.Aspects.Configuration.AspectConfiguration.SerializerType> of the configuration of this aspect class or instance. 

See section <xref:aspect-configuration> for details. 

The following code shows how to choose the serializer type for an <xref:PostSharp.Aspects.OnMethodBoundaryAspect>: 

```csharp
[OnMethodBoundaryAspectConfiguration(SerializerType=typeof(MsilAspectSerializer))]
public sealed MyAspect : OnMethodBoundaryAspect
```

## See Also

**Other Resources**

<xref:aspect-configuration>
<br>**Reference**

<xref:PostSharp.Aspects.Serialization.AspectSerializer>
<br><xref:PostSharp.Aspects.Serialization.PortableAspectSerializer>
<br><xref:PostSharp.Aspects.Serialization.MsilAspectSerializer>
<br><xref:PostSharp.Aspects.Configuration.AspectConfiguration.SerializerType>
<br>