---
uid: advice-ordering
title: "Ordering Advices"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides guidance on ordering advices in PostSharp, explaining aspect dependencies, execution order, and the use of custom attributes for defining dependencies and ordering."
---
# Ordering Advices

The section <xref:aspect-dependencies> talks in terms of *aspect dependencies* and *aspect ordering*. Most of what has been said there is also valid to advices. When we talk of the order of execution of aspects, we actually mean the execution of advices ("aspects" themselves, "quoteInline", are never executed). 

Dependencies defined at aspect level implicitly apply to all advices. When developing a composite aspect (see <xref:complex-aspects>), it is possible to add dependencies directly to advice methods by annotating them with custom attributes of the namespace <xref:PostSharp.Aspects.Dependencies>. 

Note that all advices provided by an aspect are ordered in a single block. Suppose that a method is the target of advices `Aspect1.MethodA`, `Aspect1.MethodB` and `Aspect2.MethodC`. The next table shows valid and invalid orders: 

| Valid Orders | Invalid Orders |
|--------------|----------------|
| `Aspect1.MethodA`, `Aspect1.MethodB`, `Aspect2.MethodC`  | `Aspect1.MethodA`, `Aspect2.MethodC`, `Aspect1.MethodB`  |
| `Aspect1.MethodB`, `Aspect1.MethodA`, `Aspect2.MethodC`  | `Aspect1.MethodB`, `Aspect2.MethodC`, `Aspect1.MethodA`  |
| `Aspect2.MethodC`, `Aspect1.MethodA`, `Aspect1.MethodB`  |  |
| `Aspect2.MethodC`, `Aspect1.MethodB`, `Aspect1.MethodA`  |  |

## Ordering Advices of the Same Aspect

Advices of the same aspect can be used using any custom attribute derived from <xref:PostSharp.Aspects.Dependencies.AspectDependencyAttribute>. 

Because advices of the same aspect instance are necessarily ordered in block, it is appropriate to specify dependencies between aspect classes extensively, and specify ordering of advices only in the scope of the current aspect instance. The most appropriate dependency custom attribute for this purpose is <xref:PostSharp.Aspects.Dependencies.AdviceDependencyAttribute>, which accepts the name of the advice method as a parameter. 

## See Also

**Reference**

<xref:PostSharp.Aspects.Dependencies.AdviceDependencyAttribute>
<br>
