---
uid: whats-new-40
title: "What's New in PostSharp 4.0"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 4.0

The principal focus of PostSharp 4 was to redesign the Threading Pattern Library from the ground up and make it a real solution to write thread-safe code with C# and VB. Additionally, we've introduced the undo/redo feature into the Model Pattern Library. To achieve these objectives properly, we had to implement a good old concept from UML and object-oriented modeling: aggregation and composition. We introduced significant improvements in the PostSharp Aspect Framework to support these new features.


## Aggregatable pattern

As it turns out, multiple patterns rely on the notion of parent-child relationships. These concepts are a part of the UML specification, where it is known as aggregation, but even modern programming languages don't implement the notions. We fixed that in PostSharp 4.0 with our <xref:PostSharp.Patterns.Model.AggregatableAttribute> aspect. For details, see <xref:aggregatable>. 


## Disposable pattern

Once we have a notion of parent-child relationship, it is easy to build an aspect that recursively disposes a whole object tree. This is our <xref:PostSharp.Patterns.Model.DisposableAttribute> aspect. For details, see <xref:disposable>. 


## Immutable threading model

The Immutable patterns made functional languages popular for its great usefulness in multithreaded programs. Unfortunately, the concept has traditionally been difficult to object-oriented programming. PostSharp 4.0 provides a pragmatic implementation with the <xref:PostSharp.Patterns.Threading.ImmutableAttribute> aspect. For details, see <xref:immutable>. 


## Freezable threading model

Even a well-implemented Immutable pattern can be too strict for some object-oriented scenarios. In this case, the Freezable patterns may be more suitable. Based on the Aggregatable pattern, the <xref:PostSharp.Patterns.Threading.FreezableAttribute> aspect makes it possible to build freezable object trees. For details, see <xref:freezable>. 


## Synchronized threading model

A threading model library could not be complete without it, so we added the <xref:PostSharp.Patterns.Threading.SynchronizedAttribute> aspect. For details, see <xref:synchronized>. 


## Redesign of reader-writer-synchronized, actor, and thread-unsafe threading models

We took the right way in PostSharp 3.0 with threading models, but the vision was not yet fully consistent and the implementation was only partial. With PostSharp 3.2, we felt we had a better understanding of what we wanted to achieve, and completely revisited our threading models. Based on the Aggregatable pattern, and based on a consistent object model, the Threading Pattern Library is now much more powerful and consistent.


## Recordable pattern (undo/redo)

The <xref:PostSharp.Patterns.Recording.RecordableAttribute> aspect, together with the <xref:PostSharp.Patterns.Recording.Recorder> class, make is possible to implement an undo/redo feature at the domain level. 


## Dynamic location imports

To allow to import several fields and properties into a single aspect field (which was not possible using <xref:PostSharp.Aspects.Advices.ImportMemberAttribute>, we added the <xref:PostSharp.Aspects.Advices.IAdviceProvider> interface and the <xref:PostSharp.Aspects.Advices.ImportLocationAdviceInstance> class. 


## Aspect repository

The new <xref:PostSharp.Aspects.IAspectRepositoryService> service exposes the list of all aspects added to the code model, both using custom attributes or <xref:PostSharp.Aspects.IAspectProvider>, and offer a way to execute validation logic after all aspects have been discovered. 


## OnInstanceConstructed advice

The <xref:PostSharp.Aspects.Advices.OnInstanceConstructedAdvice> custom attribute allows you to define an advice that is executed after the instance constructor exits. 


## InitializeAspectInstance advice

The <xref:PostSharp.Aspects.Advices.InitializeAspectInstanceAdvice> custom attribute allows you to define an advice that is similar to <xref:PostSharp.Aspects.IInstanceScopedAspect.RuntimeInitializeInstance> but passes information about the reason why the aspect is initialized (constructor, clone, deseralization). 


## NotifyPropertyChanged optimization

Our <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> is four times faster at run time on average. 

