---
uid: advices
title: "Adding Behaviors to Existing Members"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Adding Behaviors to Existing Members

In order to add new behaviors to (i.e. modify) existing members (methods, fields, properties, or events), two questions must be addressed:

* **What** transformation should be performed? The answer lays in the *advice*. This advice is a method of your advice, annotated with a custom attribute determining in which situation the method should be invoked. You can freely choose the name of the method, but its signature must match the one expected by the advice type. 

* **Where** should it be performed, i.e. on which elements of code? The answer lays in the *pointcut*, another custom attribute expected on the method providing the transformation. 


## How to Add a Behavior to an Existing Member


### 

1. Start with an empty aspect class deriving <xref:PostSharp.Aspects.AssemblyLevelAspect>, <xref:PostSharp.Aspects.TypeLevelAspect>, <xref:PostSharp.Aspects.InstanceLevelAspect>, <xref:PostSharp.Aspects.MethodLevelAspect>, <xref:PostSharp.Aspects.LocationLevelAspect> or <xref:PostSharp.Aspects.EventLevelAspect>. Mark it as serializable. 


2. Choose an advice type in the list below. For instance: <xref:PostSharp.Aspects.Advices.OnMethodEntryAdvice>. 


3. Create a method. The signature of this method should match exactly the signature matched by this advice type.


4. Annotate this method with a custom attribute of the advice type you chose. For instance: `[OnMethodEntryAdvice]`. 


5. Choose a pointcut type in the list below. For instance: <xref:PostSharp.Aspects.Advices.SelfPointcut>. Annotate the advice method with that custom attribute. For instance: `[SelfPointcut]`. 



### Example

The following code shows a simple tracing aspect implemented with an advice and a pointcut. This aspect is exactly equivalent to a class derived from <xref:PostSharp.Aspects.OnMethodBoundaryAspect> where only the method <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)> has been overwritten. The example is a method-level aspect and <xref:PostSharp.Aspects.Advices.SelfPointcut> means that the advice applies to the same target as the method itself. 

```csharp
using System;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Serialization;

namespace Samples6
{
    [PSerializable]
    public sealed class TraceAttribute : MethodLevelAspect
    {
        [OnMethodEntryAdvice, SelfPointcut]
        public void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("Entering {0}.{1}", args.Method.DeclaringType.Name, args.Method.Name);
        }
    }
}
```


## Advice Kinds

The following table lists all types of advices that can transform existing members. Note that all these advices are available as a part of a simple aspect (for instance <xref:PostSharp.Aspects.Advices.OnMethodEntryAdvice> corresponds to <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)>. For a complete documentation of the advice, see the documentation of the corresponding simple aspect. 


#### 

| Advice Type | Targets | Description |
|-------------|---------------------------------------------|-------------|
| <xref:PostSharp.Aspects.Advices.OnMethodEntryAdvice><br><xref:PostSharp.Aspects.Advices.OnMethodSuccessAdvice><br><xref:PostSharp.Aspects.Advices.OnMethodExceptionAdvice><br><xref:PostSharp.Aspects.Advices.OnMethodExitAdvice> | Methods | These advices are equivalent to the advices of the aspect <xref:PostSharp.Aspects.OnMethodBoundaryAspect>. The target method to be wrapped by a `try` / `catch` / `finally` construct.  |
| <xref:PostSharp.Aspects.Advices.OnMethodInvokeAdvice> | Methods | This advice is equivalent to the aspect <xref:PostSharp.Aspects.MethodInterceptionAspect>. Calls to the target methods are replaced to calls to the advice.  |
| <xref:PostSharp.Aspects.Advices.OnLocationGetValueAdvice><br><xref:PostSharp.Aspects.Advices.OnLocationSetValueAdvice> | Fields, Properties | These advices are equivalent to the advices of the aspect <xref:PostSharp.Aspects.LocationInterceptionAspect>. Fields are changed into properties, and calls to the accessors are replaced to calls to the proper advice.  |
| <xref:PostSharp.Aspects.Advices.LocationValidationAdvice> | Fields, Properties, Parameters | This advice is equivalent to the <xref:PostSharp.Aspects.ILocationValidationAspect`1.ValidateValue(`0,System.String,PostSharp.Reflection.LocationKind,PostSharp.Aspects.LocationValidationContext)> method of the <xref:PostSharp.Aspects.ILocationValidationAspect`1> aspect interface. It validates values assigned to their targets and throws an exception in case of error.  |
| <xref:PostSharp.Aspects.Advices.OnEventAddHandlerAdvice><br><xref:PostSharp.Aspects.Advices.OnEventRemoveHandlerAdvice><br><xref:PostSharp.Aspects.Advices.OnEventInvokeHandlerAdvice> | Events | These advices are equivalent to the advices of the aspect <xref:PostSharp.Aspects.EventInterceptionAspect>. Calls to `add` and `remove` semantics are replaced by calls to advices. When the event is fired, the `OnEventInvokeHandler` is invoked for each handler, instead of the handler itself.  |


## Pointcuts Kinds

Pointcuts determine *where* the transformation provided by the advice should be applied. 

From a logical point of view, pointcuts are functions that return a set of code elements. A pointcut can only select elements of code that are inside the target of the aspect itself. For instance, if an aspect has been applied to a class `A`, the pointcut can select the class `A` itself, members of `A`, but not different classes or members of different classes. 


### Multicast Pointcut

The pointcut type <xref:PostSharp.Aspects.Advices.MulticastPointcut> allows expressing a pointcut in a purely declarative way, using a single custom attribute. It works in a very similar way as <xref:PostSharp.Extensibility.MulticastAttribute> (see <xref:multicast>) the kind of code elements being selected, their name and attributes can be filtered using properties of this custom attribute. 

For instance, the following code applies the `OnPropertySet` advice to all non-abstract properties of the class to which the aspect has been applied. 

```csharp
[OnLocationSetValueAdvice, 
               MulticastPointcut( Targets = MulticastTargets.Property, 
                                  Attributes = MulticastAttributes.Instance | MulticastAttributes.NonAbstract)]
              public void OnPropertySet( LocationInterceptionArgs args )
              {
                // Details skipped.
              }
```


### Method Pointcut

The pointcut type <xref:PostSharp.Aspects.Advices.MethodPointcut> allows expressing a pointcut imperatively, using a C# or VB method. The argument of the custom attribute should contain the name of the method implementing the pointcut. 

The only parameter of this method should be type-compatible with the kind of elements of code to which the *aspect* applies. The return value of the pointcut method should be a collection (<xref:System.Collections.Generic.IEnumerable`1>) of objects that are type-compatible with the kind of elements of code to which the *advice* applies. 

For instance, the following code applies the `OnPropertySet` advice to all writable properties that are not annotated with the `IgnorePropertyChanged` custom attribute. 

```csharp
private IEnumerable<PropertyInfo> SelectProperties( Type type )
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | 
                BindingFlags.DeclaredOnly | BindingFlags.Public;

            return from property
                       in type.GetProperties( bindingFlags )
                   where property.CanWrite && !property.IsDefined(typeof(IgnorePropertyChanged))
                   select property;
        }

        [OnLocationSetValueAdvice, MethodPointcut( "SelectProperties" )]
        public void OnPropertySet( LocationInterceptionArgs args )
        {
            // Details skipped.
        }
```

As you can see in this example, pointcut methods can use the power of LINQ to query <xref:System.Reflection>. 


### Self Pointcut

The pointcut type <xref:PostSharp.Aspects.Advices.SelfPointcut> simply selects the target of the aspect. 


## Grouping Advices

The table of above shows advice types grouped in families. Advices of different type but of the same family can be grouped into a single logical *filter*, so they are considered as single transformation. 


### Why Grouping Advices

Consider for instance three advices of the family <xref:PostSharp.Aspects.OnMethodBoundaryAspect>: <xref:PostSharp.Aspects.Advices.OnMethodEntryAdvice>, <xref:PostSharp.Aspects.Advices.OnMethodSuccessAdvice> and <xref:PostSharp.Aspects.Advices.OnMethodExceptionAdvice>. The way how they are ordered is important, as it results in a different generation of the `try` / `catch` / `finally` block. 

The following table compares advice ordering strategies. In the left column, advices are executed in the order: `OnEntry`, `OnExit`, `OnException`. In the right column, advices are grouped together. 

| ```csharp
void Method()
          {
            try
            {
              OnEntry();
              
              try
              {
                // Original method body.
              }
              finally
              {
                OnExit();
              }
            }
            catch
            {
               OnException();
               throw;
            }
          }
```

 | ```csharp
void Method()
          {
            OnEntry();
           
            try
            {
                // Original method body.
            }
            catch
            {
               OnException();
               throw;
            }
            finally
            {
                OnExit();
            }
          }
```

 |

The code in the left column may make sense in some situations, but it is not consistent with the code generated by <xref:PostSharp.Aspects.OnMethodBoundaryAspect>. Note that the advices may have been ordered differently: the order `OnEntry`, `OnException`, `OnExit` would have generated the same code as in the right column. However, you would have had to use custom attributes to specify order relationships between advices (see <xref:advice-ordering>). Grouping advices is a much easier way to ensure consistency. 

Additionally, when advices of the <xref:PostSharp.Aspects.OnMethodBoundaryAspect> family are grouped together, it will be possible to share information among them using <xref:PostSharp.Aspects.MethodExecutionArgs.MethodExecutionTag>. 

The reasons to group advices of the family <xref:PostSharp.Aspects.LocationInterceptionAspect> and <xref:PostSharp.Aspects.EventInterceptionAspect> are similar: advices grouped together behave consistently as a single filter (see <xref:simple-aspects>). 


### How to Group Advices

To group several advices into a single filter:


### 

1. Choose a *master advice*. The choice of the master advice is arbitrary. All other advices of the group are called *subordinate advices*. 


2. Annotate the master advice method with one advice custom attribute (see [Advice Kinds](#advice-kinds) and one pointcut custom attribute (see [Pointcuts Kinds](#pointcuts-kinds)), as usual. 


3. Annotate all subordinate advices with one advice custom attribute. Set the property <xref:PostSharp.Aspects.Advices.GroupingAdvice.Master> of the custom attribute to the name of the master advice method. 


4. Do not specify any pointcut on subordinate advice methods.


The following code shows how two advices of type <xref:PostSharp.Aspects.Advices.OnMethodEntryAdvice> and <xref:PostSharp.Aspects.Advices.OnMethodExitAdvice> can be grouped into a single filter: 

```csharp
[OnMethodEntryAdvice, MulticastPointcut]
            public void OnEntry(MethodExecutionArgs args)
            {
            }

            [OnMethodExitAdvice(Master="OnEntry")]
            public void OnExit(MethodExecutionArgs args)
            {
            }
```

## See Also

**Reference**

<xref:PostSharp.Aspects.Advices>
<br>**Other Resources**

<xref:advice-ordering>
<br><xref:simple-aspects>
<br>