---
uid: estimating-code-saving
title: "Estimating Code Savings"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Estimating Code Savings

During build, PostSharp attempts to estimate how many lines of handwritten code were avoided thanks to aspects. By default, PostSharp considers that 2 lines of code are saved every time an advice is applied to a target. This is of course a very rough estimate. You can add information to your aspects and advices to make the estimate more accurate.

> [!TIP]
> When adding code saving estimate, ask yourself the following question: how much code would an intelligent developer have written if she has to implement the same feature without PostSharp, using the best possible strategy? Do not assume that the strategy you took to implement the feature with an aspect would be the same as the strategy for handwritten code.


## Simple aspects

*Simple aspects* are aspects built by deriving from a base class such as <xref:PostSharp.Aspects.OnMethodBoundaryAspect> and overriding virtual methods of the base class, such as <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)>. They are described in the section <xref:simple-aspects>. 

By default, PostSharp estimates that 2 lines of handwritten code are avoided for each advice method that you override, every time the aspect is applied to a target.

To override the default value, add the <xref:PostSharp.Aspects.LinesOfCodeAvoidedAttribute> custom attribute to the aspect class. The argument of the custom attribute constructor must be set to the number of lines of handwritten coded avoided every time the aspect is applied to a target. 

The following code snippet shows how to specify that 4 lines of code are avoided every time the aspect is applied. If the aspect is applied to 100 methods, PostSharp will estimate that 400 lines of handwritten code have been avoided.

```csharp
[PSerializable]
    [LinesOfCodeAvoided(4)]
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


## Composite aspects

*Composite aspects* are aspects where advices are not overridden from the base class, but are added using advice and pointcut custom attributes such as <xref:PostSharp.Aspects.Advices.OnMethodEntryAdvice> and <xref:PostSharp.Aspects.Advices.MethodPointcut>. Composite aspects are described in section <xref:complex-aspects>. Unlike simple aspects, composite aspects can have several advices, 

By default, PostSharp estimates that 2 lines of handwritten code are avoided for each advice, every time the advice is applied to a target. Some advices may have different default values. For instance, the <xref:PostSharp.Aspects.Advices.IntroduceInterfaceAttribute> advice shall count 2 lines of code per introduced interface method. 

You can still use the aspect-level <xref:PostSharp.Aspects.LinesOfCodeAvoidedAttribute> custom attribute. It will increment the estimated number of avoided lines of code every time the *aspect* is applied to a target. However, to provide more relevant estimates, you need to provide code saving information at *advice* level. 

To specify how many lines of handwritten code are avoided every time an advice is applied to a target, specify the <xref:PostSharp.Aspects.Advices.Advice> property of the advice custom attribute. 

The following code snippet shows how to specify that 1 line of code is avoided every time the advice is applied. Suppose that the aspect is applied to 100 classes and each class has in average 5 instance non-abstract properties. In this situation, PostSharp will estimate that 500 lines of handwritten code have been avoided.

```csharp
[OnLocationSetValueAdvice( LinesOfCodeAvoided = 1 ), 
               MulticastPointcut( Targets = MulticastTargets.Property, 
                                  Attributes = MulticastAttributes.Instance | MulticastAttributes.NonAbstract)]
              public void OnPropertySet( LocationInterceptionArgs args )
              {
                // Details skipped.
              }
```


## Adding code saving hints programmatically

In the previous sections, we described how to add code saving hints declarively using custom attributes. Sometimes declarative estimations are not accurate enough. To learn how to add programmatic hints, see <xref:programmatic-tooltip>. 

## See Also

**Reference**

<xref:PostSharp.Aspects.LinesOfCodeAvoidedAttribute>
<br><xref:PostSharp.Aspects.Advices.Advice>
<br>