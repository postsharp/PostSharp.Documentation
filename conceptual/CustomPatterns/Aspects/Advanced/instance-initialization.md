---
uid: instance-initialization
title: "Coping with Custom Object Serializers"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document explains how to handle custom object serializers in PostSharp. It details how to manually initialize aspects either by defining a method 'InitializeAspects' or invoking 'AspectUtilities.InitializeCurrentAspects'."
---
# Coping with Custom Object Serializers

Some aspects need to be initialized when a new instance of the class to which they are applied is created. For instance, instance-scoped aspect must be cloned from the prototype; members imported into the through <xref:PostSharp.Aspects.Advices.ImportMemberAttribute> must be bound to aspect fields. 

PostSharp enhances every constructor of every enhanced class so that aspects are properly initialized.

However, it is possible to create new instances of classes by *bypassing* the constructor. This happens, for instance, when classes are deserialized by the <xref:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter> or the <xref:System.Runtime.Serialization.DataContractSerializer>. These formatters use the method <xref:System.Runtime.Serialization.FormatterServices.GetUninitializedObject(System.Type)> to create new instances, but this method bypasses all constructors. 

PostSharp implements a workaround for the deserializers <xref:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter> and <xref:System.Runtime.Serialization.DataContractSerializer>: it creates or modifies a method annotated by the custom attribute <xref:System.Runtime.Serialization.OnDeserializingAttribute>, so that aspects are initialized properly. 

However, if you are using a custom deserializer, or for any reason create instances using the method the method <xref:System.Runtime.Serialization.FormatterServices.GetUninitializedObject(System.Type)>, you will have to initialize aspects manually. 


## Initializing Aspects Manually

There are two possible ways to initialize an aspect from user code.


### By Defining a Method InitializeAspects

You can define in your classes (typically in one of the root classes of your class hierarchy) a method with the following name and signature:

```csharp
protected virtual void InitializeAspects();
```

When PostSharp discovers this method, it will insert its own initialization logic at the beginning of the `InitializeAspects` method. The original logic is not deleted. This method can safely have an empty implementation. 

The following constraints apply:

* The method should be `virtual` unless the class is sealed. 

* The method should be `protected` or `public` unless the class is `internal`. 

For instance, the following class would enable aspects (applied to this class or on derived classes) to be initialized after deserialization (note that PostSharp automatically generates this code for <xref:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter> and <xref:System.Runtime.Serialization.DataContractSerializer>; you only need to do it manually for a custom serializer). 

```csharp
[DataContract]
              public abstract class BaseClass
              {
                protected virtual void InitializeAspects()
                {
                }

                [OnDeserializing]
                private void OnDeserializingInitializeAspects()
                {
                  this.InitializeAspects();
                }
              }
```


### By Invoking AspectUtilities.InitializeCurrentAspects

Instead of providing an empty method `InitializeAspects`, it is possible to invoke the method <xref:PostSharp.Aspects.AspectUtilities.InitializeCurrentAspects>. A call to this method will be translated into a call to `InitializeAspects`. It has to be invoked from a non-static method of an enhanced class. 

If the class from which <xref:PostSharp.Aspects.AspectUtilities.InitializeCurrentAspects> is invoked has not been enhanced by an aspect requiring initialization, the call to this method is simply ignored. 

> [!NOTE]
> Using this approach may be brittle in some situations: calls to <xref:PostSharp.Aspects.AspectUtilities.InitializeCurrentAspects> will have no effect if aspects are applied to derived classes, but not to the calling class. In this scenario, it is preferable to define the method `InitializeAspects`. 


