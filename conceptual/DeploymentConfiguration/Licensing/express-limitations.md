---
uid: express-limitations
title: "PostSharp Essentials"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp Essentials is a free edition of PostSharp with unlimited free features and premium features limited to 1,000 lines of code. Premium features include semantic advising of async methods, instrumenting explicit properties, production mode logging, and integration with higher levels of PostSharp SDK."
---
# PostSharp Essentials

PostSharp Essentials is a free edition of PostSharp that includes both free and premium features. Free features are available in unlimited amount. However, premium features can only be applied to 1,000 lines of code.

Note that any edition of PostSharp includes the free entitlements of PostSharp Essentials additionally to the premium features covered by this edition.


## Free features

The following features of PostSharp are free and unlimited in PostSharp Essentials:


### Method decorators and interceptors

Both aspects <xref:PostSharp.Aspects.OnMethodBoundaryAspect> and <xref:PostSharp.Aspects.MethodInterceptionAspect> can be used for free, but semantic advising of async methods is a premium feature. 

To disable semantic advising, you have to set the <xref:PostSharp.Aspects.MethodInterceptionAspect.SemanticallyAdvisedMethodKinds> property to `None` in the aspect constructor. 

> [!CAUTION]
> Make sure you understand the consequences of disabling of the semantic advising in your use cases. See <xref:semantic-advising> for details. 


### INotifyPropertyChanged

The <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> can be used for free - but only for auto-implemented property. Instrumenting explicit properties is a premium feature. 

Explicit properties are instrumented by default. To limit PostSharp to instrument only automatic properties, you can set the <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute.ExcludeExplicitProperties> property to `true`. 


### Contracts

All contracts can be used for free without limitation. See <xref:contracts> for details. 


### PostSharp Logging, Developer Mode

You can apply logging to any code size using PostSharp Essentials, but you need to explicitly enable the developer mode. Otherwise, production mode will be used, which is considered a premium feature.

To enable development mode, set the `LoggingDeveloperMode` property to `true` in *postsharp.config*. See <xref:logging-license> for details. 


### PostSharp SDK

You can create your own add-ins, or use community add-ins, that use the lower layers of PostSharp SDK. Integrating with higher levels (Aspect Weaver) is a premium feature covered by PostSharp Framework or PostSharp Ultimate. This premium feature is not provided at all by PostSharp Essentials.


## Enforcement of the solution-level limit

You can find more information about how we count lines of code in <xref:licensing-counting-lines>. 

The limitation of 1000 enhanced lines of code per solution is implemented not by looking at the *sln* file, but by counting the lines of code of classes in all assemblies that are referenced by the current assembly. That is, the limit is actually 1000 lines of code of enhanced types in the whole assembly closure. 


## Diagnosing licensing issues

If you don't understand why PostSharp is requiring a commercial license, you can generate a licensing diagnostic log by building your project with the following command line:

```none
msbuild /t:Rebuild /v:detailed /p:PostSharpTrace=Licensing > msbuild.log
```


