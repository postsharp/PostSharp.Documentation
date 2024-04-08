---
uid: customizing-aspect-description
title: "Customizing Aspect Description in CodeLens And Tooltips"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides guidance on customizing aspect descriptions in CodeLens and tooltips using Visual Studio Tools for Metalama and PostSharp, including instructions for both simple and composite aspects."
---
# Customizing Aspect Description in CodeLens and Tooltips

When you position the mouse cursor over a declaration that has been enhanced by an aspect, Visual Studio Tools for Metalama and PostSharp add a description of the aspect to CodeLens, and optionally to the Intellisense tooltip. The description that PostSharp generates by default is sometimes little helpful. To make the CodeLens and Intellisense description of your aspect more understandable for its users, you should override the default description.


## Simple Aspects

*Simple aspects* are aspects built by deriving from a base class such as <xref:PostSharp.Aspects.OnMethodBoundaryAspect> and overriding virtual methods of the base class, such as <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)>. They are described in the section <xref:simple-aspects>. 

To set the description of a simple aspect, add the <xref:PostSharp.Aspects.AspectDescriptionAttribute> custom attribute to the aspect class. 

This is illustrated in the following code snippet.

```csharp
[PSerializable]
    [AspectDescription("Applies the exception handling policy")]
    public sealed class ExceptionHandlerAttribute : OnExceptionAspect
    {
        public override void OnException( MethodExecutionArgs eventArgs )
        {
            if ( !ExceptionHandler.OnException( eventArgs.Exception ) )
            {
                eventArgs.FlowBehavior = FlowBehavior.Continue;
            }
        }
    }
```


## Composite Aspects

*Composite aspects* are aspects where advices are not overridden from the base class, but are added using advice and pointcut custom attributes such as <xref:PostSharp.Aspects.Advices.OnMethodEntryAdvice> and <xref:PostSharp.Aspects.Advices.MethodPointcut>. Composite aspects are described in section <xref:complex-aspects>. Unlike simple aspects, composite aspects can have several advices, 

With composite aspects, you should add a description to every advice. You can do that by setting the <xref:PostSharp.Aspects.Advices.Advice.Description> property of the advice custom attribute. 

The following code snippet illustrates how to set the description of the advice. This description will appear in CodeLens and the Intellisense tooltip of each property affected by this advice.

```csharp
[OnLocationSetValueAdvice( Description="Persists the property to disk." ), 
               MulticastPointcut( Targets = MulticastTargets.Property, 
                                  Attributes = MulticastAttributes.Instance | MulticastAttributes.NonAbstract)]
              public void OnPropertySet( LocationInterceptionArgs args )
              {
                // Details skipped.
              }
```


