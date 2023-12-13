---
uid: complex-aspects
title: "Developing Composite Aspects"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document discusses composite aspects in PostSharp's aspect-oriented development. It explains advices and pointcuts, and how to add functionalities to aspects."
---
# Developing Composite Aspects

PostSharp offers two approaches to aspect-oriented development. The first, as explained in section <xref:simple-aspects>, is very similar to object-oriented programming. It requires the aspect developer to override virtual methods or implement interfaces. This approach is very efficient for simple problems. 

One way to grow in complexity with the first approach is to use the interface <xref:PostSharp.Aspects.IAspectProvider> (see <xref:aspect-provider>). However, even this technique has its limitations. 

This chapter documents the second approach, closer to the classic paradigm of aspect-oriented programming introduced by AspectJ. This approach allows developers to implement more complex design patterns using aspects. We call the aspects developed with this approach *composite aspects*, because they are freely composed of different elements named *advices* and *pointcuts*. 

An *advice* is anything that adds a behavior or a structural element to an element of code. For instance, introducing a method into a class, intercepting a property setter, or catching exceptions, are advices. 

A *pointcut* is a function returning a set of elements of code to which advices apply. For instance, a function returning the set of properties annotated with the custom attribute `DataMember` is a pointcut. 

Classes supporting advices and pointcuts are available in the namespace <xref:PostSharp.Aspects.Advices>. 

A composite aspect generally derives from a class that does not define its own advices: <xref:PostSharp.Aspects.AssemblyLevelAspect>, <xref:PostSharp.Aspects.TypeLevelAspect>, <xref:PostSharp.Aspects.InstanceLevelAspect>, <xref:PostSharp.Aspects.MethodLevelAspect>, <xref:PostSharp.Aspects.LocationLevelAspect> or <xref:PostSharp.Aspects.EventLevelAspect>. As such, these aspects have no functionality. You can add functionalities by adding advices to the aspect. 

Advices are covered in the following sections:

| Section | Description |
|---------|-------------|
| <xref:advices> | Advices with equivalent functionality as <xref:PostSharp.Aspects.OnMethodBoundaryAspect>, <xref:PostSharp.Aspects.MethodInterceptionAspect>, <xref:PostSharp.Aspects.LocationInterceptionAspect>, and <xref:PostSharp.Aspects.EventInterceptionAspect>.  |
| <xref:code-injections> | Make the aspect introduce an interface into the target class. The interface is implemented by the aspect itself. |
| <xref:members> | Make the aspect introduce a new method, property or event into the target class. The new member is implemented by the aspect itself. Conversely, the aspect can import a member of the target so that it can invoke it through a delegate. |

